using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAllocator : MonoBehaviour {

    int passedAnimalID;
    GameObject model;
    AnimalDatabase database;

    // Use this for initialization
    void Start () {
        database = GetComponent<AnimalDatabase>();
        // Get previous clicked animal ID
        Animal animal = database.FetchAnimalByID(DataManager.animalClicked);
        Debug.Log(DataManager.animalClicked + " sadasda");

        // Instantiate model for previously clicked animal
        Debug.Log(animal.id);
        model = Instantiate((GameObject)Resources.Load("Prefab/Models" + animal.modelPath));
        model.transform.SetParent(GameObject.Find("UserDefinedTarget").transform);
        model.transform.localScale = new Vector3(0.01F,0.01F,0.01F);
        model.transform.position = Vector2.zero;
        model.name = "Model";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
