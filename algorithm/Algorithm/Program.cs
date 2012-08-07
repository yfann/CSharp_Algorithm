using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmSolution;
using SortAlgorithm;
using Structure;

namespace Algorithm
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Random ran=new Random();
            //IEnumerable<int> a = Enumerable.Range(1, 100);
            //int len = 10;
            //int[] arr = new int[len];
            //for (int i = 0; i < len; i++)
            //{
            //    arr[i] = ran.Next(100);
            //}

            //Console.WriteLine();
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
            //HexConvert.Caculate();

            //MyHashTable hash = new MyHashTable();

            //hash.Add("1", 1);
            //hash.Add("2", 3);

            //Console.WriteLine("{0} {1}", hash["1"], hash["2"]);

            //OrderTreeTest();
            AVLTreeTest();
            Console.Read();
        }

        private static void OrderTreeTest()
        {
            Random ran = new Random();
            OrderTree<int, int> tree = new OrderTree<int, int>();
            List<int> list = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                int temp = ran.Next(1, 100);
                if (!list.Contains(temp))
                {
                    list.Add(temp);
                    tree.Add(temp, temp);
                }
            }

            Console.WriteLine("List length:{0}", list.Count);
            Console.WriteLine("List:");
            foreach (var a in list)
            {
                Console.Write("{0} ", a);
            }
            Console.WriteLine();
            Console.WriteLine("Sorted List:");
            list.Sort();
            foreach (var a in list)
            {
                Console.Write("{0} ", a);
            }
            Console.WriteLine();
            Console.WriteLine("OrderTree:");
            Console.WriteLine(tree.PrintList());

            while (true)
            {
                Console.WriteLine("input a key");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("set min num:");
                        int min = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("set max num:");
                        int max = Convert.ToInt32(Console.ReadLine());
                        HashSet<int> hs = tree.SearchRange(min, max);
                        IEnumerable<int> ie = hs.ToList();
                        foreach (var a in ie)
                        {
                            Console.Write("{0} ", a);
                        }
                        Console.WriteLine();
                        break;
                    case ConsoleKey.R:
                        Console.WriteLine("set a remove num:");
                        int rm = Convert.ToInt32(Console.ReadLine());
                        tree.Remove(rm, rm);
                        Console.WriteLine(tree.PrintList());
                        break;
                    default:
                        break;
                }
            }
        }

        private static void AVLTreeTest()
        {
            AVLTree<int, int> avl = new AVLTree<int, int>();

            for (int i = 1; i < 11; i++)
            {
                avl.Add(i, i);
            }
            Console.WriteLine("Root:{0}", avl.Root.Key.ToString());
            Console.WriteLine("Height:{0}", avl.Root.Height);
            Console.WriteLine("Inorder:{0}", avl.Print());
            Queue<AVLNode<int, int>> que = new Queue<AVLNode<int, int>>();

            ViewAVLLayer(avl.Root, que);
        }

        private static void ViewAVLLayer(AVLNode<int, int> node, Queue<AVLNode<int, int>> que)
        {
            if (node.Left != null)
                que.Enqueue(node.Left);
            if (node.Right != null)
                que.Enqueue(node.Right);
            Console.Write("{0}   ", node.Key);

            if (que.Count > 0)
            {
                ViewAVLLayer(que.Dequeue(), que);
            }
        }

        private static void PrintArr(int[] arr)
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