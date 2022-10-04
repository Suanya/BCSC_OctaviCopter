using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private UIInputKey welcomeKey;
    [SerializeField] private GameObject welcomeButton;
    [SerializeField] private TMP_Text welcomeText;
    [SerializeField] private TMP_InputField userName;

    private User currentUser;
    private string welcomeMessage;

    private void Start()
    {

    }
    public void AttemptLogin(string userName)
    {
        
        // check to see if name is in the database
        currentUser = UserDatabase.GetUser(userName);

        Debug.Log($"Is user new? {currentUser == null}");
        if (currentUser == null)
        {
            // tell the Game Manager (so the cut scene will not be skippable)
            GameManager.instance.isNewUser = true;

            // Register the user
            currentUser = UserDatabase.AddNewUser(userName);
            //Welcome with option to click to re-enter name (in case they misspelled and shouldn't be new)
            welcomeMessage = $"Welcome, {userName}! Click here to change name if this is not your first time";
        }
        else
        {
            // tell the Game Manager (so the cut scene will be skippable)
            GameManager.instance.isNewUser = false;

            //Welcome with option to skip cut scene
            welcomeMessage = $"Welcome, {userName}! Click here to skip the cut scene, or click start again to play it";
        }

        welcomeKey.gameObject.SetActive(true);
        welcomeButton.gameObject.SetActive(true);
        welcomeText.gameObject.SetActive(true);


        // comment this out when database stuff works
        welcomeMessage = $"Welcome, {userName}! Click here to start (it won't, because it's still under construction - sorry)";
        welcomeText.text = welcomeMessage;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttemptLogin(userName.text);
        }
    }

}
