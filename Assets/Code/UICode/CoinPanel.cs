using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour {

    public Text text;

    public void UpdateCoins(int coins)
    {
        text.text = coins.ToString("0000");
    }
}
