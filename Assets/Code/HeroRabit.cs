using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

	public float speed = 1;

	private Rigidbody2D rabitBody = null;
	private SpriteRenderer rabitSprite;

	// Use this for initialization
	void Start () {
		rabitBody = this.GetComponent<Rigidbody2D> ();
		rabitSprite = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float value = Input.GetAxis ("Horizontal");

		Vector2 velocity = rabitBody.velocity;
		velocity.x = speed * value;
		rabitBody.velocity = velocity;

		if (value > 0) {
			rabitSprite.flipX = false;
		} else if (value < 0) {
			rabitSprite.flipX = true;
		}
	}
}
