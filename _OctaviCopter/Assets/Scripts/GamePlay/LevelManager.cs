using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// remove TMPro when debugging dome
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Mission missionController;

    public InputActionReference startMissionReference = null;
    public UnityAction <MissionDef> NewMissionToLoad = null;
    public UnityAction MissionCanStart = null;
    public UnityAction OnLevelComplete = null;

    public GameObject octaviCopter;
    public GameObject currentScaleColumn;
    public LevelDef currentLevel;

    private int missionIndex = 0;

    private bool missionInProgress = false;
    private bool missionPending = false;

    private void Awake()
    {
        // Set up the level with its properties and missions
        currentLevel = GameManager.instance.GetCurrentLevel(this);
        currentScaleColumn = SpawnScaleColumn();

    }
    private void Start()
    {
        missionController.OnMissionSetUp += PrepareToStartMission;
        NewMissionToLoad?.Invoke(currentLevel.missions[missionIndex]);
        
    }

    private void Update()
    {
        // check for user pressing trigger to start mission

        float startValue = startMissionReference.action.ReadValue<float>();
        if (!missionInProgress && missionPending && startValue > 0)
        {
            StartMission();
            missionInProgress = true;
            missionPending = false;

        }
    }

    private GameObject SpawnScaleColumn()
    {
        float missionSpawnSpacing = currentLevel.missions[missionIndex].scaleColumnSpacing;
        Vector3 spawnPoint = new Vector3(0, 0, octaviCopter.transform.position.z + missionSpawnSpacing);
        return Instantiate(currentLevel.missionKeyPrefab, spawnPoint, Quaternion.identity);
      
    }

    public void PrepareToStartMission()
    {
        missionPending = true;

    }

    public void StartMission()
    {
        missionInProgress = true;
        MissionCanStart?.Invoke();
        missionController.OnMissionCompleted += CheckMissionStatus;
    
    }

    private void CheckMissionStatus()
    {
        missionController.OnMissionCompleted -= CheckMissionStatus;
        missionIndex++;
        missionInProgress = false;
        missionPending = false;

        // check to see if there are any more missions
        if (missionIndex == currentLevel.missions.Length)
        {
            // no more missions - level is finished!!
            OnLevelComplete();
            missionPending = false;
            missionIndex = 0;
            OnLevelComplete?.Invoke();

        }
        else
        {
            // set up the next mission
            StartCoroutine(StartSubsequentMission(3));
            
        }
    }

    private IEnumerator StartSubsequentMission(int delay)
    {
        currentScaleColumn.transform.position = new Vector3(0f, 0f, octaviCopter.transform.position.z + currentLevel.missions[missionIndex].scaleColumnSpacing);

        yield return new WaitForSeconds(delay);

        NewMissionToLoad?.Invoke(currentLevel.missions[missionIndex]);
        missionController.OnMissionSetUp += PrepareToStartMission;

    }

}
