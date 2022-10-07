using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI messageText;
    
    [SerializeField] private LevelManager levelManager;

    private void OnEnable()
    {
        levelManager.OnMissionSetUp += InformStartingMission;
        levelManager.OnMissionStarted += InformMissionLaunched;
        levelManager.OnLastMissionComplete += InformLevelCompleted;
    }

    public void InformStartingLevel(string missionName, string missionInstructions)
    {
        Debug.Log("Starting new level");
        
    }

    public void InformStartingMission(string missionName, string missionInstructions)
    {
        // update mission info
        missionText.text = $"Mission: {missionName}";
        instructionText.text = missionInstructions;
        messageText.text = "Press trigger to start...";
        // play voice clip
    }

    public void InformMissionLaunched()
    {

        levelManager.currentMission.OnCorrectNoteCollected += InformCorrectNote;
        levelManager.currentMission.OnIncorrectNoteCollected += InformIncorrectNote;
        levelManager.currentMission.OnMissionCompleted += InformMissionCompleted;

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

    public void InformMissionCompleted(Mission mission)
    {
        levelManager.currentMission.OnCorrectNoteCollected -= InformCorrectNote;
        levelManager.currentMission.OnIncorrectNoteCollected -= InformIncorrectNote;
        messageText.text = $"{mission.name} Complete!";
    }

    public void InformLevelCompleted()
    {
        Debug.Log("If no text is in messageText, it isn't due to program flow, as flow gets here");

        levelManager.OnMissionStarted -= InformMissionLaunched;
        levelManager.currentMission.OnMissionCompleted -= InformMissionCompleted;

        messageText.text = "Level Complete! Stay tuned for an unforgettable ride!!!";
        Debug.Log($"Text '{messageText.text}' should have appeared in the UI");

            
    }

    public void OnDisable()
    {
        levelManager.OnMissionSetUp -= InformStartingLevel;
        levelManager.OnLastMissionComplete -= InformLevelCompleted;
    }
}
