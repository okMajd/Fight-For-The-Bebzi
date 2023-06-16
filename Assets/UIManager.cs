using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public bool canInteract;
    public bool settingsOpen = false;
    public bool gameMenuOpen = false;
    public List<GameObject> tabs = new List<GameObject>();
    public GameObject generalTab, keybindTab;
    public GameObject usernameInput;
    public TMP_InputField usernameInputText;
    public keybinds keybinds;
    public GameObject buttons;
    public GameObject gameMenu;
    public Animator anim;
    public AudioSource click, click2, close, scroll, scrollDown, scrollUp;
    public Slider slider;
    int sliderLastValue;

    private void Update()
    {
        sliderLastValue = (int)slider.value;
    }

    private void LateUpdate()
    {
        if(slider.value <= sliderLastValue)
            scroll = scrollDown;
        else{ scroll = scrollUp; } 
    }

    public void playClickSfx(int type = 1)
    {
        switch(type)
        {
            case 1:
                click.Play();
                break;
            case 2:
                click2.Play();
                break;

        }
    }
    public void playScrollSfx()
    {
        scroll.Play();
    }
    public void playCloseSfx()
    {
        close.Play();
    }
    public void play(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quit()
    {
        if(canInteract)
            Application.Quit();
    }

    public void openPlayMenu()
    {
        gameMenuOpen = !gameMenuOpen;
        // buttons.SetActive(!gameMenuOpen);
        animate(gameMenuOpen, "gamePanel");
    }
    public void toggleSettings()
    {
        if(canInteract)
        {
            settingsOpen = !settingsOpen;
            // buttons.SetActive(!settingsOpen);
            animate(settingsOpen, "settings");
        }
    }

    public void animate(bool watchThis, string animation)
    {
            if(watchThis)
                anim.Play($"{animation}Enter");
            if(!watchThis)
                anim.Play($"{animation}Exit");
    }

    public void openGeneralTab()
    {
        foreach (GameObject item in tabs)
        {
            item.SetActive(false);
        }
        generalTab.SetActive(true);

    }

    public void openKeybindTab()
    {
        foreach (GameObject item in tabs)
        {
            item.SetActive(false);
        }
        keybindTab.SetActive(true);

    }

    public void openUsernameInput(string num)
    {
        usernameInput.SetActive(true);
        latestNum = num;
    }

    string latestNum;

    public void setUsername()
    {
        switch(Int32.Parse(latestNum))
        {
            case 1:
                keybinds.username1 = usernameInputText.text;
                break;
            case 2:
                keybinds.username2 = usernameInputText.text;
                break;
        }
        usernameInputText.text = "";
        usernameInput.SetActive(false);
    }

}
