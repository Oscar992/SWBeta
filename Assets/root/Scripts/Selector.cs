using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

    private GameObject selectedObject = null;
    GameObject rightArm;
    private float timeThrown = 0f;
    PlayerBehavior pb;

	// Use this for initialization
	void Start () {
        rightArm = GameObject.FindWithTag("RightForearm");
        pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
		this.Disable();
		this.Hide();
	}
	
	// Update is called once per frame
	void Update () {
        if (selectedObject != null)
        {
            selectedObject.transform.position = transform.position;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name != "3rd Person Controller")
        {
            if (Time.time - timeThrown > 1 )//&& pb.currentGesture == PlayerBehavior.Gesture.HOLDING)
            {
                Debug.Log(collision.gameObject.name);
                selectedObject = collision.gameObject;
                selectedObject.GetComponent<Rigidbody>().detectCollisions = false;
				this.Disable();
            }
        }
    }

    public void Throw()
    {
        Debug.Log("throwing");
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Rigidbody>().detectCollisions = true;
            RightForearm rfa = rightArm.GetComponent<RightForearm>();
            selectedObject.GetComponent<Rigidbody>().AddForce(rfa.handVector.normalized * 20000);
			selectedObject.GetComponent<EnemyBehavior>().GetThrown();
            selectedObject = null;
            timeThrown = Time.time;
			this.Disable();
			this.Hide();
        }
    }
	
	public void Enable() {
		
		this.GetComponent<Collider>().enabled = true;
	}
	
	public void Show() {
		this.GetComponent<Renderer>().enabled = true;
	}
	
	public void Hide() {
		this.GetComponent<Renderer>().enabled = false;
	}
	
	public void Disable() {
		this.GetComponent<Collider>().enabled = false;
	}
}
