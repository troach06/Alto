/***
 * Author: Yunhan Li 
 * Any issue please contact yunhn.lee@gmail.com
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace VRKeyboard.Utils {
    public class KeyboardManager : MonoBehaviour {
        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;
        public int maxInputLength;
        bool canPress = true;
        [Header("UI Elements")]
        public Text inputText;
        public GameObject manager;
        [Header("Essentials")]
        public Transform characters;
        #endregion

        #region Private Variables
        public string Input {
            get { return inputText.text;  }
            set { inputText.text = value;  }
        }

        private Dictionary<GameObject, Text> keysDictionary = new Dictionary<GameObject, Text>();

        public bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        private void Awake() {

            for (int i = 0; i < characters.childCount; i++)
            {
                GameObject key = characters.GetChild(i).gameObject;
                Text _text = key.GetComponentInChildren<Text>();
                keysDictionary.Add(key, _text);

                //    key.GetComponent<Button>().onClick.AddListener(() => {
                //        GenerateInput(_text.text);
                //});
                //}
            }
                capslockFlag = isUppercase;
            CapsLock();
        }
        public void Start()
        {
            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Backspace() {
            if (Input.Length > 0) {
                Input = Input.Remove(Input.Length - 1);
                GetComponent<AudioSource>().Play();
            } else {
                return;
            }
        }

        public void Clear() {
            Input = "";
            GetComponent<AudioSource>().Play();
        }
        public void Enter()
        {
            manager.GetComponent<SpeechText>().spokenText = Input;
            manager.GetComponent<TextManager>().ToggleKeyboard();
            GetComponent<AudioSource>().Play();

        }
        public void SetUppercase()
        {
                foreach (var pair in keysDictionary)
                {
                    pair.Value.text = ToUpperCase(pair.Value.text);
                }
            GetComponent<AudioSource>().Play();
            capslockFlag = false;
        }
        public void CapsLock() {
            if (capslockFlag) {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToUpperCase(pair.Value.text);
                }
            } else {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToLowerCase(pair.Value.text);
                }
            }
            GetComponent<AudioSource>().Play();
            capslockFlag = !capslockFlag;
        }
        #endregion

        #region Private Methods
        public void GenerateInput(Text key)
        {
            if (Input.Length > maxInputLength) { return; }
            if (canPress)
            {
                Input += key.text;
                GetComponent<AudioSource>().Play();
                canPress = false;
                StartCoroutine(ResetButton());
            }
        }
        IEnumerator ResetButton()
        {
            yield return new WaitForSeconds(1f);
            canPress = true;
        }
        private string ToLowerCase(string s) {
            return s.ToLower();
        }

        private string ToUpperCase(string s) {
            return s.ToUpper();
        }
        #endregion
    }
}