using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LukeWaffel.BUI{

	[System.Serializable]
	public class BoxDataCombo {

		public GameObject boxObject;
		public UIBox boxData;

		public BoxDataCombo(GameObject BoxObject, UIBox BoxData){
			boxObject = BoxObject;
			boxData = BoxData;
		}

	}
}
