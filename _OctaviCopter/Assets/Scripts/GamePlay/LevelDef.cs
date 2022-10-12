using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="LevelDef", menuName = "New Level")]
public class LevelDef : ScriptableObject
{
    public int levelID;
    public string levelName;
    public string missionCategory;
    public GameObject missionKeyPrefab;
    //public AudioClip rewardSong;
    public string rewardSceneName;
    public MissionDef[] missions;
    public bool hintsAvailable;
    public int hintCooldownTime;

}
