using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// this is just an example which can be delet afterwards but if you check this, you know what to do straight away;)
/// </summary>

public class Haptical : MonoBehaviour
{
    // Adding the SerializeField attribute to a field will make it appear in the Inspector window
    // where a developer can drag a reference to the controller that you want to send haptics to.
    [SerializeField] XRBaseController controller;

    void SendHaptics()
    {

        Debug.Log("haptical");
        if (controller != null)
            controller.SendHapticImpulse(0.7f, 0.1f);
        
    }
}