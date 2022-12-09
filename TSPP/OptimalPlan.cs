using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSPP
{
    class OptimalPlan //оптимальный план
    {
        private double[,] cost; //масів з витратами
        private double[,] auxiliary; //допоміжний масив
        private double[,] plan; //опорний план
        private int[] stock; //масив запасів
        private int[] stock_remnant; //залишки запасів
        private int[] needs; //масив попиту 
        private int[] needs_remnant; //залишки попиту
        private int non_zero = 0; //кількість не-нульових елементів масиву plan

        private int[] U; //Масив потенціалів u
        private int[] V; //Масив потенціалів v

        private int[,] grades; // масив оцінок

        public OptimalPlan(double[,] _cost, int[] _stock, int[] _needs)
        {
            cost = _cost;
            stock = _stock;
            needs = _needs;
            Initialization();
        }
        private void Initialization()
        {
            plan = new double[cost.GetLength(0), cost.GetLength(1)];
            auxiliary = new double[cost.GetLength(0), cost.GetLength(1)];
            stock_remnant = new int[stock.Length];
            needs_remnant = new int[needs.Length];

            Array.Copy(cost, plan, cost.Length);
            Array.Copy(cost, auxiliary, cost.Length);
            Array.Copy(stock, stock_remnant, stock.Length);
            Array.Copy(needs, needs_remnant, needs.Length);

            U = new int[stock.Length]; 
            V = new int[needs.Length];

            grades = new int[cost.GetLength(0), cost.GetLength(1)];
        }

        public void Start()
        {
            CalculateGrades();
        }
        private void MinimalElement()
        {
            double min = 9999999; //минимальний елемент
            int index1 = 0, index2 = 0; //змінні для збереження індексу

            for (int k = 0; k < cost.Length; k++)
            {
                for (int i = 0; i < stock.Length; i++)
                {
                    for (int j = 0; j < needs.Length; j++)
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
        }

        private void FindingPotentials()
        {
            MinimalElement();

            int[] u_index = new int[non_zero]; //Масив для збереження індексів на перетині не-нульового елементу масива (u - А)
            int[] v_index = new int[non_zero]; //Масив для збереження індексів на перетині не-нульового елементу масива (v - В)

            int k = 0;
            for (int i = 0; i < stock.Length; i++)
            {
                for (int j = 0; j < needs.Length; j++)
                {
                    if (plan[i, j] != 0)
                    {
                        u_index[k] = i;
                        v_index[k] = j;
                        k++;
                    }
                }
            }

            U[u_index[0]] = 0;
            V[v_index[0]] = (int)cost[u_index[0], v_index[0]] - U[u_index[0]];

            int u_current, v_current;
            u_current = u_index[0];
            v_current = v_index[0];
            int new_index_u = 0;
            int new_index_v = 0;

            int[] index = new int[non_zero];
            index[0] = 0;
            int m;

            for (int t = 1; t < non_zero; t++)
            {
                m = 0;
                for (int i = 1; i < non_zero; i++)
                {
                    if (u_index[i] == u_current && i != index[m])
                    {
                        V[v_index[i]] = (int)cost[u_index[i], v_index[i]] - U[u_current];
                        new_index_v = v_index[i];
                        m++;
                        index[m] = i;
                    }

                    else if (v_index[i] == v_current && i != index[m])
                    {
                        U[u_index[i]] = (int)cost[u_index[i], v_index[i]] - V[v_current];
                        new_index_u = u_index[i];
                        m++;
                        index[m] = i;
                    }
                }
                u_current = new_index_u;
                v_current = new_index_v;
            }
        }

        public double[,] CalculateGrades()
        {
            FindingPotentials();

            for (int i = 0; i < stock.Length; i++)
            {
                for (int j = 0; j < needs.Length; j++)
                {
                    if (plan[i, j] == 0)
                    {
                        grades[i, j] = (int)cost[i, j] - (U[i] + V[j]);
                        Console.WriteLine((int)cost[i, j] + "- (" + U[i] + "+" + V[j] + ") = " + grades[i, j]);
                        if (grades[i, j] < 0)
                        {
                            Console.WriteLine("plan ne optimalniy");
                            Recalculation(i, j);
                        }
                        else
                        {
                            Console.WriteLine("plan GOOD");
                            MessageBox.Show("THIS IS GOOD");
                        }
                    }
                }
            }
            return plan;
        }

        public void Recalculation(int i, int j)
        {
            float[,] recalculation = new float[cost.GetLength(0), cost.GetLength(1)];
            int counter = 1;
            int[] current_index = new int[2];
            int[] old_index = new int[2];
            int[] deadlock = new int[2] {666, 666};
            recalculation[i, j] = 1;
            current_index[0] = i;
            current_index[1] = j;
            int stop = 0;
            int finish = 0;

            while (true)
            {
                if (counter % 2 != 0)
                {
                    for (int k = 0; k < plan.GetLength(1); k++)
                    {
                        if (k == current_index[1] || k == deadlock[1])
                        {
                            stop++;

                            if (stop > needs.Length)
                            {
                                recalculation[current_index[0], current_index[1]] = 0;
                                deadlock[0] = current_index[0];
                                current_index[0] = old_index[0];
                                counter++;
                                stop = 0;
                            }
                        }
                        else
                        {
                            if (plan[current_index[0], k] != 0)
                            {
                                recalculation[current_index[0], k] = -1;
                                old_index[1] = current_index[1];
                                current_index[1] = k;
                                counter++;
                                break;
                            }
                        }
                    }
                }

                else if (counter % 2 == 0)
                {
                    for (int k = 0; k < plan.GetLength(0); k++)
                    {
                        if (current_index[1] == j && k == i && counter > 1)
                        {
                            finish = 1;
                            goto met;
                        }

                        if (k == current_index[0] || k == deadlock[0])
                        {
                            stop++;

                            if (stop > needs.Length)
                            {
                                recalculation[current_index[0], current_index[1]] = 0;
                                deadlock[1] = current_index[1];
                                current_index[1] = old_index[1];
                                counter++;
                                stop = 0;
                            }
                        }

                        else
                        {
                            if (plan[k, current_index[1]] != 0)
                            {
                                recalculation[k, current_index[1]] = 1;
                                old_index[0] = current_index[0];
                                current_index[0] = k;
                                counter++;
                                break;
                            }
                        }
                    }
                }

            met: if (finish == 1) break;
            }

            double min = 99999999;
            for (int k = 0; k < stock.Length; k++)
            {
                for (int p = 0; p < needs.Length; p++)
                {
                    if (recalculation[k, p] == -1 && recalculation[k, p] < min)
                    {
                        min = plan[k, p];
                    }
                }
            }
            Console.WriteLine(min);

            for (int k = 0; k < plan.GetLength(0); k++)
            {
                for (int p = 0; p < plan.GetLength(1); p++)
                {

                    if (recalculation[k, p] == -1)
                    {
                        plan[k, p] = plan[k, p] - min;
                    }

                    if (recalculation[k, p] == 1)
                    {
                        plan[k, p] = plan[k, p] + min;
                    }
                }
            }
            CalculateGrades();
        }
    }
}
