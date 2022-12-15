using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public void Save_expenses_to_file(double[,] array, string fileName)
        {
            StringBuilder output = new StringBuilder();
            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int column = 0; column < array.GetLength(1); column++)
                {
                    output.Append($"{array[row, column]} ");
                }
                output.Append(Environment.NewLine);
            }

            try
            {
                File.WriteAllText(fileName, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
            }
        }

        public double[,] Recording_expenses_from_a_file (double[,] expenses)
        {
            string[] readText = File.ReadAllLines("expenses.txt");
            String text_file = "";

            for (int i = 0; i < readText.Length; i++)
            {
                text_file = text_file + readText[i];
            }
            string[] words = text_file.Split(' ');
            double[] doubleArray = new double[words.Length - 1];

            for (int i = 0; i < words.Length - 1; i++)
            {
                doubleArray[i] = Convert.ToDouble(words[i]);
            }

            int k = 0;
            for (int i = 0; i < expenses.GetLength(0); i++)
            {
                for (int j = 0; j < expenses.GetLength(1); j++)
                {
                    expenses[i, j] = doubleArray[k];
                    k++;
                }
            }
            return expenses;
        }

        ~Expenses()
        {
        }
    }
}
