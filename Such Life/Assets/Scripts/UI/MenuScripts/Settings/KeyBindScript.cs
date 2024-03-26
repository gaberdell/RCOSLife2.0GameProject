using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


/*
 * Unnecessary for basic menu 
 * Will not use for playable build at the moment
 */

public class KeyBindScript : MonoBehaviour
{
    // syntax of key must match exactly what each text in the texts array below is
    // order does not necessarily matter
    private Dictionary<string, KeyCode> defaultKeyBinds = new Dictionary<string, KeyCode> {
        {"Toggle Menu", KeyCode.Escape},
        {"Move Up", KeyCode.W},
        {"Move Left", KeyCode.A},
        {"Move Down", KeyCode.S},
        {"Move Right", KeyCode.D},
        {"Primary", KeyCode.Mouse0},
        {"Skill 1", KeyCode.R},
        {"Skill 2", KeyCode.T},
        {"Hotbar 1", KeyCode.Alpha1},
        {"Hotbar 2", KeyCode.Alpha2},
        {"Hotbar 3", KeyCode.Alpha3},
        {"Hotbar 4", KeyCode.Alpha4},
        {"Hotbar 5", KeyCode.Alpha5},
        {"Hotbar 6", KeyCode.Alpha6},
        {"Hotbar 7", KeyCode.Alpha7},
        {"Hotbar 8", KeyCode.Alpha8},
        {"Hotbar 9", KeyCode.Alpha9},
        {"Hotbar 10", KeyCode.Alpha0},
        {"Toggle Inventory", KeyCode.Tab}
    };

    public TextMeshProUGUI[] texts;
    public Button[] buttons;

    private bool waitingForInput = false;
    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        if (texts.Length == buttons.Length)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                TextMeshProUGUI text = texts[i];
                Button button = buttons[i];

                // Grab the Text component of the button
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                // Update the text of the Text component
                buttonText.text = PlayerPrefs.GetString(text.text, defaultKeyBinds[text.text].ToString());
            }
        }
        else
        {
            Debug.Log("length error in keybinding script");
        }
    }

    private List<KeyCode> inputs = new List<KeyCode> ();
    private bool finishInput = false;
    void Update()
    {
        if (waitingForInput)
        {  
            if (Input.anyKeyDown)
            {   
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    // get the player's input 
                    if (Input.GetKeyDown(keyCode) && !inputs.Contains(keyCode))
                    {
                        inputs.Add(keyCode);
                        if (keyCode != KeyCode.RightShift && keyCode != KeyCode.LeftShift &&
                            keyCode != KeyCode.RightControl && keyCode != KeyCode.LeftControl &&
                            keyCode != KeyCode.RightAlt && keyCode != KeyCode.LeftAlt &&
                            keyCode != KeyCode.RightCommand && keyCode != KeyCode.LeftCommand ||
                            inputs.Count >= 3)
                            {
                                finishInput = true;
                            }
                    }
                }

                if (finishInput) 
                {
                    // grab the corresponding texts and button
                    TextMeshProUGUI text = texts[index];
                    Button button = buttons[index];
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

                    string input = "";
                    for (int i = 0; i < inputs.Count; i++) {
                        input += inputs[i].ToString();
                        if (i != inputs.Count - 1) {
                            input += " + ";
                        }
                    }
                    buttonText.text = input;
                    PlayerPrefs.SetString(text.text, input);
                    waitingForInput = false;
                    finishInput = false;
                    inputs.Clear();

                    // TODO: add code to actually change the input values
                    // i.e if movement key bind are changed it should change in-game
                }
            }
        }
    }

    public void toggle_input(int i)
    {
        waitingForInput = true;
        index = i;
    }

    public void restore_default_settings()
    {
        if (texts.Length == buttons.Length)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                TextMeshProUGUI text = texts[i];
                Button button = buttons[i];
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                
                string keyBind = defaultKeyBinds[text.text].ToString();
                buttonText.text = keyBind;
                PlayerPrefs.SetString(text.text, keyBind);

                // TODOL add code to actually change the input values
                // i.e if movement key bind are changed it should change in-game
            }
        }
    }

}
