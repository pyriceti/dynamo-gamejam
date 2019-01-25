using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class game_manager : MonoBehaviour {

    public RawImage raw_image;
    public Text text;
    public Image Win_image; 

    public bool GameOver;

    public bool Win; 

    private void Update()
    {
        if(GameOver)
        {
            raw_image.color = new Color(raw_image.color.r, raw_image.color.g, raw_image.color.b, raw_image.color.a +10 * Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 80 * Time.deltaTime);


            if(Input.GetButton("Submit"))
            {
                SceneManager.LoadScene(1); 
            }
        }
        if(Win)
        {
            Win_image.color = new Color(Win_image.color.r, Win_image.color.g, Win_image.color.b, Win_image.color.a + 5 * Time.deltaTime);
        }
    }
}
