using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 diff;
	public float stopTime = 1f;
	public float speed = 1f;

	private Vector3 A;
	private Vector3 B;

	private float eps = 0.03f;

	private bool goingToA = false;
	private float timer;

	// Use this for initialization
	void Start () {
		A = this.transform.position;
		B = A + diff;
		timer = stopTime;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currPos = this.transform.position;
		Vector3 target;

		if (goingToA)
			target = A;
		else
			target = B;
		if (isArrive (currPos, target)) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				timer = stopTime;
				goingToA = !goingToA;
			}
		} else {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);
		}
	}
		

	bool isArrive(Vector3 currPos, Vector3 targetPos) {
		return Vector3.Distance (targetPos, currPos) < eps;
	}
}
