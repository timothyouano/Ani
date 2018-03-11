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
    bool fromAR = false;
    bool boughtAnimal = false;
    public static bool fromTutorial = false;

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
        if ((currentScene == "Stash" && !doneModify) || fromAR)
        {
            exit = GameObject.Find("Exit").GetComponent<Button>();
            exit.onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
                menuSet = false;
            });
            doneModify = true;
            fromAR = false;
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
                    GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
                    doneModify = false;
                });
                GameObject.Find("StashBtn").GetComponent<Button>().onClick.AddListener(() => {
                    SceneManager.LoadScene("Stash");
                    GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
                    doneModify = false;
                });
                GameObject.Find("AchievementBtn").GetComponent<Button>().onClick.AddListener(() => {
                    SceneManager.LoadScene("Achievements");
                    GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
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
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
            });
            doneModify = true;
        }
        else if (currentScene == "Play" && !doneModify) // Play Screen
        {
            menuSet = false;
            GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(() => {
                returnToMenu();
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
            });
            doneModify = true;
        }
        else if (currentScene == "FoundNewAnimal" && !doneModify) // Found New Animal Screen
        {
            menuSet = false;
            GameObject.Find("TapListener").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("Stash");
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
            });
            doneModify = true;
        }
        else if(currentScene == "ARScreen" && !doneModify) // AR Screen
        {
            menuSet = false;
            GameObject.Find("TargetBuilderCanvas").transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("Quiz");
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
                //doneModify = false;

            });
            GameObject.Find("TargetBuilderCanvas").transform.GetChild(8).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("Stash");
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
                doneModify = false;
                fromAR = true;

            });
            doneModify = true;
        }
        else if (currentScene == "Achievements" && !doneModify) // AR Screen
        {
            menuSet = false;
            GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenu");
                menuSet = false;
            });
            doneModify = true;
        }

        if (currentScene == "FoundNewAnimal" && boughtAnimal)
        {
            menuSet = false;
            GameObject.Find("TapListener").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("Stash");
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
            });
            doneModify = true;
            boughtAnimal = false;
        }

        // If back button on android is pressed then go back to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape)){
            menuSet = false;
            if (currentScene == "Play")
            {
                GameObject.Find("Image").GetComponent<CameraController>().Exit();
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
                returnToMenu();
            }
            else if( currentScene == "ARScreen")
            {
                GameObject.Find("TargetBuilderCanvas").transform.GetChild(8).gameObject.SetActive(true);
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().setValueSpeak("Before you go, would you mind answering a question for gold coins and experience?");
                GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Speak();
            }
            else
            {
                returnToMenu();
            }
        }

    }

    void OnApplicationPause(bool pauseStatus)
    {
        GameObject.Find("_Scene").GetComponent<Crosstales.RTVoice.Demo.AniVoice>().Stop();
    }

    public void setModify(bool val)
    {
        this.doneModify = val;
    }

    public void setBought(bool val)
    {
        this.boughtAnimal = val;
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
