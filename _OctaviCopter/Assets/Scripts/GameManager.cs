using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool useForTesting = false;
    public LevelDef[] levels;
    public bool isNewUser;
    public bool playFabLoginSuccessful;
    public string userName;
    public string rewardSceneName => levels[currentLevelIndex].rewardSceneName;
    

    private int currentLevelIndex = 0;
    private BoltFinish boltFinish;
    private PlayFabLogin playFabLogin;

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
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        if (!useForTesting)
        {
            SceneController.OnSceneChangeRequired(SceneController.SceneAction.Login);
            playFabLogin = GetComponent<PlayFabLogin>();

        }
       
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

        User currentUser = UserDatabase.GetUser(userName);
        UserDatabase.UpdateLevel(currentUser.UserName, currentUser.UserID, currentLevelIndex, levels[currentLevelIndex].levelName);

        // Registers the level completion with PlayFab
        if (playFabLoginSuccessful)
        {
            // check if logged in - don't want to prevent offline play
            playFabLogin.PlayerCompletedLevel(currentLevelIndex);
        }

        if (currentLevelIndex == levels.Length)
        {
            // TODO: Get back to exit scene?
            Debug.Log("All levels have been played!!");
        }
        else
        {
            StartCoroutine(ManageBoltScene());
       
        }

    }

    private IEnumerator ManageBoltScene()
    {
        // wait for last note decay
        yield return new WaitForSeconds(4);

        // open the bolt scene: scene controller will read name
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.BoltScene);

        // make sure it's loaded
        yield return new WaitForSeconds(2);

        // find the finish script and subscribe
        boltFinish = FindObjectOfType<BoltFinish>();
        boltFinish.boltSceneFinished += NextLevel;

    }

    private void NextLevel()
    {
        boltFinish.boltSceneFinished -= NextLevel;
        currentLevelIndex++;
        if (currentLevelIndex == levels.Length)
        {
            // Maybe make end scene version of Cut Scene
            SceneController.OnSceneChangeRequired(SceneController.SceneAction.CutScene);
        }
        else
        {
            SceneController.OnSceneChangeRequired(SceneController.SceneAction.GamePlay);
        }
        

    }

}
