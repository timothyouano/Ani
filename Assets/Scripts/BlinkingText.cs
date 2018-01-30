using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

    Text flashingText;
    string originaltext;

    void Start()
    {
        flashingText = GetComponent<Text>();
        originaltext = flashingText.text;
        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = originaltext;
            yield return new WaitForSeconds(1);
            flashingText.text = "";
            yield return new WaitForSeconds(1);
        }
    }
}
