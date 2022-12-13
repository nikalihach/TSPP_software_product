using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
    public class Expenses //витрати
    {
        public double[,] fuel_spending; //витрати на топливо
        private double[] product_costs; //витрати на товар
        private double[,] general_expenses; //Загальна сума

        public Expenses(double[] product_costs_)
        {
            product_costs = new double[product_costs_.Length];
            Array.Copy(product_costs_, product_costs, product_costs.Length);
        }

        public double[,] Return_GeneralExpenses()
        {
            general_expenses = new double[fuel_spending.GetLength(0), fuel_spending.GetLength(1)];
            for (int i = 0; i < fuel_spending.GetLength(0); i++)
                for (int j = 0; j < general_expenses.GetLength(1); j++)
                {
                    general_expenses[i, j] = fuel_spending[i, j] + product_costs[i];
                }

            return general_expenses;
        }

        ~Expenses()
        {
        }
    }
}
