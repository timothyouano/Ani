using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour {

    GameObject inventoryPanel;
    GameObject slotPanel;
    AnimalDatabase database;
    public GameObject inventorySlot;
    public GameObject InventoryAnimal;

    private int slotAmount;
    public List<Animal> animals = new List<Animal>();
    public List<GameObject> slots = new List<GameObject>();


    // Use this for initialization
    void Start()
    {
        database = GetComponent<AnimalDatabase>();
        // How many slots
        slotAmount = 25;
        // Find InventoryPanel and instantiate slots
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
        for(int i = 0; i < slotAmount; i++) {
            animals.Add(new Animal());
            slots.Add(GameObject.Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
        }

        int[] aquired = PlayerPrefsX.GetIntArray("AnimalsAquired");
        for(int i = 0; i< aquired.Length; i++)
        {
            addAnimal(aquired[i]);
        }
    }

    void addAnimal(int id)
    {
        Animal animalToAdd = database.FetchAnimalByID(id);
        Debug.Log(animalToAdd.name);
        for(int i = 0; i < animals.Count; i++)
        {
            if(animals[i].id == -1)
            {
                animals[i] = animalToAdd;
                GameObject animalObj = Instantiate(InventoryAnimal);
                animalObj.transform.SetParent(slots[i].transform);
                slots[i].GetComponent<Button>().onClick.AddListener(() => {
                    // When Animal is clicked
                    DataManager.animalClicked = id;
                    AudioSource bgm = GameObject.Find("SceneManager").GetComponent<AudioSource>();
                    bgm.Stop();
                    Debug.Log(DataManager.animalClicked + " sad");
                    SceneManager.LoadScene("ARScreen");
                });
                animalObj.transform.position = Vector2.zero;
                animalObj.GetComponent<Image>().sprite = animalToAdd.sprite;
                animalObj.name = animalToAdd.name;
                break;
            }
        }
    }
}
