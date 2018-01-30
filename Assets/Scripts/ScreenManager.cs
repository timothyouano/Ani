using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {

    public static ScreenManager Instance;
    public AudioSource tapSounds;
    Button exit;
    string currentScene;
    bool doneModify = false;
    bool menuSet = false;
    public static bool fromTutorial = false;

    public void returnToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Play");
    }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(transform.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Stash" && !doneModify)
        {
            exit = GameObject.Find("Exit").GetComponent<Button>();
            exit.onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
                menuSet = false;
            });
            doneModify = true;
        }
        else
        {
            if (!menuSet)
            {
                GameObject.Find("StashBtn").GetComponent<Button>().onClick.AddListener(() => {
                    SceneManager.LoadScene("Stack");
                    doneModify = false;
                });
                GameObject.Find("SoundBtn").GetComponent<Button>().onClick.AddListener(() => {
                    this.GetComponent<MuteBtn>().Mute();
                });
                menuSet = true;
            }
            if (fromTutorial)
            {
                menuSet = false;
                fromTutorial = false;
            }
        }
        if(currentScene == "TutorialSceneLanding")
        {
            AudioSource audioSource = GetComponent<AudioSource>(); ;
            audioSource.Stop();
        }
    }

    public void tapSound()
    {
        tapSounds.Play();
    }

}
