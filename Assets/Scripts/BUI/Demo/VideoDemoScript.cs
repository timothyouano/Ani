using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LukeWaffel.BUI;
using UnityEngine.SceneManagement;

public class VideoDemoScript : MonoBehaviour {

	[Header ("General variables")]
	public List<UIBox> boxes = new List<UIBox>();
	public int index;

	[Header ("Custom input variables")]
	public int inputBoxIndex = 8;
	public string customInput;

	public void OnEnable(){

		//Subscribing to the boxClosedCallback event
		BUI.boxClosedCallback += BoxClosed;

	}

	public void OnDisable(){
		//Unsubscribing from the boxClosedCallback event
		BUI.boxClosedCallback -= BoxClosed;
	}

	void Start(){

		//We need to setup the buttons first. Normally you'd do this when defining the message, but since we put them in a list, we're doing it this way.
		//To get more information on the normal way to setup a BUI Box, check the DemoScript.cs

		for(int i=0; i < boxes.Count; i++){

			//We loop through all the buttons for each box
			for(int k =0; k < boxes[i].buttons.Count; k++){

				//We check if it's named "Previous"
				if(boxes[i].buttons[k].buttonText == "Previous"){
					//Add a callback to the previous function
					boxes [i].buttons [k].clickCallback = Previous;
				//We also check if the name is "Next"
				}else if(boxes[i].buttons[k].buttonText == "Next"){
					//If it is we add a callback to the next function
					boxes [i].buttons [k].clickCallback = Next;
					//We also check if the name is "Main Menu" (The last box has this button)
				}else if(boxes[i].buttons[k].buttonText == "Main Menu"){
					//If it is we add a callback to the main menu function
					boxes [i].buttons [k].clickCallback = MainMenu;
				}
			}

		}

		//We queue the first box
		BUI.Instance.AddToQueue (boxes [index]);

	}

	//This runs when a previous button is pressed
	void Previous(UIBox boxInfo, UIButton buttonInfo){
		BUI.Instance.CloseBoxes (BUI.Instance.GetActiveBoxData ());
		index--;
		BUI.Instance.AddToQueue (boxes [index]);
	}

	//This runs when a next button is pressed
	void Next(UIBox boxInfo, UIButton buttonInfo){

		BUI.Instance.CloseBoxes (BUI.Instance.GetActiveBoxData ());
		index++;
		BUI.Instance.AddToQueue (boxes [index]);
	}

	//This function runs whenever a box is closed
	void BoxClosed(BoxDataCombo closedBox){

		//We check if the closed box is our custom box with the TextField
		if (closedBox.boxData.id == "customInput") {
			//If it is, we retrieve the input data from the TextField before the box is destroyed
			customInput = closedBox.boxObject.GetComponentInChildren<InputField> ().text;
			//And set the body of the next box to show our input
			boxes [index + 1].body = "Thank you for filling out our short survey. You helped us a lot!\r\n \r\n The answer we received: <color=teal> \r\n" + customInput + "</color>";
		}

	}

	void MainMenu(UIBox boxInfo, UIButton buttonInfo){
		
		SceneManager.LoadScene ("DemoMenu");

	}
}
