using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.RTVoice.Model.Event;
using UnityEngine.UI;



namespace Crosstales.RTVoice.Demo
{
    public class AniVoice : MonoBehaviour {

        #region Variables

        public Text TextSpeakerB;

        [Range(0f, 3f)]
        public float RateSpeakerB = 1.75f;

        public bool PlayOnStart = false;

        private string uidSpeakerB;

        private string textB;

        public string valueSpeak { get; set; }

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            // Subscribe event listeners
            Speaker.OnSpeakCurrentWord += speakCurrentWordMethod;
            Speaker.OnSpeakStart += speakStartMethod;
            Speaker.OnSpeakComplete += speakCompleteMethod;

            textB = TextSpeakerB.text;

            if (PlayOnStart)
            {
                Play();
            }

        }

        public void OnDestroy()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakCurrentWord -= speakCurrentWordMethod;
            Speaker.OnSpeakStart -= speakStartMethod;
            Speaker.OnSpeakComplete -= speakCompleteMethod;
        }

        #endregion


        #region Public methods

        public void Play()
        {
            TextSpeakerB.text = textB;

            SpeakerB(); //start with speaker A
        }

        public void SpeakerB()
        {
            uidSpeakerB = Speaker.SpeakNative(TextSpeakerB.text, Speaker.VoiceForCulture("en"), RateSpeakerB);
        }

        public void Speak()
        {
            uidSpeakerB = Speaker.SpeakNative(valueSpeak, Speaker.VoiceForCulture("en"), RateSpeakerB);
        }


        public void Silence()
        {
            Speaker.Silence();
            //Speaker.Silence(speakerC);
            
            TextSpeakerB.text = textB;
        }

        #endregion


        #region Callback methods

        private void speakStartMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech start: " + e);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCompleteMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech complete: " + e);
                TextSpeakerB.text = e.Wrapper.Text;
                
                //Invoke("Silence", 3f);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentWordMethod(CurrentWordEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                TextSpeakerB.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        #endregion
    }
}