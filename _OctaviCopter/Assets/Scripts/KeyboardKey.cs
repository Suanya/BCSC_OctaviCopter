using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{
    public Material hintMaterial;
    public GameObject fancyNotePrefab;
    public string noteTag;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material regularMaterial;

    private LevelManager levelManager;
    private GameObject regularNote;
    private GameObject fancyNote;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void OnHintAvailable()
    {

        // change colour of key if it's in the mission
        meshRenderer.material = hintMaterial;

    }

    public void OnHintUnavailable()
    {
        // return the key to its normal colour
        meshRenderer.material = regularMaterial;
    }

    public void OnHintRequested()
    {
        if (levelManager.missionInProgress)
        {
            regularNote = GameObject.FindWithTag(this.tag);

            Debug.Log($"Instantiate prefab at {regularNote.transform.position}");

            //instantiate fancy note at position of regular note
            //fancyNote = Instantiate(fancyNotePrefab, regularNote.transform.position, regularNote.transform.rotation);
        }
        
    }

    public void OnHintDeactivated()
    {
        // destroy fancy note
        Destroy(fancyNote);
    }
}
