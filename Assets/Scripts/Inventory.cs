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

    int[] aquired;


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

        aquired = PlayerPrefsX.GetIntArray("AnimalsAquired");
        for(int i = 1; i <= database.Count(); i++)
        {
            if (!contains(i))
            {
                addAnimal(-i);
                
            }
            else
            {
                addAnimal(i);
                
            }
        }
    }

    bool contains(int val)
    {
        bool flag = false;
        for(int i = 0; i < aquired.Length; i++)
        {
            if (aquired[i] == val)
            {
                flag = true;
            }
        }
        return flag;
    }

    void addAnimal(int id)
    {
        Animal animalToAdd;
        if (database.FetchAnimalByID(id) != null)
        {
            animalToAdd = database.FetchAnimalByID(id);
        }
        else
        {
            animalToAdd = database.FetchAnimalByID(Mathf.Abs(-id));
        }
        Debug.Log(animalToAdd.name);
        for(int i = 0; i < animals.Count; i++)
        {
            if(animals[i].id == -999)
            {
                if(id >= 0)
                {
                    animals[i] = animalToAdd;
                    GameObject animalObj = Instantiate(InventoryAnimal);
                    animalObj.transform.SetParent(slots[i].transform);
                    slots[i].GetComponent<Button>().onClick.AddListener(() => {
                        // When Animal is clicked
                        DataManager.animalClicked = id;
                        AudioSource bgm = GameObject.Find("SceneManager").GetComponent<AudioSource>();
                        bgm.Stop();
                        // Redirect To AR Screen
                        SceneManager.LoadScene("ARScreen");
                    });
                    animalObj.transform.position = Vector2.zero;
                    animalObj.GetComponent<Image>().sprite = animalToAdd.sprite;
                    animalObj.name = animalToAdd.name;
                    break;
                }
                else
                {
                    animals[i] = animalToAdd;
                    GameObject animalObj = Instantiate(InventoryAnimal);
                    animalObj.transform.SetParent(slots[i].transform);
                    slots[i].GetComponent<Button>().onClick.AddListener(() => {
                        // When Animal is clicked
                        DataManager.animalClicked = Mathf.Abs(id);
                        AudioSource bgm = GameObject.Find("SceneManager").GetComponent<AudioSource>();
                        bgm.Stop();
                        Debug.Log(animalToAdd.name);

                        GameObject panelBuyContainer = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
                        panelBuyContainer.SetActive(true);
                        // Saves when bought
                        panelBuyContainer.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                            int gold = PlayerPrefs.GetInt("goldcoins");

                            if (gold >= 250)
                            {
                                int[] animals = PlayerPrefsX.GetIntArray("AnimalsAquired");
                                int[] dummy = new int[animals.Length + 1];
                                for (int j = 0; j < animals.Length; j++)
                                {
                                    dummy[j] = animals[j];
                                }
                                dummy[dummy.Length - 1] = Mathf.Abs(id);
                                PlayerPrefsX.SetIntArray("AnimalsAquired", dummy);

                                // Subtract gold
                                gold -= 250;
                                PlayerPrefs.SetInt("goldcoins", gold);
                                PlayerPrefs.Save();

                                // Loads Scene
                                DataManager.animalClicked = Mathf.Abs(id);
                                SceneManager.LoadScene("FoundNewAnimal");
                                GameObject.Find("SceneManager").GetComponent<ScreenManager>().setBought(true);
                            }
                            else
                            {
                                StartCoroutine(notEnoughGoldPanel());
                            }
                            
                        });
                        // Cancels the transaction
                        panelBuyContainer.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {
                            panelBuyContainer.SetActive(false);
                        });

                    });
                    animalObj.transform.position = Vector2.zero;
                    animalObj.GetComponent<Image>().sprite = animalToAdd.sprite;
                    animalObj.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    animalObj.name = animalToAdd.name;
                    break;
                }
            }
        }
    }

    IEnumerator notEnoughGoldPanel()
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
    }
}
