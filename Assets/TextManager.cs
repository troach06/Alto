using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeyboard.Utils;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public GameObject keyboard;
    public bool isActive, canPress = true;
    public bool canToggle;
    public GameObject hintPanel;
    public GameObject inputText;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleKeyboard()
    {
        if (canPress && canToggle)
        {
            if (!isActive)
            {
                GetComponent<InitSetup>().isTyping = true;
                keyboard.SetActive(true);
                hintPanel.SetActive(false);
                keyboard.GetComponent<KeyboardAnimator>().ScaleUp();
                keyboard.GetComponent<KeyboardManager>().SetUppercase();
                inputText.GetComponent<Text>().text = "";
                isActive = true;
                GetComponent<SpeechText>().m_SpeechToText.StopListening();
                GetComponent<SpeechText>().StopRecording();
            }
            else
            {
                GetComponent<InitSetup>().isTyping = false;
                keyboard.GetComponent<KeyboardAnimator>().ScaleDown();
                isActive = false;
                hintPanel.SetActive(true);
                GetComponent<SpeechText>().m_SpeechToText.StartListening(GetComponent<SpeechText>().OnRecognize);
                GetComponent<SpeechText>().StartRecording();
            }
            canPress = false;
            StartCoroutine(ResetButton());
        }
    }
    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(1f);
        canPress = true;
    }
}
