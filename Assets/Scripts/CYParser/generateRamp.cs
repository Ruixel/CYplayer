using UnityEngine;
using System.Collections;

public class generateRamp : MonoBehaviour {

	public float x, y;
	public string tex;
	public float direction, level;

	private float scale;
	private float height;
	private float h_pos;
	private Texture[] textures;
	private float tex_x = 2f;
	private float tex_y = 2.1f;

	private float ramp_length;
	private float ramp_angle = 30.96376f;
	private float ramp_angle_2 = 29.49621f;

	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;
		textures = worldGen.game_textures;

		if (direction > 0 && direction < 5) {
			ramp_length = Mathf.Sqrt (Mathf.Pow (height, 2) + Mathf.Pow (20 * scale, 2));
			transform.localScale = new Vector3 (10 * scale, ramp_length, 1);
			transform.position = new Vector3 (x * scale, (level * height) + (height / 2), y * scale);
		} else {
			// 8.485282
			ramp_length = Mathf.Sqrt (Mathf.Pow (height, 2) + Mathf.Pow (8.485282f, 2));
			transform.localScale = new Vector3 (10*scale*Mathf.Sqrt(2), ramp_length, 1);

			transform.position = new Vector3 (x * scale, (level * height) + (height / 2), y * scale);
		}

		RotateRamp ();

		GameObject side2 = GameObject.CreatePrimitive (PrimitiveType.Quad);
		side2.transform.position = transform.position;
		side2.transform.localScale = transform.localScale;
		side2.transform.rotation = transform.rotation;
		side2.transform.Rotate (0, 180, 0);
		
		if (tex [0] == 'c') {
			Color32 co = getColour (tex);
			GetComponent<MeshRenderer> ().material.color = co;
			side2.GetComponent<MeshRenderer> ().material.color = co;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex, gameObject);
			side2.GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex, side2);
		}
		GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((transform.localScale.x*1f) / tex_x, (transform.localScale.y*1.09f) / tex_y);
		side2.GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((transform.localScale.x*1f) / tex_x, (transform.localScale.y*1.09f) / tex_y);

	}

	void RotateRamp(){
		switch ((int)direction) {
		case 1: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle, 180, 0); 
			transform.localPosition += new Vector3(0, 0, (20*scale)/2);
			break;
		case 2: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle, 0, 0); 
			transform.localPosition += new Vector3(0, 0, -(20*scale)/2);
			break;
		case 3: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle, 90, 0); 
			transform.localPosition += new Vector3(-(20*scale)/2, 0, 0);
			break;
		case 4:
			transform.localRotation = Quaternion.Euler(90 - ramp_angle, 270, 0); 
			transform.localPosition += new Vector3((20*scale)/2, 0, 0);
			break;
		case 5:
			transform.localRotation = Quaternion.Euler(90 - ramp_angle_2, 90+45, 0); 
			transform.localPosition += new Vector3(-(10*scale*Mathf.Sqrt(2.25f))/2, 0, (10*scale*Mathf.Sqrt(2.25f))/2);
			break;
		case 6: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle_2, 45, 0); 
			transform.localPosition += new Vector3(-(10*scale*Mathf.Sqrt(2.25f))/2, 0, -(10*scale*Mathf.Sqrt(2.25f))/2);
			break;
		case 7: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle_2, -45, 0); 
			transform.localPosition += new Vector3((10*scale*Mathf.Sqrt(2.25f))/2, 0, -(10*scale*Mathf.Sqrt(2.25f))/2);
			break;
		case 8: 
			transform.localRotation = Quaternion.Euler(90 - ramp_angle_2, -90-45, 0); 
			transform.localPosition += new Vector3((10*scale*Mathf.Sqrt(2.25f))/2, 0, (10*scale*Mathf.Sqrt(2.25f))/2);
			break;
		}
	}

	Color32 getColour(string tex) {
		int e = 6;
		int o = tex.IndexOf (",", e);
		byte red = byte.Parse (tex.Substring (e, o - e));
		
		e = o + 2;
		o = tex.IndexOf (",", e);
		byte green = byte.Parse (tex.Substring (e, o - e));
		
		e = o + 2;
		o = tex.IndexOf (")", e);
		byte blue = byte.Parse (tex.Substring (e, o - 1 - e));
		
		return new Color32 (red, green, blue, 1);
	}
	
	Texture getTexture(string tex, GameObject go) {
		int t = int.Parse (tex);
		switch (t) {
		case 3: // Brick
			tex_x = 2f; tex_y = 1.8f;
			return textures[0];
		case 4: // Stone
			tex_x = 4f; tex_y = 4.8f;
			return textures[1];
		case 1: // Grass
			tex_x = 3.7f; tex_y = 4f;
			return textures[4];
		case 5:
			tex_x = 6f; tex_y = 4f;
			return textures[5];
		case 6:
			tex_x = 1f; tex_y = 1.2f;
			return textures[6];
		case 7:
			tex_x = 6f; tex_y = 4.8f;
			return textures[9];
		case 8:
			tex_x = 3f; tex_y = 4.8f;
			go.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
			return textures[3];
		case 2:
			tex_x = 4f; tex_y = 4.8f;
			return textures[8];
		case 9:
			tex_x = 4f; tex_y = 4.8f;
			return textures[7];
		case 10:
			tex_x = 4f; tex_y = 4.8f;
			return textures[10];
		case 13:
			tex_x = 4f; tex_y = 4.8f;
			return textures[11];
		default:
			return null; //textures[0];
		}
		
	}
}
