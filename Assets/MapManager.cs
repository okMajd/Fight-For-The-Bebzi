using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MapManager : MonoBehaviour
{
    public string selectedMap = "green";
    public RawImage mapPreview;
    public TextMeshProUGUI mapNameText;
    public Texture grass, snow, desert;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setMap(string map)
    {
        switch (map)
        {
            case "grass":
                mapPreview.texture = grass;
                break;
            case "desert":
                mapPreview.texture = desert;
                break;
            case "snow":
                mapPreview.texture = snow;
                break;
        }
        selectedMap = map;
        char.ToUpper(map[0]);
        mapNameText.text = $"{map} Map";
    }
}
