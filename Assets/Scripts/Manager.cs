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
    Canvas LoginCanvas;

    [SerializeField]
    GameObject cube;

    [SerializeField]
    GameObject sphere;

    private GameObject currentObject = null;

    bool userLoggedIn = false;

    public static Manager instance;

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
            }
                

            if (GUI.Button(new Rect(10, 60, 70, 35), "Sphere"))
            {
                Debug.Log("Show a sphere");
                cube.SetActive(false);
                sphere.SetActive(true);
                currentObject = sphere;
            }
                
        }
        
    }

    // Use this for initialization
    void Start () {
        cube.SetActive(false);
        sphere.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loginOnClick()
    {
        string user = username.text;
        string pass = password.text;

        if(user == "abhijit" && pass == "1993")
        {
            LoginCanvas.enabled = false;
            userLoggedIn = true;
        }
    }
}
