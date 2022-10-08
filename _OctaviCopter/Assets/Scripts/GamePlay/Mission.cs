using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "Mission", menuName = "New Mission")]
public class Mission : ScriptableObject
{
    public int missionID;
    public string missionName;
    public Sprite missionGraphic;
    public float scaleColumnSpacing = 15.0f;
    

    public event Action OnCorrectNoteCollected;
    public event Action OnIncorrectNoteCollected;
    public event Action<Mission> OnMissionCompleted;
    public Dictionary<int, Note> requiredNotes;
    public int requiredNoteIndex = 0;

    protected GameObject octaviCopter;
    protected GameObject scaleColumn;

    protected List<Note> sceneNotes;
    protected int requiredNoteCount;
    
    protected KeyboardKey[] keyboardKeys;

    public virtual void SetUpMission(GameObject currentScaleColumn, bool hasHints)
    {
        octaviCopter = GameObject.FindGameObjectWithTag("OctaviCopter");
        bool getInactive = true;
        sceneNotes = scaleColumn.GetComponentsInChildren<Note>(getInactive).ToList();
        keyboardKeys = FindObjectsOfType<KeyboardKey>();
        requiredNotes = new Dictionary<int, Note>();
        scaleColumn = currentScaleColumn;
    }

    public virtual void ActivateHint(Note sceneNote, KeyboardKey[] keyboardKeys)
    {
        foreach (KeyboardKey key in keyboardKeys)
        {
            if (sceneNote.CompareTag(key.noteTag))
            {
                key.OnShowHint();
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
            if (scaleColumn == null)
            {
                Debug.Log("Scale column is null for some reason");
            }
            else
            {
                Debug.Log($"Scale column should have moved in front of the player");
                scaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + scaleColumnSpacing);
            }
            
        }
    }

    private void OnIncorrectNoteHit()
    {
        // TODO: Add haptics or sound effect or VO or something so the player knows it's wrong
        OnIncorrectNoteCollected?.Invoke();
        //move the note column ahead so it can be flown through again on the correct note
        scaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + scaleColumnSpacing);

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
                key.OnHideHint();
            }
        }
        requiredNoteIndex = 0;
        OnMissionCompleted?.Invoke(this);
    }

}
