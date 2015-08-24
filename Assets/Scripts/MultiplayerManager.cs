using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MultiplayerManager : MonoBehaviour
{

    public string roomName;
    public GameObject textobject;
    public GameObject objectToInstantiate;
    public GameObject baseLocation;
    public InputField username;
   

    public void initGame(string connectionName)
    {
        roomName = GameObject.Find("WorldGenerator").GetComponent<worldGen>().gameNumber.ToString();
        PhotonNetwork.ConnectUsingSettings(connectionName);
        textobject.SetActive(true);
    }


    public void join()
    {
        PhotonNetwork.playerName = username.text;
        textobject.SetActive(false);
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
    }

    void OnJoinedRoom()
    {
        GameObject obj = (GameObject)PhotonNetwork.Instantiate(objectToInstantiate.name, baseLocation.transform.position, Quaternion.identity, 0);
        baseLocation.SetActive(false);
        if (obj.GetComponent<PhotonView>().isMine)
        {
            obj.GetComponent<FirstPersonController>().enabled = true;
            obj.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        }

        
    }


}
