using UnityEngine;			
using System.Collections;

public class Feg : MonoBehaviour {

	public string url;
	private WWW www;

	// Use this for initialization
	void Start () {
		url = "http://www.challengeyou.com/ChallengeFiles/Maze/Maze160679";
		WWW www = new WWW(url);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
