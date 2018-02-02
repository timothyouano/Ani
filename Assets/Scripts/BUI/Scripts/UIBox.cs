using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeWaffel.BUI{

	[System.Serializable]
	public class UIBox{

		public BUI.UIType type;
		public string customTypeResourceName;

		public string id;

		public string header;
		public string body;

		public delegate void OnStateChangedAction(BoxDataCombo data);
		public OnStateChangedAction onOpenedCallback;
		public OnStateChangedAction onClosedCallback;

		public List<UIButton> buttons = new List<UIButton>();

		public UIBox(string ID, BUI.UIType Type){
			id = ID;
			type = Type;
		}

		public UIBox(string ID, string CustomTypeResourceName){
			id = ID;
			customTypeResourceName = CustomTypeResourceName;
		}

		public BUI.UIType GetUIType(){
			return type;
		}
	}
}
