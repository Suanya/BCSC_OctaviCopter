using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // use for testing until database is created
    [SerializeField] private Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Get the next level the user should be at
        // currentLevel = GetLevelFromDatabase

        // Instantiate the first column (based on key of level)
        Instantiate(currentLevel.missionKeyPrefab);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
