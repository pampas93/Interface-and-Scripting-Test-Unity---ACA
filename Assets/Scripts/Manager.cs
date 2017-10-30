using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    [SerializeField]
    InputField username;
    [SerializeField]
    InputField password;
    [SerializeField]
    Text errorLogin;

    [SerializeField]
    Canvas LoginCanvas;

    [SerializeField]
    GameObject cube;

    [SerializeField]
    GameObject sphere;

    private GameObject currentObject = null;
    //Shader selected_Shader;

    bool userLoggedIn = false;

    public static Manager instance;

    private Transform previousTransform;

    //private GameObject selectedObject = null;
    private GameObject previous_selectedObject = null;

    enum transformMode { Move, Scale, Rotate, Reset, None};

    transformMode currentMode = transformMode.None;

    private void Awake()
    {
        instance = this;
    }

    private void OnGUI()
    {
        if (userLoggedIn)
        {
            if (GUI.Button(new Rect(20, 20, 70, 35), "Cube"))
            {
                Debug.Log("Show a cube");
                cube.SetActive(true);
                sphere.SetActive(false);
                currentObject = cube;
                //selectedObject = cube;
                //sphere.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

                currentMode = transformMode.None;
            }

            if (GUI.Button(new Rect(20, 70, 70, 35), "Sphere"))
            {
                Debug.Log("Show a sphere");
                cube.SetActive(false);
                sphere.SetActive(true);
                currentObject = sphere;
                //selectedObject = sphere;
                //cube.GetComponent<Renderer>().material.shader = Shader.Find("Standard");

                currentMode = transformMode.None;
            }

            if (currentObject != null)
            {
                if (GUI.Button(new Rect(800, 20, 85, 35), "Move"))
                {
                    currentMode = transformMode.Move;
                    ChangeTranformMode();
                }
                if (GUI.Button(new Rect(800, 70, 85, 35), "Rotate"))
                {
                    currentMode = transformMode.Rotate;
                    ChangeTranformMode();
                }
                if (GUI.Button(new Rect(800, 120, 85, 35), "Scale"))
                {
                    currentMode = transformMode.Scale;
                    ChangeTranformMode();
                }
                if (GUI.Button(new Rect(800, 170, 85, 35), "Reset Back"))
                {
                    ResetTransform(currentMode);
                    //currentMode = transformMode.Reset;
                    //ChangeTranformMode();
                }
            }

           

            string temp = "Currently Selected : ";
            if (currentObject == null)
                temp = temp + "Null";
            else
                temp = temp + currentObject.name;

            GUI.Label(new Rect(600, 10, 250, 20), temp);
            GUI.Label(new Rect(600, 35, 250, 20), "Current Mode : " + currentMode);

        }
    }

    // Use this for initialization
    void Start () {
        //cube.SetActive(false);
        sphere.SetActive(false);

        //selected_Shader = Shader.Find("Self-Illumin/Diffuse");

    }

    void ChangeTranformMode()
    {
        var moveScript = currentObject.GetComponent<MoveScript>();
        var rotateScript = currentObject.GetComponent<RotateScrpt>();
        var scaleScript = currentObject.GetComponent<ScaleScript>();

        switch (currentMode)
        {
            case transformMode.Move:
                moveScript.isEnabled = true;
                rotateScript.isEnabled = false;
                scaleScript.isEnabled = false;
                break;
            case transformMode.Rotate:
                moveScript.isEnabled = false;
                rotateScript.isEnabled = true;
                scaleScript.isEnabled = false;
                break;
            case transformMode.Scale:
                moveScript.isEnabled = false;
                rotateScript.isEnabled = false;
                scaleScript.isEnabled = true;
                break;
            //case transformMode.Reset:

            //    moveScript.isEnabled = false;
            //    rotateScript.isEnabled = false;
            //    scaleScript.isEnabled = false;

            //    currentMode = transformMode.None;
            //    break;
            default:
                moveScript.isEnabled = false;
                rotateScript.isEnabled = false;
                scaleScript.isEnabled = false;
                break;
        }
       
    }

    private void ResetTransform(transformMode x)
    {
        switch (currentMode)
        {
            case transformMode.Move:
                var temp_script = currentObject.GetComponent<MoveScript>();
                currentObject.transform.localPosition = temp_script.previous_position;
                break;
            case transformMode.Rotate:
                var temp_script1 = currentObject.GetComponent<RotateScrpt>();
                currentObject.transform.localRotation = temp_script1.previous_rotate;
                break;
            case transformMode.Scale:
                var temp_script2 = currentObject.GetComponent<RotateScrpt>();
                currentObject.transform.localRotation = temp_script2.previous_rotate;
                break;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit))
            {
                if(hit.collider.tag == "PrimitiveObject")
                {

                    //if (selectedObject == null)
                    //{
                    //    previous_selectedObject = null;
                    //    selectedObject = hit.collider.gameObject;
                    //    Debug.Log(selectedObject.name + " is selected");
                    //    //selectedObject.GetComponent<Renderer>().material.shader = selected_Shader;
                    //}
                    //else
                    //{
                    //    //selectedObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                    //    previous_selectedObject = selectedObject;
                    //    selectedObject = null;
                    //    Debug.Log(previous_selectedObject.name + " is unselected");
                    //}
                    
                }
               
            }
        }

        //if(Input.GetMouseButtonUp(0) && currentMode != transformMode.None)
        //{
        //    if(currentObject != null)
        //    {
        //        previousTransform.localScale = currentObject.transform.localScale;
        //        previousTransform.localPosition = currentObject.transform.localPosition;
        //        previousTransform.localRotation = currentObject.transform.localRotation;
        //    }
        //    //
        //}

    }

    public void loginOnClick()
    {
        string user = username.text;
        string pass = password.text;

        if(user == "" && pass == "")
        {
            LoginCanvas.enabled = false;
            userLoggedIn = true;
        }
        else
        {
            errorLogin.text = "Wrong Credentials";
        }
    }
}
