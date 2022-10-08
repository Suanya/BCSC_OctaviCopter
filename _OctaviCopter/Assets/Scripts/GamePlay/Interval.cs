using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interval", menuName = "New Interval Mission")]

public class Interval : Mission
{
    public Note baseNote;
    public Note intervalNote;

    public override void SetUpMission(GameObject currentScaleColumn, bool hintsAvailable)
    {
        scaleColumn = currentScaleColumn;

        base.SetUpMission(scaleColumn, hintsAvailable);

        requiredNoteCount = 2;

        foreach (Note sceneNote in sceneNotes)
        {
            sceneNote.OnNoteCollected += CheckNote;
            if (sceneNote.name == baseNote.name)
            {
                requiredNotes[0] = sceneNote;
                if (hintsAvailable) ActivateHint(sceneNote, keyboardKeys);
            }    
            if (sceneNote.name == intervalNote.name)
            {
                requiredNotes[1] = sceneNote;
                if (hintsAvailable) ActivateHint(sceneNote, keyboardKeys);
            }
        }

    }

}
