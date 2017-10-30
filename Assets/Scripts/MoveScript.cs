using UnityEngine;
using System.Collections;
using System.IO;
using System;

[RequireComponent(typeof(MeshCollider))]

public class MoveScript : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;

    [HideInInspector]
    public Vector3 previous_position;

    public bool isEnabled = false;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        previous_position = transform.localPosition;
    }

    void OnMouseDrag()
    {
        if (isEnabled)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }

    }

    private void OnMouseUp()
    {
        if (isEnabled)
        {
            string t = Manager.instance.loggedin_user + " - " + Manager.instance.currentObject.name + " - Move transform to " + transform.localPosition;
            string p = Manager.instance.path;
            File.AppendAllText(@p, t + Environment.NewLine);
        }
    }

}
