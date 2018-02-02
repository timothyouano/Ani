using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace LukeWaffel.BUI{

	public class BUI : Singleton<BUI> {

		//Public enum for message type
		public enum UIType {Message, Warning, Success, Error}

		//Refrence variables
		GameObject boxCanvas;
		GameObject underlay;
		bool ready;

		//List of active boxed and queued boxes
		List<BoxDataCombo> activeBoxes = new List<BoxDataCombo>();
		List<UIBox> queuedBoxes = new List<UIBox>();

		//Setting variables
		public bool useUnderlay = true;
		public int maxActiveBoxes = 1;
		public delegate void OnBoxStateChangedEvent (BoxDataCombo changedBox);
		public static event OnBoxStateChangedEvent boxOpenedCallback;
		public static event OnBoxStateChangedEvent boxClosedCallback;

		//Default UI Box prefabs
		GameObject messageBoxPrefab;
		GameObject warningBoxPrefab;
		GameObject successionBoxPrefab;
		GameObject errorBoxPrefab;

		//Custom UI box prefabs
		List<GameObject> customBoxPrefabs = new List<GameObject> ();

		//Default UI Button prefabs
		GameObject messageButtonPrefab;
		GameObject warningButtonPrefab;
		GameObject successionButtonPrefab;
		GameObject errorButtonPrefab;

		//Custom UI button prefabs
		List<GameObject> customButtonPrefabs = new List<GameObject>();


		// Use this for initialization
		void Start () {

			//We start by retrieving the defaults and customs
			GetDefaults ();
			GetCustoms ();

			//We also get and spawna new Canvas for the UIBoxes
			GameObject canvasPrefab = (GameObject)Resources.Load ("BUI/Misc/CanvasPrefab");
			boxCanvas = Instantiate (canvasPrefab, Vector3.zero, Quaternion.identity);

			//We also load and spawn an underlay, which we then disable to make it invisible until we use it
			GameObject underlayPrefab = (GameObject)Resources.Load ("BUI/Misc/UnderlayPrefab");

			underlay = Instantiate (underlayPrefab, boxCanvas.transform, false);
			underlay.SetActive (false);

			if(EventSystem.current == null){
				GameObject eventSystemObj = (GameObject)Resources.Load ("BUI/Misc/EventSystemPrefab");
				GameObject instantiatedEventsystem = Instantiate (eventSystemObj, Vector3.zero, Quaternion.identity);
				instantiatedEventsystem.name = "EventSystem";
			}

			//Finally we stat that we're ready so that the script can do it's work
			ready = true;
		}

		//Gets all the default box prefabs and buttons
		void GetDefaults ()
		{
			messageBoxPrefab = (GameObject)Resources.Load ("BUI/DefaultBoxes/MessageBox");
			warningBoxPrefab = (GameObject)Resources.Load ("BUI/DefaultBoxes/WarningBox");
			successionBoxPrefab = (GameObject)Resources.Load ("BUI/DefaultBoxes/SuccessionBox");
			errorBoxPrefab = (GameObject)Resources.Load ("BUI/DefaultBoxes/ErrorBox");

			messageButtonPrefab = (GameObject)Resources.Load ("BUI/DefaultButtons/MessageButton");
			warningButtonPrefab = (GameObject)Resources.Load ("BUI/DefaultButtons/WarningButton");
			successionButtonPrefab = (GameObject)Resources.Load ("BUI/DefaultButtons/SuccessionButton");
			errorButtonPrefab = (GameObject)Resources.Load ("BUI/DefaultButtons/ErrorButton");
		}

		//Gets the custom prefabs and buttons
		void GetCustoms ()
		{
			customBoxPrefabs = Resources.LoadAll ("BUI/CustomBoxes/", typeof(GameObject)).Cast<GameObject>().ToList();
			customButtonPrefabs = Resources.LoadAll ("BUI/CustomButtons/", typeof(GameObject)).Cast<GameObject>().ToList();
		}

		void Update(){

			//If the script isn't ready or there are no queued boxes we return because it doesn't need to run
			if (!ready || queuedBoxes.Count == 0)
				return;

			//If there are boxes queued, we check if the amount of active boxes is less than the maxmimum
			if(activeBoxes.Count < maxActiveBoxes){
				//If it doesn't, we open a box and remove it from the queue
				OpenBox (queuedBoxes [0]);
				queuedBoxes.RemoveAt (0);
			}

		}

	
		#region HELPER_FUNCTIONS

		#region CALLBACK_HELPERS

 		//This function sets the box opened callback
		[System.Obsolete("The BoxOpenedCallback is now an event. You should subscribe to it")]
		public void SetOpenedCallback(OnBoxStateChangedEvent callback){
			boxOpenedCallback = callback;
		}

		//This function resets the box open callback
		[System.Obsolete("The BoxOpenedCallback is now an event. You should subscribe to it")]
		public void ResetOpenedCallback(){
			boxOpenedCallback = null;
		}

		//This function sets the boxed closed callback
		[System.Obsolete("The BoxClosedCallback is now an event. You should subscribe to it")]
		public void SetClosedCallback(OnBoxStateChangedEvent callback){
			boxClosedCallback = callback;
		}

		//This function resets the box closed callback
		[System.Obsolete("The BoxClosedCallback is now an event. You should subscribe to it")]
		public void ResetClosedCallbacl(){
			boxClosedCallback = null;
		}

		#endregion

		#region QUEUE_HELPERS

		//This function is used to add a box to the queue
		public void AddToQueue(UIBox box){
			//We simply add the desired box the the queue list
			queuedBoxes.Add (box);
		}

		//This function is used to remove a box from the queue by a UIBox variable
		public void RemoveFromQueue(UIBox box){
			//We simply call the other function with the ID of the UIBox variable
			RemoveFromQueue (box.id);
		}

		//This function is used to remove boxes from the queue using their ID
		public void RemoveFromQueue(string id){

			//We loop through all the boxes in the queue and check if their ID matches the given ID
			for(int i =0; i < queuedBoxes.Count; i++){
				//If it does, we remove it from the queue
				if (queuedBoxes [i].id == id)
					queuedBoxes.RemoveAt (i);
			}

		}

		#endregion

		#region CLOSING_HELPERS

		//This function closes a UIBox using a UIBox variable
		public void CloseBox(UIBox box){

			//Again we just call the CloseBox function variant that uses the ID
			CloseBox (box.id);

		}

		//This function closes multiple UI boxes
		public void CloseBoxes(UIBox[] boxes){
			//We loop through the given boxes
			for(int i =0; i < boxes.Length; i++){
				//And close them using the CloseBox function supplying the box ID
				CloseBox (boxes [i].id);
			}
		}

		//This function closes multiple UI boxes
		public void CloseBoxes(string[] ids){
			//We loop through all the ids
			for(int i =0; i <ids.Length; i++){
				//And use the CloseBox function to close the box using the ID
				CloseBox (ids [i]);
			}
		}

		//This function closes a UIBox using the ID of the box
		public void CloseBox(string boxID){

			//We loop through all the active boxes
			for(int i =0; i < activeBoxes.Count; i++){

				//Check if the ID's match
				if(activeBoxes[i].boxData.id == boxID){

					//We check if we have a close call back set
					if (boxClosedCallback != null)
						//If we have, we call it. (We call it BEFORE destroying the GameObject so scripts can retrieve information from the Box to use if they require to do so)
						boxClosedCallback (activeBoxes [i]);

					//If the box-specific close callback is set we call that too.
					if(activeBoxes[i].boxData.onClosedCallback != null)
						activeBoxes[i].boxData.onClosedCallback(activeBoxes[i]);

					//We then destroy the UI object and call the closed callback if there is one
					Destroy (activeBoxes [i].boxObject);

					//Finally we remove the box from the active box list
					activeBoxes.RemoveAt (i);
				}
			}

			//If the list of active boxes is now empty, we disable the underlay
			if (activeBoxes.Count == 0)
				underlay.SetActive (false);

		}
			
		#endregion

		#region GETACTIVE_HELPERS

		//This function returns the data of the active boxes
		public UIBox[] GetActiveBoxData(){

			//Create a new array the size of the active boxes list
			UIBox[] returnArray = new UIBox[activeBoxes.Count];

			//Loop through all the active boxes
			for(int i =0; i < activeBoxes.Count; i++){
				//Add their data to the array
				returnArray [i] = activeBoxes [i].boxData;
			}

			//Return the array
			return returnArray;

		}

		//This function returns all the ActiveBox objects
		public BoxDataCombo[] GetActiveBoxes(){

			//Create a new ActiveBox array the size of the active boxes list
			BoxDataCombo[] returnArray = new BoxDataCombo[activeBoxes.Count];

			//Loop through all actice boxes
			for(int i =0; i < activeBoxes.Count; i++){
				//Add the active box to the array
				returnArray [i] = activeBoxes [i];
			}

			//Return the array
			return returnArray;
		}

		#endregion

		#endregion

		//Handles opening UI boxes
		void OpenBox(UIBox box){

			//We start by trying to retrieve the correct object for the type of box we're showing (If it's a custom type we retrieve it)
			GameObject objectToShow = box.customTypeResourceName == null || box.customTypeResourceName == "" ? FetchObjectForEnum (box.GetUIType (), false) : FetchCustomObject(box.customTypeResourceName, false);

			//When then check if we loaded a box
			if (!objectToShow) {
				//If we haven't, show an error and exit the function
				Debug.LogError ("The Box you wanted to spawn cannot be found. This means we either had trouble loading the default boxes or you tried to load a custom box with a wrong name.");
				return;
			}

			//If we did load a box, we check if we're using an underlay, and if it's not already active
			if (useUnderlay && !underlay.activeSelf)
				//If it's not active and we do want to use one, we activate it
				underlay.SetActive (true);

			//We spawn the box onto the box canvas
			GameObject shownBox = Instantiate (objectToShow, boxCanvas.transform, false);

			//We change the title and message
			shownBox.transform.Find ("Header").GetComponentInChildren<Text> ().text = box.header;
			shownBox.transform.Find ("Body").Find ("Message").GetComponent<Text> ().text = box.body;

			//This is making a refrence to the Buttons part of the UI Box
			GameObject buttons = shownBox.transform.Find ("Body").Find ("Buttons").gameObject;

			//We check if there are any buttons the user wants to add to the UIBox
			for(int i =0; i < box.buttons.Count; i++){

				//Creating a refrence to the button we're adding
				UIButton button = box.buttons [i];

				//We create a desired button variable
				GameObject desiredButton = null;

				//Here we do a bunch of checks, if we use a different button type, load that
				if (button.hasDifferentButtonType)
					desiredButton = FetchObjectForEnum (button.customButtonType, true);
				//If the user wants to use a custom made button, we load that
				else if (button.hasCustomButtonPrefab)
					desiredButton = FetchCustomObject (button.customButtonPrefabName, true);
				//Otherwise we just use the UI type of the box it's in
				else
					desiredButton = FetchObjectForEnum (box.GetUIType (), true);

				//If there is we spawn a button of the type of the box
				GameObject spawnedButton = Instantiate (desiredButton, buttons.transform, false);

				//We change the text of the button to the desired text
				spawnedButton.GetComponentInChildren<Text> ().text = box.buttons [i].buttonText;

				//Finally we add a listener to the button
				AddButtonListener (spawnedButton.GetComponent<Button> (), box, button, i);
			}

			//We add the just opened box to the active box list
			activeBoxes.Add (new BoxDataCombo (shownBox, box));

			//If the box-specific callback has been set, we call that too
			if (box.onOpenedCallback != null)
				box.onOpenedCallback (new BoxDataCombo (shownBox, box));

			//And finally we call the callback if there is one
			if (boxOpenedCallback != null)
				boxOpenedCallback (new BoxDataCombo(shownBox,box));

		}

		//Handles adding listenerd to buttons on boxes
		public void AddButtonListener(Button b,UIBox box,UIButton button, int index){

			b.onClick.AddListener (() => {
				box.buttons[index].clickCallback(box, button);
			});

		}

		//Returns the desired GameObject for the specified UIType
		public GameObject FetchObjectForEnum(UIType type, bool button){

			if (type == UIType.Message)
				return button ? messageButtonPrefab : messageBoxPrefab;
			else if (type == UIType.Warning)
				return button ? warningButtonPrefab : warningBoxPrefab;
			else if (type == UIType.Error)
				return button ? errorButtonPrefab : errorBoxPrefab;
			else if (type == UIType.Success)
				return button ? successionButtonPrefab : successionBoxPrefab;
			else
				return null;
		}

		//This function gets custom objects for buttons and boxes
		public GameObject FetchCustomObject(string name, bool button){
			//We first check if we're dealing with a button
			if(button){

				//If we are, we loop through all the loaded custom buttons
				for(int i =0; i < customButtonPrefabs.Count; i++){

					//If the names match, we return the button
					if (customButtonPrefabs [i].name == name)
						return customButtonPrefabs [i];

				}

				//If we couldn't find the button, we return null
				return null;
			//If we're dealing with a custom box
			}else{

				//We loop through the boxes instead of the buttons
				for(int i =0; i < customBoxPrefabs.Count; i++){

					//Return the box if the names match
					if(customBoxPrefabs[i].name == name)
						return customBoxPrefabs [i];
					
				}

				//And return null if we didn't find the desired box
				return null;
			}
		}
	}
}