using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SortAlgorithm;
using AlgorithmSolution;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran=new Random();
            IEnumerable<int> a = Enumerable.Range(1, 100);
            int len = 10;
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {
                arr[i] = ran.Next(100);
            }

            Console.WriteLine();
            //PrintArr(arr);
            //BubbleSort.Sort(arr);
            //ShellSort.Sort(arr);
            //QuickSort.Sort(arr);
            //MergeSort.Sort(arr);
            //HeapSort.Sort(arr);
            //PrintArr(arr);

            //DiceProbrability.Caculate(3);
            //Fibonaccic.Caculate2(100);

            //BigNumCaculate.Caculate();
            HexConvert.Caculate();
            Console.Read(); 
        }


        static void PrintArr(int[] arr)
        {
            string temp = "";
            for (int i = 0; i < arr.Length; i++)
            {
                temp += arr[i] + " ";
            }
            Console.WriteLine(temp); 
        }
    }
}
