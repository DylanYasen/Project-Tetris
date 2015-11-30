using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public GameObject[] groups;
    //private int[] firstSelection;
    //private int[] nextSelection;
    public static List<int> firstSelection;
    public static List<int> secondSelection;
    public static int nextGroup;
    private int nextnextGroup = -1;
    
    public Transform previewPos;

    public void spawnNext()
    {
        if(nextnextGroup == -1){
            nextGroup = GenerateNextRandomGroup();
            Instantiate(groups[nextGroup], transform.position, Quaternion.identity); 
        
        }
        else{
            nextGroup = nextnextGroup;
            Instantiate(groups[nextGroup], transform.position, Quaternion.identity);   
        }
        
        nextnextGroup = GenerateNextRandomGroup();    
        Preview(groups[nextnextGroup]);
    }
    
    private GameObject previewObj;
    void Preview(GameObject obj){
        
        if(previewObj != null)
            DestroyImmediate(previewObj);
            
        previewObj = Instantiate(obj, previewPos.position, Quaternion.identity) as GameObject;
        previewObj.GetComponent<Group>().enabled = false;   
        previewObj.transform.localScale = new Vector3(0.5f,0.5f,1);
    }
    
    private int GenerateNextRandomGroup(){      
        if (firstSelection.Count == 0)
        {
            firstSelection = new List<int>(secondSelection);
            generateNextSequence();
        }
        int randResult = firstSelection[0];
        firstSelection.RemoveAt(0);
        return randResult;
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
        /*
        foreach (var num in firstSelection)
        {
            print(num);
        }
        */
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
        // Set 1P Mode/AI Mode
        Group.aimode = true;

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
