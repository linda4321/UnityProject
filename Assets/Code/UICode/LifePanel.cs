using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour {

    public Sprite life;
    public Sprite withoutLife;
    public Image[] lifes;


    public void MinusLife(int index)
    {
        if(index < lifes.Length)
            lifes[index].sprite = withoutLife;
    }

    public void PlusLife(int index)
    {
        if (index < lifes.Length)
            lifes[index].sprite = life;
    }
}
