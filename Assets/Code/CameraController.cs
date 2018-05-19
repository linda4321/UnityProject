using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public HeroRabit rabit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform rabitTransform = rabit.transform;
		Transform cameraTransform = this.transform;

		Vector3 rabitPos = rabitTransform.position;
		Vector3 cameraPos = cameraTransform.position;

		cameraPos.x = rabitPos.x;
		cameraPos.y = rabitPos.y;

		cameraTransform.position = cameraPos;
	}
}
