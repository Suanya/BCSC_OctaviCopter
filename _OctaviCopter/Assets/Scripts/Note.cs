using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    private AudioSource audioSource;
    public float altitude;

    public event Action <Note> OnNoteCollected;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OctaviCopter"))
        {
            audioSource.PlayOneShot(audioSource.clip);
            Debug.Log($"Are there subscribers to {this.name}? {OnNoteCollected != null}");
            OnNoteCollected?.Invoke(this);
        }

    }


}
