using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class AudioManager : MonoBehaviour
{
    public float volumeToSet = 1;
    public Slider slider;
    public TMP_Text text;
    void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += recieveScene;
    }


    public void changeVolume()
    {
        if(slider != null)
        {
            volumeToSet = slider.value;
            text.text = $"{Mathf.Round(volumeToSet*100)}%";
        }
        AudioListener.volume = volumeToSet;
    }

    public void recieveScene(Scene scene, LoadSceneMode mode)
    {
        changeVolume();
    }

}
