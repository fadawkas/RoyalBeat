using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Add some initial checks in case SongManager or notePrefab are not set
        if (SongManager.Instance == null)
        {
            Debug.LogError("SongManager is not initialized!");
        }

        if (notePrefab == null)
        {
            Debug.LogError("Note Prefab is not assigned in the inspector!");
        }
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        if (SongManager.midiFile == null)
        {
            Debug.LogError("MidiFile is not set in SongManager!");
            return;
        }

        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check SongManager and notePrefab before proceeding
        if (SongManager.Instance == null || notePrefab == null)
        {
            return;
        }

        // Spawning notes based on timestamps
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                var noteComponent = note.GetComponent<Note>();

                if (noteComponent != null)
                {
                    notes.Add(noteComponent);
                    noteComponent.assignedTime = (float)timeStamps[spawnIndex];
                }
                else
                {
                    Debug.LogError("Note prefab is missing the Note component!");
                }

                spawnIndex++;
            }
        }

        // Input handling for hitting notes
        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Input.GetKeyDown(input))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    Debug.Log($"Hit on {inputIndex} note");

                    // Safeguard in case the note has already been destroyed
                    if (notes[inputIndex] != null)
                    {
                        Destroy(notes[inputIndex].gameObject);
                    }

                    inputIndex++;
                }
                else
                {
                    Debug.Log($"Hit inaccurate on {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }

            // Handling missed notes
            if (timeStamp + marginOfError <= audioTime)
            {
                Miss();
                Debug.Log($"Missed {inputIndex} note");
                inputIndex++;
            }
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
        AnimManagerPlayer.Hit();
        AnimManagerOrc.Hit();

    }

    private void Miss()
    {
        ScoreManager.Miss();
        AnimManagerPlayer.Miss();
        AnimManagerOrc.Miss();
    }
}
