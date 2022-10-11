using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelDef[] levels;
    public bool isNewUser;
    public string rewardSceneName => levels[currentLevelIndex].rewardSceneName;

    private int currentLevelIndex = 0;
    //public LevelDef lastFinishedLevel => UserProgress.Something to get last completed level

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Don't have more than one Game Manager...");
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
       //Debug.Log($"level loaded {levels[0].name}");
       // SceneController.OnSceneChangeRequired(SceneController.SceneAction.Login);
    }

    public LevelDef GetCurrentLevel(LevelManager levelRequester)
    {
        // subscribe to the requester so reward scene can be played when finished
        levelRequester.OnLevelComplete += PlayRewardScene;
        return levels[currentLevelIndex];
    }

    public void PlayRewardScene()
    {
        
        // Saves the level as completed in the database

        // goto next level
        currentLevelIndex++;
        if (currentLevelIndex == levels.Length)
        {
            // TODO: Get back to exit scene?
            Debug.Log("All levels have been played!!");
        }
        // open the bolt scene: scene controller will read name
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.BoltScene);
    }

}
