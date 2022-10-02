using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    public float altitude;
    public string displayName;

    private AudioSource audioSource;

    public event Action <Note> OnNoteCollected;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(audioSource.clip);
        // maybe haptic feedback here?
        OnNoteCollected?.Invoke(this);
    }
}
