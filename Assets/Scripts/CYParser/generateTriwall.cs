using UnityEngine;
using System.Collections;

public class generateTriwall : MonoBehaviour {

	public float x, y, flip;
	public string tex;
	public float dir, level;

	private float scale;
	private float height;
	private Texture[] textures;
	private float tex_x = 2f;
	private float tex_y = 2.1f;
	private int r = 0;
	
	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;
		textures = worldGen.game_textures;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		/*mesh.Clear();

		mesh.vertices = new Vector3[] {
			new Vector3(-0.5f, -0.5f, 0), 
			new Vector3(-0.5f, 0.5f, 0), 
			new Vector3(0.5f, 0.5f, 0)
		};

		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};*/
		mesh.triangles = new int[] {0, 1, 2};
		gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;

		transform.localScale = new Vector3 (20*scale, height, 1);
		transform.position = new Vector3 (x*scale, (level*height)+height/2, y*scale);

		if (flip == 2)
			r = 180;

		RotateTri ();

		GameObject side2 = GameObject.CreatePrimitive (PrimitiveType.Quad);
		side2.transform.position = transform.position;
		side2.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		side2.transform.rotation = transform.rotation;
		side2.transform.Rotate (0, 180, 0);

		side2.GetComponent<MeshFilter> ().mesh = mesh;
		side2.GetComponent<MeshCollider> ().sharedMesh = mesh;

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

	void RotateTri() {
		switch((int)dir) {
		case 1:
			transform.rotation = Quaternion.Euler (0, 90, r);
			transform.position += new Vector3(0, 0, (10*scale));
			break;
		case 2:
			transform.rotation = Quaternion.Euler (0, 270, r);
			transform.position += new Vector3(0, 0, -(10*scale));
			break;
		case 3:
			transform.rotation = Quaternion.Euler (0, 0, r);
			transform.position += new Vector3(-(10*scale), 0, 0);
			break;
		case 4:
			transform.rotation = Quaternion.Euler (0, 180, r);
			transform.position += new Vector3((10*scale), 0, 0);
			break;
		case 5:
			transform.localScale = new Vector3 (8.485282f, height, 1);
			transform.rotation = Quaternion.Euler (0, 45, r);
			transform.position += new Vector3(-(5*scale*Mathf.Sqrt(2.25f)), 0, (5*scale*Mathf.Sqrt(2.25f)));
			break;
		case 6:
			transform.localScale = new Vector3 (8.485282f, height, 1);
			transform.rotation = Quaternion.Euler (0, -45, r);
			transform.position += new Vector3(-(5*scale*Mathf.Sqrt(2.25f)), 0, -(5*scale*Mathf.Sqrt(2.25f)));
			break;
		case 7:
			transform.localScale = new Vector3 (8.485282f, height, 1);
			transform.rotation = Quaternion.Euler (0, -90-45, r);
			transform.position += new Vector3((5*scale*Mathf.Sqrt(2.25f)), 0, -(5*scale*Mathf.Sqrt(2.25f)));
			break;
		case 8:
			transform.localScale = new Vector3 (8.485282f, height, 1);
			transform.rotation = Quaternion.Euler (0, 90+45, r);
			transform.position += new Vector3((5*scale*Mathf.Sqrt(2.25f)), 0, (5*scale*Mathf.Sqrt(2.25f)));
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
}
