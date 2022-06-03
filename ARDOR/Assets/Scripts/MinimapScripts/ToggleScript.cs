using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject map;
    public GameObject toggleDraw;
    public GameObject btn;
    public GameObject lineDrawer;
    public GameObject toggleSatellite;
    public GameObject minimapCamera;
    public GameObject loadRoadButton;
    public void flipMap(bool b)
    {
        loadRoadButton.SetActive(!b);
        map.SetActive(b);
        toggleDraw.SetActive(b);
        btn.SetActive(b);
        lineDrawer.SetActive(b);
        toggleSatellite.SetActive(b);
        minimapCamera.SetActive(b);
    }

}
