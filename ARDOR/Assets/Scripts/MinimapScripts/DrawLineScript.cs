using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    private bool minimapLocked = false;
    private List<Vector2> touchPosList = new List<Vector2>();
    public bool MinimapLocked    {get => minimapLocked; set => minimapLocked = value;    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        Touch touch;
        // if the map is not locked in place than i can not draw on it
        if (!minimapLocked)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            //if (pos.x < 1024 && pos.y < 1024)
            //    CalcPin(pos);
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.position.x < 1024 && touch.position.y < 1024)
            {

                //CalcPin(new Vector3(touch.position.x, touch.position.y, cam.transform.localPosition.y));

            }
        }
        
    }
}
