using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr1_5
{
    class Program
    {
        static (int[][], int) NorthwestCornerMethod(int[] supply, int[] demand, int[][] costs)
        {
            int[][] allocation = new int[supply.Length][]; for (int i = 0; i < supply.Length; i++)
            {
                allocation[i] = new int[demand.Length];
            }
            int[] supplyCopy = supply.ToArray(); int[] demandCopy = demand.ToArray();
            int totalCost = 0;
            int row = 0, col = 0; 
            
            while (row < supply.Length && col < demand.Length)
            {
                int x = Math.Min(supplyCopy[row], demandCopy[col]);
                allocation[row][col] = x; supplyCopy[row] -= x;
                demandCopy[col] -= x; totalCost += x * costs[row][col];
                if (supplyCopy[row] == 0)
                {
                    row++;
                }
                else
                {
                    col++;
                }
            }
            return (allocation, totalCost);
        }

        static (int[][], int) MinimumCostMethod(int[] supply, int[] demand, int[][] costs)
        {
            int[][] allocation = new int[supply.Length][]; for (int i = 0; i < supply.Length; i++)
            {
                allocation[i] = new int[demand.Length];
            }
            int[] supplyCopy = supply.ToArray();
            int[] demandCopy = demand.ToArray(); int totalCost = 0;

            while (true)
            {
                int minCost = int.MaxValue;
                int minRow = -1, minCol = -1;
                for (int row = 0; row < supply.Length; row++)
                {
                    for (int col = 0; col < demand.Length; col++)
                    {
                        if (supplyCopy[row] > 0 && demandCopy[col] > 0)
                        {
                            if (costs[row][col] < minCost)
                            {
                                minCost = costs[row][col]; minRow = row;
                                minCol = col;
                            }
                        }
                    }
                }
                if (minRow == -1 || minCol == -1)
                {
                    break;
                }
                int x = Math.Min(supplyCopy[minRow], demandCopy[minCol]);
                allocation[minRow][minCol] = x; supplyCopy[minRow] -= x;
                demandCopy[minCol] -= x; totalCost += x * minCost;
            }
            return (allocation, totalCost);
        }

        static void Main()
        {
            int[] supply = { 200, 350, 300 };
            int[] demand = { 270, 130, 190, 150, 110 }; int[][] costs = new int[][]
            {            
                 new int[] { 24, 50, 55, 27, 16 },
                 new int[] { 50, 47, 23, 17, 21 },           
                 new int[] { 35, 59, 55, 27, 41 }
            };
            var (allocationNW, totalCostNW) = NorthwestCornerMethod(supply, demand, costs);
            var (allocationMinCost, totalCostMinCost) = MinimumCostMethod(supply, demand, costs);
            Console.WriteLine("Севастьянов, Вариант18");
            Console.WriteLine();
            Console.WriteLine("МЕТОД СЕВЕРО-ЗАПАДНОГО УГЛА");
            Console.WriteLine("Распределение: ");

            foreach (var row in allocationNW)
            {
                Console.WriteLine(string.Join(", ", row));
            }
            Console.WriteLine("Общая стоимость: " + totalCostNW);
            Console.WriteLine();


            Console.WriteLine("МЕТОД МИНИМАЛЬНЫХ ЭЛЕМЕНТОВ");
            Console.WriteLine("Наилучший план (минимальные элементы): "); 
            Console.WriteLine("Распределение: ");

            foreach (var row in allocationMinCost)
            {
                Console.WriteLine(string.Join(", ", row));
            }
            Console.WriteLine("Общая стоимость: " + totalCostMinCost);
            Console.ReadLine();
        }
    }
}
