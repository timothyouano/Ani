using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LukeWaffel.BUI;

public class DemoMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		UIBox choice = new UIBox ("choice", BUI.UIType.Message);
		choice.header = "Menu";
		choice.body = "What demo would you like to open?";

		choice.buttons.Add (new UIButton ("Demo", LoadScene));
		choice.buttons.Add (new UIButton ("Video Demo", LoadScene));
		choice.buttons.Add (new UIButton ("Quit", Quit));

		BUI.Instance.AddToQueue (choice);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadScene(UIBox boxInfo, UIButton buttonInfo){
		
		SceneManager.LoadScene (buttonInfo.buttonText);

	}

	void Quit(UIBox boxInfo, UIButton buttonInfo){
		Application.Quit();
	}
}
