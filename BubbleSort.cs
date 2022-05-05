using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : SortingAlgorithm
{
    public BubbleSort(int itemCount, string type) : base(itemCount, type){} //An empty constructor

    //The bubble sort algorithm
    public override void Sort()
    {
        float startTime = DateTime.Now.Millisecond; //Keeps track of when the sorting begins for later use

        for (int i = 0; i < arr.Length; i++) //loops through the whole array
        {
            for (int j = 0; j < arr.Length; j++) //loops through the whole array
            {
                if (arr[i] < arr[j]) //If a later value is lower than an earlier value, the two are exchanged
                {
                    int temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    sortTracker.Add(new Tuple<int, int>(i, j)); //A record of the exchange is made for later visualisation
                }
                comparisonsMade++;
            }
        }
        timeTaken = (DateTime.Now.Millisecond - startTime)/1000f; //Calculates the time in milliseconds that the sort takes to complete
    }
}
