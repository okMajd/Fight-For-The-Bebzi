using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DropController : MonoBehaviour
{
    public List<GameObject> possibleItems = new List<GameObject>();
    public float beginningX, endX;
    public float y;
    public float minimumTime, maxTime;
    public TMP_Text timer;
    public GameObject placeHolderItem;
    public GameManager manager;
    float howMuchWaiting;

    private void Start()
    {
        StartCoroutine("drop");
    }

    private void Update()
    {
        timer.text = howMuchWaiting.ToString("0.0");

    }

    IEnumerator drop()
    {
        howMuchWaiting = Random.Range(minimumTime, maxTime);
        howMuchWaiting = Mathf.Round(howMuchWaiting * 10.0f) * 0.1f;
        float timer = howMuchWaiting;
        StartCoroutine("lowerTimer");
        yield return new WaitForSeconds(timer);
        GameObject dropped = Instantiate(placeHolderItem, new Vector2(Random.Range(beginningX, endX), y), Quaternion.identity);
        dropped.GetComponent<decideItem>().possibleItems = possibleItems;
        StopCoroutine("lowerTimer");
        StartCoroutine("drop");
    }

    IEnumerator lowerTimer()
    {
        yield return new WaitForSeconds(0.1f);
        howMuchWaiting -= 0.1f;
        StartCoroutine("lowerTimer");
    }
}
