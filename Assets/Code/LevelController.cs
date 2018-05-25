using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	public Vector3 rabitStartPos;

	private int coins = 0;
	private int fruits = 0;
	private int crystals = 0;
	private int mushrooms = 0;
	private int bombs = 0;

	void Awake() {
		current = this;
	}

	public void setStartPosition(Vector3 position) {
		rabitStartPos = position;
	}

	public void OnRabitDeath(HeroRabit rabit){
		rabit.transform.position = rabitStartPos;
	}

	public void AddCoins(int coins){
		this.coins += coins;
		Debug.Log ("Coin added");
	}

	public void AddFruits(int fruits){
		this.fruits += fruits;
		Debug.Log ("Fruit added");
	}

	public void AddCrystals(int crystals){
		this.crystals += crystals;
		Debug.Log ("Crystal added");
	}

	public void AddMushrooms(int mushrooms){
		this.mushrooms += mushrooms;
		Debug.Log ("Mushroom added");
	}

	public void AddBombs(int bombs){
		this.bombs += bombs;
		Debug.Log ("Bomb added");
	}
}
