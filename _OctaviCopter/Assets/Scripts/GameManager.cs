using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isNewUser;
    public LevelManager LevelManager;

    //public Level lastFinishedLevel => UserProgress.Something to get last completed level

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
        Debug.Log("Game manager triggering scene change to Login");
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.Login);
    }

    public void OnLevelStarted()
    {
        //TODO: Add logic to play more than one round of levels
        LevelManager.OnLastMissionComplete += OnLevelCompleted;
    }

    public void OnLevelCompleted(Level level)
    {
        // for now, only have one scene, so just call it; add choice logic as a TODO
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.BoltScene);
    }

}
