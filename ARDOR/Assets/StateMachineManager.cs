using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public GpsScript gpsScript;
    public GameObject initPanel;
    // Start is called before the first frame update
    void Start()
    {
        gpsScript.GpsInitialized += OnGpsInit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGpsInit(double lat, double lon, float acc)
    {
        initPanel.SetActive(false);
    }
}
