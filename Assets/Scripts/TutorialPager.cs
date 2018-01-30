using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TutorialPager : MonoBehaviour {
    string currentScene;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextLevel() {
        ScreenManager.Instance.tapSound();
        if(currentScene == "TutorialSceneLanding")
        {
            SceneManager.LoadScene("TutorialScene1");
        }
        else if (currentScene == "TutorialScene1")
        {
            SceneManager.LoadScene("TutorialScene2");
        }
        else if (currentScene == "TutorialScene2")
        {
            SceneManager.LoadScene("TutorialScene3");
        }
        else if (currentScene == "TutorialScene3")
        {
            SceneManager.LoadScene("TutorialScene4");
        }
        else if (currentScene == "TutorialScene4")
        {
            SceneManager.LoadScene("TutorialScene5");
        }
        else if (currentScene == "TutorialScene5")
        {
            SceneManager.LoadScene("TutorialScene6");
        }
        else if (currentScene == "TutorialScene6")
        {
            SceneManager.LoadScene("TutorialScene7");
        }
        else if (currentScene == "TutorialScene7")
        {
            SceneManager.LoadScene("TutorialScene8");
        }
        else if (currentScene == "TutorialScene8")
        {
            PlayerPrefs.SetInt("isFirstTime", 1);
            PlayerPrefs.SetInt("goldcoins", PlayerPrefs.GetInt("goldcoins") + 5);
            PlayerPrefs.Save();
            CheckIfFirstTime.Instance.backgroundPlay();
            ScreenManager.fromTutorial = true;
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void skiplevel()
    {
        PlayerPrefs.SetInt("isFirstTime", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
