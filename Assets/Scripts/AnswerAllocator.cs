using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerAllocator : MonoBehaviour {

    QuestionsDatabase database;
    AchievementsDatabase aDatabase;

    public Button[] buttons;
    public Image questionImage;
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public GameObject achievedPanel;
    public AudioSource rewardSound;
    public AudioSource wrongSound;

    bool[] allocated;
    List<Button> allocButton;
    string[] answerText;
    bool[] allocAnswer;
    bool achieved = false;

    // Use this for initialization
    void Start () {
        database = gameObject.GetComponent<QuestionsDatabase>();
        aDatabase = gameObject.GetComponent<AchievementsDatabase>();

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
                        PlayerPrefs.SetInt("goldcoins", PlayerPrefs.GetInt("goldcoins") + 5);
                        PlayerPrefs.SetInt("answeredQuestion", PlayerPrefs.GetInt("answeredQuestion") + 1);

                        // Check if achieved something
                        Achievement answeredAchievement = aDatabase.FetchAchievementByName("Answer 3 Questions");
                        // Get user Achievements
                        bool[] userAchievements = PlayerPrefsX.GetBoolArray("userAchievements");
                        if (answeredAchievement.requirement <= PlayerPrefs.GetInt("answeredQuestion") && !userAchievements[answeredAchievement.id])
                        {
                            PlayerPrefs.SetInt("goldcoins", PlayerPrefs.GetInt("goldcoins") + answeredAchievement.reward_gold);
                            PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + answeredAchievement.reward_exp);
                            achieved = true;

                            // Copy Achievements and save new
                            bool[] dummy = new bool[aDatabase.getCount()];
                            for (int i = 0; i < userAchievements.Length; i++)
                            {
                                dummy[i] = userAchievements[i];
                            }
                            dummy[answeredAchievement.id] = true;
                            PlayerPrefsX.SetBoolArray("userAchievements", dummy);
                        }
                        // Save
                        PlayerPrefs.Save();
                        rewardSound.Play();
                        correctPanel.SetActive(true);
                    });
                }
                else
                {
                    buttons[rand].onClick.AddListener(() => {
                        wrongPanel.SetActive(true);
                        wrongSound.Play();
                    });
                }
                allocButton.Add(buttons[rand]);
                allocated[rand] = true;
                allocAnswer[rand] = true;
            }
        }

        correctPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            if (achieved)
            {
                correctPanel.SetActive(false);
                achievedPanel.SetActive(true);
                rewardSound.Play();
            }
            else
            {
                GameObject.Find("SceneManager").GetComponent<ScreenManager>().returnToMenu();
            }
        });

        achievedPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Find("SceneManager").GetComponent<ScreenManager>().returnToMenu();
        });

        wrongPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Find("SceneManager").GetComponent<ScreenManager>().returnToMenu();
        });

        questionImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/AnimalInfoImage/" + question.animal_name + "/" + correctAnswer);
        questionImage.GetComponent<Image>().preserveAspect = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
