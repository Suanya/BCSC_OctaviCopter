using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cheat : MonoBehaviour
{
    public InputActionReference sceneChangeReference = null;
    private int secondsBetweenChanges = 5;
    // Cheats used to advance scenes for testing and demonstration
    private void Update()
    {
        float upValue = sceneChangeReference.action.ReadValue<float>();
        if (upValue > 0)
        {
            StartCoroutine(AdvanceScene());
        }
            

    }

    private IEnumerator AdvanceScene()
    {
        Debug.Log($"Change {SceneController.instance.currentSceneAction} ");
        switch (SceneController.instance.currentSceneAction)
        {
            case SceneController.SceneAction.Login:
                // don't advance - this needs to happen
                Debug.Log("to nothing");
                break;
            case SceneController.SceneAction.CutScene:
                SceneController.OnSceneChangeRequired(SceneController.SceneAction.Tutorial);
                Debug.Log("to Tutorial");
                break;
            case SceneController.SceneAction.Tutorial:
                SceneController.OnSceneChangeRequired(SceneController.SceneAction.GamePlay);
                Debug.Log("to GamePlay");
                break;
            case SceneController.SceneAction.GamePlay:
                SceneController.OnSceneChangeRequired(SceneController.SceneAction.BoltScene);
                Debug.Log("to BoltScene");
                break;
            case SceneController.SceneAction.BoltScene:
                SceneController.OnSceneChangeRequired(SceneController.SceneAction.CutScene);
                Debug.Log("to CutScene");
                break;
        }

        yield return new WaitForSeconds(secondsBetweenChanges);
    }


}
