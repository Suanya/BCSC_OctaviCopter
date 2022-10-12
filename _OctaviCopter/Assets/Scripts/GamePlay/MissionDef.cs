using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MissionDef", menuName = "New Mission")]
public class MissionDef : ScriptableObject
{
    public string missionName;
    public Sprite missionGraphic;
    public float scaleColumnSpacing = 15.0f;

    public string typeName;
}
