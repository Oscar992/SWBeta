using UnityEngine;
using System.Collections;

public class Force : MonoBehaviour {

    private Rigidbody rb;
    int count;
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 20);
    }
    void Update()
    {
        //transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+1);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Velocity" + rb.velocity.z);
    }
}
