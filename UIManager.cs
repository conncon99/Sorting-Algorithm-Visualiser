using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text sortNameText, sortTimeComplexity, sortTimeTaken, comparisonsMadeText;
    [SerializeField]
    Dropdown sortChoiceDropdown;
    [SerializeField]
    InputField numItemsInput;
    [SerializeField]
    GameObject rectangle,parentPos;

    int itemCount = 100;
    float xlength = 12,ylength = 8;

    List<GameObject> sortingVisualisation = new List<GameObject>();

    SortingAlgorithm sortingAlgorithm;

    public void onRandomizeButtonPressed()
    {
        sortNameText.text = sortChoiceDropdown.captionText.text;
        sortTimeComplexity.text = $"Average Time Complexity: {getTimeComplexity()}";
        itemCount = int.Parse(numItemsInput.text);
        sortingAlgorithm = getAlgorithmType();
        sortingAlgorithm.fillArray();
        sortingAlgorithm.randomizeArray(false);
        generateVisualisation();
    }
    public void onSortButtonPressed()
    {
        sortingAlgorithm.Sort();
        sortTimeTaken.text = $"Time Taken: {sortingAlgorithm.timeTaken.ToString()}s";
        comparisonsMadeText.text = $"Comparisons Made: {sortingAlgorithm.comparisonsMade}";
        updateVisualisation(0);
    }
    string getTimeComplexity()
    {
        switch (sortChoiceDropdown.captionText.text)
        {
            case "Bubble Sort":
                return "O(n\xB2)";
            case "Insertion Sort":
                return "O(n\xB2)";
            case "Bogo Sort":
                return "O((n+1)!)";
            case "Selection Sort":
                return "O(n\xB2)";
            case "Pidgeonhole Sort":
                return "O(N + n)";
            default:
                return "";
        }
    }

    SortingAlgorithm getAlgorithmType()
    {
        switch (sortChoiceDropdown.captionText.text)
        {
            case "Bubble Sort":
                return new BubbleSort(itemCount, "Comparison");
            case "Insertion Sort":
                return new InsertionSort(itemCount, "Comparison");
            case "Bogo Sort":
                return new BogoSort(itemCount, "Comparison");
            case "Selection Sort":
                return new SelectionSort(itemCount, "Comparison");
            case "Pidgeonhole Sort":
                return new PidgeonholeSort(itemCount, "Bucket");
            default:
                return new BubbleSort(itemCount, "Comparison");
        }
    }

    void generateVisualisation()
    {
        if (sortingVisualisation != null)
        {
            for (int i = 0; i < sortingVisualisation.Count; i++)
            {
                Destroy(sortingVisualisation[i]);
            }
            sortingVisualisation.Clear();
        }

        for (int i = 0; i < sortingAlgorithm.arr.Length; i++)
        {
            GameObject s = Instantiate(rectangle);

            if (sortingAlgorithm.arr[i]  == 0)
            {
                s.transform.localScale = new Vector3(xlength / sortingAlgorithm.arr.Length, 2.5f/sortingAlgorithm.arr.Length);
                s.transform.position = parentPos.transform.position + new Vector3(i * (xlength / sortingAlgorithm.arr.Length), 2.5f / (sortingAlgorithm.arr.Length * 2f), -7);
            }
            else
            {
                s.transform.localScale = new Vector3(xlength / sortingAlgorithm.arr.Length, (((sortingAlgorithm.arr[i] / (float)sortingAlgorithm.arr.Length) * 100) / (ylength * 2)));
                s.transform.position = parentPos.transform.position + new Vector3(i * (xlength / sortingAlgorithm.arr.Length), ((((float)sortingAlgorithm.arr[i] / sortingAlgorithm.arr.Length) * 100) / (ylength * 4)), -7);
            }
            sortingVisualisation.Add(s);
        }
    }

    void updateVisualisation(int counter)
    {
        switch (sortingAlgorithm.type)
        {
            case "Comparison":
                StartCoroutine(visualiseComparison(counter));
                break;
            case "Bucket":
                visualiseBucket();
                break;
        }
    }

    IEnumerator visualiseComparison(int counter)
    {
        if (counter == sortingAlgorithm.sortTracker.Count)
        {
            yield return new WaitForSeconds(0f);
        }
        else
        {
            GameObject item1 = sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item1];
            GameObject item2 = sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item2];

            Vector3 tempPos = item1.transform.position;
            sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item1].transform.position = new Vector3(item2.transform.position.x, tempPos.y, -7.5f);
            sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item2].transform.position = new Vector3(tempPos.x, item2.transform.position.y, -7.5f);

            GameObject tempObj = item1;
            sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item1] = item2;
            sortingVisualisation[sortingAlgorithm.sortTracker[counter].Item2] = tempObj;

            counter += 1;
            yield return new WaitForSeconds(0f);
            StartCoroutine(visualiseComparison(counter));
        }
    }

    void visualiseBucket()
    {
        for (int i = 0; i < sortingAlgorithm.arr.Length; i++)
        {
            if (sortingAlgorithm.arr[i] == 0)
            {
                sortingVisualisation[i].transform.localScale = new Vector3(xlength / sortingAlgorithm.arr.Length, 2.5f / sortingAlgorithm.arr.Length);
                sortingVisualisation[i].transform.position = parentPos.transform.position + new Vector3(i * (xlength / sortingAlgorithm.arr.Length), 2.5f / (sortingAlgorithm.arr.Length * 2f), -7);
            }
            else
            {
                sortingVisualisation[i].transform.localScale = new Vector3(xlength / sortingAlgorithm.arr.Length, (((sortingAlgorithm.arr[i] / (float)sortingAlgorithm.arr.Length) * 100) / (ylength * 2)));
                sortingVisualisation[i].transform.position = parentPos.transform.position + new Vector3(i * (xlength / sortingAlgorithm.arr.Length), ((((float)sortingAlgorithm.arr[i] / sortingAlgorithm.arr.Length) * 100) / (ylength * 4)), -7);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sortTimeTaken.text = "0";
    }
}
