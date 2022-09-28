using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour
{
    // use for testing until database is created
    [SerializeField] private Level currentLevel;

    [SerializeField] private float missionSpawnSpacing = 15f;
    private Mission currentMission;
    private int currentMissionIndex = 0;
    private List<Note> sceneNotes;
    private Dictionary<int, Note> requiredNotes;
    private int requiredNoteIndex = 0;
    private int requiredNoteCount;
    private GameObject missionColumn;
    private GameObject octaviCopter;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the next level the user should be at: deserialize when database is set up
        // currentLevel = GetLevelFromDatabase
        octaviCopter = GameObject.FindGameObjectWithTag("OctaviCopter");
        SetUpMission();
    }

    private void SetUpMission()
    {
        // Instantiate the first column (based on key of level)
        
        Vector3 spawnPoint = new Vector3(0, 3.5f, octaviCopter.transform.position.z + missionSpawnSpacing);
        if(missionColumn == null)
        {
            missionColumn = Instantiate(currentLevel.missionKeyPrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            missionColumn.transform.position = spawnPoint;
        }
        sceneNotes = FindObjectsOfType<Note>().ToList();
        requiredNotes = new Dictionary<int, Note>();

        // subscribe to the noteCollected events of the correct notes and save their collection order in the Dictionary
        switch (currentLevel.missionCategory)
        {
            case "Interval":
                requiredNoteCount = 2;
                for(int i = 0; i < requiredNoteCount; i++)
                {
                    requiredNotes[i] = null;        // make sure it's empty from previous missions
                }

                var intervalMission = (Interval)currentLevel.missions[currentMissionIndex];
                foreach (Note sceneNote in sceneNotes)
                {
                    sceneNote.OnNoteCollected += CheckNote;
                    if (sceneNote.name == intervalMission.baseNote.name)
                        requiredNotes[0] = sceneNote;
                    if (sceneNote.name == intervalMission.intervalNote.name)
                        requiredNotes[1] = sceneNote;
                }
                
                break;

            case "Chord":
                requiredNoteCount = 3;
                for (int i = 0; i < requiredNoteCount; i++)
                {
                    requiredNotes[i] = null;        // make sure it's empty from previous missions
                }
                var chordMission = (Chord)currentLevel.missions[currentMissionIndex];
                // by definition an interval consists of two notes\
                sceneNotes = FindObjectsOfType<Note>().ToList();
                foreach (Note sceneNote in sceneNotes)
                {
                    sceneNote.OnNoteCollected += CheckNote;
                    if (sceneNote.name == chordMission.baseNote.name)
                        requiredNotes[0] = sceneNote;
                    if (sceneNote.name == chordMission.thirdNote.name)
                        requiredNotes[1] = sceneNote;
                    if (sceneNote.name == chordMission.fifthNote.name)
                        requiredNotes[2] = sceneNote;
                }
        
                break;
            case "Rhythm":
                Debug.Log("Stuff for rhythms is not written yet");
                break;

        }

        Debug.Log($"Starting mission number {currentMissionIndex}: {currentLevel.missions[currentMissionIndex].missionName}");
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
        Debug.Log("Correct note hit!");
        requiredNoteIndex++;

        // check if the mission is complete
        if (requiredNoteIndex == requiredNoteCount)
        {
            requiredNoteIndex = 0;
            StartCoroutine(OnMissionComplete());
        }
        else
        {
            //move the note column ahead so it can be flown through again for the next note
            missionColumn.transform.Translate(0, 0, missionSpawnSpacing);
        }
    }

    private void OnIncorrectNoteHit()
    {
        // TODO: Add haptics or sound effect or VO or something so the player knows it's wrong
        Debug.Log("This is not the correct next note");

        //move the note column ahead so it can be flown through again on the correct note
        missionColumn.transform.Translate(0, 0, missionSpawnSpacing);

    }

    private IEnumerator OnMissionComplete()
    {
        Debug.Log("Mission accomplished!!");
        // give time for the final note to play
        yield return new WaitForSecondsRealtime(3);
        // set the starting point for the next mission column
       
        currentMissionIndex++;

        // check to see if there are any more missions
        if (currentMissionIndex == currentLevel.missions.Length)
        {
            // no more missions - level is finished!!
            OnLevelComplete();
        }
        else
        {
            
            // set up the next mission
            SetUpMission();
        }

    }

    private void OnLevelComplete()
    {
        // Play reward scene
        Debug.Log($"You will now be treated to the lovely tones of the twinkle!!");
        currentMissionIndex = 0;
        foreach (Note note in sceneNotes)
        {
            note.OnNoteCollected -= CheckNote;
        }
        Destroy(missionColumn);
    }

    private void OnDisable()
    {
        foreach(Note note in sceneNotes)
        {
            note.OnNoteCollected -= CheckNote;
        }
    }

}
