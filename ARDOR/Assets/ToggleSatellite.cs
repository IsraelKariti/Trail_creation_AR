using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
public class ToggleSatellite : MonoBehaviour
{
    public AbstractMap abstractMap;
    public void toggleSatellite(bool b)
    {
        IImageryLayer imageLayer = abstractMap.ImageLayer;
        if (imageLayer.LayerSource == ImagerySourceType.MapboxSatellite)
            imageLayer.SetLayerSource(ImagerySourceType.MapboxStreets);
        else
            imageLayer.SetLayerSource(ImagerySourceType.MapboxSatellite);
    }
}
