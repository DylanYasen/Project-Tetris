using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public GameObject[] groups;
    //private int[] firstSelection;
    //private int[] nextSelection;
    public List<int> firstSelection;
    public List<int> secondSelection;

    public void spawnNext()
    {
        //int i = Random.Range(0, groups.Length);

        //Instantiate(groups[i], transform.position, Quaternion.identity);
        if (firstSelection.Count == 0)
        {
            firstSelection = new List<int>(secondSelection);
            generateNextSequence();
        }
        int nextGroup = firstSelection[0];
        firstSelection.RemoveAt(0);
        Instantiate(groups[nextGroup], transform.position, Quaternion.identity);

    }

    public void spawnCollection()
    {
        //for (int i = 0; i < 7; i++)
        //{
        //    int firstInt = Random.Range(0, groups.Length);
        //    if (!firstSelection.Contains(firstInt)) firstSelection.Add(firstInt);
        //    int secondInt = Random.Range(0, groups.Length);
        //    if (!secondSelection.Contains(secondInt)) secondSelection.Add(secondInt);
        //}
        while (firstSelection.Count != 7)
        {
            int firstInt = Random.Range(0, groups.Length);
            if (!firstSelection.Contains(firstInt)) firstSelection.Add(firstInt);
        }
        while (secondSelection.Count != 7)
        {
            int secondInt = Random.Range(0, groups.Length);
            if (!secondSelection.Contains(secondInt)) secondSelection.Add(secondInt);
        }
        foreach (var num in firstSelection)
        {
            print(num);
        }
    }

    public void generateNextSequence()
    {
        secondSelection.Clear();
        //for (int i = 0; i < 7; i++)
        //{
        //    int secondInt = Random.Range(0, groups.Length);
        //    if (!secondSelection.Contains(secondInt)) secondSelection.Add(secondInt);
        //}
        while (secondSelection.Count != 7)
        {
            int secondInt = Random.Range(0, groups.Length);
            if (!secondSelection.Contains(secondInt)) secondSelection.Add(secondInt);
        }
    }

	// Use this for initialization
	void Start () {
        //spawnNext();
        firstSelection = new List<int>();
        secondSelection = new List<int>();
        //nextSelection = new int[7];
        spawnCollection();
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
