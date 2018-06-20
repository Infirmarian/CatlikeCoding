using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour {
    public float attractionForce;
    Rigidbody body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}

    private void FixedUpdate() {
        body.AddForce(transform.localPosition * -attractionForce);
    }

}
