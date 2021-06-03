using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using MiniJSON;
using System.Globalization;

public class InitSetup : MonoBehaviour
{
    int a;
    public bool selectedMale, selectedFemale, isStarted, isTyping;
    public GameObject alto, intropanel, genderbuttons, logo, hintpanel, detailspanel, altopanel, quotepanel;
    public bool isSpeaking, isYouth, isAdult, isElder;
    string name, gender, birthday, age, month, day, dotw, year, time, voiceGender, altobirthday, firstName, lastName;
    public string[] altoPhrases, yesPhrases, noPhrases;
    string longPause, shortPause;
    string normalSpeed, slowSpeed, startTag, endTag;
    public string[] nameResponses;
    string quote, author;
    // Use this for initialization
    void Start()
    {
        voiceGender = "male";
        longPause = "<break strength =\"strong\"></break>";
        shortPause = "<break strength =\"medium\"></break>";
        startTag = "<speak version=\"1.0\"><prosody volume =\"100\" ";
        normalSpeed = "rate=\"-5%\">";
        slowSpeed = "rate=\"-10%\">";
        endTag = "</prosody></speak>";
        DateTime currentTime = DateTime.Now.ToLocalTime();
        time = currentTime.ToLongTimeString();
        altobirthday = DateTime.Now.ToLongDateString();
    }

    // Update is called once per frame
    void Update()
    {

        if (alto.GetComponent<AudioSource>().isPlaying)
        {
            if (GetComponent<SpeechText>().m_SpeechToText.IsListening)
            {
                GetComponent<SpeechText>().m_SpeechToText.StopListening();
                GetComponent<SpeechText>().StopRecording();
            }
        }

        else
        {
            if (!GetComponent<SpeechText>().m_SpeechToText.IsListening && isStarted && !isTyping)
            {
                GetComponent<SpeechText>().m_SpeechToText.StartListening(GetComponent<SpeechText>().OnRecognize);
                GetComponent<SpeechText>().StartRecording();
            }
        }
        if (!isSpeaking && isStarted)
        {
            StartCoroutine(DetectSpeech());
        }
    }
    public void StartSetup()
    {
        StartCoroutine(ActivateUI());
    }
    IEnumerator ActivateUI()
    {
        yield return new WaitForSeconds(5);
        logo.SetActive(true);
        intropanel.SetActive(true);
        hintpanel.SetActive(true);
        isStarted = true;
    }
    public void SelectGender(string gender)
    {
        if (gender == "female" && !selectedFemale)
        {
            genderbuttons.transform.GetChild(1).GetComponent<Image>().color = Color.magenta;
            selectedFemale = true;
            GetComponent<SpeechText>().spokenText = "";
        }
        else if (gender == "male" && !selectedMale)
        {
            genderbuttons.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
            selectedMale = true;
            GetComponent<SpeechText>().spokenText = "";
        }
    }
        IEnumerator DetectSpeech()
    {
        if (altoPhrases.Any(GetComponent<SpeechText>().spokenText.Contains) && a == 0 && !isSpeaking)
        {
            a++;
            hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
            GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Hey there, how's it going? My name is Alto. I am your new virtual assistant. My purpose is to share with you helpful tips, present you with valuable information, and get to know you on a deeper, more profound level in order to enhance your quality of life and improve your overall well-being." + longPause + "Before we get to know each other, I want you to be comfortable with my voice." + shortPause + "I will say a phrase using both my male and my female voices." + shortPause + "Please let me know which voice you prefer." + endTag, voiceGender);
            isSpeaking = true;
            yield return new WaitUntil(() => !isSpeaking);
            intropanel.GetComponent<UIAnimator>().ScaleDown();
            GetComponent<TextSpeech>().Translate(startTag + slowSpeed + "This is a test of my male voice." + endTag, voiceGender);
            isSpeaking = true;
            yield return new WaitUntil(() => !isSpeaking);
            GetComponent<TextSpeech>().Translate(startTag + slowSpeed + "This is a test of my female voice." + endTag, "female");
            isSpeaking = true;
            yield return new WaitUntil(() => !isSpeaking);
            GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "So, which one do you prefer?" + shortPause + "Let me know by either speaking the gender" + shortPause +"or touching the button that corresponds to the gender of your choice." + endTag, voiceGender);
            isSpeaking = true;
            yield return new WaitUntil(() => !isSpeaking);
            genderbuttons.SetActive(true);
            hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Touch the \"Male\" button to choose the male voice, or touch the \"Female\" button to choose the female voice.";
            GetComponent<SpeechText>().spokenText = "";
            yield break;
        }
        if (a == 1 && !isSpeaking)
        {
            if (selectedFemale || GetComponent<SpeechText>().spokenText.Contains("female"))
            {
                GetComponent<SpeechText>().spokenText = "";
                a++;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                selectedFemale = false;
                voiceGender = "female";
                altopanel.transform.GetChild(1).GetComponent<Text>().text = "Gender: Female";
                genderbuttons.transform.GetChild(1).GetComponent<AudioSource>().Play();
                foreach (Transform child in genderbuttons.transform)
                {
                    child.GetComponent<UIAnimator>().ScaleDown();
                }
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Okay, thanks for letting me know." + shortPause + "I will use this voice from now on. You can always change it later if you'd like." + longPause + "In order to get to know you, I am going to need to ask you a couple of important questions. " + shortPause + shortPause + "Alright, let's get started." + longPause + "What is your name?" + longPause + "Please say your full name, or touch the keyboard icon to type it instead." + endTag, voiceGender);
                isSpeaking = true;
                yield return new WaitUntil(() => !isSpeaking);
                GetComponent<SpeechText>().spokenText = "";
                GetComponent<TextManager>().canToggle = true;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say your full name, or touch the keyboard icon to type it instead.";
                altopanel.SetActive(true);
                yield return new WaitForSeconds(1f);
                detailspanel.SetActive(true);
            }
            if (selectedMale || GetComponent<SpeechText>().spokenText.Contains("male"))
            {
                GetComponent<SpeechText>().spokenText = "";
                a++;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                selectedMale = false;
                voiceGender = "male";
                altopanel.transform.GetChild(1).GetComponent<Text>().text = "Gender: Male";
                genderbuttons.transform.GetChild(0).GetComponent<AudioSource>().Play();
                foreach (Transform child in genderbuttons.transform)
                {
                    child.GetComponent<UIAnimator>().ScaleDown();
                }
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Okay, thanks for letting me know." + shortPause + "I will use this voice from now on. You can always change it later if you'd like." + longPause + "In order to get to know you, I am going to need to ask you a couple of important questions. " + shortPause + shortPause + "Alright, let's get started." + longPause + "What is your name?" + longPause + "Please say your full name, or touch the keyboard icon to type it instead." + endTag, voiceGender);
                isSpeaking = true;
                yield return new WaitUntil(() => !isSpeaking);
                GetComponent<SpeechText>().spokenText = "";
                GetComponent<TextManager>().canToggle = true;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say your full name, or touch the keyboard icon to type it instead.";
                altopanel.SetActive(true);
                yield return new WaitForSeconds(1f);
                detailspanel.SetActive(true);
            }
            yield break;
        }
        if (GetComponent<SpeechText>().spokenText != "" && a == 2 && !isSpeaking)
        {
            isSpeaking = true;
            a++;
            name = GetComponent<SpeechText>().spokenText;
            StartCoroutine(DetermineGender());
            yield break;
        }
        if (yesPhrases.Any(GetComponent<SpeechText>().spokenText.Contains))
        {
            if (a == 3 && !isSpeaking)
            {
                a++;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                if (name.Contains(" "))
                {
                    firstName = name.Substring(0, name.IndexOf(" "));
                }
                else {
                    firstName = name.Substring(0, name.Length);
                }
                    GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Awesome! I can't wait to get to know you better, " + firstName + ". We both have a lot of learning to do. I'll start by telling you my birthday. I was born on " + DateTime.Now.ToLongDateString() + shortPause + "at " + time + longPause + "How about you? " + longPause + "Please say your full date of birth for me." + endTag, voiceGender);
                isSpeaking = true;
                if (GetComponent<TextManager>().isActive)
                {
                    GetComponent<TextManager>().ToggleKeyboard();
                }
                GetComponent<TextManager>().canToggle = false;
                GetComponent<SpeechText>().spokenText = "";
                yield return new WaitUntil(() => !isSpeaking);
                altopanel.transform.GetChild(2).GetComponent<Text>().text = "Birthday: " + DateTime.Now.ToShortDateString();
                altopanel.transform.GetChild(3).GetComponent<Text>().text = "Age: 0";
                yield return new WaitUntil(() => !isSpeaking);
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say your full date of birth.";
                GetComponent<SpeechText>().spokenText = "";
                yield break;
            }
            if (a == 5 && !isSpeaking)
            {
                int ageNum = int.Parse(age);
                if (ageNum < 30)
                {
                    a++;
                    isYouth = true;
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                    GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "We both have our whole lives ahead of us. Did you know that you were born on a " + dotw + "?" + longPause + firstName + ", Given your young age, I thought I would share a quote with you to help you out in the future." + longPause + "Would you like to hear it?" + endTag, voiceGender);
                    isSpeaking = true;
                    detailspanel.GetComponent<UIAnimator>().ScaleDown();
                    altopanel.GetComponent<UIAnimator>().ScaleDown();
                    yield return new WaitUntil(() => !isSpeaking);
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say \"Yes\" or \"No\" to respond.";
                    GetComponent<SpeechText>().spokenText = "";

                }
                else if (ageNum >= 30 && ageNum < 60)
                {
                    a++;
                    isAdult = true;
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                    GetComponent<TextSpeech>().Translate(startTag + normalSpeed  + "I hope adulthood is treating you well. Did you know that you were born on a " + dotw + "?" + longPause + firstName + ", I'm excited to learn more about your daily life, and experience being an adult together. Adulthood can be challenging yet rewarding at the same time. I found a quote that may help you conquer these challenges." + longPause + "Would you like to hear it?" + endTag, voiceGender);
                    isSpeaking = true;
                    detailspanel.GetComponent<UIAnimator>().ScaleDown();
                    altopanel.GetComponent<UIAnimator>().ScaleDown();
                    yield return new WaitUntil(() => !isSpeaking);
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say \"Yes\" or \"No\" to respond.";
                    GetComponent<SpeechText>().spokenText = "";
                }
                else
                {
                    a++;
                    isElder = true;
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                    GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "You must have a lot of life experience. Did you know that you were born on a " + dotw + "?" + longPause + firstName + ", I found a quote to share with you that I hope you will find interesting." + longPause + "Would you like to hear it?" + endTag, voiceGender);
                    isSpeaking = true;
                    detailspanel.GetComponent<UIAnimator>().ScaleDown();
                    altopanel.GetComponent<UIAnimator>().ScaleDown();
                    yield return new WaitUntil(() => !isSpeaking);
                    hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say \"Yes\" or \"No\" to respond.";
                    GetComponent<SpeechText>().spokenText = "";
                }
                StartCoroutine(GetQuote());
                yield break;
            }
            if (a == 6 && !isSpeaking)
            {
                a++;
                quotepanel.SetActive(true);
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Good answer! Okay, here it is." + endTag, voiceGender);
                isSpeaking = true;
                yield return new WaitUntil(() => !isSpeaking);
                GetComponent<TextSpeech>().Translate(startTag + slowSpeed + quote + longPause + author + endTag, voiceGender);
                if (!quote.EndsWith("."))
                {
                    if (quote.EndsWith(" "))
                    {
                        quote = quote.Remove(quote.Length - 1, 1);
                    }
                    quotepanel.transform.GetChild(0).GetComponent<Text>().text = "\"" + quote + ".\"";
                }
                else
                {
                    if (quote.EndsWith(" "))
                    {
                        quote = quote.Remove(quote.Length - 1, 1);
                    }
                    quotepanel.transform.GetChild(0).GetComponent<Text>().text = "\"" + quote + "\"";
                }
                quotepanel.transform.GetChild(1).GetComponent<Text>().text = "~ " + author;
                isSpeaking = true;
                yield return new WaitUntil(() => !isSpeaking);
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "How did you like it, " + firstName + "? I hope you found it useful. Everyone could use some inspiration every once in a while." + longPause + "Feel free to ask me for a quote whenever you are feeling inspired." + endTag, voiceGender);
                isSpeaking = true;
                yield return new WaitUntil(() => !isSpeaking);
                quotepanel.GetComponent<UIAnimator>().ScaleDown();
                GetComponent<SpeechText>().spokenText = "";
                yield break;
            }
        }
        if (noPhrases.Any(GetComponent<SpeechText>().spokenText.Contains))
        {
            if (a == 3 && !isSpeaking)
            {
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "I'm sorry, I hope I didn't hurt your feelings. Please tell me your name again so that I can pronounce it correctly." + endTag, voiceGender);
                isSpeaking = true;
                detailspanel.transform.GetChild(0).GetComponent<Text>().text = "Name: ";
                detailspanel.transform.GetChild(1).GetComponent<Text>().text = "Gender: ";
                a--;
                if (GetComponent<TextManager>().isActive)
                {
                    GetComponent<TextManager>().ToggleKeyboard();
                }
                GetComponent<TextManager>().canToggle = true;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say your full name, or touch the keyboard icon to type it instead.";
                GetComponent<SpeechText>().spokenText = "";
                yield break;
            }
            if (a == 5 && !isSpeaking)
            {
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "My apologies, " + firstName + shortPause + "I must have heard you wrong. I will get better as our relationship improves." + shortPause + "Please say your birthday again for me." + endTag, voiceGender);
                isSpeaking = true;
                detailspanel.transform.GetChild(2).GetComponent<Text>().text = "Birthday: ";
                detailspanel.transform.GetChild(3).GetComponent<Text>().text = "Age: ";
                a--;
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say your full date of birth.";
                GetComponent<SpeechText>().spokenText = "";
                yield break;
            }
            if (a == 6 && !isSpeaking)
            {
                hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
                GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "That's okay. Don't be afraid to ask me for some inspiration whenever you feel like it." + longPause + "Feel free to ask me for a quote whenever you are feeling inspired." + endTag, voiceGender);
                a++;
                isSpeaking = true;
                GetComponent<SpeechText>().spokenText = "";
                yield break;
            }
        }
        if (GetComponent<SpeechText>().spokenText.Contains("/") && a == 4 && !isSpeaking)
        {
            DateTime dob;
            String dateString = GetComponent<SpeechText>().spokenText.Substring(GetComponent<SpeechText>().spokenText.ToList().FindIndex(c => Char.IsDigit(c)));
            //print("Date = " + dateString);
            if (Convert.ToDateTime(GetComponent<SpeechText>().spokenText) != null)
            {
                dob = Convert.ToDateTime(dateString);
            }
            else
            {
                yield return new WaitUntil(() => Convert.ToDateTime(GetComponent<SpeechText>().spokenText) != null);
                dob = Convert.ToDateTime(GetComponent<SpeechText>().spokenText);
            }
            hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
            a++;
            day = dob.Day.ToString();
            month = dob.ToString("MMMM");
            year = dob.Year.ToString();
            dotw = dob.DayOfWeek.ToString();
            birthday = month + " " + day + ", " + year;
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int birth = int.Parse(dob.ToString("yyyyMMdd"));
            int ageNum = (now - birth) / 10000;
            age = ageNum.ToString();
            GetComponent<TextSpeech>().Translate(startTag + normalSpeed + "Okay, so your birthday is " + birthday +  shortPause + "and you are " + age + " years old. Am I correct?" + endTag, voiceGender);
            isSpeaking = true;
            detailspanel.transform.GetChild(2).GetComponent<Text>().text = "Birthday: " + dateString;
            detailspanel.transform.GetChild(3).GetComponent<Text>().text = "Age: " + age;
            hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say \"Yes\" or \"No\" to respond.";
            GetComponent<SpeechText>().spokenText = "";
            yield break;
        }
    }
    IEnumerator DetermineGender()
    {
        string url;
        hintpanel.transform.GetChild(0).GetComponent<Text>().text = "";
        if (name.Contains(" "))
        {
            url = "https://gender-api.com/get?name=" + name.Split(' ')[0] + "&key=bbtZUEhNSyDDLunrgQ";
        }
        else
        {
            url = "https://gender-api.com/get?name=" + name + "&key=bbtZUEhNSyDDLunrgQ";
        }
        WWW www = new WWW(url);
        yield return www;
        if (www.text.Contains("female"))
        {
            gender = "female";
        }
        else
        {
            gender = "male";
        }
        if (GetComponent<TextManager>().isActive)
        {
            GetComponent<TextManager>().ToggleKeyboard();
        }
        GetComponent<TextManager>().canToggle = false;
        GetComponent<TextSpeech>().Translate(startTag + normalSpeed + nameResponses[UnityEngine.Random.Range(0, nameResponses.Length)] + shortPause + "I will try to pronounce your name. Please let me know if I get it wrong so that I can fix it for later. Your name is: " + name + shortPause + "And you are a " + gender + longPause + "Did I get it right?" + endTag, voiceGender);
        isSpeaking = true;
        detailspanel.transform.GetChild(0).GetComponent<Text>().text = "Name: " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
        if (gender == "male")
        {
            detailspanel.transform.GetChild(1).GetComponent<Text>().text = "Gender: Male";
        }
        else
        {
            detailspanel.transform.GetChild(1).GetComponent<Text>().text = "Gender: Female";
        }

        yield return new WaitUntil(() => !isSpeaking);
        hintpanel.transform.GetChild(0).GetComponent<Text>().text = "Hint: Say \"Yes\" or \"No\" to respond.";
        GetComponent<SpeechText>().spokenText = "";
    }


    IEnumerator GetQuote()
    {
        if (isYouth)
        {
            WWW www = new WWW("http://quotes.rest/quote/search.json?api_key=HxDY8GMU807yPT3VK3_7JAeF&category=youth");
            yield return www;
            string response = www.text;
            var resultDict = Json.Deserialize(response) as IDictionary;
            if (resultDict.Contains("contents"))
            {
                var contents = resultDict["contents"] as IDictionary;
                quote = contents["quote"].ToString();
                if (contents["author"] == null)
                {
                    StartCoroutine(GetQuote());
                }
                else
                {
                    author = contents["author"].ToString();
                }
            }
        }
        if (isAdult)
        {
            WWW www = new WWW("http://quotes.rest/quote/search.json?api_key=HxDY8GMU807yPT3VK3_7JAeF&category=adulthood");
            yield return www;
            string response = www.text;
            var resultDict = Json.Deserialize(response) as IDictionary;
            if (resultDict.Contains("contents"))
            {
                var contents = resultDict["contents"] as IDictionary;
                quote = contents["quote"].ToString();
                if (contents["author"] == null)
                {
                    StartCoroutine(GetQuote());
                }
                else
                {
                    author = contents["author"].ToString();
                }
            }
        }
        if (isElder)
        {
            WWW www = new WWW("http://quotes.rest/quote/search.json?api_key=HxDY8GMU807yPT3VK3_7JAeF&category=elderly");
            yield return www;
            string response = www.text;
            var resultDict = Json.Deserialize(response) as IDictionary;
            if (resultDict.Contains("contents"))
            {
                var contents = resultDict["contents"] as IDictionary;
                quote = contents["quote"].ToString();
                if (contents["author"] == null)
                {
                    StartCoroutine(GetQuote());
                }
                else
                {
                    author = contents["author"].ToString();
                }
            }
        }
    }
}