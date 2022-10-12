using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private MeshRenderer meshRenderer;

    private User currentUser;
    private string welcomeMessage;
    private string tempUserName;

    private bool loginAchieved = false;

    [SerializeField] private TMP_Text debugText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastUser"))
        {
            // present the welcome back message (pre-load the PlayerPrefs userName)
            nameInput.gameObject.SetActive(false);
            welcomeText.gameObject.SetActive(true);
            tempUserName = PlayerPrefs.GetString("LastUser");
            AttemptLogin();
        }
        else
        {
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
                StartCutScene();
                
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
  
            // tell the Game Manager (so the cut scene will not be skippable)
            GameManager.instance.isNewUser = true;

            // Add the user
            UserDatabase.AddNewUser(tempUserName);

            // check again to see if name is in the database now
            currentUser = UserDatabase.GetUser(tempUserName);

            if(currentUser == null)
            {
                //Welcome with option to click to re-enter name (in case they misspelled and shouldn't be new)
                welcomeMessage = $"Sorry, you can't be added now (database error).  Your progress will not be saved. Touch START to begin";
            }
            else
            {
                //Welcome with option to click to re-enter name (in case they misspelled and shouldn't be new)
                welcomeMessage = $"Welcome, {currentUser.UserName}! Touch BACK to change name if this is not your first time, or touch START to begin";
            }
            
        }
        else
        {

            // tell the Game Manager (so the cut scene will be skippable)
            GameManager.instance.isNewUser = false;

            //Just Plain Welcome 
            welcomeMessage = $"Welcome, {currentUser.UserName}! Touch BACK to change name if this is not you, or touch START to begin";
        }

        loginAchieved = true;

        // Add user as last user
        PlayerPrefs.SetString("LastUser", currentUser.UserName);

        // Tell the game manager the UserID
        GameManager.instance.userName = currentUser.UserName;

        // present the welcome message
        nameInput.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(true);

        welcomeText.text = welcomeMessage;

    }

    public void StartCutScene()
    {
        Debug.Log("This is where the cut scene will start playing");
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.CutScene);
    }

}
