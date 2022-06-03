using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawImageScript : MonoBehaviour
{
    public RectTransform rect;
    public RenderTexture renderTexture;
    // Start is called before the first frame update
    void Start()
    {
        int dim = getDim();
        rect.sizeDelta = new Vector2(dim, dim);
        renderTexture.width = dim;
        renderTexture.height = dim;
    }

    private int getDim()
    {
        return Screen.height > Screen.width ? Screen.height : Screen.width;
    }
    // Update is called once per frame
    void Update()
    {

    }
}