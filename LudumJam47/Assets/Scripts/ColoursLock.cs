using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoursLock : MonoBehaviour
{
    [SerializeField] string buttonOrder = "GYRB";

    [Header("Buttons")]
    [SerializeField] ColouredButton redButton;
    [SerializeField] ColouredButton greenButton;
    [SerializeField] ColouredButton yellowButton;
    [SerializeField] ColouredButton blueButton;

    private string buttonPressedOrder;

    // Start is called before the first frame update
    void Start()
    {
        buttonPressedOrder = "";
    }

    void CheckOrder()
    {
        // correct, open lock 
        if (buttonPressedOrder == buttonOrder)
        {
            Unlock();
        }
        else
        {
            // play wrong sound effect
            print("WRONG");

            Reset();
        }
    }

    private void ButtonPressed()
    {
        // once the string is 4 long then check to see if correct
        if (buttonPressedOrder.Length == 4)
            CheckOrder();
    }

    public void RedButtonPressed()
    {
        buttonPressedOrder += "R";
        ButtonPressed();
    }
    public void GreenButtonPressed()
    {
        buttonPressedOrder += "G";
        ButtonPressed();
    }
    public void YellowButtonPressed()
    {
        buttonPressedOrder += "Y";
        ButtonPressed();
    }
    public void BlueButtonPressed()
    {
        buttonPressedOrder += "B";
        ButtonPressed();
    }

    public void Reset()
    {
        buttonPressedOrder = "";
        redButton.Reset();
        greenButton.Reset();
        yellowButton.Reset();
        blueButton.Reset();
    }

    void Unlock()
    {
        // play open animation


        // turn light green (?)

        // play correct audio clip
        print("UNLOCK");
    }
}
