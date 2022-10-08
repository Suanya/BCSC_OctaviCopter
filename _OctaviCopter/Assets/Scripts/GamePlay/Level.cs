using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level", menuName = "New Level")]
public class Level : ScriptableObject
{
    public int levelID;
    public string levelName;
    public string missionCategory;
    public GameObject missionKeyPrefab;
    public AudioClip rewardSong;
    public Mission[] missions;
    public bool hintsAvailable;
    public int hintCooldownTime;

}
