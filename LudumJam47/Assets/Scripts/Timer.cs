using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float maxTime = 60f;
    [SerializeField] GameEvent timeUpEvent;
    [SerializeField] AudioClip tickClip;

    private float _timer;
    private int _prevSecondValue;
    private TextMesh _text;
    private bool _isTiming;
    private AudioSource _audioSource;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMesh>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // decrease time
        _timer -= Time.deltaTime;

        // get the rounded second value, if it's not the same as the previous frames value then update timer
        int curSecond = Mathf.FloorToInt(_timer % 60);
        if(curSecond != _prevSecondValue)
        {
            UpdateTimerText();
            //AudioExtensions.AudioHandling.PlayClipWithRandomPitch(_audioSource, tickClip, 0.8f, 1f);
            _audioSource.PlayOneShot(tickClip);
            _prevSecondValue = curSecond;
        }

        if (_timer <= 0f)
        {
            timeUpEvent.Raise();
            _isTiming = false;
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt(_timer % 60);
        _text.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
    }

    public void Reset()
    {
        _timer = maxTime;
        _isTiming = true;
        _prevSecondValue = -1;
    }
}
