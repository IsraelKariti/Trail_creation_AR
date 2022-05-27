using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
public class ToggleSatellite : MonoBehaviour
{
    public AbstractMap abstractMap;
    public GameObject image;
    public Sprite street;
    public Sprite satellite;

    public void toggleSatellite(bool b)
    {

        IImageryLayer imageLayer = abstractMap.ImageLayer;
        if (imageLayer.LayerSource == ImagerySourceType.MapboxSatellite)
        {
            imageLayer.SetLayerSource(ImagerySourceType.MapboxStreets);
            image.GetComponent<Image>().sprite = satellite;
        }
        else
        {
            imageLayer.SetLayerSource(ImagerySourceType.MapboxSatellite);
            image.GetComponent<Image>().sprite = street;

        }
    }
}
