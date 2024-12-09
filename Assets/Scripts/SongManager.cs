using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError; // in seconds
    public int inputDelayInMilliseconds;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static MidiFile midiFile;

    // Awake is called before the first frame update to handle Singleton logic
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy if there's already an instance
            return;
        }
    }

    // Start is called on initialization
    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in SongManager!");
            return;
        }

        if (lanes.Length == 0)
        {
            Debug.LogError("No lanes assigned in SongManager!");
            return;
        }

        Debug.Log("SongManager initialized.");

        // Load MIDI file
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    // Coroutine to read MIDI from website
    private IEnumerator ReadFromWebsite()
    {
        Debug.Log("Reading MIDI file from website: " + Application.streamingAssetsPath + "/" + fileLocation);
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    if (midiFile != null)
                    {
                        Debug.Log("MIDI file loaded successfully from website.");
                        GetDataFromMidi();
                    }
                    else
                    {
                        Debug.LogError("Failed to load MIDI file from website.");
                    }
                }
            }
        }
    }

    // Read MIDI file from local file
    private void ReadFromFile()
    {
        Debug.Log("Reading MIDI file from file: " + Application.streamingAssetsPath + "/" + fileLocation);
        try
        {
            midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
            if (midiFile != null)
            {
                Debug.Log("MIDI file loaded successfully from file.");
                GetDataFromMidi();
            }
            else
            {
                Debug.LogError("Failed to load MIDI file from file.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading MIDI file: " + e.Message);
        }
    }

    // Process the MIDI file and assign notes to lanes
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        if (lanes == null || lanes.Length == 0)
        {
            Debug.LogError("No lanes available to assign notes.");
            return;
        }

        Debug.Log("Assigning timestamps to lanes.");
        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(array);
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    // Start playing the song
    public void StartSong()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            Debug.Log("Song started playing.");
        }
        else
        {
            Debug.LogError("AudioSource is not available!");
        }
    }

    public static void StopSong()
    {
        if (Instance != null && Instance.audioSource != null)
        {
            Instance.audioSource.Stop();
            Debug.Log("Song stopped playing.");
        }
        else
        {
            Debug.LogError("SongManager Instance or AudioSource is null!");
        }
    }


    // Get the current time of the audio playing
    public static double GetAudioSourceTime()
    {
        if (Instance == null || Instance.audioSource == null)
        {
            Debug.LogError("SongManager Instance or AudioSource is null!");
            return 0;
        }

        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    // Update method for debugging purposes
    void Update()
    {
        // This is currently empty, add any updates you want to track.
    }
}
