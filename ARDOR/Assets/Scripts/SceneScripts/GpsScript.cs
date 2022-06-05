using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GpsScript : MonoBehaviour
{
    //public Text text;
    public Camera arCam;

    private string TAG = "GpsScript";
    private int SKIP_SAMPLES = 10;
    private double prevTimeStamp;
    private int _skipSamples = Values.SKIP_SAMPLES;
    private float _avgLat;
    private float _avgLon;
    private bool _gpsOn;
    private int _sampleCountForInitialMapPosition=0;
    private int gpsSampleCounter = 0;
    private bool initMap2D = false;
    private double inLat;// the input location lat 
    private double inLon;
    private float inHorizontalAcc;
    private double inAlt;
    private float inAltAcc;
    private float emuLon = 34f;
    private bool initiated = false;
    bool flag = false;

    public int sampleCountForInitialMapPosition { get { return _sampleCountForInitialMapPosition; } }
    public float avgLat { get { return _avgLat; } }
    public float avgLon { get { return _avgLon; } }
    public int skipSamples { get { return _skipSamples; } }
    // listener for the map+ground

    public delegate void GpsUpdatedSetSampleEventHandler(double lat, double lon, float acc);
    public event GpsUpdatedSetSampleEventHandler GpsUdated_InitialCenterMap2D;
    public event GpsUpdatedSetSampleEventHandler GpsUpdated_EnableARButton;
    public event GpsUpdatedSetSampleEventHandler GpsInitialized;
    public event GpsUpdatedSetSampleEventHandler GpsUpdated_SetARMap;
    public delegate void GpsUpdatedLeastSquaresEventHandler();
    public event GpsUpdatedLeastSquaresEventHandler GpsUpdated_CalcLeastSquares;
    public bool gpsOn { set { _gpsOn = value; } }


    private void Awake()
    {
        if (!Input.location.isEnabledByUser) //FIRST IM CHACKING FOR PERMISSION IF "true" IT MEANS USER GAVED PERMISSION FOR USING LOCATION INFORMATION
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        //File.Delete(Application.persistentDataPath + "/coordinates.txt");
        //File.Delete(Application.persistentDataPath + "/gps.txt");
        //File.AppendAllText(Application.persistentDataPath + "/coordinates.txt", "lat,lon\n");

        // unity gps is superrior to native android gps. when i walk in a straight line everything is fine,
        // but when i aim the phone sideways to look at a building the android gps throws the results around.
        // it doesn't deal well with moving the phone all over
        // this effect is much less notable in unity gps
    }
    // Start is called before the first frame update
    void Start()
    {
        _gpsOn = true;
        Input.location.Start(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
        //text.text = _skipSamples.ToString();
        unityGPS();
        //EmulateGPS();
        //File.AppendAllText(Application.persistentDataPath + "/gps.txt", "update\n");

    }

    IEnumerator EmulateGps()
    {
        yield return new WaitForSeconds(1);

        GpsUdated_InitialCenterMap2D(31.262619, 34.793353, 1);
        GpsUpdated_EnableARButton(31.262619, 34.793353, 1);
        GpsUpdated_SetARMap(31.262619, 34.793353, 1);
        GpsUpdated_CalcLeastSquares();
    }

    private void unityGPS()
    {
        File.AppendAllText(Application.persistentDataPath + "/gps.txt", "unityGPS\n");
#if (UNITY_EDITOR)
        
        if (!flag)
        {
            flag = true;
            StartCoroutine("EmulateGps");
        }
#endif
        if (Input.location.status == LocationServiceStatus.Running && Input.location.lastData.timestamp > prevTimeStamp && _gpsOn && Input.location.lastData.horizontalAccuracy < 8.0f)
        {
            inLat = Input.location.lastData.latitude;
            inLon = Input.location.lastData.longitude;
            inHorizontalAcc = Input.location.lastData.horizontalAccuracy;
            inAlt = Input.location.lastData.altitude;
            inAltAcc = Input.location.lastData.verticalAccuracy;
            prevTimeStamp = Input.location.lastData.timestamp;

            // when i get the first gps sample, it is googd enough for me to create 2D map

            if(gpsSampleCounter == 0)
            {
                GpsUdated_InitialCenterMap2D(inLat, inLon, inHorizontalAcc);
                gpsSampleCounter++;
                return;
            }
            // skip first 10 unaccurate samples
            if(gpsSampleCounter>0 && gpsSampleCounter <= SKIP_SAMPLES)
            {
                gpsSampleCounter++;
                return;
            }
            // 
            if (gpsSampleCounter == SKIP_SAMPLES+1)
            {
                GpsUpdated_EnableARButton(inLat, inLon, inHorizontalAcc);
                gpsSampleCounter++;
                return;
            }
            OnGpsUpdated();

            /////////////////////
            //if (_skipSamples > SKIP_SAMPLES)
            //{

            //    _skipSamples--;
            //}
            //else
            //{
            //    if (initiated == false)
            //    {
            //        GpsInitialized(inLat, inLon, inHorizontalAcc);
            //        initiated = true;
            //    }

            //    _sampleCountForInitialMapPosition++;

            //    OnGpsUpdated();
            //}
        }
    }

    public void OnGpsUpdated()
    {
        GpsUpdated_SetARMap(inLat, inLon, inHorizontalAcc);
        GpsUpdated_CalcLeastSquares();
    }
}
