using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonApproveFileNameScript : MonoBehaviour
{
    public InputField inputField;
    public DrawScript drawScript;
    public void createCoordFileWithName()
    {
        string name = inputField.text;

        drawScript.CreateCoordFile(name);
    }
}
