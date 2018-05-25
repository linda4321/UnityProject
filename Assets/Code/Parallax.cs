using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public float speed = 0.5f;

	private Vector3 lastPos;

	// Use this for initialization
	void Start () {
		lastPos = Camera.main.transform.position;	
	}

	void LateUpdate() {
		Vector3 newPos = Camera.main.transform.position;
		Vector3 diff = newPos - lastPos;
		lastPos = newPos;

		Vector3 backgroundPos = this.transform.position;
		backgroundPos.x += speed * diff.x;
		this.transform.position = backgroundPos;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
