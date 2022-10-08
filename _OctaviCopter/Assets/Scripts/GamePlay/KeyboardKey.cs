using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardKey : MonoBehaviour
{
    public Material hintMaterial;
    public string noteTag;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material regularMaterial;

    private LevelManager levelManager;
    private Note[] missionNotes;
    private Note fancyTwin;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        missionNotes = FindObjectsOfType<Note>();
    }
    public void OnShowHint()
    {
        // change colour of key if it's in the mission
        meshRenderer.material = hintMaterial;
        if (fancyTwin == null)
        {
            foreach(Note note in missionNotes)
            {
                if (note.CompareTag(gameObject.tag))
                {
                    fancyTwin = note;
                }
            }
            
        }
        if (fancyTwin != null)
        {
            fancyTwin.TurnOnHintEffect();
        }
        else
        {
            Debug.Log("Fancy twin was called but has not been found - happens at beginning and is ok");
        }

    }

    public void OnHideHint()
    {
        // return the key to its normal colour
        meshRenderer.material = regularMaterial;
    }

    // Note for next time I think I need to code the notes  audio playing
    // The notes are played as part of the XR Grab Interactable component

}
