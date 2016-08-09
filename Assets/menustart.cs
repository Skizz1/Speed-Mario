using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menustart : MonoBehaviour {
    public GameObject Map;
    public GameObject Menu;

    // Use this for initialization
    void Start()
    {
        Map.active = false;
   //     Menu.active = false;

        GameObject.Find("NetworkManager").GetComponent<NetworkManager_Custom>().Menu = Menu;
        GameObject.Find("NetworkManager").GetComponent<NetworkManager_Custom>().Map = Map;
        GameObject.Find("Rules").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Rules").GetComponent<Button>().onClick.AddListener(Rouls);
        GameObject.Find("QuitGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("QuitGame").GetComponent<Button>().onClick.AddListener(quit_game);

    }
    public void quit_game()
    {
        Application.Quit();
    }
    public void Rouls()
    {
        Application.LoadLevel("game 1");

    }

    // Update is called once per frame
    void Update () {
	
	}
}
