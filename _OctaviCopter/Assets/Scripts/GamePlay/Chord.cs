using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Chord", menuName = "New Chord Mission")]

public class Chord : MissionDef
{
    public Note baseNote;
    public Note thirdNote;
    public Note fifthNote;

}


