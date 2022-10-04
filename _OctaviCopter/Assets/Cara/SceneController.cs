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
    public static bool newSession;

    public SceneAction currentSceneAction;
    public string mainMenuButton;

    private Scene currentScene;
    private bool unloadPreviousScene;

    public InputActionReference octaviQuitReference = null;

    public enum SceneAction
    {
        None,
        Login,
        Tutorial,
        CutScene,
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
        newSession = true;
        instance.OnMenuSelectionInternal(SceneAction.None);
        currentSceneAction = SceneAction.None;
    }

    internal static void OnMenuSelection(SceneAction newAction)
    {
        instance.OnMenuSelectionInternal(newAction);
    }

    private void OnMenuSelectionInternal(SceneAction newAction)
    {
        // Handle the different scene change scenarios
        // Note that the set up scene (containing singleton classes) is loaded first, and remains loaded throughout
        switch (newAction)
        {
            case SceneAction.None:
                // Should only happen at the very beginning of the game

                unloadPreviousScene = false;
                StartCoroutine(ChangeScene("Login", unloadPreviousScene));

                return;

            case SceneAction.Login:

                // Unload the current scene if it is not already the main menu
                // This the only scene that can be selected while it is the currently active scene (with controller menu button)

                if (SceneManager.GetActiveScene().name != "Login")
                {
                    unloadPreviousScene = true;
                    StartCoroutine(ChangeScene("Login", unloadPreviousScene));
                }

                return;

            case SceneAction.Tutorial:

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("Tutorial", unloadPreviousScene));

                return;

            case SceneAction.CutScene:

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("CutScene", unloadPreviousScene));
                SoundManager.PlayMusic("CutSceneMusic");

                return;

            case SceneAction.GamePlay:
                // set the new Game flag off, so the resume button will be available to the menu next time it is loaded
                newSession = false;

                unloadPreviousScene = true;   
                StartCoroutine(ChangeScene("GamePlay", unloadPreviousScene));

                return;


            case SceneAction.BoltScene:

                unloadPreviousScene = true;
                StartCoroutine(ChangeScene("Credits", unloadPreviousScene));

                return;

            case SceneAction.Exit:
                Debug.Log("Game exited");
                // Exit the game
                Application.Quit();

                return;

        }

        currentSceneAction = newAction;
    }

    private void Update()
    {
        // Check the input to see if the player needs to change scene

        float startValue = octaviQuitReference.action.ReadValue<float>();
        if (startValue > 0)
        {
            OnMenuSelection(SceneAction.Exit);
        }

    }

    private IEnumerator ChangeScene(string newScene, bool unloadOldScene)
    {
        // Unload previous scene if required
        if (unloadOldScene)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        // Load new scene
        SceneManager.LoadScene(newScene, LoadSceneMode.Additive);

        // Wait until next frame
        yield return null;

        // Set new scene active
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

    }



}
