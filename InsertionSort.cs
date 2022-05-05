using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : SortingAlgorithm
{
    public InsertionSort(int itemCount, string type) : base(itemCount, type) {}

    public override void Sort()
    {
        float startTime = DateTime.Now.Millisecond;

        for (int i = 1; i < arr.Length; i++)
        {
            int temp = arr[i];
            int predIndex = i - 1;

            while (predIndex >= 0 && arr[predIndex] > temp)
            {
                arr[predIndex + 1] = arr[predIndex];
                sortTracker.Add(new Tuple<int, int>(predIndex + 1, predIndex));
                predIndex--;
                comparisonsMade++;
            }
            arr[predIndex + 1] = temp;
        }
        timeTaken = (DateTime.Now.Millisecond - startTime) / 1000f;
    }
}
