using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIInputKey : MonoBehaviour
{
    [SerializeField] protected TMP_InputField nameInput;
    protected bool acceptingInput = true;

    [SerializeField] private string keyLetter;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.enabled = true;
            UpdateInputField();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.enabled = false;
        }
        
    }
    protected virtual void UpdateInputField()
    {
        if (acceptingInput)
        {
            // Add letter
            nameInput.text += keyLetter;
        }
 
    }

}
