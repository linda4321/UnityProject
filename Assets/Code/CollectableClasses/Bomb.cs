using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRabitHit(HeroRabit rabit) {
		if (rabit.IsDefenseless) {
			LevelController.current.AddBombs (1);
			CollectedHide ();
			if (!rabit.HasDefaultSize)
				rabit.Inlarge (1f);
			else
				rabit.Die ();
		}
	}
}
