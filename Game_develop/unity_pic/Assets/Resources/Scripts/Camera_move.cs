using UnityEngine;
using System.Collections;

public class Camera_move : MonoBehaviour {

    private float y;
    public float speed;
    private double y_add_factor;
    public double y_add_value;
    private GameObject box;
	// Use this for initialization
	void Start () {
        y = gameObject.transform.position.y;
        box = GameObject.Find("box0");
        y_add_factor = box.transform.localScale.y;
        y_add_value = (new_box.count + 3.5) * y_add_factor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (y < y_add_value) //當y軸位置<count時 就會緩慢向上移動 才不會移動過快
        {
            gameObject.transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
            y = gameObject.transform.position.y;
            y_add_value = (new_box.count + 3.5) * y_add_factor;
        }
        if (y > y_add_value)
        {
            y_add_value = (new_box.count + 3.5) * y_add_factor;
        }   
    }
}
