using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Rigidbody rb;
    public float Thrust = 1f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        rb.AddForce(Thrust * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
