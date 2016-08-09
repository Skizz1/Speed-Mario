#pragma strict
import UnityEngine.Networking;
import UnityEngine.UI;
var prefab1:GameObject;
var prefab2:GameObject;
var test:GameObject;
private var spawners = new Array();


function Start () {
   // Network.Connect("localhost", 8080);
    var spawner = GameObject.FindGameObjectsWithTag("Spawn");
    spawners.push(prefab1);
    var rand:int = Random.Range(0, spawners.length);
    //var thePlayer:GameObject = GameObjectInstantiate(spawners[rand], new Vector3(0, 0, -15), Quaternion.identity);
    //NetworkServer.AddPlayerForConnection("localhost", spawners[rand], 0);
    //Instantiate(spawners[rand]);
}

function Update () {

}