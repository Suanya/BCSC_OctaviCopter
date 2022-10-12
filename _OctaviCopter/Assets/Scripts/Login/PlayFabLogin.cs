using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    public void LogUserInToPlayFab(UserProgress currentUserState)
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "A8FF9";
        }
        
        var request = new LoginWithCustomIDRequest { CustomId = currentUserState.UserID.ToString(), CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("User is logged in to PlayFab!");
        GameManager.instance.playFabLoginSuccessful = true;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with loggin in the user.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }


    public void PlayerCompletedLevel(int currentLevel)
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest
        {
            EventName = "player_completed_level",
            Body = new Dictionary<string, object>()
            {
                { "level_finished", currentLevel }
            }
        }, null, null);
    }

}
