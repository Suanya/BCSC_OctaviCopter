using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "New Mission")]
public class Mission : ScriptableObject
{
    public int missionID;
    public string missionName;
    public Sprite missionGraphic;

}
