using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : SortingAlgorithm
{
    public SelectionSort(int itemCount, string type) : base(itemCount, type) {} //An empty constructor

    int findMinIndex(int[] unsortedArr, int boundary)
    {
        int minVal = int.MaxValue;
        int minIndex = -1;

        for (int i = boundary; i < unsortedArr.Length; i++)
        {
            if (unsortedArr[i] < minVal)
            {
                minVal = unsortedArr[i];
                minIndex = i;
            }
            comparisonsMade++;
        }
        return minIndex;
    }

    //The selection sort algorithm
    public override void Sort()
    {
        float startTime = DateTime.Now.Millisecond; //Keeps track of when the sorting begins for later use

        for (int i = 0; i < arr.Length; i++)
        {
            int minIndex = findMinIndex(arr, i);
            sortTracker.Add(new Tuple<int, int>(i, minIndex)); //A record of the exchange is made for later visualisation
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }

        timeTaken = (DateTime.Now.Millisecond - startTime) / 1000f; //Calculates the time in milliseconds that the sort takes to complete
    }
}
