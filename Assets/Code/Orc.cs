using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {

    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }

    public float speed = 1;
    public Vector3 pointA;
    public Vector3 pointB;

    public AudioClip attackClip;

    protected Rigidbody2D orcBody = null;
    protected Animator orcAnim = null;
    protected SpriteRenderer orcSprite;

    protected float eps = 0.03f;
    protected float deathEps = 0.5f;
    protected Mode mode;
    protected Vector3 rabit_pos;
    protected Vector3 my_pos;

    private AudioSource attackSource;

    // Use this for initialization
    void Start () {
        orcBody = this.GetComponent<Rigidbody2D>();
        orcSprite = this.GetComponent<SpriteRenderer>();
        orcAnim = this.GetComponent<Animator>();

        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackClip;

        mode = Mode.GoToA;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float value = this.GetDirection();

        Vector2 velocity = orcBody.velocity;
        velocity.x = speed * value;
        orcBody.velocity = velocity;

        if (value > 0)
        {
            orcSprite.flipX = true;
        }
        else if (value < 0)
        {
            orcSprite.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        HeroRabit rabit = collider.gameObject.GetComponent<HeroRabit>();
        if (rabit != null)
        {
            Debug.Log("Hit rabit");
            if ((rabit.transform.position.y - this.transform.position.y) > deathEps)
                OnOrcDie(rabit);
            else
            {
                OnRabitDie(rabit);
            }
        }
    }

    protected virtual bool IsArrived(Vector3 point)
    {
        return Mathf.Abs(this.transform.localPosition.x - point.x) < eps;
    }

    protected virtual float GetDirection()
    {
        return 0;
    }

    protected virtual void OnOrcDie(HeroRabit rabit)
    {
    }

    protected virtual void OnRabitDie(HeroRabit rabit)
    {
    }

    protected virtual void PlayAttackSound()
    {
        if(SoundManager.Instance.IsSoundOn())
            attackSource.Play();
    }

    protected virtual IEnumerator WaitForOrcDeathAnim()
    {
        yield return new WaitForSeconds(orcAnim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}
