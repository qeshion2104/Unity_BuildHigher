using UnityEngine;
using System.Collections;

public class music : MonoBehaviour {

    public static music instance;
    public AudioSource source;
    public AudioClip Bgm;
    public AudioClip sound_effect1;
    public AudioClip sound_effect2;
	// Use this for initialization
	void Start () {
        instance = this;
        source = gameObject.GetComponent<AudioSource>();
        source.clip = Bgm;
        source.loop = true;
        source.Play();
	}
	
	// Update is called once per frame
    public void sound_effect()
    {
        float volume = source.volume;
        //source.PlayOneShot();
        source.PlayOneShot(sound_effect1,volume/2);
        source.PlayOneShot(sound_effect2,volume/2);
    }
}
