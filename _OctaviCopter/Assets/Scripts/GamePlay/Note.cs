using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VFX;

public class Note : MonoBehaviour
{
    public float altitude;
    public string displayName;
    public GameObject visualEffectContainer;
    public int effectDuration = 4;
    public AudioSource audioSource;

    public event Action<Note> OnNoteCollected;

    private void Start()
    {
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OctaviCopter"))
        {
            NoteHitActivities();

        }

    }

    private void NoteHitActivities()
    {
        Debug.Log($"Playing {audioSource.clip.name}");

        audioSource.PlayOneShot(audioSource.clip);
        // maybe haptic feedback here?
        OnNoteCollected?.Invoke(this);

    }

    public void TurnOnHintEffect()
    {
        Debug.Log($"Activating {gameObject.name}; currently active? {visualEffectContainer.activeSelf}");
        visualEffectContainer.SetActive(true);
        
    }

    public void TurnOffHintEffect()
    {
        visualEffectContainer.SetActive(false);
        Debug.Log($"Note {gameObject.name} deactivated");

    }
}
