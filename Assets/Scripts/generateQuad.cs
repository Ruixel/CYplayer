using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class generateQuad : MonoBehaviour {

	public float x1, y1;
	public float x2, y2;
	public float x3, y3;
	public float x4, y4;
	public string tex1, tex2;
	public float level;

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

		Mesh quad = CreateQuad ();
		GetComponent<MeshFilter>().mesh = quad;

	}

	Mesh CreateQuad() {
		Mesh mesh = new Mesh ();

		Vector3[] vertices = new Vector3[] {
			new Vector3 (y3*scale/400, 0, x3*scale/400), //3
			new Vector3 (y2*scale/400, 0, x2*scale/400), //2
			new Vector3 (y4*scale/400, 0, x4*scale/400), //4
			new Vector3 (y1*scale/400, 0, x1*scale/400), //1
		};
		transform.position = new Vector3 (0, (level*height)-0.01f, 0);
		transform.localScale = new Vector3 (400, 1, 400);
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
