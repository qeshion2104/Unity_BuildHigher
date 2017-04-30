using UnityEngine;
using System.Collections;

public class new_box : MonoBehaviour {

    public static new_box instance;
    public static float count;
    public static bool gamestart = false;
    public static float good_count = 0;
    public static float box_speed;

    public GameObject box_x;
    private GameObject created_Box_x;
    public GameObject box_z;
    private GameObject created_Box_z;
    public GameObject last_box;

    public GameObject shine_light;
    private GameObject created_light;

    private string color_change = "0";
    private int color_count = 2;
    public double color_change_factor = 0.05;
    // Use this for initialization
    void Awake()
    {
        new_box.box_speed = 3f;

    }

	void Start () {
        instance = this;
        count = 0;
        last_box = GameObject.Find("box" + count);

        Score.createtxt();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void create_box()
    {
        //在呼叫這個前 已經增加count
        //需改進部分
        /*
         * 1.產生新的物件大小必須符合剩下來的尺寸(x,z) ---FN   
         * 2.產生新德物件位置必須與剩下來的中心位置對其(x-x z-z)
         * 方法: 取得上一個物件的位置作為參照 將X Z改回對應值
         * 
         */
        last_box = GameObject.Find("box" + count); //box1
        if (last_box.transform.localScale.x > 0.2 && last_box.transform.localScale.z > 0.2)//當方塊X小於0.2 就輸了  如果是用3D的 則計算面積
        {
            //第1(3)個 位置就是 scale.y*1+3/2scale.y = 2.5 scale.y
            //地2(4)個 位置就是 scale.y*2+3/2scale.y = 3.5 scale.y
            //地n(n+2)個 位置就是 scale.y*n+3/2scale.y = n+1.5 scale.y
            double y_add_value = (count + 1.5) * last_box.transform.localScale.y;
            Debug.Log(y_add_value);
            if (count %  2 != 0)// 基數產Z
            {
                created_Box_x = Instantiate(box_z); //box2預備軍 要使用box1的scale
                created_Box_x.transform.localScale = last_box.transform.localScale;
                created_Box_x.transform.position = new Vector3(last_box.transform.position.x, (float)y_add_value, 2);
                change_color(created_Box_x);

            }
            else  // 基數產X
            {
                created_Box_x = Instantiate(box_x); //box2預備軍 要使用box1的scale
                created_Box_x.transform.localScale = last_box.transform.localScale;
                created_Box_x.transform.position = new Vector3(-2, (float)y_add_value, last_box.transform.position.z);
                change_color(created_Box_x);
            }
        }
        else
        {
            Debug.Log("YOU FAIL");  //顯示you fail 等圖像 並顯示分數 跟 重新開始
            Panel_game.instance.failed();
            gamestart = false;
        } 
    }

    public void create_light(Vector3 v)
    {
        created_light = Instantiate(shine_light);
        created_light.transform.position = v;
        Destroy(created_light, 1);
    }

    public void reset_game()
    {
        UIManager.Instance.CloseAllPanel();
        Application.LoadLevel("sence1");
        //CanvasRootHandle.instance.reset();
        gamestart = false;
        count = 0;
    }

    public void change_color(GameObject create_box)
    {
        // r->0  0
        // g->0  1
        // b->0  2
        // r->1  3
        // g->1  4
        // b->1  5
        // return
        switch (color_change)
        {
            case "0":
                create_box.GetComponent<Renderer>().material.color = new Color((float)(1- color_change_factor * color_count),1,1,0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.r < 0.05)
                {
                    color_change = "1";
                    color_count = 1;
                }
                break;
            case "1":
                create_box.GetComponent<Renderer>().material.color = new Color(0, (float)(1 - color_change_factor * color_count), 1, 0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.g < 0.1)
                {
                    color_change = "2";
                    color_count = 1;
                }
                break;
            case "2":
                create_box.GetComponent<Renderer>().material.color = new Color(0, 0, (float)(1 - color_change_factor * color_count), 0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.b < 0.1)
                {
                    color_change = "3";
                    color_count = 1;
                }
                break;
            case "3":
                create_box.GetComponent<Renderer>().material.color = new Color((float)(color_change_factor * color_count), 0, 0, 0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.r > 0.9)
                {
                    color_change = "4";
                    color_count = 1;
                }
                break;
            case "4":
                create_box.GetComponent<Renderer>().material.color = new Color(1, (float)(color_change_factor * color_count), 0, 0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.g > 0.9)
                {
                    color_change = "5";
                    color_count = 1;
                }
                break;
            case "5":
                create_box.GetComponent<Renderer>().material.color = new Color(1, 1, (float)(color_change_factor * color_count), 0);
                color_count += 1;
                if (create_box.GetComponent<Renderer>().material.color.b > 0.9)
                {
                    color_change = "0";
                    color_count = 1;
                }
                break;
        }
    }
}
