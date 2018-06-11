using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public static HeroRabit lastRabit = null;

    public float speed = 1;
	public float jumpSpeed = 1f;
	public float jumpTime = 1f;

	public float defenselessTime = 4f;

    public AudioClip walk;
    public AudioClip jump;
    public AudioClip death;

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

    private int lifes = 3;

	public bool IsDefenseless {get {return isDefenseless; } }
	public bool HasDefaultSize { get { return hasDefaultSize; } }

    public bool IsGrounded { get { return isGrounded; } }

    public int Lifes { get { return lifes; } }

    private AudioSource walkSource;
    private AudioSource deathSource;
    private AudioSource jumpSource;

    void Awake()
    {
        lastRabit = this;
    }

    // Use this for initialization
    void Start () {
		rabitBody = this.GetComponent<Rigidbody2D> ();
		rabitSprite = this.GetComponent<SpriteRenderer> ();
		rabitAnim = this.GetComponent<Animator> ();

        walkSource = gameObject.AddComponent<AudioSource>();
        walkSource.clip = walk;

        deathSource = gameObject.AddComponent<AudioSource>();
        deathSource.clip = death;

        jumpSource = gameObject.AddComponent<AudioSource>();
        jumpSource.clip = jump;

        LevelController.current.SetStartPosition (this.transform.position);
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
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground") {
				SetNewParent (this.transform, hit.transform);
               
			}
		} else {
			isGrounded = false;
            SetNewParent(this.transform, parent);
            if (SoundManager.Instance.IsSoundOn())
                jumpSource.Play();
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
        {
            rabitAnim.SetBool("jump", false);
        }
        else
        {
            rabitAnim.SetBool("jump", true);
        }

        if (rabitBody.velocity.x != 0 && isGrounded)
        {
            if (SoundManager.Instance.IsSoundOn() && !walkSource.isPlaying)
                walkSource.Play();
        }    
        else
            walkSource.Stop();
    }

    public void PlayDeathSound()
    {
        if (SoundManager.Instance.IsSoundOn())
            deathSource.Play();
    }

    public void Jump()
    {
        rabitBody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
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

    public void MinusLife()
    {
        if (lifes > 0)
            lifes--;
    }

    public void PlusLife()
    {
        if (lifes < 3)
            lifes++;
    }

    public void Stop()
    {
        rabitAnim.SetBool("run", false);
        walkSource.Stop();
        Vector2 velocity = rabitBody.velocity;
        velocity.x = 0;
        rabitBody.velocity = velocity;
    }

    public void Die()
    {
        PlayDeathSound();
        rabitAnim.SetTrigger("dead");
        StartCoroutine(WaitForDeathAnim());
    }

	IEnumerator WaitForDeathAnim()
	{
		yield return new WaitForSeconds(rabitAnim.GetCurrentAnimatorStateInfo(0).length/rabitAnim.GetCurrentAnimatorStateInfo(0).speed);
		LevelController.current.OnRabitDeath (this);
	}

	static void SetNewParent(Transform obj, Transform new_parent){
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
}
