using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MuteBtn : MonoBehaviour {

    AudioSource audioSource;
    public Sprite OffSprite;
    public Sprite OnSprite;
    Button SoundBtn;
    bool isMute;

    // Use this for initialization
    void Start () {
        isMute = false;
        //Fetch the AudioSource from the GameObject
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Mute(){
        SoundBtn = GameObject.Find("SoundBtn").GetComponent<Button>();
        if (isMute)
        {
            audioSource.Play();
            SoundBtn.image.sprite = Resources.Load<Sprite>("Img/btn-mute");
        }
        else
        {
            audioSource.Stop();
            SoundBtn.image.sprite = Resources.Load<Sprite>("Img/btn-unmute");
        }
        isMute = isMute ? false : true;
    }
}
