using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AchievementsDatabase : MonoBehaviour {

    private List<Achievement> database = new List<Achievement>();
    private LitJson.JsonData achievementData;

    // Use this for initialization
    void Start()
    {
        string jsonString = "";
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Achievements.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
            achievementData = LitJson.JsonMapper.ToObject(jsonString);
        }
        else
        {
            achievementData = LitJson.JsonMapper.ToObject(File.ReadAllText(filePath));
        }

        ConstructAchievementDatabase();
    }

    public Achievement FetchAchievementByID(int id)
    {
        Achievement achiemenent = new Achievement();
        for (int i = 0; i < database.Count; i++)
        {
            if (id == (int)database[i].id)
            {
                achiemenent = database[i];
                break;
            }
        }
        return achiemenent;
    }

    public Achievement FetchAchievementByName(string name)
    {
        Achievement achievement = null;
        for (int i = 0; i < database.Count; i++)
        {
            if (name.Equals(database[i].name.ToLower()))
            {
                achievement = database[i];
                break;
            }
        }
        return achievement;
    }

    void ConstructAchievementDatabase()
    {
        for (int i = 0; i < achievementData.Count; i++)
        {
            database.Add(new Achievement((int)achievementData[i]["id"], achievementData[i]["name"].ToString(), achievementData[i]["type"].ToString(), (int)achievementData[i]["requirement"]));
        }
    }

    public int getCount()
    {
        return achievementData.Count;
    }

}

public class Achievement
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int requirement { get; set; }

    public Achievement()
    {
        this.id = -1;
    }

    public Achievement(int id, string name, string type, int requirement)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.requirement = requirement;
    }
}
