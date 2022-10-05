using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chord", menuName = "New Chord Mission")]

public class Chord : Mission
{
    public Note baseNote;
    public Note thirdNote;
    public Note fifthNote;

    public override void SetUpMission(bool hintsAvailable)
    {
        base.SetUpMission(hintsAvailable);

        requiredNoteCount = 3;
        var keyboardKeys = FindObjectsOfType<KeyboardKey>();

        foreach (Note sceneNote in sceneNotes)
        {
            sceneNote.OnNoteCollected += CheckNote;
            if (sceneNote.name == baseNote.name)
            {
                requiredNotes[0] = sceneNote;
                if (hintsAvailable) ActivateHint(sceneNote, keyboardKeys);
            }
                
            if (sceneNote.name == thirdNote.name)
            {
                requiredNotes[1] = sceneNote;
                if (hintsAvailable) ActivateHint(sceneNote, keyboardKeys);
            }
                
            if (sceneNote.name == fifthNote.name)
            {
                requiredNotes[2] = sceneNote;
                if (hintsAvailable) ActivateHint(sceneNote, keyboardKeys);
            }
                
        }
    }

}


