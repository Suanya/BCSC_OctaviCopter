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

    private tutorialStage currentStage;
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
        fKey.hintKeyPlayed += KeyboardPlayed;
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
        awaitingInput = false;
        tutorialDirector.time = tutorialDirector.time;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);

    }

    public void StopTutorialDirector()
    {
        Debug.Log($"Stopping at {currentStage}");
        awaitingInput = true;
        tutorialDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (currentStage == tutorialStage.Intro)
        {
            IntroFinished();
        }

    }
}
