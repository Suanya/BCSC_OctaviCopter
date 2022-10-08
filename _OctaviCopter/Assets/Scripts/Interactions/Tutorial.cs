using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    public InputActionReference exitTutorialReference = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float startValue = exitTutorialReference.action.ReadValue<float>();
        if (!GameManager.instance.isNewUser && exitTutorialReference && startValue > 0)
        {
            // switch from cut scene to game scene (currently assume skipping cut scene = also skipping tutorial for now)
            OnTutorialFinished();
        }
    }

    public void OnTutorialFinished()
    {
        // switch from cut scene to tutorial
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.GamePlay);
    }
}
