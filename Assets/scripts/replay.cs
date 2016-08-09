using UnityEngine;
using System.Collections;

public class replay : MonoBehaviour
{
    private bool fin;
    private bool start;
    private int cmpt = 0;
    private float cmptSpawn = 0;
    private float cmptSpawnY = 2;
    private GameObject[] spawners;
    private GameObject[] spawnersDestroy;
    public GameObject menuScore;
    private int compt = 0;
    private int comptResapwn = 3;

    private int menuSizeX = Screen.width - 170;
    private int comptPartie = 0;

    private int menuSizeY = 342;
    private float menuPosX = 90;
    [SerializeField]
    public GameObject[] textgameobject;


    void Start()
    {
        start = false;
        fin = true;
        menuScore.active = false;


    }
    void OnGUI()
    {

        GUI.backgroundColor = new Color(1, 1, 1, 10f);
        GUI.backgroundColor = Color.white;
        GUI.color = Color.black;

        spawners = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject j in spawners)
        {
            PlayerPrefs.GetInt(j.name);
        }
    }

    void Update()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawn");
        cmpt = spawners.Length;

        foreach (GameObject j in spawners)
        {
            bool result = j.GetComponent<player>().isActiveAndEnabled;
            if (!result)
            {
                cmpt--;
            }
            if (cmpt > 1 && fin == true)
            {
                start = true;
                fin = false;

            }
            if (start && cmpt < 2)
            {
                comptPartie++;

                if (comptPartie == 3)
                {
                    PlayerPrefs.DeleteAll();
                    comptPartie = 0;

                }

                StartCoroutine(slow());
                start = false;
                fin = true;

            }
        }

    }
    IEnumerator score(GameObject i)
    {
        yield return new WaitForSeconds(3);

        cmptSpawn -= 1.5f;
  

        i.transform.position = new Vector3(cmptSpawn, cmptSpawnY, 0);
        i.transform.gameObject.tag = "Spawn";

        i.GetComponent<SpriteRenderer>().enabled = true;
        i.GetComponent<player>().enabled = true;
        comptResapwn += 2;
        cmptSpawnY += 2;
    }
    IEnumerator slow()
    {
        //GUI.backgroundColor = new Color(1, 1, 1, 10f);
        //GUI.backgroundColor = Color.white;
        //GUI.color = Color.black;

        spawners = GameObject.FindGameObjectsWithTag("Spawn");
        spawnersDestroy = GameObject.FindGameObjectsWithTag("destroy");


        foreach (GameObject j in spawners)
        {
            PlayerPrefs.SetInt(j.name, PlayerPrefs.GetInt(j.name) + 1);
            textgameobject[compt].GetComponent<UnityEngine.UI.Text>().text = j.name.Substring(0, j.name.Length - 7) + " "+ PlayerPrefs.GetInt(j.name) + " point";
            compt++;
            StartCoroutine(score(j));
        }
        foreach (GameObject i in spawnersDestroy)
        {
            textgameobject[compt].GetComponent<UnityEngine.UI.Text>().text = i.name.Substring(0, i.name.Length -7) + " " + PlayerPrefs.GetInt(i.name) + " point";
            compt++;
            StartCoroutine(score(i));
        }
        compt = 0;

        menuScore.active = true;
        yield return new WaitForSeconds(5);
        menuScore.active = false;
        cmptSpawn = 0;

        cmptSpawnY = 0;
        GameObject.Find("Camera").GetComponent<camera_suivie_c>().bol = true;


    }
}
