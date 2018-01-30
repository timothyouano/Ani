using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TutorialScreenSoundCheck : MonoBehaviour {

    AudioSource audioSource;
    string currentScene;
    bool isMainMenuAfterTutorial = true;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        checkForMute();
	}

    public void checkForMute()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu" && isMainMenuAfterTutorial == false)
        {
            audioSource.Stop();
        }
        else
        {
            isMainMenuAfterTutorial = false;
        }
    }
}
