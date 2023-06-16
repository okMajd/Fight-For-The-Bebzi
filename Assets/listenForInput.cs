using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class listenForInput : MonoBehaviour
{
    public GameObject listeningForInput;
    public string keyPressed;
    public keybinds keybinds;
    public string myKey;
    public bool listening;
    public UIManager ui;
    public void listen()
    {
        if(!listening)
        {
            listening = true;
            listeningForInput.SetActive(true);
            ui.canInteract = false;
        }

    }


    private void Update()
    {
        if(listening)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                listening = false;
                listeningForInput.SetActive(false);
                ui.canInteract = true;
                return;
            }
            if(Input.anyKeyDown && !Input.GetButtonDown("Fire1"))
            {
                keyPressed = Input.inputString;
                GetComponentInChildren<TMP_Text>().text = keyPressed;
                keybinds.recieve(myKey, keyPressed);
                listeningForInput.SetActive(false);
                listening = false;
                ui.canInteract = true;
            }
        }
    }
}
