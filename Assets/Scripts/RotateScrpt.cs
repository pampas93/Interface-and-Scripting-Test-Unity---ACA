using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RotateScrpt : MonoBehaviour {

    public float rotationFactor = 5.0f;
    private Vector3 offset;

    [HideInInspector]
    public Quaternion previous_rotate;

    public bool isEnabled = false;

    void OnMouseDown()
    {
        offset = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        previous_rotate = transform.localRotation;
        Vector3 temp = transform.localEulerAngles;
        //Debug.Log(temp.x);
    }

    void OnMouseDrag()
    {
        if (isEnabled)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            float temp_x = curScreenPoint.x - offset.x;
            float temp_y = curScreenPoint.y - offset.y;

            float x = (float)(temp_x * rotationFactor * 0.001);
            float y = (float)(temp_y * rotationFactor * 0.001);

            Vector3 original = transform.localEulerAngles;
            transform.eulerAngles = new Vector3(original.x - y, original.y - x, original.z);
        }

    }   

    private void OnMouseUp()
    {
        if (isEnabled)
        {
            string t = Manager.instance.loggedin_user + " - " + Manager.instance.currentObject.name + " - Rotate transform to " + transform.eulerAngles;
            string p = Manager.instance.path;
            File.AppendAllText(@p, t + Environment.NewLine);
        }
    }
}
