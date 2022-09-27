using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    // use for testing until database is created
    [SerializeField] private Level currentLevel;
    private Mission currentMission;
    private int currentMissionIndex = 0;
    private List<Note> sceneNotes;
    private List<Note> requiredNotes = new List<Note>();
    private int requiredNoteIndex = 0;
    private int requiredNoteCount;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the next level the user should be at: deserialize when database is set up
        // currentLevel = GetLevelFromDatabase

        // Instantiate the first column (based on key of level)
        Instantiate(currentLevel.missionKeyPrefab);
        SetUpMission();

    }

    public void CheckNote(Note note)
    {
        // collect the note
        Debug.Log($"Note collected: {note.name}");
        if(note == requiredNotes[requiredNoteIndex])
        {
            note.OnNoteCollected -= CheckNote;
        }

        requiredNoteIndex++;
        
        // check if the mission is complete
        if (requiredNoteIndex == requiredNoteCount)
        {
            Debug.Log("Mission accomplished!!");
        }
        
    }

    private void SetUpMission()
    {

        // subscribe to the noteCollected events of the correct notes
        switch (currentLevel.missionCategory)
        {
            case "Interval":
                requiredNoteCount = 2;
                Interval mission = (Interval)currentLevel.missions[currentMissionIndex];
                // by definition an interval consists of two notes\
                sceneNotes = FindObjectsOfType<Note>().ToList();
                foreach (Note sceneNote in sceneNotes)
                {
                    sceneNote.OnNoteCollected += CheckNote;
                    if (sceneNote.name == mission.baseNote.name)
                        requiredNotes.Add(sceneNote);
                }
                foreach (Note sceneNote in sceneNotes)
                {
                    if (sceneNote.name == mission.intervalNote.name)
                        requiredNotes.Add(sceneNote);
                }
                break;
            case "Chord":
                Debug.Log("Stuff for chords is not written yet");
                break;
            case "Rhythm":
                Debug.Log("Stuff for rhythms is not written yet");
                break;

        }
    }

    private void GetNextMission()
    {

    }

    private void OnMissionComplete()
    {

    }

    private void OnLevelComplete()
    {

    }

}
