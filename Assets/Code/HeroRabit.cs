using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

	public float speed = 1;
	public float jumpSpeed = 1f;
	public float jumpTime = 1f;

	public float defenselessTime = 4f;

	private bool isGrounded = false;
	private bool isJumping = false;
	private float currJumpTime = 0;

	private Rigidbody2D rabitBody = null;
	private Animator rabitAnim = null;
	private SpriteRenderer rabitSprite;

	private Transform parent = null;

	private bool hasDefaultSize = true;
	private bool isDefenseless = true;
	private float bombTimer = 0;

    private Color bonusColor;
    private Color defaultSpriteColor;

	public bool IsDefenseless {get {return isDefenseless; } }
	public bool HasDefaultSize { get { return hasDefaultSize; } }

	// Use this for initialization
	void Start () {
		rabitBody = this.GetComponent<Rigidbody2D> ();
		rabitSprite = this.GetComponent<SpriteRenderer> ();
		rabitAnim = this.GetComponent<Animator> ();

		LevelController.current.setStartPosition (this.transform.position);
		parent = this.transform.parent;
   
		bombTimer = defenselessTime;
        bonusColor = new Color(1f, 0.75f, 0.85f, 255f);
        defaultSpriteColor = rabitSprite.color;
    }
	
	// Update is called once per frame
	void Update () {
		if (!isDefenseless) {
            
            if (bombTimer > 0)
            {
                bombTimer -= Time.deltaTime;
                rabitSprite.color = bonusColor;
            }		
			else {
				isDefenseless = true;
				bombTimer = defenselessTime;
                rabitSprite.color = defaultSpriteColor;
			}
		}
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

		if (Mathf.Abs (value) > 0) {
			rabitAnim.SetBool ("run", true);
		} else {
			rabitAnim.SetBool ("run", false);
		}

		Vector3 fromVect = this.transform.position + Vector3.up * 0.3f;
		Vector3 toVect = this.transform.position + Vector3.down * 0.1f;

		int layerId = 1 << LayerMask.NameToLayer ("Ground");
		RaycastHit2D hit = Physics2D.Linecast (fromVect, toVect, layerId);

		if (hit) {
			isGrounded = true;
			if (hit.transform != null && hit.transform.GetComponent<MovingPlatform> () != null) {
				SetNewParent (this.transform, hit.transform);
			}
		} else {
			isGrounded = false;
			SetNewParent(this.transform, parent);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded)
			this.isJumping = true;

		if (isJumping) {
			if (Input.GetButton ("Jump")) {
				currJumpTime += Time.deltaTime;
				if (currJumpTime < jumpTime) {
					Vector2 vel = rabitBody.velocity;
					vel.y = jumpSpeed - currJumpTime/jumpTime + 0.5f;
					rabitBody.velocity = vel;
				}
			} else {
				isJumping = false;
				currJumpTime = 0;
			}
		}

		if (isGrounded)
			rabitAnim.SetBool ("jump", false);
		else
			rabitAnim.SetBool ("jump", true);
	}

	public void Enlarge(float factor) {
		this.transform.localScale = new Vector3 (factor, factor, factor);
		hasDefaultSize = false;
	}

	public void Inlarge(float factor) {
		this.transform.localScale = new Vector3 (factor, factor, factor);
		hasDefaultSize = true;
		isDefenseless = false;
	}

	public void Die(){
		rabitAnim.SetBool ("dead", true);
		StartCoroutine (WaitForDeathAnim ());
	}

	IEnumerator WaitForDeathAnim()
	{
		yield return new WaitForSeconds(rabitAnim.GetCurrentAnimatorStateInfo(0).length);
		rabitAnim.SetBool ("dead", false);
		LevelController.current.OnRabitDeath (this);
		Debug.Log ("Death anim");
	}

	static void SetNewParent(Transform obj, Transform new_parent){
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
}
