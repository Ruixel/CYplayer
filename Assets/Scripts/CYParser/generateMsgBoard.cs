using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class generateMsgBoard : MonoBehaviour {
	
	public float x, y;
	public string msg;
	public float direction, zidx, level;

	private Texture[] textures;
	private float scale;
	private float height;
	private float h_pos;

	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;

		h_pos = height / 4;
		transform.position = new Vector3 (x*scale, (level*height)+h_pos, (y)*scale);
		RotateBoard ();
		transform.position += (transform.right / 8);

		// Set Message
		GameObject canvas = transform.Find ("Canvas").gameObject;
		GameObject text = canvas.transform.Find ("Text").gameObject;
		text.GetComponent<Text> ().text = msg;
	}

	void RotateBoard(){
		switch ((int)direction) {
		case 1: transform.Rotate (0, -90, 0); break;
		case 2: transform.Rotate (0, 90, 0); break;
		case 3: transform.Rotate(0, 180, 0); break;
		case 5: transform.Rotate (0, 225, 0); break;
		case 6: transform.Rotate (0, 45+90, 0); break;
		case 7: transform.Rotate (0, 225-180, 0); break;
		case 8: transform.Rotate (0, 45+270, 0); break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
