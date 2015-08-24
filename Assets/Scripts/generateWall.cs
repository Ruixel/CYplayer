using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class generateWall : MonoBehaviour {
	
	public float xLength, yLength;
	public float x, y;
	public string tex1, tex2;
	public int zidx, level;

	private float scale;
	private float height;

	GameObject otherside;
	
	// Use this for initialization
	void Start () {
		scale = worldGen.scale;
		height = worldGen.height;

		Mesh quad = CreateQuad (true);
		GetComponent<MeshFilter>().mesh = quad;
		GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.y);
		// Get Colour
		if (tex2 [0] == 'c') {
			Color32 co = getTexture (tex2);
			GetComponent<MeshRenderer>().material.color = co;
		}

		otherside = new GameObject ("wall2");
		otherside.AddComponent<MeshFilter> ();
		otherside.AddComponent<MeshCollider> ();
		otherside.AddComponent<MeshRenderer> ();
		Mesh quad2 = CreateQuad (false);
		otherside.GetComponent<MeshFilter> ().mesh = quad2;
		otherside.transform.Rotate(0, 90, 0);
		otherside.GetComponent<MeshRenderer> ().material = GetComponent<MeshRenderer> ().material;
		if (tex1 [0] == 'c') {
			Color32 co = getTexture (tex1);
			otherside.GetComponent<MeshRenderer> ().material.color = co;
		}
	}

	Color32 getTexture(string tex) {
		int e = 6;
		int o = tex1.IndexOf (",", e);
		byte red = byte.Parse (tex.Substring (e, o - e));
		
		e = o + 2;
		o = tex1.IndexOf (",", e);
		byte green = byte.Parse (tex.Substring (e, o - e));
		
		e = o + 2;
		o = tex1.IndexOf (")", e);
		byte blue = byte.Parse (tex.Substring (e, o - 1 - e));
		 
		return new Color32 (red, green, blue, 1);
	}
	
	Mesh CreateQuad(bool side) {
		Mesh mesh = new Mesh ();

		float x2 = x + xLength;
		float y2 = y + yLength;

		float top = (height*level)+height;
		float bot = (height*level);
		switch (zidx) {
		case 1:
			top = (height*level)+height;
			bot = (height*level);
			break;
		case 2:
			top = (height*level)+(3*height/4);
			bot = (height*level);
			break;
		case 3:
			top = (height*level)+(height/2);
			bot = (height*level);
			break;
		case 4:
			top = (height*level)+(height/4);
			bot = (height*level);
			break;
		case 5:
			top = (height*level)+(height/2);
			bot = (height*level)+(height/4);
			break;
		case 6:
			top = (height*level)+(3*height/4);
			bot = (height*level)+(height/2);
			break;
		case 7:
			top = (height*level)+height;
			bot = (height*level)+(3*height/4);
			break;
		case 8:
			top = (height*level)+height;
			bot = (height*level)+(height/2);
			break;
		case 9:
			top = (height*level)+height;
			bot = (height*level)+(height/4);
			break;
		case 10:
			top = (height*level)+(3*height/4);
			bot = (height*level)+(height/4);
			break;
		}
		//print ("zindex: " + zidx + ", " + top + ", " + bot );

		Vector3[] vertices;
		if (side) {
			vertices = new Vector3[] {
				new Vector3 (y, top, x), //3
				new Vector3 (y2, top, x2), //2
				new Vector3 (y, bot, x), //4
				new Vector3 (y2, bot, x2), //1
			};
		} else {
			vertices = new Vector3[] {
				new Vector3 (y, bot, x), //3
				new Vector3 (y2, bot, x2), //2
				new Vector3 (y, top, x), //4
				new Vector3 (y2, top, x2), //1
			};
		}

		for (int c=0; c<vertices.Length; c++) {
			vertices[c] = new Vector3(vertices[c].x*scale, vertices[c].y*scale, vertices[c].z*scale);
		}

		/*new Vector3( 1, 0,  1),
		new Vector3( 1, 0, -1),
		new Vector3(-1, 0,  1),
		new Vector3(-1, 0, -1),*/
		
		Vector2[] uv = new Vector2[] {
			new Vector2 (1, 1),
			new Vector2 (1, 0),
			new Vector2 (0, 1),
			new Vector2 (0, 0),
		};
		
		int[] triangles = new int[] {
			0, 1, 2,
			2, 1, 3,
		};
		
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		
		return mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
