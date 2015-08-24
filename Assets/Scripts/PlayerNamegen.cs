using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNamegen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = GetComponent<PhotonView>().owner.name;
    }
	

}
