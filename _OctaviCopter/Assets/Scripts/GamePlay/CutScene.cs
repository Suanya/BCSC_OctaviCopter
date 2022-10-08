using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class CutScene : MonoBehaviour
{
    public InputActionReference skipCutSceneReference = null;
    [SerializeField] private TextMeshProUGUI skipMessage;

    private void Awake()
    {
        skipMessage.gameObject.SetActive(!GameManager.instance.isNewUser);
    }

    void Update()
    {
        float startValue = skipCutSceneReference.action.ReadValue<float>();
        if (!GameManager.instance.isNewUser && skipCutSceneReference && startValue > 0)
        {
            // switch from cut scene to game scene (currently assume skipping cut scene = also skipping tutorial for now)
            SceneController.OnSceneChangeRequired(SceneController.SceneAction.GamePlay);
        }
    }

    public void OnSceneEnded()
    {
        // switch from cut scene to tutorial
        SceneController.OnSceneChangeRequired(SceneController.SceneAction.Tutorial);
    }
}
