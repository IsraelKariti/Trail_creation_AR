using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Examples;
public class ToggleDrawButtonScript : MonoBehaviour
{
    public QuadTreeCameraMovement quadTreeCameraMovement;
    public DrawScript drawScript;
    public Sprite grey;
    public Sprite red;
    public Image img;
    private bool b = false;



    public void enableDrawRedLine()
    {
        if (!b)
        {
            img.sprite = red;
            quadTreeCameraMovement.MinimapLocked = true;
            drawScript.MinimapLocked = true;
            b = !b;
        }
        else
        {
         

            img.sprite = grey;
            quadTreeCameraMovement.MinimapLocked = false;
            drawScript.MinimapLocked = false;
            b = !b;
        }
    }


}
