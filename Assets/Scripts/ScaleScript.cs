using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour {

    public float scaleFactor = 5.0f;
    private Vector3 offset;

    public bool isEnabled = false;

    void OnMouseDown()
    {
        offset = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        
    }

    void OnMouseDrag()
    {
        if (isEnabled)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            float temp = curScreenPoint.x - offset.x;
            //Debug.Log(temp * scaleFactor * 0.005);

            float f = (float)(temp * scaleFactor * 0.001);
            Vector3 original = transform.localScale;
            transform.localScale = new Vector3(original.x + f, original.y + f, original.z + f);
        }

    }
}
