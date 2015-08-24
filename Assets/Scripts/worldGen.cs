using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class worldGen : MonoBehaviour {

	public int gameNumber;
	public GameObject wall;
	public GameObject plat;
	public GameObject pillar;
	public GameObject floor;
	public GameObject board;
	public Texture[] textures;
	
	public static float scale = 0.4f;
	public static float height = 12f*scale;
	public static Texture[] game_textures;
	private float tex_x = 2f;
	private float tex_y = 2.1f;

	private int e;

	void Awake() {
		game_textures = textures;
	}
	
	// Use this for initialization
	void Generate () {
		string url = "http://cy.dronespot.co/getJson.php?maze=" + gameNumber;

		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (www));
	}
		
	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		string code = www.text;

		JSONObject json = new JSONObject (code);

		// Get Details
		string title = json [0].ToString ();
		title = title.Substring(1, title.Length-2);
		print ("Title: " + title);

		string author = json [3].ToString ().Substring(1, json [3].ToString ().Length-2);
		print ("By: " + author);

		List<float[]> holeList = new List<float[]> ();
		json.GetField ("hole", delegate(JSONObject holes) {
			foreach (JSONObject obj in holes.list) {
				float[] hole = new float[4] { obj[0].n, obj[1].n, obj[2].n, obj[3].n };
				holeList.Add(hole);
			}
		});

		json.GetField ("floor", delegate(JSONObject floors) {
			int level = 1;
			foreach(JSONObject obj in floors.list) {
				if (obj[9].n == 1) {
					string tex1;
					if (obj[8].type == JSONObject.Type.NUMBER) { tex1 = obj[8].n.ToString(); } else { tex1 = obj[8].str; }
					
					string tex2;
					if (obj[10].type == JSONObject.Type.NUMBER) { tex2 = obj[10].n.ToString(); } else { tex2 = obj[10].str; }

					Poly2Mesh.Polygon poly = new Poly2Mesh.Polygon ();
					poly.outside = new List<Vector3> () {
						new Vector3(obj[5].n*scale, obj[4].n*scale, 0), //3
						new Vector3(obj[3].n*scale, obj[2].n*scale, 0), //2
						new Vector3(obj[1].n*scale, obj[0].n*scale, 0), // 1
						new Vector3(obj[7].n*scale, obj[6].n*scale, 0), //4
					};

					holeList.ForEach(delegate(float[] hole_array) {
						if (hole_array[3] == level) {
							int d = 5;
							switch((int)hole_array[2]) {
								case 2: d = 10; break;
								case 3: d = 20; break;
								case 4: d = 40; break;
							}

							poly.holes.Add(new List<Vector3>() {
								new Vector3((hole_array[1]+d)*scale, (hole_array[0]-d)*scale, 0),
								new Vector3((hole_array[1]+d)*scale, (hole_array[0]+d)*scale, 0),
								new Vector3((hole_array[1]-d)*scale, (hole_array[0]+d)*scale, 0),
								new Vector3((hole_array[1]-d)*scale, (hole_array[0]-d)*scale, 0)
							});
						}
					});

					GameObject ground = Poly2Mesh.CreateGameObject(poly);
					ground.transform.Rotate (90, -90, 0);
					ground.transform.position = new Vector3(0, (level*height)+0.005f, 0);

					if (tex1[0] == 'c') {
						Color32 co = getColour (tex1);
						ground.GetComponent<MeshRenderer> ().material.color = co;
					} else {
						ground.GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex1, ground);
					}
					ground.GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((2f * scale) / tex_x, (2f * scale) / tex_y);
				    ground.AddComponent<MeshCollider>();

					// Ceiling
					GameObject ceil = Poly2Mesh.CreateGameObject(poly);
					ceil.transform.Rotate (-90, -90, 0);
					ceil.transform.localScale = new Vector3(1, -1, 1);
					ceil.transform.position = new Vector3(0, (level*height)-0.005f, 0);

					if (tex2[0] == 'c') {
						Color32 co = getColour (tex2);
						ceil.GetComponent<MeshRenderer> ().material.color = co;
					} else {
						ceil.GetComponent<MeshRenderer>().material.mainTexture = getTexture (tex2, ceil);
					}
					ceil.GetComponent<MeshRenderer> ().material.mainTextureScale = new Vector2 ((2f * scale) / tex_x, (2f * scale) / tex_y);

				};
				level++;
			}
		});

		json.GetField ("walls", delegate(JSONObject walls) {
			foreach(JSONObject obj in walls.list) {
				string tex1;
				if (obj[4].type == JSONObject.Type.NUMBER) { tex1 = obj[4].n.ToString(); } else { tex1 = obj[4].str; }

				string tex2;
				if (obj[5].type == JSONObject.Type.NUMBER) { tex2 = obj[5].n.ToString(); } else { tex2 = obj[5].str; }

				if (obj.list.Count == 8) {
					createWall(obj[0].n, obj[1].n, obj[2].n, obj[3].n, tex1, tex2, obj[6].n, obj[7].n);
				} else {
					createWall(obj[0].n, obj[1].n, obj[2].n, obj[3].n, tex1, tex2, 1, obj[6].n);
				}
			}
		});

		json.GetField ("plat", delegate(JSONObject plat) {
			foreach(JSONObject obj in plat.list) {
				string tex1;
				if (obj[3].type == JSONObject.Type.NUMBER) { tex1 = obj[3].n.ToString(); } else { tex1 = obj[3].str; }

				if (obj.list.Count == 6) {
					createPlat(obj[0].n, obj[1].n, obj[2].n, tex1, obj[4].n, obj[5].n);
				}
				if (obj.list.Count == 5) {
					createPlat(obj[0].n, obj[1].n, obj[2].n, tex1, 1, obj[4].n);
				}
				if (obj.list.Count == 4) {
					createPlat(obj[0].n, obj[1].n, obj[2].n, "5", 1, obj[3].n);
				}
			}
		});

		json.GetField ("pillar", delegate(JSONObject pillar) {
			foreach(JSONObject obj in pillar.list) {
				string tex1;
				if (obj[3].type == JSONObject.Type.NUMBER) { tex1 = obj[3].n.ToString(); } else { tex1 = obj[3].str; }
				
				if (obj.list.Count == 7) {
					createPillar(obj[0].n, obj[1].n, obj[2].n, tex1, obj[4].n, obj[5].n, obj[6].n);
				}
				if (obj.list.Count == 4) {
					createPillar(obj[0].n, obj[1].n, obj[2].n, "1", 1, 1, obj[3].n);
				}
			}
		});

		json.GetField ("board", delegate(JSONObject boards) {
			foreach(JSONObject obj in boards.list) {
				if (obj.list.Count == 5) {
					createBoard(obj[0].n, obj[1].n, obj[2].str, obj[3].n, 1, obj[4].n);
				}
			}
		});

		GameObject.Find ("Canvas/Panel").SetActive (false);
		GameObject.Find ("Main Camera").transform.position = new Vector3 (-68.30644f, 20, 122.477f);
		//GameObject.Find ("Main Camera").transform.Rotate = new Vector3 (0, 160, 0);
	}

	void accessData(JSONObject obj){
		switch(obj.type){
			case JSONObject.Type.OBJECT:
				for(int i = 0; i < obj.list.Count; i++){
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];
					Debug.Log(key);
					accessData(j);
				}
				break;
			case JSONObject.Type.ARRAY:
				foreach(JSONObject j in obj.list){
					accessData(j);
				}
				break;
			case JSONObject.Type.STRING:
				Debug.Log(obj.str);
				break;
			case JSONObject.Type.NUMBER:
				Debug.Log(obj.n);
				break;
			case JSONObject.Type.BOOL:
				Debug.Log(obj.b);
				break;
			case JSONObject.Type.NULL:
				Debug.Log("NULL");
				break;
				
		}
	}

	void createFloor(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, string tex1, string tex2, float level) {
		GameObject gen_floor = (GameObject)Instantiate(floor);
		gen_floor.GetComponent<generateQuad> ().x1 = x1;
		gen_floor.GetComponent<generateQuad> ().y1 = y1;
		gen_floor.GetComponent<generateQuad> ().x2 = x2;
		gen_floor.GetComponent<generateQuad> ().y2 = y2;
		gen_floor.GetComponent<generateQuad> ().x3 = x3;
		gen_floor.GetComponent<generateQuad> ().y3 = y3;
		gen_floor.GetComponent<generateQuad> ().x4 = x4;
		gen_floor.GetComponent<generateQuad> ().y4 = y4;
		gen_floor.GetComponent<generateQuad> ().level = level;
	}

	void createWall(float xlength, float ylength, float x1, float y1, string tex1, string tex2, float zidx, float level) {
		GameObject gen_wall = (GameObject)Instantiate(wall);
		gen_wall.GetComponent<generateWall2> ().xLength = -xlength;
		gen_wall.GetComponent<generateWall2> ().yLength = ylength;
		gen_wall.GetComponent<generateWall2> ().x = -x1;
		gen_wall.GetComponent<generateWall2> ().y = y1;
		gen_wall.GetComponent<generateWall2> ().tex1 = tex1;
		gen_wall.GetComponent<generateWall2> ().tex2 = tex2;
		gen_wall.GetComponent<generateWall2> ().zidx = zidx;
		gen_wall.GetComponent<generateWall2> ().level = level;
	}

	void createPlat (float x, float y, float size, string tex, float zidx, float level) {
		GameObject gen_plat = (GameObject)Instantiate(plat);
		gen_plat.GetComponent<generatePlat> ().x = -x;
		gen_plat.GetComponent<generatePlat> ().y = y;
		gen_plat.GetComponent<generatePlat> ().size = size;
		gen_plat.GetComponent<generatePlat> ().tex = tex;
		gen_plat.GetComponent<generatePlat> ().zidx = zidx;
		gen_plat.GetComponent<generatePlat> ().level = level;

	}

	void createPillar (float x, float y, float angle, string tex, float size, float zidx, float level) {
		GameObject gen_pillar = (GameObject)Instantiate (pillar);
		gen_pillar.GetComponent<generatePillar>().x = -x;
		gen_pillar.GetComponent<generatePillar>().y = y;
		gen_pillar.GetComponent<generatePillar>().angle = angle;
		gen_pillar.GetComponent<generatePillar>().tex = tex;
		gen_pillar.GetComponent<generatePillar> ().size = size;
		gen_pillar.GetComponent<generatePillar>().zidx = zidx;
		gen_pillar.GetComponent<generatePillar>().level = level;
	}

	void createBoard (float x, float y, string msg, float direction, float zidx, float level) {
		GameObject gen_board = (GameObject)Instantiate (board);
		gen_board.GetComponent<generateMsgBoard> ().x = -x;
		gen_board.GetComponent<generateMsgBoard> ().y = y;
		gen_board.GetComponent<generateMsgBoard> ().msg = msg;
		gen_board.GetComponent<generateMsgBoard> ().direction = direction;
		gen_board.GetComponent<generateMsgBoard> ().zidx = zidx;
		gen_board.GetComponent<generateMsgBoard> ().level = level;
	}







	/* Functions for the floor since they have to be generated inside this script */
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
			go.GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Diffuse");
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
}
