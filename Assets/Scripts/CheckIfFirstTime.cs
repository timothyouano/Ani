using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckIfFirstTime : MonoBehaviour {

    // Use this for initialization

    AudioSource audioSource;
    public static CheckIfFirstTime Instance;

    void Start () {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("isFirstTime") || PlayerPrefs.GetInt("isFirstTime") != 1)
        {
            PlayerPrefs.SetInt("goldcoins", 100);
            SceneManager.LoadScene("TutorialSceneLanding");
        }
        else
        {
            audioSource.Play();
        }
    }

    public void backgroundPlay()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
