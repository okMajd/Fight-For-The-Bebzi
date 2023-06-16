using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class tutorial : MonoBehaviour
{
    public List<GameObject> tutorialTexts = new List<GameObject>();
    public List<KeyCode> keybindsToPress = new List<KeyCode>();
    public AudioSource goodJobSfx;
    public GameObject goodJobText;
    public float goodJobDelay = 1; 

    public int stage = 1;
    public int tutIndex;
    public int keyIndex;

    private void Start()
    {
        tutorialTexts[0].SetActive(true);
    }
    private void Update()
    {
        if(tutIndex >= tutorialTexts.Count)
            StartCoroutine(end());
        if(tutIndex > keybindsToPress.Count)
        {
            stage = 2;
            keyIndex = 0;
        }
        if(Input.GetKeyDown(keybindsToPress[keyIndex]))
        {
            if(stage == 1)
                StartCoroutine(goodJob());
            if(stage == 2)
            {
                tutorialTexts[tutIndex].SetActive(false);
                tutIndex++;
                tutorialTexts[tutIndex].SetActive(true);   
            }
        }
    }

    public IEnumerator end()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menus");
    }

    public IEnumerator goodJob()
    {
        goodJobSfx.Play();
        tutorialTexts[tutIndex].SetActive(false);
        goodJobText.SetActive(true);
        yield return new WaitForSeconds(goodJobDelay);
        goodJobText.SetActive(false);
        tutIndex++;
        keyIndex = tutIndex;
        tutorialTexts[tutIndex].SetActive(true);
    }
}
