using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnimalDatabase : MonoBehaviour {

    private List<Animal> database = new List<Animal>();
    private LitJson.JsonData animalData;

	// Use this for initialization
	void Start () {
        string jsonString = "";
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Animals.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
            animalData = LitJson.JsonMapper.ToObject(jsonString);
        }
        else
        {
            animalData = LitJson.JsonMapper.ToObject(File.ReadAllText(filePath));
        }
        
        ConstructAnimalDatabase();
	}

    public Animal FetchAnimalByID(int id)
    {
        Animal animal = null;
        for(int i = 0; i < database.Count; i++)
        {
            if(id == (int)database[i].id)
            {
                animal = database[i];
                break;
            }
        }
        return animal;
    }

    public Animal FetchAnimalByName(string name)
    {
        Animal animal = null;
        for (int i = 0; i < database.Count; i++)
        {
            if (name.Equals(database[i].name.ToLower()))
            {
                animal = database[i];
                break;
            }
        }
        return animal;
    }

    public int Count()
    {
        return database.Count;
    }

    void ConstructAnimalDatabase() {
        for(int i = 0; i < animalData.Count; i++)
        {
            database.Add(new Animal((int)animalData[i]["id"], animalData[i]["name"].ToString(), float.Parse(animalData[i]["scale"].ToString()), float.Parse(animalData[i]["rotation"].ToString()), animalData[i]["part1_vector"].ToString(), animalData[i]["part2_vector"].ToString(), animalData[i]["part3_vector"].ToString(), animalData[i]["part1_rotation"].ToString(), animalData[i]["part2_rotation"].ToString(), animalData[i]["part3_rotation"].ToString(), animalData[i]["modelpath"].ToString(), animalData[i]["introduction"].ToString(), animalData[i]["info1"].ToString(), animalData[i]["info2"].ToString(), animalData[i]["info3"].ToString(), animalData[i]["type"].ToString()));
        }
    }

}

public class Animal{
    public int id { get; set; }
    public string name { get; set; }
    public float scale { get; set; }
    public float rotation { get; set; }
    public string part1_vector { get; set; }
    public string part2_vector { get; set; }
    public string part3_vector { get; set; }
    public string part1_rotation { get; set; }
    public string part2_rotation { get; set; }
    public string part3_rotation { get; set; }
    public string modelPath { get; set; }
    public Sprite sprite { get; set; }
    public string intro { get; set; }
    public string info1 { get; set; }
    public string info2 { get; set; }
    public string info3 { get; set; }
    public string type { get; set; }

    public Animal() {
        this.id = -999;
    }

    public Animal(int id, string name, float scale, float rotation, string part1_vector, string part2_vector, string part3_vector, string part1_rotation, string part2_rotation, string part3_rotation, string modelPath, string intro,string info1, string info2, string info3, string type)
    {
        this.id = id;
        this.name = name;
        this.rotation = rotation;
        this.part1_vector = part1_vector;
        this.part2_vector = part2_vector;
        this.part3_vector = part3_vector;
        this.part1_rotation = part1_rotation;
        this.part2_rotation = part2_rotation;
        this.part3_rotation = part3_rotation;
        this.scale = scale;
        this.modelPath = modelPath;
        this.sprite = Resources.Load<Sprite>("Sprite/Animals/" + name);
        this.intro = intro;
        this.info1 = info1;
        this.info2 = info2;
        this.info3 = info3;
        this.type = type;
    }
}