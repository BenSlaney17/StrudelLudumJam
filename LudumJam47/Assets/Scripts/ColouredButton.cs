using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColouredButton : MonoBehaviour
{
    [SerializeField] float distToButton = 3f;
    [SerializeField] UnityEvent pressedEvent;

    private bool _beenPressed;
    private Animator _animator;

    private void Start()
    {
        _beenPressed = false;
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (_beenPressed) return;

        // TODO once player move in: check dist
       
        // raise event
        pressedEvent.Invoke();

        // play animation
        _animator.SetBool("IsButtonPressed", true);
       
        _beenPressed = true;
    }

    public void Reset()
    {
        _beenPressed = false;

        //play animation
        _animator.SetBool("IsButtonPressed", false);
    }
}
