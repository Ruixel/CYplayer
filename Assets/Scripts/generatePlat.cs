using UnityEngine;
using System.Collections;using UnityEngine;
using System.Collections;

public class generatePlat : MonoBehaviour {

	public float x, y;
	public string tex;
	public float size, zidx, level;
	
	private float scale;
	private float height;
	private float h_pos;
	private Texture[] textures;
	private float tex_x = 2f;
	private float tex_y = 2.1f;
	
	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;
		textures = worldGen.game_textures;

		getHeight ();
		if (size == 4) size = 8;
		if (size == 3) size = 4;
		transform.localScale = new Vector3 (10*size*scale, 10*size*scale, 1);
		transform.position = new Vector3 (x*scale, (level*height)+h_pos+0.01f, y*scale);
		
		// Other side
		GameObject side2 = GameObject.CreatePrimitive (PrimitiveType.Quad);
		side2.transform.position = transform.position;
		side2.transform.localScale = transform.localScale;
		side2.transform.Rotate(new Vector3(270, 0, 0));

		if (tex [0] == 'c') {
			Color32 co = getColour (tex);
			GetComponent<MeshRenderer> ().material.color = co;
			side2.GetComponent<MeshRenderer> ().material.color = co;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex, gameObject);
			side2.GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex, side2);
		}
		GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((10*size * scale) / tex_x, (10*size * scale) / tex_y);
		side2.GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((10*size * scale) / tex_x, (10*size * scale) / tex_y);
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
	
	void getHeight() {
		switch ((int)zidx) {
		case 1:
			h_pos = 0;
			break;
		case 2:
			h_pos = height / 4;
			break;
		case 3:
			h_pos = height / 2;
			break;
		case 4:
			h_pos = 3 * height / 4;
			break;
		}
	}
}
