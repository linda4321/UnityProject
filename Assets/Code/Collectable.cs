using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{ 
    public AudioClip pickedClip;
    
    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null)
        {
            if (SoundManager.Instance.IsSoundOn())
                Music.music.PlayCLip(pickedClip);
            this.OnRabitHit(rabit);
        }
    }

    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnRabitHit(HeroRabit rabit)
    {
    }
}
