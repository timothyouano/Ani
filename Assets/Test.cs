using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    //This code should attached to an empty object
    //I would name it "Camera Controller"
    private Transform target;
    public Transform camera;
    private Vector3 targetpos;
    bool startSpin = false;
    float stopper = 0.0f;
    Transform model;

    void Start()
    {
        
    }

    void Update()
    {
        model = GameObject.Find("Model-duplicate").transform;
        camera.transform.LookAt(model);
        stopper += Time.deltaTime;

        if (stopper >= 1)
        {
            startSpin = true;
            stopper = 0;
        }

        if (startSpin)
        {
            targetpos = model.position - new Vector3(-0.6f,0.2f, 0);
            //camera.transform.Translate(Vector3.back * Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopper);
        }
        else
        {
            targetpos = new Vector3(camera.transform.position.x, 5f, camera.transform.position.z);
            //camera.transform.Translate(Vector3.back * Time.deltaTime);
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetpos, stopper);
        }
        //}
        
    }
}
