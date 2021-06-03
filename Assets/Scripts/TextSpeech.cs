/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.TextToSpeech.v1;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Connection;

public class TextSpeech : MonoBehaviour
{
    private string _username;
    private string _password;
    private string _url;

    TextToSpeech _textToSpeech;
    string _testString = "<speak version=\"1.0\"><say-as interpret-as=\"letters\">I'm sorry</say-as>. <prosody pitch=\"150Hz\">This is Text to Speech!</prosody><express-as type=\"GoodNews\">I'm sorry. This is Text to Speech!</express-as></speak>";
    AudioSource source;
    string _createdCustomizationId;
    CustomVoiceUpdate _customVoiceUpdate;
    string _customizationName = "unity-example-customization";
    string _customizationLanguage = "en-US";
    string _customizationDescription = "A text to speech voice customization created within Unity.";
    string _testWord = "Watson";
    GameObject alto;
    private bool _synthesizeTested = false;
    private bool _getVoicesTested = false;
    private bool _getVoiceTested = false;
    private bool _getPronuciationTested = false;
    private bool _getCustomizationsTested = false;
    private bool _createCustomizationTested = false;
    private bool _deleteCustomizationTested = false;
    private bool _getCustomizationTested = false;
    private bool _updateCustomizationTested = false;
    private bool _getCustomizationWordsTested = false;
    private bool _addCustomizationWordsTested = false;
    private bool _deleteCustomizationWordTested = false;
    private bool _getCustomizationWordTested = false;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        _username = "d04c11c1-4f48-4fd2-a5ed-d5e15f62a603";
        _password = "VWOUxHIutikF";
        _url = "https://stream.watsonplatform.net/text-to-speech/api";
                //  Create credential and instantiate service
                Credentials credentials = new Credentials(_username, _password, _url);

        _textToSpeech = new TextToSpeech(credentials);

    }
    public void Translate(string speech, string gender)
    {
        if (gender == "male")
        {
            _textToSpeech.Voice = VoiceType.en_US_Michael;
        }
        else if (gender == "female")
        {
            _textToSpeech.Voice = VoiceType.en_US_Allison;
        }
        _textToSpeech.ToSpeech(HandleToSpeechCallback, OnFail, speech, true);

    }

    void HandleToSpeechCallback(AudioClip clip, Dictionary<string, object> customData = null)
    {
        StartCoroutine(PlayClip(clip));
    }

    IEnumerator PlayClip(AudioClip clip)
    {
        if (Application.isPlaying && clip != null && alto == null)
        {
            alto = GameObject.Find("Alto");
            source = alto.GetComponent<AudioSource>();
        }
            source.spatialBlend = 0.0f;
            source.loop = false;
            source.clip = clip;
            source.Play();
            yield return new WaitUntil(() => !source.isPlaying);
            GetComponent<InitSetup>().isSpeaking = false;
        }

    //private void OnGetVoices(Voices voices, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetVoices()", "Text to Speech - Get voices response: {0}", customData["json"].ToString());
    //    _getVoicesTested = true;
    //}

    //private void OnGetVoice(Voice voice, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetVoice()", "Text to Speech - Get voice  response: {0}", customData["json"].ToString());
    //    _getVoiceTested = true;
    //}

    //private void OnGetPronunciation(Pronunciation pronunciation, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetPronunciation()", "Text to Speech - Get pronunciation response: {0}", customData["json"].ToString());
    //    _getPronuciationTested = true;
    //}

    //private void OnGetCustomizations(Customizations customizations, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetCustomizations()", "Text to Speech - Get customizations response: {0}", customData["json"].ToString());
    //    _getCustomizationsTested = true;
    //}

    //private void OnCreateCustomization(CustomizationID customizationID, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnCreateCustomization()", "Text to Speech - Create customization response: {0}", customData["json"].ToString());
    //    _createdCustomizationId = customizationID.customization_id;
    //    _createCustomizationTested = true;
    //}

    //private void OnDeleteCustomization(bool success, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnDeleteCustomization()", "Text to Speech - Delete customization response: {0}", customData["json"].ToString());
    //    _createdCustomizationId = null;
    //    _deleteCustomizationTested = true;
    //}

    //private void OnGetCustomization(Customization customization, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetCustomization()", "Text to Speech - Get customization response: {0}", customData["json"].ToString());
    //    _getCustomizationTested = true;
    //}

    //private void OnUpdateCustomization(bool success, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnUpdateCustomization()", "Text to Speech - Update customization response: {0}", customData["json"].ToString());
    //    _updateCustomizationTested = true;
    //}

    //private void OnGetCustomizationWords(Words words, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetCustomizationWords()", "Text to Speech - Get customization words response: {0}", customData["json"].ToString());
    //    _getCustomizationWordsTested = true;
    //}

    //private void OnAddCustomizationWords(bool success, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnAddCustomizationWords()", "Text to Speech - Add customization words response: {0}", customData["json"].ToString());
    //    _addCustomizationWordsTested = true;
    //}

    //private void OnDeleteCustomizationWord(bool success, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnDeleteCustomizationWord()", "Text to Speech - Delete customization word response: {0}", customData["json"].ToString());
    //    _deleteCustomizationWordTested = true;
    //}

    //private void OnGetCustomizationWord(Translation translation, Dictionary<string, object> customData = null)
    //{
    //    Log.Debug("ExampleTextToSpeech.OnGetCustomizationWord()", "Text to Speech - Get customization word response: {0}", customData["json"].ToString());
    //    _getCustomizationWordTested = true;
    //}

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleTextToSpeech.OnFail()", "Error received: {0}", error.ToString());
    }
}
