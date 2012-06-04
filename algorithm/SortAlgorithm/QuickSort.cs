using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortAlgorithm
{
    //O(nlog(n))
    //不稳定
    public class QuickSort
    {
        public static void Sort(int[] arr)
        {
            QSort(arr,0,arr.Length-1);
        }
        public static void QSort(int[] arr,int low,int high)
        {
            if (low < high)
            {
                int mid = GetPosition(arr, low, high);
                QSort(arr, low, mid - 1);
                QSort(arr, mid + 1, high);
            }
            else
            {
                return;
            }
        }
        public static int GetPosition(int[] arr,int low,int high)
        {
            int pivot = arr[low];
            while (low < high)
            {
                while (low < high && arr[high] > pivot)
                {
                    high--;
                }
                if (low < high)
                {
                    Swap(arr,low,high);
                }
                while (low < high && arr[low] <= pivot)
                {
                    low++;
                }
                if (low < high)
                {
                    Swap(arr, low, high);
                }
            }
            return low;
        }
        public static void Swap(int[] arr,int a,int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
