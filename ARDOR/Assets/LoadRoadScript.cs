using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class LoadRoadScript : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    
    public void createDropDownFromCoordFiles()
    {
        //dropdown.options.Clear();
        dropdown.ClearOptions();
        dropdown.options.Add(new TMP_Dropdown.OptionData("choose_file"));
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("coords*.txt");
        foreach (FileInfo f in info)
        {
            string name = f.Name;
            int start = name.IndexOf("s");
            start++;
            int end = name.IndexOf(".");
            string trimmedName = name.Substring(start, end - start);
            dropdown.options.Add(new TMP_Dropdown.OptionData( trimmedName ) );
        }
        dropdown.value = 0;
        dropdown.RefreshShownValue();


    }
}
