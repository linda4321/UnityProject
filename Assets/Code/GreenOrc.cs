using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc
{
    void Update()
    {
        if (mode == Mode.GoToA)
            if (IsArrived(pointA))
                mode = Mode.GoToB;
        else if (mode == Mode.GoToB)
            if (IsArrived(pointB))
                mode = Mode.GoToA;

        if (HeroRabit.lastRabit.transform.parent == this.transform.parent)
        {
            rabit_pos = HeroRabit.lastRabit.transform.localPosition;
            my_pos = this.transform.localPosition;

            if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x) && rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
            {
                mode = Mode.Attack;
                Debug.Log("Is attacking");
            }
            else
                TurnOnWalkMode();
        }
        else
            TurnOnWalkMode();
       

        if (mode == Mode.Attack)
        {
            orcAnim.SetBool("run", true);
            orcAnim.SetBool("walk", false);
        }
        else
        {
            orcAnim.SetBool("walk", true);
            orcAnim.SetBool("run", false);
        }
    }

    protected override float GetDirection()
    {
        my_pos = this.transform.localPosition;

        if (mode == Mode.Attack)
        {
            rabit_pos = HeroRabit.lastRabit.transform.localPosition;
            if (my_pos.x < rabit_pos.x)
                return 1;
            else
                return -1;
        }

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

    private void TurnOnWalkMode()
    {
        if (orcSprite.flipX)
            mode = Mode.GoToB;
        else
            mode = Mode.GoToA;
    }

    protected override void OnOrcDie(HeroRabit rabit)
    {
        rabit.Jump();
        this.orcAnim.SetTrigger("dead");
        this.orcAnim.SetBool("walk", false);
        this.orcAnim.SetBool("run", false);
        StartCoroutine(WaitForOrcDeathAnim());
    }

    protected override void OnRabitDie(HeroRabit rabit)
    {
        PlayAttackSound();
        this.orcAnim.SetTrigger("attack");

        if (rabit.IsDefenseless)
        {
            if (!rabit.HasDefaultSize)
                rabit.Inlarge(1f);
            else
                rabit.Die();
        }      
  //      rabit.Die();
    }
}