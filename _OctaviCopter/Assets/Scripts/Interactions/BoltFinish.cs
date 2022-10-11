using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoltFinish : MonoBehaviour
{
    public UnityAction boltSceneFinished = null;

    [SerializeField] AudioSource audioSource;

    public void Update()
    {
        if (!audioSource.isPlaying)
        {
            BoltEnded();
        }
    }

    public void BoltEnded()
    {
        boltSceneFinished?.Invoke();
    }
}
