using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Panel_game : MonoBehaviour {

    public static Panel_game instance;

    public Text more;
    public Text build_number;
    void Start()
    {
        instance = this;
        reset();
    }


    public void changebuild_number()
    {
        float i = new_box.count;
        build_number.text = i.ToString();
    }
    public void failed()
    {
        if (UIManager.Instance.ShowPanel("Panel_score") == null)
        {
            UIManager.Instance.TogglePanel("Panel_score", true);
            Score.instance.setstart(new_box.count.ToString());
        }
        else
        {
            Score.instance.setstart(new_box.count.ToString());
        }
        //UIManager.Instance.TogglePanel("Panel_game", false);
    }
    public void restartBtnOnClick()
    {
        //game reset
        new_box.instance.reset_game();
        //ui reset
        reset();
    }
    public void MenuBtnOnClick()
    {
        //game reset
        new_box.instance.reset_game();
        //ui reset
        reset();
    }
    public void reset()
    {
        more.gameObject.SetActive(false);
        build_number.text = new_box.count.ToString();
    }
    public void continue_more(float i)
    {
        more.gameObject.SetActive(true);
        // >=3 初始50+90 15次後 500 差450
        if (more.fontSize < 500)
        {
            more.fontSize = (int)(50 + 30 * i);
        }
        Invoke("more_disappear", 2);
    }
    private void more_disappear()
    {
        more.gameObject.SetActive(false);
    }
}
