using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		HeroRabit rabit = collider.GetComponent<HeroRabit> ();
		if (rabit != null) {
			LevelController.current.OnRabitDeath (rabit);
		}
	}
}
