using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackSpaceKey : UIInputKey
{
    [SerializeField] private TextMeshProUGUI welcomeText;
    protected override void UpdateInputField()
    {
        if (acceptingInput && nameInput.isActiveAndEnabled)
        {
            // back = backspace
            BackSpace();
        }
        else
        {
            // back = back to re-enter login:
            ResetLogin();
        }
    }

    private void BackSpace()
    {
        Debug.Log("Backspacing");

        string temp = null;

        for (int i = 0; i < nameInput.text.Length; i++)
        {
            if (i < nameInput.text.Length - 1)
            {
                temp += nameInput.text[i];
            }
        }
        nameInput.text = temp;
    }    

    private void ResetLogin()
    {
        Debug.Log("Resetting login");
        welcomeText.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(true);
        nameInput.text = null;
        
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (!acceptingInput)
        {
            Debug.Log("Allowing input again");
            acceptingInput = true;
        }
    }

}

