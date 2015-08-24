using UnityEngine;
using System.Collections;

public class Roll : MonoBehaviour {
	
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddForce (new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0));
		rb.AddForce (new Vector3 (0, 0, Input.GetAxisRaw ("Vertical")));
	}
}
