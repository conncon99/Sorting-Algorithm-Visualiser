using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SortingAlgorithm: MonoBehaviour
{
    public int[] arr; //The array containing all integer values to be sorted
    public string type;

    public int comparisonsMade = 0;

    public List<GameObject> selected = new List<GameObject>();

    //The list that contains all movements made by the sorting algorithm, for use of visualisation
    public List<Tuple<int, int>> sortTracker = new List<Tuple<int, int>>(); 

    public double timeTaken = 0; //The time it takes for the sorting algorithm to complete

    //A constructor that determines how many the legnth of the array to be sorted
    protected SortingAlgorithm(int itemCount, string type)
    {
        arr = new int[itemCount];
        this.type = type;
    }

    //An abstract function containing a different sorting algorithm for each child class - e.g. bubble, bogo, merge, quick
    public abstract void Sort();

    //A function that determines whether an array is fully sorted
    public bool isSorted()
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i+1] < arr[i])
            {
                return false;
            }
        }
        return true;
    }

    //A function that provides each value of the array with a number
    public void fillArray()
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = i;
        }
    }

    //A version of the Fisher-Yates shuffle algorithm that randomises the order of the array to be sorted
    public void randomizeArray(bool checkIfSorted)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int j = UnityEngine.Random.Range(i, arr.Length);
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;

            //Used for bogosort to check record each exchange for visualisation
            if (checkIfSorted)
            {
                sortTracker.Add(new Tuple<int, int>(i, j));
            }
        }
    }

    public IEnumerator visualiseSelected()
    {
        for (int i = 0; i < selected.Count; i++)
        {
            if (i == 0)
            {
                selected[i].GetComponent<Material>().SetColor("",Color.green);
            }
            else
            {
                selected[i - 1].GetComponent<Material>().SetColor("", Color.red);
                selected[i].GetComponent<Material>().SetColor("", Color.green);
            }
            yield return new WaitForSeconds(0f);
        }
    }
}