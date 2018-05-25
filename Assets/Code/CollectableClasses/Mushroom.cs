using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRabitHit(HeroRabit rabit) {
		if (rabit.HasDefaultSize) {
			rabit.Enlarge (2f);
		}
		LevelController.current.AddMushrooms (1);
		CollectedHide ();
	}
}
