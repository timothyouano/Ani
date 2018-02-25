using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sync : MonoBehaviour {

    public bool synch = false;
    public Camera uiCamera;
    public Camera arCamera;

    bool duplicated = false;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (!synch)
        {
            uiCamera.projectionMatrix = arCamera.projectionMatrix;
            uiCamera.transform.position = arCamera.transform.position;
            uiCamera.transform.rotation = arCamera.transform.rotation;
            uiCamera.fieldOfView = arCamera.fieldOfView;
            uiCamera.farClipPlane = arCamera.farClipPlane;
            uiCamera.nearClipPlane = arCamera.nearClipPlane;
        }
    }

    public void offSync()
    {
        this.synch = true;
    }

    public bool isDuplicated()
    {
        return duplicated;
    }

    public void duplicateModel()
    {
        GameObject duplicate;
        GameObject model = GameObject.Find("UserDefinedTarget-1").transform.GetChild(1).gameObject;

        duplicate = Instantiate(model);
        duplicate.transform.SetParent(GameObject.Find("Synch").transform);
        duplicate.transform.localScale = model.transform.localScale;
        duplicate.transform.position = model.transform.position;
        duplicate.transform.rotation = model.transform.rotation;
        duplicate.name = "Model-duplicate";

        //Destroy(GameObject.Find("UserDefinedTarget-1"));
        GameObject.Find("UserDefinedTarget-1").SetActive(false);

        duplicated = true;

        //StartCoroutine(move(duplicate));
    }

    public IEnumerator move(GameObject model)
    {
        yield return 100;
        model.transform.position += new Vector3(0,1f,0);
    }
}
