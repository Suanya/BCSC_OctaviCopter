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
    }

    public void InformStartingLevel(string missionName, string missionInstructions)
    {
        Debug.Log("Starting new level");
        
    }

    public void InformStartingMission()
    {

        missionText.text = $"Mission: {missionController.currentMission.missionName}";
        instructionText.text = missionController.missionInstructions;
        messageText.text = "Push green button to start...";
        // play voice clip


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
        messageText.text = $"{missionController.currentMission.missionName} Complete!";

    }

    private void OnDestroy()
    {
        levelManager.MissionCanStart -= InformMissionLaunched;
        missionController.OnMissionSetUp -= InformStartingMission;
        missionController.OnCorrectNoteCollected -= InformCorrectNote;
        missionController.OnIncorrectNoteCollected -= InformIncorrectNote;
        missionController.OnMissionCompleted -= InformMissionCompleted;
    }

}
