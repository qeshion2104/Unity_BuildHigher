using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class Score: MonoBehaviour
{
    public static Score instance;
    private string[] writestring = new string[10];
    private string FILE_ROOT = "score.txt"; //在document/top.txt
    public Text number;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        setstart("0");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //檢查檔案是否存在
    //創造檔案
    //讀取檔案 並寫入UI
    //寫入檔案 

    private bool fileisexist()
    {
        if (System.IO.File.Exists(FILE_ROOT))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void createtxt()
    {
        if (!System.IO.File.Exists("score.txt"))
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo("score.txt");
                fi.Create();
                fi = null;
            }
            catch (Exception ex)
            {
                Debug.Log("create fail");
            }
        }
    }
    private void readtxt(string thistimenuber)
    {
        string[] top10 = new string[11];
        int count = 0;
        string line;
        using (System.IO.StreamReader fileread = new System.IO.StreamReader(FILE_ROOT))
        {
            try
            {
                //裝10個
                while ((line = fileread.ReadLine()) != null)
                {
                    if (count < 10)
                    {
                        top10[count] = line;
                        count++;
                    }
                }
                fileread.Close();
                for (int i = 0; i < 10; i++)
                {
                    if (top10[i] == null || top10[i]=="")
                    {
                        top10[i] = "0";
                    }
                }
                //裝地11個 這次的分數
                top10[10] = thistimenuber;
                //Debug.Log(top10[10]);
                //排大小取前10
                var sort = (from s in top10
                            orderby int.Parse(s) descending
                            select s).Take(10);
                count = 0;
                foreach (string c in sort)
                {
                    number.text += c + Environment.NewLine;
                    //裝10個最高的 用來寫入
                    try
                    {
                        writestring[count] = c;
                        count++;
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("count 超過");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("read fail");
                Debug.Log(ex.ToString());
            }
        }
    }
    private void writealltotxt(string[] i)
    {

        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(FILE_ROOT))
        {
            try
            {
                foreach (string line in i)
                {
                    file.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Debug.Log("write fail");
            }
        }
    }
    private void reset()
    {
        number.text = "";
    }
    public void setstart(string thisnumber)
    {
        reset();
        createtxt();
        //讀取顯示
        readtxt(thisnumber);
        //寫入更新
        writealltotxt(writestring);
    }

    public void btnBack()
    {
        //UIManager.Instance.TogglePanel("Panel_score", false);
        //UIManager.Instance.TogglePanel("Panel_main", true);
        //game reset
        new_box.instance.reset_game();
        //ui reset
        reset();
    }

    public void restartBtnOnClick()
    {
        //game reset
        new_box.instance.reset_game();
        //ui reset
        reset();
    }
}
