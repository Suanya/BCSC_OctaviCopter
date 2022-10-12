using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextUITutorial : MonoBehaviour
{
    public TextMeshPro Text;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Text = FindObjectOfType<TextMeshPro>();
        StartCoroutine(ShowText());   
    }

    private IEnumerator ShowText()
    {
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

        isPaused = true;
        while (isPaused)
        {
            yield return new WaitForSeconds(1);
        }

        Text.text = "Very good!!!";
        yield return new WaitForSeconds(2);
     
        Text.text = "In order to complete the missions,";
        yield return new WaitForSeconds(4);

        Text.text = "you need to fly trough the notes";
        yield return new WaitForSeconds(2);

        Text.text = "to collect them.";
        yield return new WaitForSeconds(2);

        Text.text = "You can fly the OctaviCopter";
        yield return new WaitForSeconds(2);

        Text.text = "up and down.";
        yield return new WaitForSeconds(2);
      
        Text.text = "Using the bottons";
        yield return new WaitForSeconds(2);
       
        Text.text = "on either controller";
        yield return new WaitForSeconds(2);

        Text.text = "With B & Y you fly higher";
        yield return new WaitForSeconds(3);
      
        Text.text = "Try to move up, now!";

        isPaused = true;
        while (isPaused)
        {
            yield return new WaitForSeconds(1);
        }

        Text.text = "Super!!!";
        yield return new WaitForSeconds(2);

        Text.text = "With A & X you fly lower";
        yield return new WaitForSeconds(4);

        Text.text = "Try to move down, now!";

        isPaused = true;
        while (isPaused)
        {
            yield return new WaitForSeconds(1);
        }

        Text.text = "Fantastic!!!";
        yield return new WaitForSeconds(2);
        
        Text.text = "Noticed the changing";
        yield return new WaitForSeconds(2);

        Text.text = "colors in front of you?";
        yield return new WaitForSeconds(2);

        Text.text = "This shows you,";
        yield return new WaitForSeconds(1);

        Text.text = "your current height";
        yield return new WaitForSeconds(2);

        Text.text = "This helps you to";
        yield return new WaitForSeconds(2);

        Text.text = "aim at the note.";
        yield return new WaitForSeconds(2);
       
        Text.text = "To fly through";
        yield return new WaitForSeconds(1);

        Text.text = "you must move forward.";
        yield return new WaitForSeconds(2);
      
        Text.text = "Push the joystick";
        yield return new WaitForSeconds(2);

        Text.text = "forward on either controller";
        yield return new WaitForSeconds(3);

        Text.text = "Give it a go!";

        isPaused = true;
        while (isPaused)
        {
            yield return new WaitForSeconds(1);
        }
        Debug.Log($"Coroutine restarted");

        Text.text = "Sparkling!";
        yield return new WaitForSeconds(2);

        Text.text = "Noticed the green & red buttons?";
        yield return new WaitForSeconds(3);

        Text.text = "Push the red button";
        yield return new WaitForSeconds(2);
          
        Text.text = "to exit the game";
        yield return new WaitForSeconds(2);
  
        Text.text = "Don't push the red button!";
        yield return new WaitForSeconds(3);

        Text.text = "After being presented,";
        yield return new WaitForSeconds(2);

        Text.text = "with your mission";
        yield return new WaitForSeconds(2);
        
        Text.text = "Push the green button";
        yield return new WaitForSeconds(2);

        Text.text = "to start the game!";
    }
}
