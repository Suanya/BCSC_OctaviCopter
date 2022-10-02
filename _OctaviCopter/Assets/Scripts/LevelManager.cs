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
    [SerializeField] private TMP_Text messageText;

    public InputActionReference startMissionReference = null;
    public UnityAction <string, string> OnMissionSetUp = null;
    public UnityAction OnMissionStarted = null;
    public UnityAction OnLastMissionComplete = null;

    public GameObject octaviCopter;
    public Mission currentMission;
    public GameObject currentScaleColumn;

    private int currentMissionIndex = 0;
    private bool missionInProgress = false;
    private bool missionPending = false;
    private UpdateUI updateUI;
    private string missionInstructions;

    //public UnityEvent Response;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the next level the user should be at: deserialize when database is set up
        // currentLevel = GetLevelFromDatabase

        octaviCopter = GameObject.FindGameObjectWithTag("OctaviCopter");
        updateUI = GetComponent<UpdateUI>();
        SpawnScaleColumn();
        missionPending = true;
        currentMission = FindMission();
    }

    private void Start()
    {
        OnMissionSetUp?.Invoke(currentMission.name, missionInstructions);
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

    private Mission FindMission()
    {
        switch (currentLevel.missionCategory)
        {
            case "Interval":
                var intervalMission = (Interval)currentLevel.missions[currentMissionIndex];
                intervalMission.SetUpMission();
                missionInstructions = $"Base note: {intervalMission.baseNote.displayName}  " +
                                      $"Interval note: {intervalMission.intervalNote.displayName}";
                break;

            case "Chord":
                var chordMission = (Chord)currentLevel.missions[currentMissionIndex];
                chordMission.SetUpMission();
                missionInstructions = $"Base note: {chordMission.baseNote.displayName}  " +
                                      $"Third note: {chordMission.thirdNote.displayName}  " +
                                      $"Fifth note: {chordMission.fifthNote.displayName}";
                break;

            case "Rhythm":
                missionInstructions = ("Stuff for rhythms is not written yet");
                break;

        }
        
        return currentLevel.missions[currentMissionIndex];
    }

    public void StartMission()
    {
        currentScaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + currentMission.scaleColumnSpacing);
        OnMissionStarted?.Invoke();
    }

    private void SpawnScaleColumn()
    {
        Vector3 spawnPoint = new Vector3(0, 0, octaviCopter.transform.position.z + -1);
        currentScaleColumn = Instantiate(currentLevel.missionKeyPrefab, spawnPoint, Quaternion.identity);
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
        OnMissionSetUp?.Invoke(currentMission.name, missionInstructions);
        currentMission.OnMissionCompleted += CheckMission;
        missionPending = true;
    }

    private void OnLevelComplete()
    {
        // Play reward scene
        missionPending = false;
        currentMissionIndex = 0;
        OnLastMissionComplete?.Invoke();
    }

}
