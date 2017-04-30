using UnityEngine;
using System.Collections;

public class Box_z : MonoBehaviour {
    //方塊基底x大小=1
    public float basic_x = 1;
    //創造留下來的方塊
    public GameObject cube_left;
    private GameObject cube_left_created;
    //方塊的移動
    public float box_speed;
    public bool box_FB = true;
    //創造掉下去的方塊
    public GameObject cube_drop;
    private GameObject cube_drop_created;


    //上一顆方塊的左右位置 x 左(0.01-0.5) 右(0.01+0.5)
    // Use this for initialization
    void Start()
    {
        box_speed = new_box.box_speed;
    }

    // Update is called once per frame


    //平常就左右移動-2 <-> 2 直到滑鼠按下左鍵點擊 才會固定

    void Update()
    {
        //按下滑鼠左鍵 執行消滅+切塊+生成
        if (new_box.gamestart)
        {
            //前後移動
            FBmove();
            if (Input.GetButtonDown("Fire1"))
            {
                Box_left();
                Panel_game.instance.changebuild_number();
            }
        }
    }

    public void FBmove()
    {
        if (gameObject.transform.position.z > 2)
        {
            this.box_FB = true; //反向 向 -2移動     
        }
        else if (gameObject.transform.position.z < -2)
        {
            this.box_FB = false; //正向 向2移動
        }

        if (this.box_FB) //true=反向
        {
            gameObject.transform.Translate(0, 0, -box_speed * Time.deltaTime);
        }
        else //false=正向
        {
            gameObject.transform.Translate(0, 0, box_speed * Time.deltaTime);
        }
    }
    //還需要更改
    public void Box_left()
    {
        Vector3 loaction = gameObject.transform.position;
        float last_center = new_box.instance.last_box.transform.position.z;
        float last_half_length = new_box.instance.last_box.transform.localScale.z / 2;
        float now_center = gameObject.transform.position.z;
        float now_half_length = gameObject.transform.localScale.z / 2;

        if (now_center < last_center + 0.1 && now_center > last_center - 0.1)
        {
            music.instance.sound_effect();
            Debug.Log("Good work");
            //連擊+1

            //改成new_box的
            new_box.good_count += 1;


            //產生留下的
            //新的center位置為 原先的
            loaction.z = last_center;
            //新的scale.x = 舊的scale
            float new_scale_z = gameObject.transform.localScale.z;
            new_box.count++;
            Destroy(gameObject);
            cube_left_created = Instantiate(cube_left);
            //除了X採用新比例 其他都照舊
            cube_left_created.transform.localScale =
                new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, new_scale_z);
            cube_left_created.transform.position = loaction;
            cube_left_created.name = "box" + new_box.count;

            //留下的改顏色
            cube_left_created.GetComponent<Renderer>().material.color =
                gameObject.GetComponent<Renderer>().material.color;
            //連擊達一定值 第四次 方塊變大 SCALE+ 0.02
            if (new_box.good_count >= 3)
            {
                while (cube_left_created.transform.localScale.z < (double)(new_scale_z + 0.02))
                {
                    cube_left_created.transform.localScale = new Vector3(
                        (float)(cube_left_created.transform.localScale.x + 0.01 * Time.deltaTime)
                        , cube_left_created.transform.localScale.y
                        , (float)(cube_left_created.transform.localScale.z + 0.01 * Time.deltaTime)
                        );
                }
                //產生連擊字幕
                Panel_game.instance.continue_more(new_box.good_count);
            }
            //生一個新的 
            loaction.y -= (float)0.25;
            new_box.instance.create_box();
            new_box.instance.create_light(loaction);
        }


            //小於中心點 且右端大於原左端點
        else if (now_center < last_center
            && (now_center + now_half_length) > (last_center - last_half_length)) //左區塊 取原先左邊+ 右極值
        {
            new_box.good_count = 0;
            //產生留下的
            //新的center位置為 (原先的左端+ 現有的右端)/2
            loaction.z = ((last_center - last_half_length) + (now_center + now_half_length)) / 2;
            //新的scale =(現有右端-原先左端)/基底全長
            float new_scale_z = (((now_center + now_half_length) - (last_center - last_half_length)))
                / basic_x;
            new_box.count++;
            Destroy(gameObject);
            cube_left_created = Instantiate(cube_left);
            cube_left_created.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, new_scale_z);
            cube_left_created.transform.position = loaction;
            cube_left_created.name = "box" + new_box.count;
            //留下的改顏色
            cube_left_created.GetComponent<Renderer>().material.color =
                gameObject.GetComponent<Renderer>().material.color;
            //產生掉落的
            //左邊 取現有左端+原先左端/2 = 中心
            // scale = 舊Scale - 新scale
            loaction.z = ((now_center - now_half_length) + (last_center - last_half_length)) / 2;
            float drop_scale_z = gameObject.transform.localScale.z - new_scale_z;
            cube_drop_created = Instantiate(cube_drop);
            cube_drop_created.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, drop_scale_z);
            cube_drop_created.transform.position = loaction;
            //落下的改顏色
            cube_drop_created.GetComponent<Renderer>().material.color =
                gameObject.GetComponent<Renderer>().material.color;
            Destroy(cube_drop_created, 3);

            //生一個新的
            new_box.instance.create_box();
            //Debug.Log("新的CENTER: " + loaction.z);
            //Debug.Log("原先的左端: " + (last_center - last_half_length));
            //Debug.Log("現有的右端: " + (now_center + now_half_length));
        }
        //描寫右區塊
        //大於中心點 且左端小於原右端點
        else if (now_center > last_center
            && (now_center - now_half_length) < (last_center + last_half_length)) //右區塊 取左極值 < 原先右邊)
        {
            new_box.good_count = 0;  
            //新的center位置為 (原先的右端+ 現有的左端)/2
            loaction.z = ((last_center + last_half_length) + (now_center - now_half_length)) / 2;
            //新的scale =(原先右端-現有左端)/基底全長
            float new_scale_z = (((last_center + last_half_length) - (now_center - now_half_length)))
                / basic_x;
            new_box.count++;
            Destroy(gameObject);
            cube_left_created = Instantiate(cube_left);
            cube_left_created.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, new_scale_z);
            cube_left_created.transform.position = loaction;
            cube_left_created.name = "box" + new_box.count;

            //留下的改顏色
            cube_left_created.GetComponent<Renderer>().material.color =
                gameObject.GetComponent<Renderer>().material.color;

            //產生掉落的
            //左邊 取現有右端+原先右端/2 = 中心
            // scale = 舊Scale - 新scale
            loaction.z = ((now_center + now_half_length) + (last_center + last_half_length)) / 2;
            float drop_scale_z = gameObject.transform.localScale.z - new_scale_z;
            cube_drop_created = Instantiate(cube_drop);
            cube_drop_created.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, drop_scale_z);
            cube_drop_created.transform.position = loaction;
            //落下的改顏色
            cube_drop_created.GetComponent<Renderer>().material.color =
                gameObject.GetComponent<Renderer>().material.color;
            Destroy(cube_drop_created, 3);//3秒後消失


            //生一個新的 
            new_box.instance.create_box();
        }
    }
}
