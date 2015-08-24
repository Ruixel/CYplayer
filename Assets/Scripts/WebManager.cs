using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebManager : MonoBehaviour {
	public GameObject text_obj;

	// Use this for initialization
	void Start () {
		if (Application.isWebPlayer) {
			Application.ExternalCall ("Loaded", "Success");
		} else {
			GameObject wg = GameObject.Find("WorldGenerator");
			wg.SendMessage("Generate");
		}
	}
	
	void LoadLevel(string gameNumber) {
		int gn = int.Parse (gameNumber);
		GameObject wg = GameObject.Find("WorldGenerator");
		wg.GetComponent<worldGen> ().gameNumber = gn;
		wg.SendMessage("Generate");
	}

	void GiveError(string err) {
		text_obj.GetComponent<Text> ().text = err;
	}
}
