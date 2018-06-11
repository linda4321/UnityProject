using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCoinsPanel : CoinPanel {

	// Use this for initialization
	void Start () {
        UpdateCoins(LevelsManager.Instance.AllCoins);	
	}
}
