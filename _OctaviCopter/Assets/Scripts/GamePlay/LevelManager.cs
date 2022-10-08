using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // use for testing until database is created
    [SerializeField] private Level currentLevel;
    [SerializeField] private float missionSpawnSpacing = 15f;
    [SerializeField] private GameObject octaviCopter;

    public InputActionReference startMissionReference = null;
    public UnityAction <string, string> OnMissionSetUp = null;
    public UnityAction OnMissionStarted = null;
    public UnityAction OnLastMissionComplete = null;

    public Mission currentMission;
    public GameObject currentScaleColumn;
    public bool missionInProgress = false;

    private int currentMissionIndex = 0;
    private bool missionPending = false;
    private bool hintsAvailable;
    private string missionInstructions;

    private void Awake()
    {
        // Get the next level the user should be at: deserialize when database is set up
        // currentLevel = GetLevelFromDatabase

        currentScaleColumn = SpawnScaleColumn();

    }
    private void Start()
    {
        // find and set up the first mission (FindMission calls specific set up for mission type)
        currentMission = FindMission();

        // inform anyone who cares that mission is ready to start
        missionPending = true;
        OnMissionSetUp?.Invoke(currentMission.name, missionInstructions);

        // wait for word that the mission is complete
        currentMission.OnMissionCompleted += CheckMission;
    }

    private void Update()
    {
        float startValue = startMissionReference.action.ReadValue<float>();
        if (!missionInProgress && missionPending && startValue > 0)
        {
            missionInProgress = true;
            StartMission();
        }
    }

    private GameObject SpawnScaleColumn()
    {
        Vector3 spawnPoint = new Vector3(0, 0, octaviCopter.transform.position.z + missionSpawnSpacing);
        return Instantiate(currentLevel.missionKeyPrefab, spawnPoint, Quaternion.identity);
      
    }

    private Mission FindMission()
    {
        hintsAvailable = currentLevel.hintsAvailable;
        switch (currentLevel.missionCategory)
        {
            case "Interval":
                var intervalMission = (Interval)currentLevel.missions[currentMissionIndex];
                intervalMission.SetUpMission(currentScaleColumn, hintsAvailable);
                missionInstructions = $"Base note: {intervalMission.baseNote.displayName}  " +
                                      $"Interval note: {intervalMission.intervalNote.displayName}";
                break;

            case "Chord":
                var chordMission = (Chord)currentLevel.missions[currentMissionIndex];
                chordMission.SetUpMission(currentScaleColumn, hintsAvailable);
                missionInstructions = $"Base note: {chordMission.baseNote.displayName}  " +
                                      $"Third note: {chordMission.thirdNote.displayName}  " +
                                      $"Fifth note: {chordMission.fifthNote.displayName}";
                break;

            case "Rhythm":
                missionInstructions = ("Stuff for rhythms is not written yet");
                break;

        }

        float columnSpacing = currentLevel.missions[currentMissionIndex].scaleColumnSpacing;
        currentScaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + columnSpacing);

        return currentLevel.missions[currentMissionIndex];
    }

    public void StartMission()
    {
        if (hintsAvailable)
        {
            StartCoroutine(PlayMissionNotes());
        }

        OnMissionStarted?.Invoke();
    }

    private IEnumerator PlayMissionNotes()
    {
        for (int i = 0; i < currentMission.requiredNotes.Count; i++)
        
        {
            Note note = currentMission.requiredNotes[i];
            // activate note fx
            note.visualEffectContainer.SetActive(true);

            // play the octaviTone
            note.audioSource.Play();

            // pause before the next note is played
            yield return new WaitForSeconds(note.effectDuration);

            // deactivate to reset
            note.visualEffectContainer.SetActive(false);

        }

    }
    
    private void CheckMission(Mission mission)
    {

        currentMission.OnMissionCompleted -= CheckMission;
        currentMissionIndex++;
        missionInProgress = false;
        missionPending = false;

        // check to see if there are any more missions
        if (currentMissionIndex == currentLevel.missions.Length)
        {
            // no more missions - level is finished!!
            OnLevelComplete();
        }
        else
        {
            // set up the next mission
            StartCoroutine(StartSubsequentMission(3));
            
        }
    }

    private IEnumerator StartSubsequentMission(int delay)
    {

        yield return new WaitForSeconds(delay);

        currentMission = FindMission();
        missionPending = true;
        OnMissionSetUp?.Invoke(currentMission.name, missionInstructions);
        currentMission.OnMissionCompleted += CheckMission;
        
    }

    private void OnLevelComplete()
    {
        // Play reward scene
        missionPending = false;
        currentMissionIndex = 0;
        OnLastMissionComplete?.Invoke();
    }

}
