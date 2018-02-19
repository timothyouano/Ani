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
        // Note: Done modify helps to not go and assign again any button after every loop of an update, not putting doneModify wil make the app lag
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
                    SceneManager.LoadScene("Stash");
                    doneModify = false;
                });
                GameObject.Find("SoundBtn").GetComponent<Button>().onClick.AddListener(() => {
                    this.GetComponent<MuteBtn>().Mute();
                });
                menuSet = true;
            }
        }
        if(currentScene == "TutorialSceneLanding" && !doneModify) // Tutorial Screen
        {
            AudioSource audioSource = GetComponent<AudioSource>(); ;
            audioSource.Stop();
            fromTutorial = true;
            doneModify = true;
        }
        else if (currentScene == "RequireInternetModal" && !doneModify) // No Internet Screen
        {
            menuSet = false;
            GameObject.Find("TapListener").GetComponent<Button>().onClick.AddListener(() => {
                returnToMenu();
            });
            doneModify = true;
        }
        else if (currentScene == "Play" && !doneModify) // Play Screen
        {
            menuSet = false;
            GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() => {
                returnToMenu();
            });
            doneModify = true;
        }
        else if (currentScene == "FoundNewAnimal" && !doneModify)
        {
            menuSet = false;
            GameObject.Find("TapListener").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("Stash");
            });
            doneModify = true;
        }

        // If back button on android is pressed then go back to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape)){
            menuSet = false;
            if (currentScene == "Play")
            {
                GameObject.Find("Image").GetComponent<CameraController>().Exit();
            }
            returnToMenu();
        }
    }

    public void setModify(bool val)
    {
        this.doneModify = val;
    }

    public void tapSound()
    {
        tapSounds.Play();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
