using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPP
{
    class Expenses //затраты
    {
        public double[,] fuel_spending; //витрати на топливо
        public double[] product_costs; //витрати на товар
        public double [,] general_expenses; // Общая сумма

        public double[,] Count()
         {
            general_expenses = new double [fuel_spending.GetLength(0), fuel_spending.GetLength(1)];
            for(int i = 0; i < fuel_spending.GetLength(0); i++)
            for (int j = 0; j < general_expenses.GetLength(1); j++)
            {
                general_expenses[i, j] = fuel_spending[i,j] + product_costs[i];
            }

            return general_expenses;
         }

        ~Expenses()
        {
        }
    }
}
