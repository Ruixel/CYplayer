using UnityEngine;
using System.Collections;

public class oldWorldGen : MonoBehaviour {
	
	public int gameNumber;
	public GameObject wall;
	public GameObject plat;
	public GameObject pillar;
	
	public static float scale = 0.4f;
	public static float height = 12f*scale;
	
	private int e;
	private string floors;
	private string walls;
	private string plats;
	private string pillars;
	
	// Use this for initialization
	void Generate () {
		string url = "http://cy.dronespot.co/getMaze.php?maze=" + gameNumber;
		
		WWW www = new WWW (url);
		StartCoroutine (WaitForRequest (www));
	}
	
	IEnumerator WaitForRequest(WWW www) {
		yield return www;
		
		string code = www.text; //System.IO.File.ReadAllText("E:/Programs/CYBackup/Games/86276.Spikey Pixel.Xard.cy");
		int i;
		int p = 9;
		
		// Get Title
		i = code.IndexOf ('"', p);
		string title = code.Substring (p, i - p);
		print (title);
		
		p = code.IndexOf ("#creator:") + 11;
		i = code.IndexOf ('"', p);
		string author = code.Substring (p, i - p);
		print (author);
		
		/*/ Get Floors
			p = code.IndexOf ("#Floor:") + 8;
		i = code.IndexOf ("#walls:") - 2;
		floors = code.Substring (p, i - p);
		
		e = -1;
		int clevel = 1;
		bool loop = true;
		while (loop) {
			getFloor();
			loop = false;
		}*/
			
			// Get Walls
			p = code.ToLower().IndexOf("#walls:") + 8;
		i = code.ToLower().IndexOf("#begin:") - 2;
		walls = code.Substring(p, i-p);
		//print (walls);
		
		e = -1;
		bool loop = true;
		while (loop) {
			getWall();
			
			if (walls[e] != ',') {
				loop = false;
			}
		}
		
		// Get Platforms
		p = code.ToLower().IndexOf("#plat:") + 7;
		i = code.ToLower().IndexOf("#triplat:") - 2;
		plats = code.Substring(p, i-p);
		//print (walls);
		
		e = -1;
		loop = true;
		while (loop) {
			getPlat();
			
			if (plats[e] != ',') {
				loop = false;
			}
		}
		
		// Get Pillars
		p = code.ToLower().IndexOf("#pillar:") + 9;
		i = code.ToLower().IndexOf("#backmusic:") - 2;
		pillars = code.Substring(p, i-p);
		print (pillars);
		
		e = -1;
		loop = true;
		while (loop) {
			getPillar();
			
			if (pillars[e] != ',') {
				loop = false;
			}
		}
		
		GameObject.Find ("Canvas/Panel").SetActive (false);
		GameObject.Find ("Main Camera").transform.position = new Vector3 (-68.30644f, 20, 122.477f);
		//GameObject.Find ("Main Camera").transform.Rotate = new Vector3 (0, 160, 0);
	}
	
	void getPillar() {
		e += 4;
		
		int o = pillars.IndexOf (",", e);
		float x = float.Parse(pillars.Substring (e, o - e));
		print ("x = " + x);
		
		e = o + 2;
		o = pillars.IndexOf ("]", e);
		float y = float.Parse(pillars.Substring (e, o - e));
		print ("y = " + y);
		
		e = o + 4;
		o = pillars.IndexOf (",", e);
		int angle = int.Parse(pillars.Substring (e, o - e));
		print ("angle = " + angle);
		
		e = o + 2;
		string tex;
		if (pillars [e]=='c') { 
			o = pillars.IndexOf(")", e)+1;
			tex = pillars.Substring(e, o-e);
			e = o + 6;
		} else {
			o = pillars.IndexOf(",", e);
			tex = pillars.Substring(e, o-e);
		}
		print ("tex = " + tex);
		
		e = o + 2;
		o = pillars.IndexOf (",", e);
		int size = int.Parse(pillars.Substring (e, o - e));
		print ("size = " + size);
		
		e = o + 2;
		o = pillars.IndexOf ("]", e);
		int zidx = int.Parse(pillars.Substring (e, o - e));
		print ("zidx = " + zidx);
		
		e = o + 3;
		o = pillars.IndexOf ("]", e);
		int level = int.Parse(pillars.Substring (e, o - e));
		print ("level = " + level);
		
		createPillar (x, y, angle, tex, size, zidx, level);
		
		e = o + 1;
	}
	
	void getPlat() {
		e += 4;
		
		int o = plats.IndexOf (",", e);
		float x = float.Parse(plats.Substring (e, o - e));
		
		e = o + 2;
		o = plats.IndexOf ("]", e);
		float y = float.Parse(plats.Substring (e, o - e));
		
		e = o + 4;
		o = plats.IndexOf (",", e);
		int size = int.Parse(plats.Substring (e, o - e));
		
		e = o + 2;
		string tex;
		if (plats [e]=='c') { 
			o = plats.IndexOf(")", e)+1;
			tex = plats.Substring(e, o-e);
			e = o + 6;
		} else {
			o = plats.IndexOf(",", e);
			tex = plats.Substring(e, o-e);
		}
		
		e = o + 2;
		o = plats.IndexOf ("]", e);
		int zidx = int.Parse(plats.Substring (e, o - e));
		
		e = o + 3;
		o = plats.IndexOf ("]", e);
		int level = int.Parse(plats.Substring (e, o - e));
		
		createPlat (x, y, size, tex, zidx, level);
		
		e = o + 1;
	}
	
	void getWall() {
		e += 3;
		
		int o = walls.IndexOf (",", e);
		int xlength = int.Parse(walls.Substring (e, o - e));
		
		e = o + 2;
		o = walls.IndexOf(",", e);
		int ylength = int.Parse(walls.Substring (e, o - e));
		
		e = o + 2;
		o = walls.IndexOf(",", e);
		int x1 = int.Parse(walls.Substring (e, o - e));
		
		e = o + 2;
		o = walls.IndexOf(",", e);
		int y1 = int.Parse(walls.Substring (e, o - e));
		
		e = o + 3;
		string tex1;
		if (walls [e]=='c') { 
			o = walls.IndexOf(")", e)+1;
			tex1 = walls.Substring(e, o-e);
			e = o + 6;
		} else {
			o = walls.IndexOf(",", e);
			tex1 = walls.Substring(e, o-e);
		}
		
		e = o + 2;
		string tex2;
		bool is_colour = false;
		if (walls [e]=='c') { 
			o = walls.IndexOf(")", e)+1;
			is_colour = true;
		} else {
			o = walls.IndexOf(",", e);
		}
		
		int zidx = 1;
		bool has_zidx = false;
		//print (walls [o - 1]);
		if (is_colour) {
			if (walls [o] == ']') {
				tex2 = walls.Substring (e, o - e);
				//print ("tex2 = " + tex2);
			} else {
				tex2 = walls.Substring (e, o - e);
				//print ("tex2 = " + tex2);
				
				e = o + 2;
				o = walls.IndexOf ("]", e);
				has_zidx = true;
				zidx = int.Parse (walls.Substring (e, o - e));
				//print ("z = " + zidx);
			}
		} else {
			if (walls [o - 1] == ']') {
				tex2 = walls.Substring (e, o - 1 - e);
				//print ("tex2 = " + tex2);
			} else {
				tex2 = walls.Substring (e, o - e);
				//print ("tex2 = " + tex2);
				
				e = o + 2;
				o = walls.IndexOf ("]", e);
				has_zidx = true;
				zidx = int.Parse (walls.Substring (e, o - e));
				//print ("z = " + zidx);
			}
		}
		
		e = o + 2;
		o = walls.IndexOf("]", e);
		int level = int.Parse(walls.Substring (e, o - e));
		//print ("level = " + level);
		
		createWall (xlength, ylength, x1, y1, tex1, tex2, zidx, level);
		
		e = o + 1;
	}
	
	void getFloor() {
		e += 5;
		int o;
		string temp;
		
		o = floors.IndexOf (',', e);
		temp = floors.Substring (e, o - e);
		int x1 = int.Parse (temp);
		print (x1);
	}
	
	void createWall(int xlength, int ylength, int x1, int y1, string tex1, string tex2, int zidx, int level) {
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
	
	void createPlat (float x, float y, int size, string tex, int zidx, int level) {
		GameObject gen_plat = (GameObject)Instantiate(plat);
		gen_plat.GetComponent<generatePlat> ().x = -x;
		gen_plat.GetComponent<generatePlat> ().y = y;
		gen_plat.GetComponent<generatePlat> ().size = size;
		gen_plat.GetComponent<generatePlat> ().tex = tex;
		gen_plat.GetComponent<generatePlat> ().zidx = zidx;
		gen_plat.GetComponent<generatePlat> ().level = level;
		
	}
	
	void createPillar (float x, float y, int angle, string tex, int size, int zidx, int level) {
		GameObject gen_pillar = (GameObject)Instantiate (pillar);
		gen_pillar.GetComponent<generatePillar>().x = -x;
		gen_pillar.GetComponent<generatePillar>().y = y;
		gen_pillar.GetComponent<generatePillar>().angle = angle;
		gen_pillar.GetComponent<generatePillar>().tex = tex;
		gen_pillar.GetComponent<generatePillar> ().size = size;
		gen_pillar.GetComponent<generatePillar>().zidx = zidx;
		gen_pillar.GetComponent<generatePillar>().level = level;
	}
}
