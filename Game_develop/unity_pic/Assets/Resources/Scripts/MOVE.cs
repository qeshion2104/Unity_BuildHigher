using UnityEngine;
using System.Collections;

public class MOVE : MonoBehaviour {

    private Space sp = Space.Self;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            sp = Space.Self;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            sp = Space.World;
        }

        float distanceH = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float distanceV = speed * Time.deltaTime * Input.GetAxis("Vertical");
        gameObject.transform.Translate(Vector3.right * distanceH ,sp); //朝右移動 (1,0,0)
        //gameObject.transform.Translate(Vector3.up * distanceV , sp); //朝上移動 (0,1,0)
        gameObject.transform.Translate(Vector3.forward * distanceV, sp); //朝前移動(0,0,1)
        

    }
}
