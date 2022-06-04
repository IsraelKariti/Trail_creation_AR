using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Examples;
public class StateMachineManager : MonoBehaviour
{
    public GpsScript gpsScript;
    public SpawnUserLocationIndicationOnMap spawnUserLocationIndication;
    public GameObject panelInitialization;

    public GameObject panelMap2DButtons;
    public GameObject buttonChangeMapToAR;
    public GameObject map;
    public AbstractMap abstractMap;
    public GameObject map2DCam;

    public GameObject panelAR;
    public QuadTreeCameraMovement quadTreeCameraMovement;
    public DrawScript drawScript;


        // Start is called before the first frame update
    void Start()
    {
        gpsScript.GpsUdated_InitialCenterMap2D += OnGpsInit;
        gpsScript.GpsUdated_InitialCenterMap2D += spawnUserLocationIndication.updateUserLocationIndication;
        gpsScript.GpsUpdated_EnableARButton += OnEnableAR;

    }
    public void OnGpsInit(double lat, double lon, float acc)
    {
        panelInitialization.SetActive(false);
        panelMap2DButtons.SetActive(true);
        abstractMap.UpdateMap(new Vector2d(lat, lon));
    }
    
    public void OnEnableAR(double lat, double lon, float acc)
    {
        buttonChangeMapToAR.SetActive(true);
    }

    public void enableDrawRedLine(bool b)
    {
        quadTreeCameraMovement.MinimapLocked = b;
        drawScript.MinimapLocked = b;
    }
    public void ChangeToAR()
    {
        panelMap2DButtons.SetActive(false);
        map.SetActive(false);
        map2DCam.SetActive(false);
        panelAR.SetActive(true);
    }
    public void ChangeToMap2D()
    {
        panelMap2DButtons.SetActive(true);
        map.SetActive(true);
        map2DCam.SetActive(true);
        panelAR.SetActive(false);
    }

}
