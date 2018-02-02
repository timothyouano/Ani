using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeWaffel.BUI;
using UnityEngine.SceneManagement;

public class DemoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

		ShowPicker ();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void CloseBox(string id)
    {
        BUI.Instance.CloseBox(id);
    }

    void CloseButtonPressed(UIBox boxInfo, UIButton buttonInfo){

		CloseBox (boxInfo.id);
		ShowPicker ();

	}

	//This function shows the message box
	void ShowMessage(UIBox boxInfo, UIButton buttonInfo){

		//We first close the box picker
		BUI.Instance.CloseBox ("selector");

		//We then start off by creating a new UIBox with the unique ID 'message' with a UIType of Message
		UIBox message = new UIBox ("message", BUI.UIType.Message);

		//We change the title to "Message" using the header variable
		message.header = "Message";
		//By changing the body variable we change the message of the box
		message.body = "This is a message box. This is often used to display generic messages to the user.";
		//We also add one button. We do this by creating a new UI button. The first paramater is the text we want the button to have. The second one is the function we want to run
		message.buttons.Add (new UIButton ("Close", CloseButtonPressed));

		//When we're done creating the box, we add it to the queue system so that the BUI system can ensure it get's displayed at the right moment.
		BUI.Instance.AddToQueue (message);
	}

	//This function shows the warning box
	void ShowWarning(UIBox boxInfo, UIButton buttonInfo){

		//We first close the box picker
		BUI.Instance.CloseBox ("selector");

		//We then start off by creating a new UIBox with the unique ID 'message' with a UIType of Message
		UIBox warning = new UIBox ("warning", BUI.UIType.Warning);

		//We change the title to "Message" using the header variable
		warning.header = "Warning";
		//By changing the body variable we change the message of the box
		warning.body = "This is a warning box. This is often used to display warnings to the user.";
		//We also add one button. We do this by creating a new UI button. The first paramater is the text we want the button to have. The second one is the function we want to run
		warning.buttons.Add (new UIButton ("Close", CloseButtonPressed));

		//When we're done creating the box, we add it to the queue system so that the BUI system can ensure it get's displayed at the right moment.
		BUI.Instance.AddToQueue (warning);
	}

	//This function shows the success box
	void ShowSuccess(UIBox boxInfo, UIButton buttonInfo){

		//We first close the box picker
		BUI.Instance.CloseBox ("selector");

		//We then start off by creating a new UIBox with the unique ID 'message' with a UIType of Message
		UIBox succes = new UIBox ("success", BUI.UIType.Success);

		//We change the title to "Message" using the header variable
		succes.header = "Success";
		//By changing the body variable we change the message of the box
		succes.body = "This is a success box. This is often used to display successes to the user to the user.";
		//We also add one button. We do this by creating a new UI button. The first paramater is the text we want the button to have. The second one is the function we want to run
		succes.buttons.Add (new UIButton ("Close", CloseButtonPressed));

		//When we're done creating the box, we add it to the queue system so that the BUI system can ensure it get's displayed at the right moment.
		BUI.Instance.AddToQueue (succes);
	}

	//This function shows the error box
	void ShowError(UIBox boxInfo, UIButton buttonInfo){

		//We first close the box picker
		BUI.Instance.CloseBox ("selector");

		//We then start off by creating a new UIBox with the unique ID 'message' with a UIType of Message
		UIBox error = new UIBox ("error", BUI.UIType.Error);

		//We change the title to "Message" using the header variable
		error.header = "Error";
		//By changing the body variable we change the message of the box
		error.body = "This is a error box. This is often used to display errors to the user.";
		//We also add one button. We do this by creating a new UI button. The first paramater is the text we want the button to have. The second one is the function we want to run
		error.buttons.Add (new UIButton ("Close", CloseButtonPressed));

		//When we're done creating the box, we add it to the queue system so that the BUI system can ensure it get's displayed at the right moment.
		BUI.Instance.AddToQueue (error);
	}

	//This function shows the custom input box
	void ShowCustomInput(UIBox boxInfo, UIButton buttonInfo){

		//We first close the box picker
		BUI.Instance.CloseBox ("selector");

		//We then start off by creating a new UIBox with the unique ID 'message' with custom UI type. BUI will load this custom type for you.
		UIBox custom = new UIBox ("customInput", "videoDemoUserInput");

		//We change the title to "Message" using the header variable
		custom.header = "A custom box";
		//By changing the body variable we change the message of the box
		custom.body = "This custom box proves you can make anything you want with BUI. You can even use information from this box in your scripts. Check the " +
			"video demo for more info.";
		//We also add one button. We do this by creating a new UI button. The first paramater is the text we want the button to have. The second one is the function we want to run
		custom.buttons.Add (new UIButton ("Close", CloseButtonPressed));

		custom.onOpenedCallback = Test;

		//When we're done creating the box, we add it to the queue system so that the BUI system can ensure it get's displayed at the right moment.
		BUI.Instance.AddToQueue (custom);

	}

	//This code displays the box picker
	void ShowPicker(){

		//We create a new UIBox with ID: selector and custom type: demoBoxPicker
		UIBox selectorBox = new UIBox ("selector", "demoBoxPicker");
		//We change the title using the header variable
		selectorBox.header = "Select a box";
		//We also change the message by using the body variable (Both of these actions aren't required);
		selectorBox.body = "Click a button to see how that type of box looks!";

		//We also add four buttons, the first parameter is the text on the button and the second is the function to call
		selectorBox.buttons.Add (new UIButton ("Message box", ShowMessage));
		//The next 3 buttons have 3 parameters, that's because I want them to look like the type of the box they'll open.
		//The first one doesn't have this parameter because the default style is already Message, so there's no reason to edit it.
		selectorBox.buttons.Add (new UIButton ("Warning box", ShowWarning, BUI.UIType.Warning));
		selectorBox.buttons.Add (new UIButton ("Succes box", ShowSuccess, BUI.UIType.Success));
		selectorBox.buttons.Add (new UIButton ("Error box", ShowError, BUI.UIType.Error));

		//We also add a button for a custom box. This box will use a custom button type
		selectorBox.buttons.Add (new UIButton ("Custom box", ShowCustomInput, "customInputButton"));

		//The last button we add will lead us back to the main menu
		selectorBox.buttons.Add (new UIButton ("Main Menu", MainMenu, BUI.UIType.Error));

		//In the end, we add our newly created box to the queue system. The BUI system will now ensure it'll be displayed at the right time
		BUI.Instance.AddToQueue (selectorBox);
	}

	void MainMenu(UIBox boxInfo, UIButton buttonInfo){

		SceneManager.LoadScene ("DemoMenu");

	}

	void Test(BoxDataCombo combo){
		
	}
}
