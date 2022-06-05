using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DropDownScript : MonoBehaviour
{
    public MapToppingsScript mapToppingsScript;
    public GameObject dropDown;
    private List<string> fileNames = new List<string>();
    
    public void GetChosenFile(int index)
    {
        fileNames = new List<string>();
        fileNames.Add("nofile");
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("coords*.txt");
        foreach (FileInfo f in info)
        {
            fileNames.Add(f.Name);
        }

        mapToppingsScript.createMapToppings(fileNames[index]);
        dropDown.SetActive(false);
    }


}
