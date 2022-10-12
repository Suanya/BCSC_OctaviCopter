using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Interval", menuName = "New Interval Mission")]

public class Interval : MissionDef
{
    public Note baseNote;
    public Note intervalNote;
}
