using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class game_manager : MonoBehaviour {

    public RawImage raw_image;
    public Text text; 

    public bool GameOver;

    private void Update()
    {
        if(GameOver)
        {
            raw_image.color = new Color(raw_image.color.r, raw_image.color.g, raw_image.color.b, raw_image.color.a +10 * Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 80 * Time.deltaTime);
        }
    }
}
