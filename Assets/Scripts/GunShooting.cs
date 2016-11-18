using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour {

	public GameObject m_shotPrefab;
	public GameObject stormTrooper;
	public AudioSource laser;
	public GameObject hand;

	// Use this for initialization

	private float elapsedTime; 
	private bool isFirstTime;

	void Start () {
		elapsedTime = 0.0f;
		isFirstTime = true;
	}

	// Update is called once per frame
	void Update () {

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= 2.0 && isFirstTime) {
			isFirstTime = false;
			Shoot ();
		}
//		float distance = Vector3.Distance(hand.transform.position,go.transform.position);
//			float move = 35f * Time.deltaTime;
//
//		go.transform.Translate(hand.transform.position * move);
	}

	public void Shoot () {
		laser.Play();
			Quaternion finalRotation = Quaternion.Euler(hand.transform.rotation.x,
			 hand.transform.rotation.y/*+(180-stormTrooper.transform.rotation.y) */  
			,hand.transform.rotation.z);
		GameObject go = Instantiate(m_shotPrefab,hand.transform.position,finalRotation ) as GameObject;
	}
}
