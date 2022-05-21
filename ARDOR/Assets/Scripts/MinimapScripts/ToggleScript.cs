using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject map;
    public GameObject UImap;
    public GameObject toggleDraw;
    public GameObject btn;
    public GameObject lineDrawer;
    public void flipMap(bool b)
    {
        map.SetActive(b);
        UImap.SetActive(b);
        toggleDraw.SetActive(b);
        btn.SetActive(b);
        lineDrawer.SetActive(b);
    }

}
