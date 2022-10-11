using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Tutorial : MonoBehaviour
{
    public InputActionReference moveUpReference = null;
    public InputActionReference moveDownReference = null;

    //[SerializeField] private AudioListener audioListener;
    public KeyboardKey fKey;

    [SerializeField] private Transform octaviCopter;
    [SerializeField] private PlayableDirector tutorialDirector;

    [SerializeField] private tutorialStage currentStage;
    private float startingZPosition;
    private bool awaitingInput = false;

    private enum tutorialStage
    {
        Intro,
        Keyboard,
        FlyUp,
        FlyDown,
        FlyForward,
        StartMission
    }
    // Start is called before the first frame update
    void Start()
    {
        currentStage = tutorialStage.Intro;
        startingZPosition = octaviCopter.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(awaitingInput)
        {
            switch (currentStage)
            {
                case tutorialStage.FlyUp:
                    {
                        Debug.Log($"Waiting for FlyUp Input");
                        float upButtonValue = moveUpReference.action.ReadValue<float>();
                        if (upButtonValue > 0)
                        {
                            currentStage++;
                            StartTutorialDirector();

                        }
                        break;
                    }
                case tutorialStage.FlyDown:
                    {
                        Debug.Log($"Waiting for FlyDown Input");
                        float downButtonValue = moveDownReference.action.ReadValue<float>();
                        if (downButtonValue > 0)
                        {
                            currentStage++;
                            StartTutorialDirector();

                        }
                        break;
                    }
                case tutorialStage.FlyForward:
                    {
                        Debug.Log($"Waiting for FlyForward Input");
                        if (octaviCopter.position.z > startingZPosition)
                        {
                            currentStage++;
                            StartTutorialDirector();
                        }
                        break;
                    }
            }
        }
        
    }

    public void IntroFinished()
    {
        fKey.GetComponent<MeshRenderer>().material = fKey.hintMaterial;
        currentStage++;
        fKey.hintKeyPlayed += KeyboardPlayed;
        Debug.Log($"Intro finished - waiting for keyboard input");
    }
    public void KeyboardPlayed()
    {
        currentStage++;
        fKey.hintKeyPlayed -= KeyboardPlayed;
        StartTutorialDirector();
    }

    public void StartButtonPushed()
    {
        if(currentStage == tutorialStage.StartMission || !GameManager.instance.isNewUser)
        {
            SceneController.OnSceneChangeRequired(SceneController.SceneAction.GamePlay);
        }
        
    }

    public void QuitButtonPushed()
    {
        Application.Quit();
    }

    public void StartTutorialDirector()
    {
        Debug.Log($"Restarting at {currentStage}");
        awaitingInput = false;
        tutorialDirector.time = tutorialDirector.time;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        AudioListener.pause = false;
    }

    public void StopTutorialDirector()
    {
        Debug.Log($"Stopping at {currentStage}");
        awaitingInput = true;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        AudioListener.pause = true;

        if (currentStage == tutorialStage.Intro)
        {
            IntroFinished();
        }

    }
}
