using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Examples;

public class ToggleDrawRedLineScript : MonoBehaviour
{
    public Sprite grey;
    public Sprite red;
    public Image img;

    public void enableDrawRedLine(bool b)
    {
        if (b)
        {
            img.sprite =red;
        }
        else
        {
            img.sprite = grey;
        }
    }
}
