using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chord", menuName = "New Chord Mission")]

public class Chord : Mission
{
    public Note baseNote;
    public Note thirdNote;
    public Note fifthNote;
}
