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
    private Note[] scaleNotes;
    private Note scaleTwin;
    private bool noteInMission = false;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        scaleNotes = FindObjectsOfType<Note>();

    }
    public void OnPlayHint()
    {
        // play the fancy note corresponding to this key 

        if (levelManager.hintsAvailable && noteInMission)
        {
            // find the note if we haven't already
            if (scaleTwin == null) scaleTwin = FindTwinFancyNote();
            StartCoroutine(PlayHintNote(levelManager.hintCooldown));

        }

    }
    public IEnumerator PlayHintNote(int coolDownTime)
    {
        if (scaleTwin != null)
        {
            scaleTwin.TurnOnHintEffect();
        }

        yield return new WaitForSeconds(coolDownTime);

        if (scaleTwin != null)
        {
            scaleTwin.TurnOffHintEffect();
        }
 
    }
    public void OnShowHintColours()
    {
        // change the key to its hint colour
        meshRenderer.material = hintMaterial;
        // find the fancy note if we haven't already (as it should be playable now)
        if (scaleTwin == null) scaleTwin = FindTwinFancyNote();
        noteInMission = true;
    }

    public void OnHideHintColours()
    {
        // return the key to its normal colour
        meshRenderer.material = regularMaterial;
        // reset the fancy note association (as it shouldn't be playable now)
        scaleTwin = null;
        noteInMission = false;
    }

    private Note FindTwinFancyNote()
    {

        foreach (Note note in scaleNotes)
        {
            if (note.CompareTag(noteTag))
            {
                return note;
            }
        }

        return null;
    }

    // Note for next time I think I need to code the notes  audio playing
    // The notes are played as part of the XR Grab Interactable component

}
