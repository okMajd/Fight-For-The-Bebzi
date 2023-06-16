using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keybinds : MonoBehaviour
{
    public KeyCode jump, left, right, weak, medium, strong, guard, ability, aim, pickup, jump2, left2, right2, weak2, medium2, strong2, guard2, ability2, aim2, pickup2;
    public string username1, username2;    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
    
    public void recieve(string recievedIndicator, string keyS)
    {
        keyS = keyS.ToUpper();
        if((Regex.IsMatch(keyS, @"^\d+$")))
        {
            keyS = $"Alpha{keyS}";
        }
        switch (keyS)
        {
            case ";":
                keyS = "Semicolon";
                break;
            case ".":
                keyS = "Period";
                break;
            case ",":
                keyS = "Comma";
                break;
            case "/":
                keyS = "Slash";
                break;
        }
        switch (recievedIndicator)
        {
            case "aim":
                aim = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "aim2":
                aim2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "jump":
                jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "left":
                left = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "right":
                right = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "weak":
                weak = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "medium":
                medium = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "strong":
                strong = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "guard":
                guard = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "ability":
                ability = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "pickup":
                pickup = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "jump2":
                jump2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "left2":
                left2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "right2":
                right2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "weak2":
                weak2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "medium2":
                medium2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "strong2":
                strong2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "guard2":
                guard2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "ability2":
                ability2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
            case "pickup2":
                pickup2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyS);
                break;
        }
    }
}
