using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

	public CharacterController cc;
	public float movementSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		float rotX = Input.GetAxis ("Mouse X");
		transform.Rotate(new Vector3(0, rotX, 0));

		// Movement
		float forwardSpeed = Input.GetAxis ("Vertical");
		float sideSpeed = Input.GetAxis ("Horizontal");

		Vector3 speed = new Vector3(sideSpeed * movementSpeed, 0, forwardSpeed * movementSpeed);
		speed = transform.rotation * speed;
		cc.SimpleMove (speed);
	}
}
