using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonholeSort : SortingAlgorithm
{
    public PidgeonholeSort(int itemCount, string type) : base(itemCount, type) {}

    int findMin(int[] arr)
    {
        int minVal = int.MaxValue;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < minVal)
            {
                minVal = arr[i];
            }
        }
        return minVal;
    }

    int findMax(int[] arr)
    {
        int maxVal = -1;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > maxVal)
            {
                maxVal = arr[i];
            }
        }
        return maxVal;
    }

    public override void Sort()
    {
        float startTime = DateTime.Now.Millisecond;

        int min = findMin(arr);
        int max = findMax(arr);
        int range = max - min + 1;

        int[] pidgeonHoles = new int[range];

        for (int i = 0; i < pidgeonHoles.Length; i++)
        {
            pidgeonHoles[i] = 0;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            pidgeonHoles[arr[i] - min]++;
        }

        int index = 0;
        for (int i = 0; i < pidgeonHoles.Length; i++)
        {
            while (pidgeonHoles[i]-- > 0 && index < arr.Length)
            {
                arr[index++] = i + min;
            }
        }
        timeTaken = (DateTime.Now.Millisecond - startTime) / 1000f;
    }
}
