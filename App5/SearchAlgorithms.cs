using System;
using System.Diagnostics;

namespace App5
{
    public class SearchAlgorithms
    {
        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }

        public static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target) return i;
            }
            return -1;
        }

        public static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (array[mid] == target) return mid;
                if (array[mid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }

        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static string TestAlgorithms(int size = 1000)
        {
            var random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 10000);
            }

            int target = array[random.Next(0, size)];

            int[] sortedArray = (int[])array.Clone();
            BubbleSort(sortedArray);

            var stopwatch = new Stopwatch();
            string results = "Результаты теста (массив из 1000 элементов):\n\n";

            stopwatch.Start();
            LinearSearch(array, target);
            stopwatch.Stop();
            results += $"Линейный поиск: {stopwatch.ElapsedMilliseconds} мс (O(n))\n";
            stopwatch.Reset();

            stopwatch.Start();
            BinarySearch(sortedArray, target);
            stopwatch.Stop();
            results += $"Бинарный поиск: {stopwatch.ElapsedMilliseconds} мс (O(log n))\n";
            stopwatch.Reset();

            stopwatch.Start();
            BubbleSort((int[])array.Clone());
            stopwatch.Stop();
            results += $"Сортировка пузырьком: {stopwatch.ElapsedMilliseconds} мс (O(n²))\n";
            stopwatch.Reset();

            stopwatch.Start();
            QuickSort((int[])array.Clone(), 0, array.Length - 1);
            stopwatch.Stop();
            results += $"Быстрая сортировка: {stopwatch.ElapsedMilliseconds} мс (O(n log n))";

            return results;
        }
    }
}
