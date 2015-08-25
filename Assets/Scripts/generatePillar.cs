using UnityEngine;
using System.Collections;

public class generatePillar : MonoBehaviour {

	public float x,y;
	public float angle;
	public string tex;
	public float size, zidx, level;

	private float scale;
	private float height;
	private float h_pos, h_size;
	private Texture[] textures;
	private float tex_x = 2f;
	private float tex_y = 2.1f;

	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;
		textures = worldGen.game_textures;

		float si = getSize ();
		getHeight ();
		transform.localScale = new Vector3 (si*scale, h_size, si*scale);
		transform.position = new Vector3 (x*scale, (level*height)+h_pos, y*scale);

		if (angle == 2) transform.Rotate(new Vector3(0, 45, 0));

		if (tex [0] == 'c') {
			Color32 co = getColour (tex);
			GetComponent<MeshRenderer> ().material.color = co;
		} else {
			GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex, gameObject);
		}
		GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((si * scale) / tex_x, h_size / tex_y);

	}

	float getSize() {
		switch ((int)size) {
		case 1:
			return 0.5f;
		}
		return size-1;
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
