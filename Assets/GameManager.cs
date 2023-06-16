using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public GameObject bg;
    public string gameType;
    public List<Player1> playerList = new List<Player1>();

    public List<GameObject> grassMapItems, desertMapItems, snowMapItems = new();
    MapManager mapMang;


    private void Start()
    {
        mapMang = GameObject.Find("MapManager").GetComponent<MapManager>();
        winText.gameObject.SetActive(false);
        bg.SetActive(false);
        for (int i = 0; i < playerList.Count; i++)
        {
            if(i+1 >= playerList.Count)
            {
                playerList[i].otherPlayer = playerList[i-1].gameObject;
            }
            else{ playerList[i].otherPlayer = playerList[i+1].gameObject; }
        }
        switch (mapMang.selectedMap)
        {
            case "grass":
                foreach (GameObject item in grassMapItems)
                {
                    item.SetActive(true);
                }
                break;
            case "desert":
                foreach (GameObject item in desertMapItems)
                {
                    item.SetActive(true);
                }
                break;
            case "snow":
                foreach (GameObject item in snowMapItems)
                {
                    item.SetActive(true);
                }
                break;
            
        }

    }

    private void Update()
    {
        if(gameType == "main")
        {
            foreach (Player1 player in playerList)
            {
                if(player.lives < 1)
                {
                    winText.gameObject.SetActive(true);
                    bg.SetActive(true);
                    winText.text = $"{player.username.text} WINS!";
                    Destroy(player.keybinds.gameObject);
                    StartCoroutine("leave");
                }
            }
        }
    } 

    IEnumerator leave()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}
