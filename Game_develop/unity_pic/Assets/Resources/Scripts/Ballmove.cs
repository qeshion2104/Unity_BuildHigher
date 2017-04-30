using UnityEngine;
using System.Collections;

public class Ballmove : MonoBehaviour {
    public float rspeed = 60;
    public float mspeed = 5;
    private Space sp = Space.World;
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
        //前進
        float rotationforward = Input.GetAxis("Vertical") * rspeed;
        transform.Rotate(rotationforward * Time.deltaTime, 0, 0,sp);
        float goahead = Input.GetAxis("Vertical") * mspeed;
        transform.Translate(Vector3.forward * goahead * Time.deltaTime, sp);
        //左右
        float rotationright = Input.GetAxis("Horizontal") * rspeed;
        transform.Rotate(0, 0, rotationright * Time.deltaTime *-1, sp);
        float goright = Input.GetAxis("Horizontal") * mspeed;
        transform.Translate(Vector3.right * goright * Time.deltaTime, sp);
    }
}
