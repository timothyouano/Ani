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

        // Instantiate model for previously clicked animal
        Debug.Log(animal.id);
        model = Instantiate((GameObject)Resources.Load("Prefab/Models" + animal.modelPath));
        model.transform.SetParent(GameObject.Find("UserDefinedTarget").transform);
        model.transform.localScale = new Vector3(0.01F,0.01F,0.01F);
        model.transform.position = Vector2.zero;
        model.transform.rotation = new Quaternion(0,-90F,0,0);
        model.SetActive(false);
        model.name = "Model";
    }

    public void enable()
    {
        model.SetActive(true);
    }

    public void disable()
    {
        StartCoroutine(disableIE());
    }

    public void enableInfo()
    {
        GameObject child;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Information");
        for(int i = 1; i < objects.Length; i++)
        {
            child = objects[i].transform.GetChild(0).gameObject;
            child.SetActive(true);
        }
        //GameObject.Find("Information").GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator disableIE()
    {
        yield return new WaitForSeconds(0.0005f);
        model.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
