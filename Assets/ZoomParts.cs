using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomParts : MonoBehaviour {

    private Transform target;
    public Transform camera;
    public Transform ARCamera;
    private Vector3 targetpos;
    bool startZoom = false;

    float stopperOut = 0.0f;
    float stopperIn = 0.0f;
    Transform model;
    Vector3 pointToLook;

    Transform ARModelTransform;

    float offset = -0.15f;

    bool part1 = false;
    bool part2 = false;
    bool part3 = false;
    bool backToAR = false;

    void Start()
    {

    }

    void Update()
    {
        ARModelTransform = gameObject.GetComponent<sync>().getARTransform();
        if (part1)
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0,0, offset);
            camera.transform.LookAt(pointToLook);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - new Vector3(-0.6f, -1f, 0);
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(0,-90f,0), stopperIn);
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
        else if (part2)
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0, 0, offset);
            camera.transform.LookAt(pointToLook);


            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - new Vector3(-0.6f, -1f, 0);
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(0, 90f, 0), stopperIn);
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
        else if (part3)
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position - new Vector3(0, 0, offset);
            camera.transform.LookAt(pointToLook);
            

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - new Vector3(-0.6f, 0.2f, 0);
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to look at camera
                model.rotation = Quaternion.Slerp(model.transform.rotation, camera.transform.rotation * Quaternion.Euler(0, -90f, 0), stopperIn);
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
        else if (backToAR)
        {
            model = GameObject.Find("Model-duplicate").transform;
            pointToLook = model.position;
            camera.transform.LookAt(pointToLook);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = ARCamera.position;
                // zoom to model
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
                // Rotate the model to go back to it's original position in the AR
                model.transform.position = Vector3.Lerp(model.transform.position, ARModelTransform.position, stopperIn);
                model.rotation = Quaternion.Slerp(model.transform.rotation, ARModelTransform.rotation * Quaternion.Euler(0, -180f, 0), stopperIn);
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
                backToAR = false;
                startZoom = false;
                gameObject.GetComponent<sync>().EnableARModel();
                stopperIn = 0;
                stopperOut = 0;
            }
        }
    }

    public void showPart1()
    {
        part1 = true;
    }

    public void showPart2()
    {
        part2 = true;
    }

    public void showPart3()
    {
        part3 = true;
    }

    public void BackToAR()
    {
        backToAR = true;
    }

}
