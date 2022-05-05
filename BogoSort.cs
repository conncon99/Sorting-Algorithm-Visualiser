using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BogoSort : SortingAlgorithm
{
    public BogoSort(int itemCount, string type) : base(itemCount, type){} //An empty constructor

    //The bogosort algorithm
    public override void Sort()
    {
        float startTime = DateTime.Now.Millisecond; //Keeps track of when the sorting begins for later use

        //The order of the array is repeatedly randomized until it is in the correct order
        while (!isSorted())
        {
            randomizeArray(true);
        }
        timeTaken = (DateTime.Now.Millisecond - startTime) / 1000f; //Calculates the time in milliseconds that the sort takes to complete
    }
}
