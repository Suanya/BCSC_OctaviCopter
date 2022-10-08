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
    private bool activeOnLoad;

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

                if (SceneManager.GetActiveScene().name != "LoginScene")
                {
                    unloadPreviousScene = false;
                    activeOnLoad = true;
                    StartCoroutine(ChangeScene("LoginScene", unloadPreviousScene));
                }

                return;

            case SceneAction.CutScene:

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("CutScene", unloadPreviousScene));
                //SoundManager.PlayMusic("CutSceneMusic");

                return;

            case SceneAction.Tutorial:

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("Tutorial", unloadPreviousScene));

                return;

            case SceneAction.GamePlay:

                unloadPreviousScene = true;   
                StartCoroutine(ChangeScene("GameScene", unloadPreviousScene));

                return;


            case SceneAction.BoltScene:

                unloadPreviousScene = true;
                //Note: this will have logic for selecting the Bolt scene related to the level when we have more than one
                StartCoroutine(ChangeScene("BoltBase", unloadPreviousScene));

                return;

            case SceneAction.Exit:
                Debug.Log("Game exited");
                // Exit the game
                Application.Quit();

                return;

        }

        currentSceneAction = newAction;
    }

    private IEnumerator ChangeScene(string newScene, bool unloadOldScene)
    {
        // Unload previous scene if required
        if (unloadOldScene)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        // Load new scene
        SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        // Wait until next frame
        yield return new WaitForSeconds(3);

        // Set new scene current and active
        currentScene = SceneManager.GetSceneByName(newScene);
        SceneManager.SetActiveScene(currentScene);

        if (newScene == "GameScene")
        GameManager.instance.LevelManager = GetComponent<LevelManager>();
    }

}
