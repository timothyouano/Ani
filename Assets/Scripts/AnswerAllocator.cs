using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerAllocator : MonoBehaviour {

    QuestionsDatabase database;

    public Button[] buttons;
    public Image questionImage;
    public GameObject correctPanel;
    public AudioSource rewardSound;

    bool[] allocated;
    List<Button> allocButton;
    string[] answerText;
    bool[] allocAnswer;

    // Use this for initialization
    void Start () {
        database = gameObject.GetComponent<QuestionsDatabase>();
        allocButton = new List<Button>();
        allocAnswer = new bool[4];
        allocated = new bool[4];
        answerText = new string[4];
        System.Random srand = new System.Random();

        int rand = 0;
        int correctAnswer = srand.Next(1,4); // change this
        Question question = database.FetchQuestionByID(2);

        answerText[0] = question.answer1;
        answerText[1] = question.answer2;
        answerText[2] = question.answer3;
        answerText[3] = question.answer4;

        while (allocButton.Count != 4)
        {
            rand = srand.Next(0,4);
            
            if (!allocated[rand] && !allocAnswer[rand])
            {
                buttons[rand].transform.GetChild(0).GetComponent<Text>().text = answerText[rand];
                if (rand == correctAnswer-1)
                {
                    buttons[rand].onClick.AddListener(() => {
                        correctPanel.SetActive(true);
                        PlayerPrefs.SetInt("goldcoins", PlayerPrefs.GetInt("goldcoins") + 5);
                        PlayerPrefs.SetInt("answeredQuestion", PlayerPrefs.GetInt("answeredQuestion") + 1);
                        PlayerPrefs.Save();
                        rewardSound.Play();
                    });
                }
                allocButton.Add(buttons[rand]);
                allocated[rand] = true;
                allocAnswer[rand] = true;
            }
        }

        correctPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Find("SceneManager").GetComponent<ScreenManager>().returnToMenu();
        });

        questionImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoImage/" + question.animal_name + "/" + correctAnswer);
        questionImage.GetComponent<Image>().preserveAspect = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
