using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrc : Orc
{
    public float attackRadius = 3f;
    public float attackInterval = 2f;

    public GameObject prefabCarrot;
    private float last_carrot = 0;

    void Update()
    {
        if (mode == Mode.GoToA)
            if (IsArrived(pointA))
                mode = Mode.GoToB;
        else if (mode == Mode.GoToB)
            if (IsArrived(pointB))
                mode = Mode.GoToA;

        float diff = HeroRabit.lastRabit.transform.position.x - this.transform.position.x;
        if (Mathf.Abs(diff) < attackRadius)
        {
            mode = Mode.Attack;
            float direction = 0;
            if (diff > 0)
            {
                orcSprite.flipX = true;
                direction = 1;
            }
            else
            {
                orcSprite.flipX = false;
                direction = -1;
            }
                
            if (Time.time - last_carrot > attackInterval)
            {
                orcAnim.SetTrigger("attack");
                PlayAttackSound();
                this.LaunchCarrot(direction);
            }
        }
        else
        {
            if (orcSprite.flipX)
                mode = Mode.GoToB;
            else
                mode = Mode.GoToA;
        }

        if (mode == Mode.Attack)
            orcAnim.SetBool("walk", false);
        else
            orcAnim.SetBool("walk", true);
    }

    private void LaunchCarrot(float direction)
    {
        GameObject obj = GameObject.Instantiate(this.prefabCarrot, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        Carrot carrot = obj.GetComponent<Carrot>();
        carrot.Launch(direction);
        last_carrot = Time.time;
    }

    protected override float GetDirection()
    {
        my_pos = this.transform.localPosition;

        if (mode == Mode.GoToA)
        {
            if (my_pos.x < pointA.x)
                return 1;
            else
                return -1;
        }

        if (mode == Mode.GoToB)
        {
            if (my_pos.x < pointB.x)
                return 1;
            else
                return -1;
        }
        return 0;
    }

    protected override void OnOrcDie(HeroRabit rabit)
    {
        rabit.Jump();
        this.orcAnim.SetTrigger("dead");   
        StartCoroutine(WaitForOrcDeathAnim());
    }

    protected override IEnumerator WaitForOrcDeathAnim()
    {
        yield return new WaitForSeconds(orcAnim.GetCurrentAnimatorStateInfo(0).length);
        this.orcAnim.SetBool("walk", false);
        this.orcAnim.SetBool("deathquit", true);
        Destroy(this.gameObject);
    }
}
