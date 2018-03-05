using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {

    GameObject achievementPanel;
    GameObject slotPanel;
    AchievementsDatabase database;
    public GameObject inventorySlot;
    public GameObject InventoryAchievement;

    private int slotAmount;
    public List<Achievement> achivements = new List<Achievement>();
    public List<GameObject> slots = new List<GameObject>();


    // Use this for initialization
    void Start()
    {
        database = GetComponent<AchievementsDatabase>();
        // How many slots
        slotAmount = database.getCount();
        // Find InventoryPanel and instantiate slots
        achievementPanel = GameObject.Find("AchievementPanel");
        slotPanel = achievementPanel.transform.Find("SlotPanel").gameObject;
        //slotPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(GameObject.Find("AchievementPanel").GetComponent<RectTransform>().rect.width, 80);
        for (int i = 0; i < slotAmount; i++)
        {
            achivements.Add(new Achievement());
            slots.Add(GameObject.Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.position = new Vector3(0,0,0);
        }

        int[] aquired = PlayerPrefsX.GetIntArray("AnimalsAquired");
        for (int i = 0; i < database.getCount(); i++)
        {
            addAchievement(i);
        }
    }

    void addAchievement(int id)
    {
        Achievement achievementToAdd = database.FetchAchievementByID(id);
        for (int i = 0; i < achivements.Count; i++)
        {
            if (achivements[i].id == -1)
            {
                achivements[i] = achievementToAdd;
                GameObject animalObj = Instantiate(InventoryAchievement);
                animalObj.transform.SetParent(slots[i].transform);
                animalObj.transform.position = Vector2.zero;
                animalObj.transform.GetChild(0).GetComponent<Text>().text = achievementToAdd.name;
                animalObj.name = achievementToAdd.name;
                int progress = (int)animalObj.transform.GetChild(1).GetComponent<RectTransform>().rect.width / achievementToAdd.requirement;
                // Get how many animals
                progress *= PlayerPrefs.GetInt(achievementToAdd.type);
                // Set progress bar
                animalObj.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().offsetMin = new Vector2(progress, 0);
                animalObj.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetInt(achievementToAdd.type) + "/" + achievementToAdd.requirement;
                break;
            }
        }
    }
}
