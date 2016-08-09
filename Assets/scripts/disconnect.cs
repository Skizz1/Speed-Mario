using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class disconnect : MonoBehaviour {

    private string ipS = "10.100.254.142:2020";
    System.Net.IPHostEntry host;
    // Use this for initialization
    public string ipD;
    public void remove()
    {

        if (NetworkServer.active)
        {
            string ip = Network.player.ipAddress;
            string url = "http://" + ipS + "/remove?ip=" + ip;
            WWW www = new WWW(url);
        } else
        {
            Debug.Log(ipD +" ipD");
            string ip = Network.player.ipAddress;
            string url = "http://" + ipS + "/removeC?ip=" + ipD;
            WWW www = new WWW(url);
        }
 
        
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
        Application.LoadLevel("Menu");
    }
    void Start () {
     //   Debug.Log("JE suis disconnect");
	}
	 
	// Update is called once per frame
	void Update () {

    }
}
