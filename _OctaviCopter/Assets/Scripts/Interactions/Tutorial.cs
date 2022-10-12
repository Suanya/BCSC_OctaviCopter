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
    public KeyboardKey fKey;

    [SerializeField] private Transform octaviCopter;
    [SerializeField] private PlayableDirector tutorialDirector;
    [SerializeField] private TextUITutorial textCoroutine;

    [SerializeField] private TutorialStage currentStage;
    private float startingZPosition;
    private bool awaitingInput = false;

    private enum TutorialStage
    {
        Intro,
        Keyboard,
        FlyUp,
        FlyDown,
        FlyForward,
        StartMission
    }

    void Start()
    {
        currentStage = TutorialStage.Intro;
        startingZPosition = octaviCopter.position.z;
        
    }

    void Update()
    {
        if(awaitingInput)
        {
            switch (currentStage)
            {
                case TutorialStage.FlyUp:
                    {
                        float upButtonValue = moveUpReference.action.ReadValue<float>();
                        if (upButtonValue > 0)
                        {
                            currentStage++;
                            StartTutorialDirector();

                        }
                        break;
                    }
                case TutorialStage.FlyDown:
                    {
                        float downButtonValue = moveDownReference.action.ReadValue<float>();
                        if (downButtonValue > 0)
                        {
                            currentStage++;
                            StartTutorialDirector();

                        }
                        break;
                    }
                case TutorialStage.FlyForward:
                    {
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
        if(currentStage == TutorialStage.StartMission || !GameManager.instance.isNewUser)
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
        awaitingInput = false;
        tutorialDirector.time = tutorialDirector.time;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        AudioListener.pause = false;
        textCoroutine.isPaused = false;
    }

    public void StopTutorialDirector()
    {
        awaitingInput = true;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        AudioListener.pause = true;

        if (currentStage == TutorialStage.Intro)
        {
            IntroFinished();
        }

    }
}
