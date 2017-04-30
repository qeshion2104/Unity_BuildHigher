using UnityEngine;
using System.Collections;

public class color_test : MonoBehaviour {
    private double i = 0;
    private double change = 0;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Renderer>().material.color.r > 0.99)
        {
            change = 0;
            i = 0.05;
        }
        else if(gameObject.GetComponent<Renderer>().material.color.r <0.01)
        {
            change = 1;
            i= 0.05;
        }
        if (change == 0)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color((float)(1 - i), 1, 1, 0);
            i += 0.2*Time.deltaTime;
        }
        else if (change == 1)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color((float)(0 + i), 1, 1, 0);
            i += 0.2*Time.deltaTime;
        }
    }
}
