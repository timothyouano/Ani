using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomParts : MonoBehaviour {

    private Transform target;
    public Transform camera;
    public Transform ARCamera;
    private Vector3 targetpos;
    bool startZoom = false;

    public GameObject fader;

    float stopperOut = 0.0f;
    float stopperIn = 0.0f;
    float fadeOut = 0.0f;
    Transform model;
    Vector3 pointToLook;

    Vector3 part1_vector;
    Vector3 part2_vector;
    Vector3 part3_vector;
    Vector3 part1_rotation;
    Vector3 part2_rotation;
    Vector3 part3_rotation;

    Transform ARModelTransform;

    float offset = -0.15f;

    bool part1 = false;
    bool part2 = false;
    bool part3 = false;
    bool backToAR = false;
    bool fadeStart = false;

    sync syncComponent;

    void Start()
    {
        syncComponent = gameObject.GetComponent<sync>();
    }

    void Update()
    {
        if (syncComponent.synch)
        {
            ARModelTransform = syncComponent.getARTransform();
        }
        if (part1) // If animal part info 1 is pressed
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0,0, offset);
            camera.transform.LookAt(pointToLook);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - part1_vector;
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(part1_rotation), stopperIn);
            }
            else
            {
                stopperOut += Time.deltaTime;
                targetpos = new Vector3(camera.transform.position.x, 5f, camera.transform.position.z);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperOut);
            }

            if (stopperOut >= 1)
            {
                startZoom = true;
            }
            if (stopperIn >= 1)
            {
                part1 = false;
                startZoom = false;
                stopperIn = 0;
                stopperOut = 0;
            }
        }
        else if (part2) // If animal part info 2 is pressed
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0, 0, offset);
            camera.transform.LookAt(pointToLook);


            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - part2_vector;
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(part2_rotation), stopperIn);
            }
            else
            {
                stopperOut += Time.deltaTime;
                targetpos = new Vector3(camera.transform.position.x, 5f, camera.transform.position.z);
                //camera.transform.Translate(Vector3.back * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperOut);
            }

            if (stopperOut >= 1)
            {
                startZoom = true;
            }
            if (stopperIn >= 1)
            {
                part2 = false;
                startZoom = false;
                stopperIn = 0;
                stopperOut = 0;
            }
        }
        else if (part3) // If animal part info 3 is pressed
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0, 0, offset);
            camera.transform.LookAt(pointToLook);
            

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - part3_vector;
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(part3_rotation), stopperIn);
            }
            else
            {
                stopperOut += Time.deltaTime;
                targetpos = new Vector3(camera.transform.position.x, 5f, camera.transform.position.z);
                //camera.transform.Translate(Vector3.back * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperOut);
            }

            if (stopperOut >= 1)
            {
                startZoom = true;
            }
            if (stopperIn >= 1)
            {
                part3 = false;
                startZoom = false;
                stopperIn = 0;
                stopperOut = 0;
            }
        }
        else if (backToAR) // If back to AR button is pressed
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = ARModelTransform.position;
            camera.transform.LookAt(pointToLook);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = ARCamera.position;
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to go back to it's original position in the AR
                model.transform.position = ARModelTransform.position;
                model.rotation = Quaternion.Slerp(model.transform.rotation, ARModelTransform.rotation * Quaternion.Euler(0, -180f, 0), stopperIn);

                fader.GetComponent<Image>().color = Color.Lerp(fader.GetComponent<Image>().color, Color.black, stopperIn);
            }
            else if (fadeStart)
            {
                fadeOut += Time.deltaTime;
                fader.GetComponent<Image>().color = Color.Lerp(fader.GetComponent<Image>().color, Color.clear, fadeOut);
            }
            else
            {
                stopperOut += Time.deltaTime;
                targetpos = new Vector3(camera.transform.position.x, 5f, camera.transform.position.z);
                //camera.transform.Translate(Vector3.back * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperOut);
            }


            if (stopperOut >= 1)
            {
                // Starts the zooming
                startZoom = true;
                fader.SetActive(true);
            }
            if (stopperIn >= 0.3)
            {
                // If zooming already and reached 0.3 seconds
                startZoom = false;
                fadeStart = true;

                camera.transform.position = ARCamera.position;
                model.transform.position = ARModelTransform.position;
                model.rotation = ARModelTransform.rotation * Quaternion.Euler(0, -180f, 0);

                stopperIn = 0;
                stopperOut = 0;
            }
            if(fadeOut >= 0.1)
            {
                // If fading out animation is active and reached 0.1 seconds
                gameObject.GetComponent<sync>().EnableARModel();
            }
            if(fadeOut >= 0.2)
            {
                // If fading out animation is active and reached 0.3 seconds
                model.localScale = new Vector3(0, 0, 0);
            }
            if (fadeOut >= 0.4)
            {
                // If fading out animation is active and reached 1 second
                fader.SetActive(false);
                fadeStart = false;
                backToAR = false;
                fadeOut = 0;
                model.localScale = new Vector3(1, 1, 1);
                Destroy(model.gameObject);
            }
        }
    }

    public void showPart1(float[] part1_vector, float[] part1_rotation)
    {
        this.part1_vector = new Vector3(part1_vector[0], part1_vector[1], part1_vector[2]);
        this.part1_rotation = new Vector3(part1_rotation[0], part1_rotation[1], part1_rotation[2]);
        part1 = true;
    }

    public void showPart2(float[] part2_vector, float[] part2_rotation)
    {
        this.part2_vector = new Vector3(part2_vector[0], part2_vector[1], part2_vector[2]);
        this.part2_rotation = new Vector3(part2_rotation[0], part2_rotation[1], part2_rotation[2]);
        part2 = true;
    }

    public void showPart3(float[] part3_vector, float[] part3_rotation)
    {
        this.part3_vector = new Vector3(part3_vector[0], part3_vector[1], part3_vector[2]);
        this.part3_rotation = new Vector3(part3_rotation[0], part3_rotation[1], part3_rotation[2]);
        part3 = true;
    }

    public void BackToAR()
    {
        backToAR = true;
    }

}
