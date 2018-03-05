using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageToComputerVisionAPI : MonoBehaviour {

    string VISIONKEY = "f477fa9f646842279aa8dbf02ec6075b"; // replace with your Computer Vision API Key

    string emotionURL = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/tag";

    public string fileName { get; private set; }
    string responseData;
    AnimalDatabase database;
    CameraController cam;

    // Use this for initialization
    void Start () {
        //fileName = Application.streamingAssetsPath + "/cityphoto.jpg"; // Replace with your file
        //fileName = System.IO.Path.Combine(Application.streamingAssetsPath, "lion.jpg");
        cam = gameObject.GetComponent<CameraController>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void getData(byte[] bytes)
    {
        StartCoroutine(GetVisionDataFromImages(bytes));
    }

    /// <summary>
    /// Get emotion data from the Cognitive Services Emotion API
    /// Stores the response into the responseData string
    /// </summary>
    /// <returns> IEnumerator - needs to be called in a Coroutine </returns>
    IEnumerator GetVisionDataFromImages(byte[] bytes)
    {
        #if UNITY_WINRT
                        byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(fileName);
        #else
                //WWW reader = new WWW(fileName);
                //while (!reader.isDone) { }
                //byte[] bytes = reader.bytes;
        #endif

        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", VISIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string
        FoundImageObject c = GetComponent<ParseComputerVisionResponse>().ParseJSONData(responseData);

        processImage(c);
    }

    public void processImage(FoundImageObject c)
    {
        // Acquiring animal phase
        database = GameObject.Find("Image").GetComponent<AnimalDatabase>();
        ScreenManager sManager = GameObject.Find("SceneManager").GetComponent<ScreenManager>();
        Animal animal;

        foreach (Category cat in c.categories)
        {
            // Fetch animal from database if animal fetched is not null then add to acquired animals
            animal = database.FetchAnimalByName(cat.name.Replace("\"", ""));
            if (animal != null)
            {
                int[] animals = PlayerPrefsX.GetIntArray("AnimalsAquired");
                int[] dummy = new int[animals.Length + 1];
                for (int i = 0; i < animals.Length; i++)
                {
                    dummy[i] = animals[i];
                }
                if (!contains(animals,animal.id))
                {
                    dummy[dummy.Length - 1] = animal.id;
                    PlayerPrefsX.SetIntArray("AnimalsAquired", dummy);
                    DataManager.animalClicked = animal.id;
                    // Set Found Animals
                    PlayerPrefs.SetInt("foundAnimals", PlayerPrefs.GetInt("foundAnimals") + 1);
                    PlayerPrefs.SetInt(animal.type, PlayerPrefs.GetInt(animal.type) + 1);
                    // Set ScreenManager modified to false
                    sManager.setModify(false);
                    // Off camera
                    cam.Exit();
                    // Redirect to FoundNewAnimal Screen
                    SceneManager.LoadScene("FoundNewAnimal");
                }
                else
                {
                    StartCoroutine(activateDuplicatePanel());
                }
                break;
            }
            Debug.Log("Category = " + cat.name);
        }
    }

    // Shows error message then closes after 3 sescond
    IEnumerator activateDuplicatePanel()
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(false);
        cam.StartCam();
    }
    
    // Checks if user already has that animal
    public bool contains(int[] animals, int animalFind)
    {
        bool flag = false;
        for(int i = 0; i < animals.Length; i++)
        {
            if(animals[i] == animalFind)
            {
                flag = true;
            }
        }
        return flag;
    }
}
