using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextUITutorial : MonoBehaviour
{
    public TextMeshPro Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = FindObjectOfType<TextMeshPro>();
        StartCoroutine(ShowText());
        
    }

    private IEnumerator ShowText()
    {
        //throw new NotImplementedException();
        Text.text = "Welcome inside the OctaviCopter!";
        yield return new WaitForSeconds(9);

        Text.text = "Hear I Am";
        yield return new WaitForSeconds(3);

        Text.text = "To guide you to endless space,";
        yield return new WaitForSeconds(4);

        Text.text = "to show you how to interact";
        yield return new WaitForSeconds(4);

        Text.text = "with the OctaviCopter!";
        yield return new WaitForSeconds(4);

        Text.text = "To complete your missions";
        yield return new WaitForSeconds(3);

        Text.text = "and to train your ear!";
        yield return new WaitForSeconds(3);

        Text.text = "and to train your ear!";
        yield return new WaitForSeconds(3);

        Text.text = "The mission's instructions will be";
        yield return new WaitForSeconds(2);

        Text.text = "displayed right in front of you";
        yield return new WaitForSeconds(3);

        Text.text = "where this text is.";
        yield return new WaitForSeconds(3);

        Text.text = "It shows you";
        yield return new WaitForSeconds(2);

        Text.text = "which notes you need to collect.";
        yield return new WaitForSeconds(3);

        Text.text = "Notice the keyboard in front of you!";
        yield return new WaitForSeconds(3);

        Text.text = "The notes required for the mission";
        yield return new WaitForSeconds(4);

        Text.text = "will be colored for you!";
        yield return new WaitForSeconds(4);

        Text.text = "Touch the keys";
        yield return new WaitForSeconds(2);

        Text.text = "if you need a hint";
        yield return new WaitForSeconds(3);

        Text.text = "Try!";
        yield return new WaitForSeconds(1);

        Text.text = "Touching the green F Key, now!";
        yield return new WaitForSeconds(3);

        Text.text = "Very good!!!";
        yield return new WaitForSeconds(2);

        Text.text = "In order to complete the missions,";
        yield return new WaitForSeconds(4);

        Text.text = "you need to fly trough the notes";
        yield return new WaitForSeconds(4);

        Text.text = "to collect them.";
        yield return new WaitForSeconds(2);
    }

  

  
}
