using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
public class StateMachineManager : MonoBehaviour
{
    public AbstractMap abstractMap;
    public GpsScript gpsScript;
    public GameObject initPanel;

    public GameObject map;
    public GameObject toggleDraw;
    public GameObject btn;
    public GameObject lineDrawer;
    public GameObject toggleSatellite;
    public GameObject toggleMinimap;

    // Start is called before the first frame update
    void Start()
    {
        gpsScript.GpsUdated_InitialCenterMap2D += OnGpsInit;
        gpsScript.GpsUpdated_EnableARButton += OnEnableAR;
    }

    public void OnEnableAR(double lat, double lon, float acc)
    {
        toggleMinimap.SetActive(true);
    }
    public void OnGpsInit(double lat, double lon, float acc)
    {
        initPanel.SetActive(false);
        flipMap2DButtons(true);
        abstractMap.UpdateMap(new Vector2d(lat,lon));
    }
    public void flipMap2DButtons(bool b)
    {

        toggleDraw.SetActive(b);
        btn.SetActive(b);
        lineDrawer.SetActive(b);
        toggleSatellite.SetActive(b);

    }
}
