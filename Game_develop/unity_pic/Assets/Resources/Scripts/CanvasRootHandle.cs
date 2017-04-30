using UnityEngine;
using System.Collections;

public class CanvasRootHandle : MonoBehaviour {

    public static CanvasRootHandle instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
        reset();
    }
    public void reset()
    {
        UIManager.Instance.m_CanvasRoot = gameObject;
        UIManager.Instance.ShowPanel("Panel_main");
        UIManager.Instance.ShowPanel("Panel_game");
        UIManager.Instance.TogglePanel("Panel_game", false);
    }

}
