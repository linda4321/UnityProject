using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    public float timeToSelfDestroy = 3f;
    
 //   private Transform destination;
    private float direction;
    private bool _isFlying = false;
    private float speed = 2;

    // Use this for initialization
    void Start () {
        StartCoroutine(DestroyLater());
    }
	
	void FixedUpdate () {
        if (_isFlying)
        {
            this.transform.Translate((direction * speed * Time.deltaTime), 0, 0, Space.World);
        }
    }

    public void Launch(float direction)
    {
        this.direction = direction;
        _isFlying = true;
    }

    IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(timeToSelfDestroy);
        Destroy(this.gameObject);
    }

    protected override void OnRabitHit(HeroRabit rabit)
    {
        CollectedHide();
        if (rabit.IsDefenseless)
        {
 //           CollectedHide();
            if (!rabit.HasDefaultSize)
                rabit.Inlarge(1f);
            else
                rabit.Die();
        }
 //       rabit.Die();
    }
}
