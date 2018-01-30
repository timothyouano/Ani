using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

    Text coinLabel;
    int coins;

	// Use this for initialization
	void Start () {
        coinLabel = this.GetComponent<Text>();
        coins = PlayerPrefs.GetInt("goldcoins");
        coinLabel.text = coins + "";
	}
}
