using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("isFirstTime", 0);
        //PlayerPrefsX.SetIntArray("AnimalsAquired", new int[6]);
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("goldcoins", 1000);
        PlayerPrefs.Save();

        Debug.Log("test = " + PlayerPrefs.GetInt("random"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
