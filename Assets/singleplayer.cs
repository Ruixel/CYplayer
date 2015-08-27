using UnityEngine;
using System.Collections;

public class singleplayer : MonoBehaviour {

    public GameObject objectToInstantiate;
    public GameObject baseLocation;

    public void startGame()
    {
        GameObject obj = (GameObject)PhotonNetwork.Instantiate(objectToInstantiate.name, baseLocation.transform.position, Quaternion.identity, 0);
        baseLocation.SetActive(false);
            obj.GetComponent<FirstPersonController>().enabled = true;
            obj.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
        
    }
}
