using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	
	public AudioClip music;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().volume = 0.2f;
		GetComponent<AudioSource>().PlayOneShot(music);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
