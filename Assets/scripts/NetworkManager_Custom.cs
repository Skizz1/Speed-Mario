using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using SocketIO;
using System.Collections.Generic;
using System;

public class NetworkManager_Custom : NetworkManager
{
    public int pref = 3;
    public Transform playerPrefab;
    public bool isOn;
    private SocketIOComponent socket;
    private bool soc = false;
    private bool test;
    private string ipS = "10.100.254.142:2020";
    [SerializeField]
    String[] Levels;
    [SerializeField]
   GameObject[] spawners;
    public GameObject Menu;
    public GameObject Map;
    public int SelectMap;



    public Toggle toggle;



    public void StartupHost()
    {
        test = GameObject.Find("private").GetComponent<Toggle>().isOn;
        Menu.active = false;
        Map.active = true;
        StartCoroutine("buttons");

    }

    IEnumerator menuquit()
    {
        yield return new WaitForSeconds(0.3f);

    }

        IEnumerator buttons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("Map").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map").GetComponent<Button>().onClick.AddListener(map);
        GameObject.Find("Map1").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map1").GetComponent<Button>().onClick.AddListener(map1);
        GameObject.Find("Map2").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map2").GetComponent<Button>().onClick.AddListener(map2);
        GameObject.Find("Map3").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map3").GetComponent<Button>().onClick.AddListener(map3);
        GameObject.Find("Map4").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map4").GetComponent<Button>().onClick.AddListener(map4);
        GameObject.Find("Map5").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Map5").GetComponent<Button>().onClick.AddListener(map5);
    }
    public void map()
    {
        SelectMap = 0;
        Host();
    }
    public void map1()
    {
        SelectMap = 1;
        Host();
    }
    public void map2()
    {
        SelectMap = 2;
        Host();
    }
    public void map3()
    {
        SelectMap = 3;
        Host();
    }
    public void map4()
    {
        SelectMap = 4;
        Host();
    }
    public void map5()
    {
        SelectMap = UnityEngine.Random.Range(0 , 5);
        Host();
    }

    public void Host()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
            string ip = Network.player.ipAddress;
            string url = "http://" + ipS + "/host?ip=" + ip+"&map="+SelectMap+"&private="+test;
            WWW www = new WWW(url);
            StartCoroutine(WaitForRequest(www));
        Menu.active = false;
        Map.active = false;
        Application.LoadLevel(Levels[SelectMap]);
        StartCoroutine("Start_Socket");
   
        StartCoroutine("menuquit");



    }
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator calls()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["ch"] = "kkk";
        yield return new WaitForSeconds(0.02f);
        if (data.Count != 0) 
        socket.Emit("choix", new JSONObject(data));
    }

    private IEnumerator calls_send()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["choix"] = pref.ToString(); ;
        yield return new WaitForSeconds(0.3f);
        socket.Emit("choix1", new JSONObject(data));
    }
  
    public void toto(SocketIOEvent e)
    {
   
        string json = JsonUtility.ToJson(e.data[0]);
        var result = JSON.Parse(json);
        pref = Int32.Parse(result[2]);
       
    }

    public void JoinGame()
    {
        StartCoroutine("joins");
        
        //SetPort();
     //   NetworkManager.singleton.StartClient();
       // Application.LoadLevel(Levels[SelectMap]);

        StartCoroutine("Start_Socket");
    }
 
 
    IEnumerator joins()
    {
        string url = "http://" + ipS + "/serveur";
        WWW www = new WWW(url);
        yield return new WaitForSeconds(3);
        Debug.Log(www.data);
        var N = JSON.Parse(www.data);
        string ipAddress = N["ip"];
        SelectMap = Int32.Parse(N["map"]);
        // yield return new WaitForSeconds(1);
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        Application.LoadLevel(Levels[SelectMap]);
        StartCoroutine("Start_Socket");
        StartCoroutine(sendip(ipAddress));
        StartCoroutine(slow());

    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            StartCoroutine(SetupMenuSceneButtons());
        }
        else
        {
               StartCoroutine(slow());
            SetupOtherSceneButtons();
        }
    }
    IEnumerator Start_Socket()
    {
        yield return new WaitForSeconds(1);
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("choixClient", toto);
        soc = true;
    }
    void Update()
    {
        if(soc)
        StartCoroutine("calls");
       
    }
    void sendIp()
    {
        string ip = Network.player.ipAddress;
        string url = "http://"+ ipS + "/host?ip=" + ip;
        WWW www = new WWW(url);
    }

    IEnumerator getData()
    {
        string url = "http://"+ ipS + "/serveur";
        WWW www = new WWW(url);
        yield return new WaitForSeconds(3);
        Debug.Log(www.data);
        var N = JSON.Parse(www.data);
     
        string ipAddress = N["ip"];
        SelectMap = Int32.Parse(N["map"]);
       // yield return new WaitForSeconds(1);

        NetworkManager.singleton.networkAddress = ipAddress;
        SetPort();
        NetworkManager.singleton.StartClient();
        Application.LoadLevel(Levels[SelectMap]);
        StartCoroutine("Start_Socket");
        StartCoroutine(sendip(ipAddress));
        StartCoroutine(slow());

    }

    IEnumerator sendip(string ipAddress)
    {
         yield return new WaitForSeconds(1);

        GameObject.Find("ButtonDisconnect").GetComponent<disconnect>().ipD = ipAddress;
    }

   public void gameRandom()
    {
        StartCoroutine(getData());
       
    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void select()
    {
        pref = 0;
        StartCoroutine(fin());
    }
    public void select1()
    {
        pref = 2;
        StartCoroutine(fin());
    }
    public void select2()
    {
        pref = 1;
        StartCoroutine(fin());
    }
    public void select3()
    {
        pref = 3;
        StartCoroutine(fin());
    }
    public void select4()
    {
        pref = 4;
        StartCoroutine(fin());
    }
    IEnumerator fin()
    {
        StartCoroutine("calls_send");
        yield return new WaitForSeconds(0.55f);
        ClientScene.AddPlayer(client.connection, 0);
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Buttons");

        foreach (GameObject i in Buttons)
        {
            i.SetActive(false);
        }

    }

    IEnumerator slow()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("Button").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(select);
        GameObject.Find("Button1").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button1").GetComponent<Button>().onClick.AddListener(select1);
        GameObject.Find("Button2").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button2").GetComponent<Button>().onClick.AddListener(select2);
        GameObject.Find("Button3").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button3").GetComponent<Button>().onClick.AddListener(select3);
        GameObject.Find("Button4").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Button4").GetComponent<Button>().onClick.AddListener(select4);
        GameObject[] Spawners = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject i in Spawners)
        {
            switch (i.name)
            {
                case "Mario(Clone)":
                    GameObject.Find("Button").GetComponent<Button>().interactable = false;
                    break;
                case "Yoshi(Clone)":
                    GameObject.Find("Button1").GetComponent<Button>().interactable = false;
                    break;
                case "Peach(Clone)":
                    GameObject.Find("Button2").GetComponent<Button>().interactable = false;
                    break;
                case "Donkey Kong(Clone)":
                    GameObject.Find("Button3").GetComponent<Button>().interactable = false;
                    break;
                case "Luigi(Clone)":
                    GameObject.Find("Button4").GetComponent<Button>().interactable = false;
                    break;

            }
        }


    }





    IEnumerator SetupMenuSceneButtons()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

        GameObject.Find("gameRandom").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("gameRandom").GetComponent<Button>().onClick.AddListener(gameRandom);
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {

        GameObject player = (GameObject)Instantiate(spawners[pref]);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

}
