using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("isFirstTime", 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
