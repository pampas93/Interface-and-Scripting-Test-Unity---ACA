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
    Shader selected_Shader;

    bool userLoggedIn = false;

    public static Manager instance;

    private GameObject selectedObject = null;
    private GameObject previous_selectedObject = null;

    private void Awake()
    {
        instance = this;
    }

    private void OnGUI()
    {
        if (userLoggedIn)
        {
            if (GUI.Button(new Rect(10, 10, 70, 35), "Cube"))
            {
                Debug.Log("Show a cube");
                cube.SetActive(true);
                sphere.SetActive(false);
                currentObject = cube;
                selectedObject = null;
                sphere.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            }
            
            if (GUI.Button(new Rect(10, 60, 70, 35), "Sphere"))
            {
                Debug.Log("Show a sphere");
                cube.SetActive(false);
                sphere.SetActive(true);
                currentObject = sphere;
                selectedObject = null;
                cube.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            }
            string temp = "Currently Selected : ";
            if (selectedObject == null)
                temp = temp + "Null";
            else
                temp = temp + selectedObject.name;
            GUI.Label(new Rect(600, 10, 250, 20), temp);
        }
        
    }

    // Use this for initialization
    void Start () {
        cube.SetActive(false);
        sphere.SetActive(false);

        selected_Shader = Shader.Find("Self-Illumin/Diffuse");

    }
	
	// Update is called once per frame
	void Update () {
		
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

                    if (selectedObject == null)
                    {
                        previous_selectedObject = null;
                        selectedObject = hit.collider.gameObject;
                        Debug.Log(selectedObject.name + " is selected");
                        selectedObject.GetComponent<Renderer>().material.shader = selected_Shader;
                    }
                    else
                    {
                        selectedObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                        previous_selectedObject = selectedObject;
                        selectedObject = null;
                        Debug.Log(previous_selectedObject.name + " is unselected");
                    }
                    
                }
               
            }
        }

    }

    public void loginOnClick()
    {
        string user = username.text;
        string pass = password.text;

        if(user == "abhijit" && pass == "pass")
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
