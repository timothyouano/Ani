using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconAllocator : MonoBehaviour {

    AnimalDatabase database;

	// Use this for initialization
	void Start () {
        database = gameObject.GetComponent<AnimalDatabase>();
        Animal animal = database.FetchAnimalByID(DataManager.animalClicked);
        GameObject.Find("AnimalIcon").GetComponent<Image>().sprite = animal.sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
