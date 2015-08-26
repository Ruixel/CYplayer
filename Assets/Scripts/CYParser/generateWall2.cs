using UnityEngine;
using System.Collections;

public class generateWall2 : MonoBehaviour {

	public float xLength, yLength;
	public float x, y;
	public string tex1, tex2;
	public float zidx, level;

	private Texture[] textures;
	private float scale;
	private float height;
	private float h_size;
	private float h_pos;
	private float tex_x = 2f;
	private float tex_y = 2.1f;
	
	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;
		textures = worldGen.game_textures;

		float x2 = x + xLength;
		float y2 = y + yLength;

		getHeight ();
		float length = Mathf.Sqrt (Mathf.Pow((x - x2), 2) + Mathf.Pow((y - y2), 2));
		transform.localScale = new Vector3 (length*scale, h_size, 1);
		transform.position = new Vector3 (((x + x2) / 2)*scale, (level*height)+h_pos, ((y + y2) / 2)*scale);

		Vector3 v3B = new Vector3 (x2, 0, y2);
		Vector3 v3A = new Vector3 (x, 0, y);
		Vector3 v3 = v3A - v3B;
		
		float angle = Mathf.Atan2(v3.z, v3.x) * Mathf.Rad2Deg;
		transform.Rotate (new Vector3(0, -angle, 0));

		if (tex1 [0] == 'c') {
			Color32 co = getColour (tex1);
			GetComponent<MeshRenderer> ().material.color = co;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex1, gameObject);
		}
		GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((length * scale) / tex_x, h_size / tex_y);

		// Other side
		GameObject side2 = GameObject.CreatePrimitive (PrimitiveType.Quad);
		side2.transform.position = transform.position;
		side2.transform.localScale = transform.localScale;
		side2.transform.Rotate(new Vector3(0, -angle + 180, 0));

		if (tex2 [0] == 'c') {
			Color32 co = getColour (tex2);
			side2.GetComponent<MeshRenderer>().material.mainTexture = GetComponent<MeshRenderer>().material.mainTexture;
			side2.GetComponent<MeshRenderer> ().material.color = co;
		} else {
			side2.GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex2, side2);
		}
		side2.GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((length * scale) / tex_x, h_size / tex_y);
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

	Texture getTexture(string tex, GameObject go)
	{
		int t = int.Parse (tex);
		switch (t) {
		case 1:
			tex_x = 2f; tex_y = 1.8f;
			return textures[0];
		case 2:
			tex_x = 1.2f; tex_y = 4.8f;
			go.GetComponent<MeshRenderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
                return textures[2];
		case 3:
			tex_x = 4f; tex_y = 4.8f;
			return textures[1];
		case 4:
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
		case 9:
			tex_x = 4f; tex_y = 4.8f;
			return textures[8];
		case 10:
			tex_x = 4f; tex_y = 4.8f;
			return textures[7];
		case 11:
			tex_x = 4f; tex_y = 4.8f;
			return textures[10];
		case 13:
			tex_x = 4f; tex_y = 4.8f;
			return textures[11];
		default:
			return null; //textures[0];
		}

	}

	void getHeight() {
		switch ((int)zidx) {
		case 1:
			h_size = height;
			h_pos = height/2;
			break;
		case 2:
			h_size = 3 * height / 4;
			h_pos = height/2 - (height/8);
			break;
		case 3:
			h_size = height / 2;
			h_pos = height/4;
			break;
		case 4:
			h_size = height / 4;
			h_pos = height/8;
			break;
		case 5:
			h_size = height / 4;
			h_pos = height/4 + height/8;
			break;
		case 6:
			h_size = height / 4;
			h_pos = height/2 + height/8;
			break;
		case 7:
			h_size = height / 4;
			h_pos = (3*height/4) + height/8;
			break;
		case 8:
			h_size = height / 2;
			h_pos = 3*height/4;
			break;
		case 9:
			h_size = 3 * height / 4;
			h_pos = height/2 + (height/8);
			break;
		case 10:
			h_size = height / 2;
			h_pos = height/2;
			break;
		}
	}
}
