using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

    private bool isCollected;

    public bool IsCollected { get { return isCollected; } set { isCollected = value; } }

	protected override void OnRabitHit(HeroRabit rabit) {
		LevelController.current.AddFruits (1, transform.position);
		CollectedHide ();
	}
}
