using UnityEngine;
using System.Collections;

public class Panel_main : MonoBehaviour {

    public void startBtnOnClick()
    {
        //game reset
        //new_box.instance.reset_game();
        //Panel_game.instance.reset();
        //close menu
        UIManager.Instance.TogglePanel("Panel_main", false);
        //set game start
        new_box.gamestart = true;
        //open game
        UIManager.Instance.TogglePanel("Panel_game", true);
    }
    public void TopBtnOnClick()
    {
        UIManager.Instance.TogglePanel("Panel_main", false);
        if (UIManager.Instance.ShowPanel("Panel_score") != null)
        {
            //不=null的話就會開啟了
            // =null 就代表已開啟 或是其他狀況
        }
        else {
            UIManager.Instance.TogglePanel("Panel_score", true);
        }
    }
    public void ExitBtnOnClick()
    {
        Application.Quit();
    }

    public void ConfigBtnOnClick()
    {
        UIManager.Instance.TogglePanel("Panel_main", false);
        if (UIManager.Instance.ShowPanel("Panel_config") != null)
        {
            //不=null的話就會開啟了
            // =null 就代表已開啟 或是其他狀況
        }
        else
        {
            UIManager.Instance.TogglePanel("Panel_config", true);
        }
    }
}
