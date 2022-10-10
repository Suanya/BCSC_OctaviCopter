using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text missionText;
    [SerializeField] private Text instructionText;
    [SerializeField] private TMP_Text messageText;
    
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Mission missionController;

    private void OnEnable()
    {
        missionController.OnMissionSetUp += InformStartingMission;
        levelManager.MissionCanStart += InformMissionLaunched;
        // levelManager.OnLastMissionComplete += InformLevelCompleted;  wasn't displaying in UI anyway; look at again if needed
    }

    public void InformStartingLevel(string missionName, string missionInstructions)
    {
        Debug.Log("Starting new level");
        
    }

    public void InformStartingMission()
    {
        // update mission info

        {
            missionText.text = $"Mission: {missionController.currentMission.missionName}";
            instructionText.text = missionController.missionInstructions;
            messageText.text = "Push green button to start...";
            // play voice clip
        }

    }

    public void InformMissionLaunched()
    {

        missionController.OnCorrectNoteCollected += InformCorrectNote;
        missionController.OnIncorrectNoteCollected += InformIncorrectNote;
        missionController.OnMissionCompleted += InformMissionCompleted;

        messageText.text = "Good luck!";
        // play voice clip
    }

    public void InformIncorrectNote()
    {

        messageText.text = "This is not the right note...try again";
        // play voice clip
    }

    public void InformCorrectNote()
    {

        messageText.text = "Correct!";
        // play voice clip
    }

    public void InformMissionCompleted()
    {
        missionController.OnCorrectNoteCollected -= InformCorrectNote;
        missionController.OnIncorrectNoteCollected -= InformIncorrectNote;
        messageText.text = $"{missionController.name} Complete!";
    }

}
