using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Config : MonoBehaviour {

    public Scrollbar game_speed_scrollbar;
    public Text game_speed_text;
    private int speed;
    private int old_speed;

    public Scrollbar music_scrollbar;
    public Text music_text;
    private int old_music_volume;
    private int music_volume;
    // Use this for initialization
    void Start () {
        speed = (int)(new_box.box_speed);
        old_speed = speed;
        game_speed_scrollbar.value = ((float)speed /10);
        game_speed_text.text = speed.ToString();

        music_scrollbar.value = 1;
        music_text.text = "100";
        
    }

    // Update is called once per frame
    void Update() {
        speed = (int)(game_speed_scrollbar.value * 10);
        if (speed != old_speed)
        {
            if (speed >= 1)
            {
                new_box.box_speed = speed;
                old_speed = speed;
                game_speed_text.text = speed.ToString();
            }
            else
            {
                new_box.box_speed = 1;
                old_speed = 1;
                game_speed_text.text = "1";
            }
        }
        music_volume = (int)(music_scrollbar.value * 100);
        if (music_volume != old_music_volume)
        {
            music_text.text = music_volume.ToString();
            old_music_volume = music_volume;
            music.instance.source.volume = music_scrollbar.value;
        }

	}

    public void Backbtn()
    {
        UIManager.Instance.TogglePanel("Panel_config", false);
        UIManager.Instance.TogglePanel("Panel_main", true);
    }
}
