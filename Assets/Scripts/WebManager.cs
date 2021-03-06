﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebManager : MonoBehaviour {
	public GameObject text_obj;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("levelID", GameObject.Find ("WorldGenerator").GetComponent<worldGen> ().gameNumber);
        text_obj.GetComponent<Text>().text = "Connecting to Multiplayer Server...";
        GameObject netman = GameObject.Find("NetworkManager");
        netman.GetComponent<MultiplayerManager>().initGame("TechDemo");
        text_obj.GetComponent<Text>().text += "\nLoading Level...";
	    try
	    {
	        if (Application.isWebPlayer)
	        {
	            Application.ExternalCall("Loaded", "Success");
	        }
	        else
	        {
	            GameObject wg = GameObject.Find("WorldGenerator");
	            wg.GetComponent<worldGen>().gameNumber = PlayerPrefs.GetInt("levelID");
	            wg.SendMessage("Generate");
	        }
	    }
	    catch (Exception e)
	    {
	        GiveError("Error :( :" + e.GetBaseException());
	    }
	}
	
	void LoadLevel(string gameNumber) {
		int gn = int.Parse (gameNumber);
		GameObject wg = GameObject.Find("WorldGenerator");
		wg.GetComponent<worldGen> ().gameNumber = gn;
		wg.SendMessage("Generate");
	}

	void GiveError(string err) {
		text_obj.GetComponent<Text> ().text += "\n" + err;
    }
}
