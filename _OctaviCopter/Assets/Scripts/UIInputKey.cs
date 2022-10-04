using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIInputKey : MonoBehaviour
{
    [SerializeField] private string keyLetter;
    [SerializeField] private TMP_InputField nameInput;

    private bool acceptingInput = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(keyLetter == "Back")
            {
                BackSpace();
            }
            else
            {
                AddLetter();
            }

        }
    }

    private void AddLetter()
    {
        nameInput.text += keyLetter;

    }

    private void BackSpace()
    {
        string temp = null;

        for(int i = 0; i < nameInput.text.Length; i++)
        {
            if(i < nameInput.text.Length - 1)
            {
                temp += nameInput.text[i];
            }
        }

        nameInput.text = temp;
    }

}
