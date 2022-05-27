using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public GpsScript gpsScript;
    public GameObject initPanel;
    public GameObject rawImage;

    private int screenWidth;
    private int screenHeight;
    private int dimentions;
    public int ScreenWidth { get => screenWidth;  }
    public int ScreenHeight { get => screenHeight; }
    public int Dimentions { get => dimentions; }

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        dimentions = screenHeight > screenWidth ? screenHeight : screenWidth;
        RectTransform rect = rawImage.GetComponent<RectTransform>();
        Debug.Log("yuyuyu atttempt change rect");
        rect.sizeDelta = new Vector2(dimentions, dimentions);
        Debug.Log("yuyuyu success");
        Debug.Log("yuyuyu size:"+ rect.sizeDelta);

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
