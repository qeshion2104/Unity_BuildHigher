using UnityEngine;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    
    private string UI_GAMEPANEL_ROOT = "Prefabs/Panel/";
    //存取canvas 所有UI必再Canvas下
    public GameObject m_CanvasRoot;
    //存取 有開啟的ui介面  會存進m_panelList 用key可得物件
    public Dictionary<string, GameObject> m_PanelList = new Dictionary<string, GameObject>();


    //判斷Canvas在不再
    private bool CheckCanvasRootIsNull()
    {
        if (m_CanvasRoot == null)
        {
            Debug.LogError("m_CanvasRoot is Null, Please in your Canvas add UIRootHandler.cs");
            return true;
        }
        else
        {
            return false;
        }
    }
    //判斷Panel是否已開啟
    private bool IsPanelLive(string name)
    {
        return m_PanelList.ContainsKey(name);
    }
    //打開Panel
    public GameObject ShowPanel(string name)
    {
        //canvas不存在　失敗
        if (CheckCanvasRootIsNull())
            return null;
        //panel已存在　失敗
        if (IsPanelLive(name))
        {
            Debug.LogErrorFormat("[{0}] is Showing, if you want to show, please close first!!", name);
            return null;
        }
        //讀取資料夾中的panel　如找不到　回傳null
        GameObject loadGo = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>(UI_GAMEPANEL_ROOT + name);
        if (loadGo == null)
        {
            Debug.LogErrorFormat( "[{0}] is missing, if you want to show, please set the pefabs first!!", name);
            return null;
        }
        // 在父物件下建立子物件(使用名稱建立一個新物件)
        // InstantiateGameObject(GameObject parent, GameObject prefab)
        GameObject panel = Utility.GameObjectRelate.InstantiateGameObject(m_CanvasRoot, loadGo);
        panel.name = name;


        m_PanelList.Add(name, panel);


        return panel;
    }
    //操縱panel顯示消失　　直接destory太浪費效率
    public void TogglePanel(string name, bool isOn)
    {
        if (IsPanelLive(name))
        {
            if (m_PanelList[name] != null)
                m_PanelList[name].SetActive(isOn);
        }
        else
        {
            Debug.LogErrorFormat("TogglePanel [{0}] not found.", name);
        }
    }
    //消滅panel物件節省資源
    public void ClosePanel(string name)
    {
        if (IsPanelLive(name))
        {
            if (m_PanelList[name] != null)
                Object.Destroy(m_PanelList[name]);

            m_PanelList.Remove(name);
        }
        else
        {
            Debug.LogErrorFormat("ClosePanel [{0}] not found.", name);
        }
    }

    public void CloseAllPanel()
    {
        foreach (KeyValuePair<string, GameObject> item in m_PanelList)
        {
            if (item.Value != null)
                Object.Destroy(item.Value);
        }

        m_PanelList.Clear();
    }
    //得到大小
    public Vector2 GetCanvasSize()
    {
        if (CheckCanvasRootIsNull())
            return Vector2.one * -1;

        RectTransform trans = m_CanvasRoot.transform as RectTransform;

        return trans.sizeDelta;
    }

}
