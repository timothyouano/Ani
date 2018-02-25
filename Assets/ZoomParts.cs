using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomParts : MonoBehaviour {

    private Transform target;
    public Transform camera;
    private Vector3 targetpos;
    bool startZoom = false;

    float stopperOut = 0.0f;
    float stopperIn = 0.0f;
    Transform model;

    bool part2 = false;
    bool part3 = false;

    void Start()
    {

    }

    void Update()
    {
        if (part2)
        {
            Debug.Log("part2");
            model = GameObject.Find("Model-duplicate").transform;
            camera.transform.LookAt(model);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - new Vector3(-0.6f, -0.2f, 0);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
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
                part2 = false;
                startZoom = false;
                stopperIn = 0;
                stopperOut = 0;
            }
        }
        else if (part3)
        {
            Debug.Log("part3");
            model = GameObject.Find("Model-duplicate").transform;
            camera.transform.LookAt(model);

            if (startZoom)
            {
                stopperIn += Time.deltaTime;
                targetpos = model.position - new Vector3(-0.6f, 0.2f, 0);
                //camera.transform.Translate(Vector3.back * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopperIn);
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
    }

    public void showPart2()
    {
        part2 = true;
    }

    public void showPart3()
    {
        part3 = true;
    }

}
