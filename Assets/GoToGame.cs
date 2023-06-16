using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    void OnEnable()
    {
        StartCoroutine(load(time));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(load());

    }

    // Update is called once per frame
    IEnumerator load(float fauxTime = 0)
    {
        yield return new WaitForSeconds(fauxTime);
        SceneManager.LoadScene("MainGame");
    }
}
