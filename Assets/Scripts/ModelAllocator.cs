using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelAllocator : MonoBehaviour {

    int passedAnimalID;
    GameObject model;
    AnimalDatabase database;
    Animal animal;

    public GameObject scene;
    public GameObject info1;
    public GameObject info2;
    public GameObject info3;

    Crosstales.RTVoice.Demo.AniVoice _scene;

    // Use this for initialization
    void Start () {
        database = GetComponent<AnimalDatabase>();
        // Get previous clicked animal ID
        animal = database.FetchAnimalByID(DataManager.animalClicked);
        _scene = scene.GetComponent<Crosstales.RTVoice.Demo.AniVoice>();

        // Instantiate model for previously clicked animal
        Debug.Log(animal.id);
        model = Instantiate((GameObject)Resources.Load("Prefab/Models" + animal.modelPath));
        model.transform.SetParent(GameObject.Find("UserDefinedTarget").transform);
        model.transform.localScale = new Vector3(0.01F,0.01F,0.01F);
        model.transform.position = Vector2.zero;
        model.transform.rotation = new Quaternion(0,-90F,0,0);
        model.SetActive(false);
        model.name = "Model";

        Debug.Log("Animal info " + animal.info1 + " Animal ID" + animal.id);
    }

    public void speakIntro()
    {
        _scene.valueSpeak = animal.intro;
        _scene.Speak();
    }

    public void activateInfoSpeak()
    {
        // Add button informations for previously clicked animal

        info1.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/1");
        info2.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/1");
        info3.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoButton/" + animal.name + "/1");

        Debug.Log("ANIMAL NAME " + animal.name);

        info1.GetComponent<Button>().onClick.AddListener(() => {
            _scene.valueSpeak = animal.info1;
            _scene.Speak();
        });

       info2.GetComponent<Button>().onClick.AddListener(() => {
            _scene.valueSpeak = animal.info2;
            _scene.Speak();
        });

       info3.GetComponent<Button>().onClick.AddListener(() => {
            _scene.valueSpeak = animal.info3;
            _scene.Speak();
        });
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
