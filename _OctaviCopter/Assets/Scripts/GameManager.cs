using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isNewUser;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Don't have more than one Game Manager...");
        }
        else
        {
            instance = this;
        }
    }


}
