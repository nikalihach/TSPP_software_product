using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSPP
{
    public class OptimalPlan //опорний план
    {
        private double[,] cost; //масив з витратами
        private double[,] plan; //опорний план
        private uint[] stock; //масив запасів
        private uint[] order; //масив попиту 

        private int non_zero; //кількість не-нульових елементів масиву plan

        private int[] U; //Масив потенціалів u
        private int[] V; //Масив потенціалів v

        int index_i_zero;
        int index_j_zero;

        public bool plan_optimality;
        public bool final_plan = false;
        public OptimalPlan(double[,] _cost, uint[] _stock, uint[] _needs)
    {
        cost = _cost;
        stock = _stock;
        order = _needs;
        Initialization();
        Start_calculating();
    }
    private void Initialization()
    {
        plan = new double[cost.GetLength(0), cost.GetLength(1)];
        Array.Copy(cost, plan, cost.Length);

        U = new int[stock.Length]; //Масив потенціалів u
        V = new int[order.Length]; //Масив потенціалів v
    }

    private void Start_calculating()
    {
        Metod_MinimalElement();
        FindingPotentials();

    }

    private void Metod_MinimalElement()
    {
        uint[] stock_remnant; //залишки запасів
        stock_remnant = new uint[stock.Length];
        uint[] needs_remnant; //залишки попиту
        needs_remnant = new uint[order.Length];
        Array.Copy(stock, stock_remnant, stock.Length);
        Array.Copy(order, needs_remnant, order.Length);

            double[,] auxiliary; //допоміжний масив
        auxiliary = new double[cost.GetLength(0), cost.GetLength(1)];
        Array.Copy(cost, auxiliary, cost.Length);

        int condition; //умова невиродженності
        non_zero = 0; //кількість базисних клітинок

            double min = 9999999; //мінімальний елемент
        int index1 = 0, index2 = 0; //змінні для збереження індексу

        for (int k = 0; k < cost.Length; k++)
        {
            for (int i = 0; i < stock.Length; i++)
            {
                for (int j = 0; j < order.Length; j++)
                {
                    if (auxiliary[i, j] < min)
                    {
                        min = auxiliary[i, j];
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            if (stock_remnant[index1] >= needs_remnant[index2]) //якщо запасів більше ніж потрібно замовнику, то
            {
                plan[index1, index2] = needs_remnant[index2]; //в план записується попит

                if (plan[index1, index2] != 0)
                {
                    non_zero++;
                }

                stock_remnant[index1] = stock_remnant[index1] - needs_remnant[index2]; //розрахунок залишку запасів на складі
                needs_remnant[index2] = 0;
            }

            else if (stock_remnant[index1] <= needs_remnant[index2])
            {
                plan[index1, index2] = stock_remnant[index1];

                if (plan[index1, index2] != 0)
                {
                    non_zero++;
                }

                needs_remnant[index2] = needs_remnant[index2] - stock_remnant[index1]; //розрахунок залишку потреб замовника
                stock_remnant[index1] = 0;
            }

            auxiliary[index1, index2] = 88888;
            min = 9999999;
        }

        condition = stock.Length + order.Length - 1;

        if (condition == non_zero)//перевірка на невиродженість опорного плану
        {
            Console.WriteLine("GOOD");
        }

        else
        {
            double minimal = 999999;
            index_i_zero = 0;
            index_j_zero = 0;

            for (int i = 0; i < stock.Length; i++)
            {
                for (int j = 0; j < order.Length; j++)
                {
                    if (plan[i, j] == 0)
                    {
                        if (cost[i, j] < minimal)
                        {
                            minimal = cost[i, j];
                            index_i_zero = i;
                            index_j_zero = j;
                        }

                    }
                }
            }

            plan[index_i_zero, index_j_zero] = -1;
            non_zero = non_zero + 1;
        }

    }

    static double[] gauss(double[,] a, double[] y, int n)
    {
        double[] x;
        double max;
        int k, index;
        const double eps = 0.00001;  // точность
        x = new double[n];
        k = 0;
        while (k < n)
        {
            // Пошук рядку з максимальним a[i][k]
            max = Math.Abs(a[k, k]);
            index = k;
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Abs(a[i, k]) > max)
                {
                    max = Math.Abs(a[i, k]);
                    index = i;
                }
            }
            // Перестановка рядків
            if (max < eps)
            {
                double[] error = new double[1] { 0 };
                // нет ненулевых диагональных элементов
                return error;
            }
            double temp;
            for (int j = 0; j < n; j++)
            {
                temp = a[k, j];
                a[k, j] = a[index, j];
                a[index, j] = temp;
            }
            temp = y[k];
            y[k] = y[index];
            y[index] = temp;
            // Нормализация уравнений
            for (int i = k; i < n; i++)
            {
                temp = a[i, k];
                if (Math.Abs(temp) < eps) continue; // для нулевого коэффициента пропустить
                for (int j = 0; j < n; j++)
                    a[i, j] = a[i, j] / temp;
                y[i] = y[i] / temp;
                if (i == k) continue; // уравнение не вычитать само из себя
                for (int j = 0; j < n; j++)
                    a[i, j] = a[i, j] - a[k, j];
                y[i] = y[i] - y[k];
            }
            k++;
        }
        // обратная подстановка
        for (k = n - 1; k >= 0; k--)
        {
            x[k] = y[k];
            for (int i = 0; i < k; i++)
                y[i] = y[i] - a[i, k] * x[k];
        }
        return x;
    }

    private void FindingPotentials()
    {

        int[] u_index = new int[non_zero]; //Масив для збереження індексів на перетині не-нульового елементу масива (u - А)
        int[] v_index = new int[non_zero]; //Масив для збереження індексів на перетині не-нульового елементу масива (v - В)

        int[] v_index_correction = new int[non_zero];

    recalc:
        int k = 0;

        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < order.Length; j++)
            {
                if (plan[i, j] != 0)
                {
                    u_index[k] = i;
                    v_index[k] = j;
                    k++;
                }
            }
        }

        int coincidence = 0;

        for (int p = 1; p < non_zero; p++)
        {
            if (u_index[0] == u_index[p] || v_index[0] == v_index[p])
            {
                coincidence++;
            }
        }

        if (coincidence > 0)
        {
            int[,] index = new int[non_zero + 1, 2];
            double[,] a = new double[non_zero + 1, non_zero + 1];
            double[] y = new double[non_zero + 1];
            y[non_zero] = 0;
            int count = 0;

            for (int i = 0; i < plan.GetLength(0); i++)
            {
                for (int j = 0; j < plan.GetLength(1); j++)
                {
                    if (plan[i, j] != 0)
                    {
                        y[count] = cost[i, j];
                        count++;
                    }
                }
            }

            for (int i = 0; i < non_zero; i++)
            {
                v_index_correction[i] = v_index[i] + stock.Length;
            }

            for (int i = 0; i < non_zero; i++)
            {
                index[i, 0] = u_index[i];
                index[i, 1] = v_index_correction[i];
            }

            for (int i = 0; i < non_zero; i++)
            {
                a[i, index[i, 0]] = 1;
                a[i, index[i, 1]] = 1;
            }

            a[non_zero, 0] = 1;

            for (int i = 1; i < non_zero + 1; i++)
            {
                a[non_zero, i] = 0;
            }

           double[] potentials = new double[non_zero + 1];

            potentials = gauss(a, y, non_zero + 1);

            for (int i = 0; i < stock.Length; i++)
            {
                U[i] = (int)potentials[i];
            }

            count = 0;
            for (int i = stock.Length; i < potentials.Length; i++)
            {
                V[count] = (int)potentials[i];
                count++;
            }

        }

        else
        {
            plan[index_i_zero, index_j_zero] = 0; //прибираємо псевдоноль 

            double minimal = 999999;

            for (int j = 0; j < order.Length; j++)
            {
                if (plan[0, j] == 0)
                {
                    if (cost[0, j] < minimal)
                    {
                        minimal = cost[0, j];
                        index_i_zero = 0;
                        index_j_zero = j;
                    }
                }
            }

            plan[index_i_zero, index_j_zero] = -1; //розміщаємо "нульовy" підставку
            goto recalc;
        }


        for (int i = 0; i < V.Length; i++)
        {
            Console.WriteLine(" V[" + i + "]=" + V[i]);
        }

        for (int i = 0; i < U.Length; i++)
        {
            Console.WriteLine(" U[" + i + "]=" + U[i]);
        }


        CalculateGrades();
    }

    private void CalculateGrades()
    {
        int[] grades;


        int stop = 0;
        int index_i = 0, index_j = 0;
        int count = 0;
        int null_cells = 0;

        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < order.Length; j++)
            {
                if (plan[i, j] == 0)
                {
                    null_cells++;
                }
            }
        }

        grades = new int[null_cells];

        for (int i = 0; i < stock.Length; i++)
        {
            for (int j = 0; j < order.Length; j++)
            {
                if (plan[i, j] == 0)
                {
                    grades[count] = (int)cost[i, j] - (U[i] + V[j]);
                    Console.WriteLine((int)cost[i, j] + "- (" + U[i] + "+" + V[j] + ") = " + grades[count]);


                    if (grades[count] < 0)
                    {
                        stop = 1;
                        index_i = i;
                        index_j = j;
                    }
                    count++;
                }
            }
        }

        if (stop != 1 || final_plan == true)
        {
            Console.WriteLine("The plan is optimal");
            plan_optimality = true;
        }
        else
        {
            Console.WriteLine("The plan is not optimal");
            plan_optimality = false;
            Recalculation(index_i, index_j);
        }
    }

    private double Find_minimal(double[,] array)
    {
       double minimal = 9999;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == -1 && minimal > plan[i, j])
                {
                    minimal = plan[i, j];
                    Console.WriteLine("min = " + minimal);
                }
            }
        }
        return minimal;
    }

    private void Recalculation(int i, int j)
    {
        double[,] recalculation = new double[cost.GetLength(0), cost.GetLength(1)];

        int counter = 1;
        int[] current_index = new int[2];
        int[] old_index = new int[2];
        int[] deadlock = new int[2] { 666, 666 };
        recalculation[i, j] = 1;
        current_index[0] = i;
        current_index[1] = j;
        int stop_minus;
        int stop_plus;
        int finish = 0;
        Console.WriteLine("START: " + current_index[0] + " " + current_index[1]);
        while (true)
        {
            if (counter % 2 != 0)
            {
                stop_minus = 0;

                for (int horizon = 0; horizon < order.Length; horizon++)
                {
                    if (horizon != current_index[1] && plan[current_index[0], horizon] != 0 && horizon != deadlock[1])
                    {
                        recalculation[current_index[0], horizon] = -1;
                        old_index[1] = current_index[1];
                        current_index[1] = horizon;

                        Console.WriteLine("horizon: " + current_index[0] + " " + current_index[1]);
                        break;
                    }

                    else
                    {
                        stop_minus++;

                        if (stop_minus > order.Length - 1)
                        {
                            recalculation[current_index[0], current_index[1]] = 0;
                            deadlock[0] = current_index[0];
                            current_index[0] = old_index[0];
                            stop_minus = 0;
                            break;
                        }
                    }
                }
            }


            else if (counter % 2 == 0)
            {
                stop_plus = 0;
                for (int vertical = 0; vertical < stock.Length; vertical++)
                {
                    if (current_index[1] == j && vertical == i && counter > 1)
                    {
                        finish = 1;
                        break;
                    }


                    if (vertical != current_index[0] && plan[vertical, current_index[1]] != 0 && vertical != deadlock[0])
                    {
                        recalculation[vertical, current_index[1]] = 1;
                        old_index[0] = current_index[0];
                        current_index[0] = vertical;
                        Console.WriteLine("vertical: " + current_index[0] + " " + current_index[1]);
                        break;
                    }

                    else
                    {
                        stop_plus++;

                        if (stop_plus > stock.Length - 1)
                        {
                            recalculation[current_index[0], current_index[1]] = 0;
                            deadlock[1] = current_index[1];
                            current_index[1] = old_index[1];
                            stop_plus = 0;
                            break;
                        }
                    }
                }
            }

            counter++;

            if (finish == 1) break;
        }

        for (i = 0; i < plan.GetLength(0); i++)
        {
            for (j = 0; j < plan.GetLength(1); j++)
            {

                Console.WriteLine("recalculation: " + recalculation[i, j]);
            }
        }


        double minimal;
        minimal = Find_minimal(recalculation);
        if (minimal == -1)
        {
            final_plan = true;
            goto finish;
        }

        int index_nullcell_i = 0;
        int index_nullcell_j = 0;

        for (int k = 0; k < plan.GetLength(0); k++)
        {
            for (int p = 0; p < plan.GetLength(1); p++)
            {

                if (recalculation[k, p] == -1)
                {
                    plan[k, p] = plan[k, p] - minimal;

                    if (plan[k, p] == 0)
                    {
                        index_nullcell_i = k;
                        index_nullcell_j = p;
                    }
                }

                if (recalculation[k, p] == 1)
                {
                    plan[k, p] = plan[k, p] + minimal;
                }
            }
        }
        int filled_cells = 0;
        for (i = 0; i < plan.GetLength(0); i++)
        {
            for (j = 0; j < plan.GetLength(1); j++)
            {
                Console.WriteLine(plan[i, j]);

                if (plan[i, j] != 0)
                {
                    filled_cells++;
                }
            }
        }

        if (filled_cells != non_zero)
        {
            Console.WriteLine("Pezda");
            plan[index_nullcell_i, index_nullcell_j] = -1;
        }

    finish:;
        FindingPotentials();
    }

    public double[,] return_plan()
    {
        for (int i = 0; i < plan.GetLength(0); i++)
        {
            for (int j = 0; j < plan.GetLength(1); j++)
            {
                if (plan[i, j] == -1)
                {
                    plan[i, j] = 0;
                }
            }
        }
        return plan;
    }


}
}
