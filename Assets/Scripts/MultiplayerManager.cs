﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{

    public string roomName;
    public GameObject textobject;
    public GameObject objectToInstantiate;
    public GameObject baseLocation;
    public InputField username;
    public InputField room;
   

    public void initGame(string connectionName)
    {
        roomName = GameObject.Find("WorldGenerator").GetComponent<worldGen>().gameNumber.ToString() + room.text;
        PhotonNetwork.ConnectUsingSettings(connectionName);
        textobject.SetActive(true);
    }


    public void join()
    {
        PhotonNetwork.playerName = username.text;
        textobject.SetActive(false);
        PhotonNetwork.JoinRoom(roomName,true);
    }

    public void create()
    {
        PhotonNetwork.playerName = username.text;
        textobject.SetActive(false);
        PhotonNetwork.CreateRoom(roomName, true, true, 16);
    }

    void OnJoinedRoom()
    {
        GameObject obj = (GameObject)PhotonNetwork.Instantiate(objectToInstantiate.name, baseLocation.transform.position, Quaternion.identity, 0);
        obj.GetComponent<MouseLook>().enabled = false ;
        baseLocation.SetActive(false);
        if (obj.GetComponent<PhotonView>().isMine)
        {
            obj.GetComponent<FirstPersonController>().enabled = true;
            obj.GetComponent<MouseLook>().enabled = true;
            obj.transform.FindChild("FirstPersonCharacter").gameObject.SetActive(true);
        }

        
    }


}
