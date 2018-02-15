using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LukeWaffel.BUI;

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

    IEnumerator checkConnection()
    {
        string url = "https://google.com";
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone && www.bytesDownloaded > 0)
        {
            print("done");
            SceneManager.LoadScene("Play");
        }
        if (www.isDone && www.bytesDownloaded == 0)
        {
            print("no connection");

        }
    }

    public void PlayButton()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SceneManager.LoadScene("RequireInternetModal");
        }
        else
        {
            SceneManager.LoadScene("Play");
        }
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
        if (currentScene == "Stack" && !doneModify)
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
            if (fromTutorial)
            {
                menuSet = false;
                fromTutorial = false;
            }
            if (!menuSet && currentScene == "MainMenu")
            {
                GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(() => {
                    PlayButton();
                    doneModify = false;
                });
                GameObject.Find("StashBtn").GetComponent<Button>().onClick.AddListener(() => {
                    SceneManager.LoadScene("Stack");
                    doneModify = false;
                });
                GameObject.Find("SoundBtn").GetComponent<Button>().onClick.AddListener(() => {
                    this.GetComponent<MuteBtn>().Mute();
                });
                menuSet = true;
            }
        }
        if(currentScene == "TutorialSceneLanding")
        {
            AudioSource audioSource = GetComponent<AudioSource>(); ;
            audioSource.Stop();
            fromTutorial = true;
        }
    }

    public void tapSound()
    {
        tapSounds.Play();
    }

}
