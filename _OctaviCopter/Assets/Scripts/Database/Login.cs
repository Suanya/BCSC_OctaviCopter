using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameInput;

    private string userName;

    public void OnLetterKeyPressed(string letter)
    {
        userName = nameInput.ToString();

        if (letter == "<")
        {
            // remove the last letter
            RemoveLastLetter(userName);
        }
        else
        {
            userName += letter;
        }

        nameInput.text = userName;
    }

    private void OnNameEntered(string userName)
    {

    }

    private void ValidateName(string userName)
    {

    }

    private string RemoveLastLetter(string word)
    {
        string newWord = null;

        for(int i = 0; i < word.Length; i++)
        {
            if (i != word.Length - 1)
            {
                // not last letter
                newWord += word[i];
            }
        }
        return newWord;
    }
}
