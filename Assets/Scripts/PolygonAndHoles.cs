using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonAndHoles : MonoBehaviour {
	private float scale = 0.08f;

	// Use this for initialization
	void Start () {
		Poly2Mesh.Polygon poly = new Poly2Mesh.Polygon ();
		poly.outside = new List<Vector3> () {
			new Vector3(350*scale, 50*scale, 0),
			new Vector3(400*scale, 400*scale, 0),
			new Vector3(0*scale, 400*scale, 0),
			new Vector3(0*scale, 0*scale, 0),

		};
		poly.holes.Add(new List<Vector3>() {
			new Vector3(50*scale, 30*scale, 0),
			new Vector3(50*scale, 50*scale, 0),
			new Vector3(30*scale, 50*scale, 0),
			new Vector3(30*scale, 30*scale, 0),
		});
		poly.holes.Add(new List<Vector3>() {
			new Vector3(200*scale, 100*scale, 0),
			new Vector3(200*scale, 200*scale, 0),
			new Vector3(100*scale, 200*scale, 0),
			new Vector3(100*scale, 100*scale, 0),
		});
		
		// Set up game object with mesh;
		GameObject butt = Poly2Mesh.CreateGameObject(poly);
		butt.transform.Rotate (90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
