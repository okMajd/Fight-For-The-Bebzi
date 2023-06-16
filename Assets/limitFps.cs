using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class limitFps : MonoBehaviour
{
    public int max;
    public Slider slider;
    public TMP_Text fpsText;
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = max;
    }

    public void setFps()
    {
        int setTo = (int)slider.value;
        fpsText.text = setTo.ToString();
        Application.targetFrameRate = setTo;
    }
}
