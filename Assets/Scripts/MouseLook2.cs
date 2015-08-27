using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseLook2 : MonoBehaviour
{

    public float sensibility;
    public bool enableX = true;
    public bool enableY = true;

	// Use this for initialization
	void Start () {
	Cursor.lockState = CursorLockMode.Locked;
	    Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(enableX)
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensibility, 0), Space.World);
        if(enableY)
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * sensibility, 0, 0));
    }
}
