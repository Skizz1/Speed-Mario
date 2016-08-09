using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class local : MonoBehaviour {

    [SerializeField]
    GameObject[] spawners;

    // Use this for initialization
    public void playerLocal()
    {
        for (int i = 0; i < 4; i++)
        {
        //    Instantiate(spawners[0]);

        }
      //  Application.LoadLevel("game");

    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
