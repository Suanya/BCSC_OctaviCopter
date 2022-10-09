using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Mission : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public MissionDef currentMission;

    public event Action OnMissionSetUp;
    public event Action OnCorrectNoteCollected;
    public event Action OnIncorrectNoteCollected;
    public event Action OnMissionCompleted;
    public Dictionary<int, Note> requiredNotes;
    public int requiredNoteIndex = 0;
    public string missionInstructions;

    protected GameObject octaviCopter;
    protected GameObject scaleColumn;

    protected List<Note> sceneNotes;
    protected int requiredNoteCount;

    protected KeyboardKey[] keyboardKeys;

    public enum MissionType
    {
        Interval,
        Chord,
        Rhythm
    }

    private void Awake()
    {
        levelManager.NewMissionToLoad += SetUpMission;

    }

    public void SetUpMission(MissionDef newMission)
    {
        currentMission = newMission;
        octaviCopter = levelManager.octaviCopter;
        scaleColumn = levelManager.currentScaleColumn;
        bool getInactive = true;
        sceneNotes = scaleColumn.GetComponentsInChildren<Note>(getInactive).ToList();
        keyboardKeys = FindObjectsOfType<KeyboardKey>();
        requiredNotes = new Dictionary<int, Note>();

        switch (levelManager.currentLevel.missionCategory.ToString())
        {
            case "Interval":
            {
                SetUpIntervalMission(levelManager.currentLevel.hintsAvailable);
                break;
            }

            case "Chord":
            {
                SetUpChordMission(levelManager.currentLevel.hintsAvailable);
                break;
            }
        }

        OnMissionSetUp?.Invoke();
        levelManager.MissionCanStart += PlayMissionDemo;

    }

    private void SetUpIntervalMission(bool hasHints)
    {
        Interval currentInterval = (Interval)currentMission;
        requiredNoteCount = 2;

        foreach (Note sceneNote in sceneNotes)
        {
            sceneNote.OnNoteCollected += CheckNote;
            if (sceneNote.name == currentInterval.baseNote.name)
            {
                requiredNotes[0] = sceneNote;
                if (hasHints) ActivateHint(sceneNote, keyboardKeys);
            }
            if (sceneNote.name == currentInterval.intervalNote.name)
            {
                requiredNotes[1] = sceneNote;
                if (hasHints) ActivateHint(sceneNote, keyboardKeys);
            }
        }

        missionInstructions = $"Base note:{currentInterval.baseNote.displayName} " + 
                              $"Interval note: {currentInterval.intervalNote.displayName}";

    }

    public void SetUpChordMission(bool hasHints)
    {
        Chord currentChord = (Chord)currentMission;

        requiredNoteCount = 3;
        var keyboardKeys = FindObjectsOfType<KeyboardKey>();

        foreach (Note sceneNote in sceneNotes)
        {
            sceneNote.OnNoteCollected += CheckNote;
            if (sceneNote.name == currentChord.baseNote.name)
            {
                requiredNotes[0] = sceneNote;
                if (hasHints) ActivateHint(sceneNote, keyboardKeys);
            }

            if (sceneNote.name == currentChord.thirdNote.name)
            {
                requiredNotes[1] = sceneNote;
                if (hasHints) ActivateHint(sceneNote, keyboardKeys);
            }

            if (sceneNote.name == currentChord.fifthNote.name)
            {
                requiredNotes[2] = sceneNote;
                if (hasHints) ActivateHint(sceneNote, keyboardKeys);
            }

        }
        missionInstructions = $"Base note:{currentChord.baseNote.displayName} " +
                              $"Third note:{currentChord.thirdNote.displayName} " +
                              $"Fifth note: {currentChord.fifthNote.displayName}";

    }

    public void PlayMissionDemo()
    {
        if (levelManager.currentLevel.hintsAvailable)
        {
            StartCoroutine(PlayMissionNotes());

        }
    }

    private IEnumerator PlayMissionNotes()
    {
        for (int i = 0; i < requiredNotes.Count; i++)

        {
            Note note = requiredNotes[i];
            // activate note fx
            note.TurnOnHintEffect();

            // play the octaviTone
            note.audioSource.Play();

            // pause before the next note is played
            yield return new WaitForSeconds(note.effectDuration);

            // deactivate to reset
            note.TurnOffHintEffect();

        }

    }
    public virtual void ActivateHint(Note sceneNote, KeyboardKey[] keyboardKeys)
    {
        foreach (KeyboardKey key in keyboardKeys)
        {
            if (sceneNote.CompareTag(key.noteTag))
            {
                key.OnShowHintColours();

            }
        }

    }

    public void CheckNote(Note note)
    {
        if (note == requiredNotes[requiredNoteIndex])
        {
            OnCorrectNoteHit(note);

        }
        else
        {
            OnIncorrectNoteHit();

        }

    }

    private void OnCorrectNoteHit(Note note)
    {
        //Debug.Log("Correct note hit!");
        requiredNoteIndex++;

        // check if the mission is complete
        if (requiredNoteIndex == requiredNoteCount)
        {
            MissionCompleted();

        }
        else
        {
            OnCorrectNoteCollected?.Invoke();

            //move the note column ahead so it can be flown through again for the next note
            scaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + currentMission.scaleColumnSpacing);

        }
    }

    private void OnIncorrectNoteHit()
    {
        // TODO: Add haptics or sound effect or VO or something so the player knows it's wrong
        OnIncorrectNoteCollected?.Invoke();
        //move the note column ahead so it can be flown through again on the correct note
        scaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + currentMission.scaleColumnSpacing);

    }

    public void MissionCompleted()
    {
        foreach (Note note in sceneNotes)
        {
            note.OnNoteCollected -= CheckNote;

        }

        foreach (KeyboardKey key in keyboardKeys)
        {
            {
                key.OnHideHintColours();

            }
        }
        requiredNoteIndex = 0;
        OnMissionCompleted?.Invoke();

    }
}
