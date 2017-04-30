using UnityEngine;
using System.Collections;

public class BoxSwell : MonoBehaviour {

    public float cc;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        cc = transform.localScale.x;
        if (transform.localScale.x < 2.1)
        {
            gameObject.transform.localScale = new Vector3((float)(transform.localScale.x + 0.1*Time.deltaTime), transform.localScale.y, (float)(transform.localScale.z + 0.1*Time.deltaTime));
        }
    }
}
