using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OctaviCopterVertical : MonoBehaviour
{
    public InputActionReference moveUpReference = null;
    public InputActionReference moveDownReference = null;
    public float speed = 1f;

    private Transform thisTransform = null;

    // Start is called before the first frame update
    void Awake()
    {
        thisTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        float upValue = moveUpReference.action.ReadValue<float>();
        MoveOctaviCopterUp(upValue);

        float downValue = moveDownReference.action.ReadValue<float>();
        MoveOctaviCopterDown(downValue);
    }

    private void MoveOctaviCopterUp(float value)
    {
        thisTransform.Translate(0, value * speed * Time.deltaTime, 0);
    }

    private void MoveOctaviCopterDown(float value)
    {
        thisTransform.Translate(0, value * -1 * speed * Time.deltaTime, 0);
    }

}
