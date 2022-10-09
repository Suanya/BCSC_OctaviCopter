using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private MeshRenderer meshRenderer;

    private User currentUser;
    private string welcomeMessage;
    private string tempUserName;

    private bool loginAchieved = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastUser"))
        {
            Debug.Log("LastUser exists in PlayerPrefs");
            debugText.text = "LastUser exists in PlayerPrefs";
            // present the welcome back message (pre-load the PlayerPrefs userName)
            nameInput.gameObject.SetActive(false);
            welcomeText.gameObject.SetActive(true);
            tempUserName = PlayerPrefs.GetString("LastUser");
            AttemptLogin();
        }
        else
        {
            Debug.Log("LastUser does NOT exist in PlayerPrefs");
            debugText.text = "LastUser does NOT exist in PlayerPrefs";
            // present the input for the player to log in
            nameInput.gameObject.SetActive(true);
            welcomeText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // check for reset of login (have logged in but name field is active, meaning player touched BACK)
        if (loginAchieved && nameInput.gameObject.activeSelf)
        {
            Debug.Log("Login bool reset");
            debugText.text = "Login bool reset";
            loginAchieved = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.enabled = true;

            if (loginAchieved)
            {
                Debug.Log($"Login achieved: Goto cutscene start method");
                //StartCutScene();
                
            }
            else
            {
                // make sure the user entered something
                if (nameInput == null) return;

                // Attempt to log in the current user (either pre-loaded or entered)
                tempUserName = nameInput.text;
                Debug.Log($"Attempting login of {tempUserName}");
                AttemptLogin();

            }
   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshRenderer.enabled = false;
        }
            
    }

    public void AttemptLogin()
    {
        
        // check to see if name is in the database
        currentUser = UserDatabase.GetUser(tempUserName);

        if (currentUser == null)            // user is new
        {
            string cumulativeText = "User was initially null";
            debugText.text = cumulativeText;
            // tell the Game Manager (so the cut scene will not be skippable)
            GameManager.instance.isNewUser = true;

            // Add the user
            currentUser = UserDatabase.AddNewUser(tempUserName);

            if(currentUser == null)
            {
                cumulativeText += "; user is still null";
                
            }
            else
            {
                cumulativeText += "; user is now found";
                //Welcome with option to click to re-enter name (in case they misspelled and shouldn't be new)
                welcomeMessage = $"Welcome, {currentUser.UserName}! Touch BACK to change name if this is not your first time, or touch START to begin";
            }
            debugText.text = cumulativeText;
        }
        else
        {
            debugText.text = "User already in database";
            // tell the Game Manager (so the cut scene will be skippable)
            GameManager.instance.isNewUser = false;

            //Just Plain Welcome 
            welcomeMessage = $"Welcome, {currentUser.UserName}! Touch BACK to change name if this is not you, or touch START to begin";
        }

        loginAchieved = true;

        // Add user as last user
        PlayerPrefs.SetString("LastUser", currentUser.UserName);

        // present the welcome message
        nameInput.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(true);

        welcomeText.text = welcomeMessage;

    }

    public void StartCutScene()
    {
        Debug.Log("This is where the cut scene will start playing");
        debugText.text = "This is where the cut scene will start playing";
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.CutScene);
    }

}
