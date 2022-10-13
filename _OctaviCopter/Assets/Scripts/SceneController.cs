using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public SceneAction currentSceneAction;
    public string mainMenuButton;

    private Scene currentScene;
    private bool unloadPreviousScene;
    private bool activateOnLoad;

    public InputActionReference octaviQuitReference = null;

    public enum SceneAction
    {
        None,
        Login,
        CutScene,
        Tutorial,
        GamePlay,
        BoltScene,
        Exit
    }

    private void Awake()
    {
        // Are there any other Scene Controls yet?
        if (instance != null)
        {
            // Error
            Debug.LogError("There was more than 1 Scene Control");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }
    private void Start()
    {
        currentSceneAction = SceneAction.None;
    }

    internal static void OnSceneChangeRequired(SceneAction newAction)
    {
        instance.OnSceneChangeRequiredInternal(newAction);
    }

    private void OnSceneChangeRequiredInternal(SceneAction newAction)
    {
        // Handle the different scene change scenarios
        // Note that the Managers scene (containing singleton classes) is loaded first, and remains loaded throughout
        switch (newAction)
        {
            case SceneAction.None:
                // Should only happen at the very beginning of the game
                Debug.Log("Start (manager scene)");

                return;

            case SceneAction.Login:

                // The login scene loads first and stays open
                currentSceneAction = SceneAction.Login;

                if (SceneManager.GetActiveScene().name != "LoginScene")
                {
                    unloadPreviousScene = false;
                    activateOnLoad = true;
                    StartCoroutine(ChangeScene("LoginScene", unloadPreviousScene, activateOnLoad));
                    // preload the Cut Scene
                    //activateOnLoad = false;
                    //StartCoroutine(ChangeScene("CutScene", unloadPreviousScene, ));
                }

                return;

            case SceneAction.CutScene:

                currentSceneAction = SceneAction.CutScene;

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("CutScene", unloadPreviousScene, activateOnLoad));
                //SoundManager.PlayMusic("CutSceneMusic");

                return;

            case SceneAction.Tutorial:

                currentSceneAction = SceneAction.Tutorial;

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("Tutorial", unloadPreviousScene, activateOnLoad));

                return;

            case SceneAction.GamePlay:

                currentSceneAction = SceneAction.GamePlay;

                unloadPreviousScene = true;   
                StartCoroutine(ChangeScene("GameScene", unloadPreviousScene, activateOnLoad));

                return;


            case SceneAction.BoltScene:

                currentSceneAction = SceneAction.BoltScene;

                unloadPreviousScene = true;
                string sceneName = GameManager.instance.rewardSceneName;
                //Note: this will have logic for selecting the Bolt scene related to the level when we have more than one
                StartCoroutine(ChangeScene(sceneName, unloadPreviousScene, activateOnLoad));

                return;

            case SceneAction.Exit:

                currentSceneAction = SceneAction.Exit;

                Debug.Log("Game exited");
                // Exit the game
                Application.Quit();

                return;

        }

        currentSceneAction = newAction;
    }

    private IEnumerator ChangeScene(string newScene, bool unloadOldScene, bool activeOnLoad)
    {
        // Unload previous scene if required
        if (unloadOldScene)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            Resources.UnloadUnusedAssets();
        }

        // Load new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene);

        while (!asyncLoad.isDone)
        {
            // Wait until next frame
            yield return null;
        }
        

        // Set new scene current and active
        currentScene = SceneManager.GetSceneByName(newScene);
        SceneManager.SetActiveScene(currentScene);

    }

}
