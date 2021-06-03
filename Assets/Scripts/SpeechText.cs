using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Linq;
public class SpeechText : MonoBehaviour
{
    private int m_RecordingRoutine = 0;
    private string _username;
    private string _password;
    private string _url;
    private string m_MicrophoneID = null;
    private AudioClip m_Recording = null;
    private int m_RecordingBufferSize = 2;
    private int m_RecordingHZ = 22050;
    public string spokenText;
    public Credentials credentials;
    public SpeechToText m_SpeechToText;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
        _username = "5ed87794-6a68-4a43-8037-b7739a54f11e";
        _password = "MMd6XDLC8EWY";
        _url = "https://stream.watsonplatform.net/speech-to-text/api";
        Log.Debug("ExampleStreaming", "Start();");
        credentials = new Credentials(_username, _password, _url);
        m_SpeechToText = new SpeechToText(credentials);
        Active = true;

        StartRecording();
    }

    public bool Active
    {
        get { return m_SpeechToText.IsListening; }
        set
        {
            if (value && !m_SpeechToText.IsListening)
            {
                m_SpeechToText.DetectSilence = true;
                m_SpeechToText.EnableWordConfidence = false;
                m_SpeechToText.EnableTimestamps = false;
                m_SpeechToText.SilenceThreshold = 0.03f;
                m_SpeechToText.InactivityTimeout = -1;
                m_SpeechToText.MaxAlternatives = 1;
                m_SpeechToText.SmartFormatting = true;
                //m_SpeechToText.EnableContinousRecognition = true;
                m_SpeechToText.EnableInterimResults = true;
                m_SpeechToText.OnError = OnError;
                m_SpeechToText.StartListening(OnRecognize);
            }
            else if (!value && m_SpeechToText.IsListening)
            {
                m_SpeechToText.StopListening();
            }
        }
    }

    public void StartRecording()
    {
        if (m_RecordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            m_RecordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    public void StopRecording()
    {
        if (m_RecordingRoutine != 0)
        {
            Microphone.End(m_MicrophoneID);
            Runnable.Stop(m_RecordingRoutine);
            m_RecordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("ExampleStreaming", "Error! {0}", error);
    }

    private IEnumerator RecordingHandler()
    {
        m_Recording = Microphone.Start(m_MicrophoneID, true, m_RecordingBufferSize, m_RecordingHZ);
        yield return null;      // let m_RecordingRoutine get set..

        if (m_Recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = m_Recording.samples / 2;
        float[] samples = null;

        while (m_RecordingRoutine != 0 && m_Recording != null)
        {
            int writePos = Microphone.GetPosition(m_MicrophoneID);
            if (writePos > m_Recording.samples || !Microphone.IsRecording(m_MicrophoneID))
            {
                Log.Error("MicrophoneWidget", "Microphone disconnected.");

                StopRecording();
                StartRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
                || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                m_Recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(samples);
                record.Clip = AudioClip.Create("Recording", midPoint, m_Recording.channels, m_RecordingHZ, false);
                record.Clip.SetData(samples, 0);

                m_SpeechToText.OnListen(record);

                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio, 
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (m_Recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)m_RecordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }

        }

        yield break;
    }

    public void OnRecognize(SpeechRecognitionEvent result)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    if (res.final)
                    {
                        spokenText = alt.transcript;
                        Log.Debug("ExampleStreaming", string.Format("{0} ({1}, {2:0.00})\n", spokenText, res.final ? "Final" : "Interim", alt.confidence));
                    }
                    else
                    {
                        spokenText = "";
                    }
                }
            }
        }
    }
}
