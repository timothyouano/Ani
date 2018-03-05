using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionsDatabase : MonoBehaviour {

    private List<Question> database = new List<Question>();
    private LitJson.JsonData questionData;

    // Use this for initialization
    void Start()
    {
        string jsonString = "";
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Questions.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }

            jsonString = reader.text;
            questionData = LitJson.JsonMapper.ToObject(jsonString);
        }
        else
        {
            questionData = LitJson.JsonMapper.ToObject(File.ReadAllText(filePath));
        }

        ConstructQuestionDatabase();
    }

    public Question FetchQuestionByID(int id)
    {
        Question question = new Question();
        for (int i = 0; i < database.Count; i++)
        {
            if (id == (int)database[i].id)
            {
                question = database[i];
                break;
            }
        }
        return question;
    }

    public Question FetchQuestionByName(string name)
    {
        Question question = null;
        for (int i = 0; i < database.Count; i++)
        {
            if (name.Equals(database[i].animal_name.ToLower()))
            {
                question = database[i];
                break;
            }
        }
        return question;
    }

    void ConstructQuestionDatabase()
    {
        for (int i = 0; i < questionData.Count; i++)
        {
            database.Add(new Question((int)questionData[i]["id"], questionData[i]["animal_name"].ToString(), questionData[i]["answer1"].ToString(), questionData[i]["answer2"].ToString(), questionData[i]["answer3"].ToString(), questionData[i]["answer4"].ToString()));
        }
    }

}

public class Question
{
    public int id { get; set; }
    public string animal_name { get; set; }
    public string answer1 { get; set; }
    public string answer2 { get; set; }
    public string answer3 { get; set; }
    public string answer4 { get; set; }

    public Question()
    {
        this.id = -1;
    }

    public Question(int id, string animal_name, string answer1, string answer2, string answer3, string answer4)
    {
        this.id = id;
        this.animal_name = animal_name;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.answer4 = answer4;
    }
}
