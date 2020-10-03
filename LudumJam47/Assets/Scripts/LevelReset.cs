using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReset : MonoBehaviour
{
    [SerializeField] GameEvent levelResetEvent;

    public Transform[] pickupableObjects;

    private Transform[] _startingTransforms;

    private void Awake()
    {
        // get all starting transforms of pickup-able objects?
    }

    public void Reset()
    {
        // reset pickupable objects
        ResetPickupableTransforms();

        // reset timer

        // reset player

        levelResetEvent.Raise();
    }

    void ResetPickupableTransforms()
    {
        for (int i = 0; i < pickupableObjects.Length; i++)
        {
            pickupableObjects[i] = _startingTransforms[i]; 
        }
    }
}
