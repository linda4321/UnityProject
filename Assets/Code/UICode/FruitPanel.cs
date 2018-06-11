using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitPanel : MonoBehaviour {

    public Text text;

    private int allFruits;

    public void SetAllFruitsLabel(int allFruits)
    {
        this.allFruits = allFruits;
        text.text = "00" + "/" + allFruits.ToString("00");
    }

	public void UpdatePanel(int fruits)
    {
        text.text = fruits.ToString("00") + "/" + this.allFruits.ToString("00");
    }

    public void UpdatePanel(int fruits, int allFruits)
    {
        text.text = fruits.ToString("00") + "/" + allFruits.ToString("00");
    }
}
