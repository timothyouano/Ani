using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public Camera GameCamera;

    IEnumerator ZoomIn()
    {
        while (GameCamera.orthographicSize > 2)
        {
            yield return new WaitForSeconds(0.01f);
            GameCamera.orthographicSize -= 0.2f;
        }
    }

    public void ZoomCamera()
    {
        StartCoroutine(ZoomIn());
    }

    public void TargetPlayer()
    {
        Transform child = GameObject.Find("cat").transform;
        Vector3 TempCameraPos = transform.position;
        TempCameraPos.x = child.position.x;
        TempCameraPos.y = child.position.y;
        transform.position = TempCameraPos;
    }
}
