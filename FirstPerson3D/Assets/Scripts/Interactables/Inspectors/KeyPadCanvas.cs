using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class KeyPadCanvas : MonoBehaviour
{
    public GameObject keypadUI;
    public GameObject displayField1;
    public GameObject displayField2;
    public GameObject displayField3;

    private string password = "";
    private int maxDigits;
    private string input = "";


    public void Activate(){
        //open Keypad and freeze Game
        GameManager.gameManager.SwitchCameras("2D");
        keypadUI.SetActive(true);
        Time.timeScale = 0f;
        //Debug.Log("Keypad activated");
    }

    public void Close(){
        //Close Keypad, unfreeze Game
        GameManager.gameManager.SwitchCameras("3D");
        StopCoroutine(Reference.instance.currentKeypad.GetComponent<KeyPadInspector>().CheckPassword());

        keypadUI.SetActive(false);
        Time.timeScale = 1f;
        //Close Canvas
        gameObject.SetActive(false);
        //reset Input and Display
        input = "";
        displayField1.GetComponent<TextMeshProUGUI>().text = "";
        displayField2.GetComponent<TextMeshProUGUI>().text = "";
        displayField3.GetComponent<TextMeshProUGUI>().text = "";
        //Debug.Log("Keypad deactivated");
    }


    public void InputFromButton(Button btn)
    {
        //get password and passwordlength
        if(Reference.instance.currentKeypad.password != "0")
        {
            password = Reference.instance.currentKeypad.password;
            //Debug.Log("Password: " + password);
            maxDigits = password.Length;
        }
        // in case there is no password:
        else
        {
            password = null;
            maxDigits = 3;
        }
        
        
        //Debug.Log("Pressed Button " + btn.name);

        //check input
        if (btn.name == "C"){

            CButton();
        }
        //Only check Password when OK was pressed
        else if(btn.name == "OK")
        {
            //if there was no password: Open Smash-Canvas, reset everything and wait for input
            //Debug.Log("Passwort: " + password);
            if(password == null)
            {
                CButton();
                Reference.instance.smashDoorCanvas.Activate();
            }
            else if (input == password){

                CorrectPassword();
            }
            else{
                //check if the password was entered correctly before
                // -> not needed anymore bc kepad only activates when pw is not correct yet?
                if (!Reference.instance.currentKeypad.passwordCorrect)
                {
                    WrongPassword();
                }
            }
        }
        // If the button was neither C nor OK:
        else
        {
            //check if the max number if digits has already been put in
            if (input.Length <= maxDigits)
            {
                input += btn.name;

                switch (input.Length)
                {
                    case 1:
                        displayField1.GetComponent<TextMeshProUGUI>().text = input;
                        //clear the other display fields
                        displayField2.GetComponent<TextMeshProUGUI>().text = "";
                        displayField3.GetComponent<TextMeshProUGUI>().text = "";
                        break;
                    case 2:
                        displayField2.GetComponent<TextMeshProUGUI>().text = input.Substring(input.Length - 1);
                        break;
                    case 3:
                        displayField3.GetComponent<TextMeshProUGUI>().text = input.Substring(input.Length - 1);
                        break;
                }
            }
        }
    }

    //Clear input and input field
    public void CButton()
    {
        input = "";
        displayField1.GetComponent<TextMeshProUGUI>().text = "";
        displayField2.GetComponent<TextMeshProUGUI>().text = "";
        displayField3.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void CorrectPassword()
    {
        Debug.Log("Correct Password!");

        displayField1.GetComponent<TextMeshProUGUI>().text = "O";
        displayField2.GetComponent<TextMeshProUGUI>().text = "K";
        displayField3.GetComponent<TextMeshProUGUI>().text = "!";
        input = "";
        //Reference.instance.currentKeypad.door.doorUnlocked = true;
        Reference.instance.currentKeypad.passwordCorrect = true;
    }

    public void WrongPassword()
    {
        Debug.Log("Wrong Password! Access Denied.");

        displayField1.GetComponent<TextMeshProUGUI>().text = "N";
        displayField2.GetComponent<TextMeshProUGUI>().text = "O";
        displayField3.GetComponent<TextMeshProUGUI>().text = "!";
        input = "";
    }

    public void SmashedKeyPad()
    {
        displayField1.GetComponent<TextMeshProUGUI>().text = "X";
        displayField2.GetComponent<TextMeshProUGUI>().text = "X";
        displayField3.GetComponent<TextMeshProUGUI>().text = "X";
        input = "";
        //Reference.instance.currentKeypad.door.doorUnlocked = true;
        Reference.instance.currentKeypad.passwordCorrect = true;
    }
}
