using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    public GameObject canvas;

    public GameObject settings;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(ShowWindow);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowWindow()
    {
        Debug.Log("Show dialog");
        settings.GetComponent<SettingsWindow>().Show(GameObject.Find("Canvas"));
    } 
}
