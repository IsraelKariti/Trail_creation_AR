using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public GpsScript gpsScript;
    public GameObject initPanel;

    public GameObject map;
    public GameObject UImap;
    public GameObject toggleDraw;
    public GameObject btn;
    public GameObject lineDrawer;
    public GameObject toggleSatellite;
    public void flipMap(bool b)
    {
        map.SetActive(b);
        UImap.SetActive(b);
        toggleDraw.SetActive(b);
        btn.SetActive(b);
        lineDrawer.SetActive(b);
        toggleSatellite.SetActive(b);
    }
    // Start is called before the first frame update
    void Start()
    {
        gpsScript.GpsUdated_LoadMap2D += OnGpsInit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGpsInit(double lat, double lon, float acc)
    {
        initPanel.SetActive(false);
        flipMap(true);
    }
}
