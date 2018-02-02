using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeWaffel.BUI{

	[System.Serializable]
	public class UIButton{

		public string buttonText;

		public delegate void OnClickedEvent(UIBox boxData, UIButton buttonData);
		public OnClickedEvent clickCallback;

		public BUI.UIType customButtonType;
		public string customButtonPrefabName;

		public bool hasDifferentButtonType;
		public bool hasCustomButtonPrefab;

		public UIButton(string ButtonText, OnClickedEvent ClickCallback){
			buttonText = ButtonText;
			clickCallback = ClickCallback;
		}

		public UIButton(string ButtonText, OnClickedEvent ClickCallback, BUI.UIType DifferentButtonType){
			buttonText = ButtonText;
			clickCallback = ClickCallback;
			customButtonType = DifferentButtonType;
			hasDifferentButtonType = true;
		}

		public UIButton(string ButtonText, OnClickedEvent ClickCallback, string CustomButtonPrefabName){
			buttonText = ButtonText;
			clickCallback = ClickCallback;
			customButtonPrefabName = CustomButtonPrefabName;
			hasCustomButtonPrefab = true;
		}

	}
}
