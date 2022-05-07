using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine.UI;
using System;
using System.IO;

public class DrawScript : MonoBehaviour
{
    public Camera cam;
    public AbstractMap map;
    //public Text banner;
    private bool minimapLocked = false;
    private double EARTH_CIRCUMFERENCE = 40075016.685578d;
    private List<Vector2d> coordList = new List<Vector2d>();
    public bool MinimapLocked { get => minimapLocked; set => minimapLocked = value; }
    private void Awake()
    {
            //banner.text = "boom";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void CalcPin(Vector3 p)
    {
        string str = "";
        var mousePosCam = p;
        var mousePosWorld = cam.ScreenToWorldPoint(mousePosCam);
        var diffUnityMeters = (mousePosWorld - map.transform.position);

        double lat = map.CenterLatitudeLongitude.x;
        // divide the world circumference meters by the original number of tiles (which is 2^3=8 when the world is zoomed out to fit the entire screen) 
        // and than divide again with the number of zoom actions (substruct the number of zoom actions to fit the world map in screen)
        double geoMetersPerTile = 40075016.685578 * Math.Cos(Mathf.Deg2Rad * map.CenterLatitudeLongitude.x) / Math.Pow(2f, 3f)/ Math.Pow(2d, map.AbsoluteZoom-1) ;
        double zoomDecimal = map.Zoom - map.AbsoluteZoom;
        double tileSizeInPixel = 128.0 + zoomDecimal * 128.0;//original tile size is 128 pixels
        double tileCountPerTexture = 1024.0 / tileSizeInPixel;// Ex. 1024/128 = 8
        double geoMetersPerTexture = geoMetersPerTile * tileCountPerTexture;
        double geoMetersPerUnityMeter = geoMetersPerTexture / 200.0; // orthographic camera frustom is 200*200 in unity meters
        Vector2d diffGeoMeters = new Vector2d(diffUnityMeters.x * geoMetersPerUnityMeter, diffUnityMeters.z * geoMetersPerUnityMeter);
        var latlongDelta = DiffMetersToLatLon(diffGeoMeters, lat);
        var newLatLong = map.CenterLatitudeLongitude + latlongDelta;
        coordList.Add(newLatLong);

        //str += ("touch.position: " + p + "\n");
        //str += ("mousePosCam: " + mousePosCam + "\n");
        //str += ("mousePosWorld: " + mousePosWorld + "\n");
        //str += ("map center: " + map.CenterLatitudeLongitude + "\n");
        //str += ("mouse diff: " + diffUnityMeters + "\n");
        //str += ("abs zoom: " + map.AbsoluteZoom + "\n");
        //str += ("zoom: " + map.Zoom + "\n");
        //str += ("lat: " + lat + "\n");
        //str += ("geoMetersPerTile: " + geoMetersPerTile + "\n");
        //str += ("zoomDecimal: " + zoomDecimal + "\n");
        //str += ("tileSizeInPixel: " + tileSizeInPixel + "\n");
        //str += ("tileCountPerTexture: " + tileCountPerTexture + "\n");
        //str += ("geoMetersPerTexture: " + geoMetersPerTexture + "\n");
        //str += ("diffGeoMeters: " + diffGeoMeters + "\n");
        //str += ("geoMetersPerUnityMeter: " + geoMetersPerUnityMeter + "\n");
        //str += ("latlongDeltaEXPERIMENTAL: " + latlongDeltaEXPERIMENTAL + "\n");
        //str += ("Hit: " + newLatLongEXPERIMENTAL + "\n");

        //banner.text = str;
    }

    private Vector2d DiffMetersToLatLon(Vector2d diffGeoMeters, double lat)
    {
        var diffLat = (diffGeoMeters.y / EARTH_CIRCUMFERENCE) * 360;

        var diffLon = (diffGeoMeters.x / EARTH_CIRCUMFERENCE/Math.Cos(Mathf.Deg2Rad*lat)) * 360;
        //vy = 180 / Math.PI * (2 * Math.Atan(Math.Exp(vy * Math.PI / 180)) - Math.PI / 2);
        Vector2d v = new Vector2d(diffLat, diffLon);
        //Vector2d v = new Vector2d(vy, vx*Math.Cos(Mathf.Deg2Rad*vy));
        return v;
    }

    private void LateUpdate()
    {
        Touch touch;
        // if the map is not locked in place than i can not draw on it
        if (!minimapLocked)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            if(pos.x<1024 && pos.y <1024)
            CalcPin(pos);
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && touch.position.x<1024 && touch.position.y<1024)
            {

                CalcPin(new Vector3(touch.position.x, touch.position.y, cam.transform.localPosition.y));

            }
        }
    }

    public void CreateCoordFile(bool b)
    {
        if(b == false)
        {
            Debug.Log("creat file");
            File.Delete(Application.persistentDataPath + "/pois.txt");
            File.AppendAllText(Application.persistentDataPath + "/pois.txt", "lat,lon\n");
            foreach(Vector2d v in coordList)
            {
                File.AppendAllText(Application.persistentDataPath + "/pois.txt", v.x+","+v.y+"\n");

            }
        }
    }
}
