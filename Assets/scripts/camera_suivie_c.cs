using UnityEngine;
using System.Collections;

public class camera_suivie_c : MonoBehaviour
{

    [SerializeField]
    private GameObject[] spawners;
    private GameObject[] ScoreMenu;
    public float positionx;
    public float positiony;
    public float maxx;
    public GameObject refe;
    public bool bol = true;
    public GameObject Des;
    private int cmptScore = 0;

    void Start()
    {
        ScoreMenu = GameObject.FindGameObjectsWithTag("Spawn");
    }

    void Update()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject i in spawners)
        {
            positionx = i.transform.position.x;
            positiony = i.transform.position.y;

            if (bol == true || (positiony > -6 && positionx >= maxx) || (positiony <= -6 && positionx <= maxx))
            {
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                refe = i;
                maxx = positionx;
                bol = false;
            }

            if ((positionx < refe.transform.position.x - 15 && positiony > -6) || (positionx > refe.transform.position.x + 15 && positiony < -6))
            {
                Debug.Log("Je suis morrrrrrrrrrrrrtttttttttttttttttttttt 1111111111111");
                Debug.Log("if 1 " + (positionx < refe.transform.position.x - 15 && positiony > -6));
                Debug.Log("if 2 " + (positiony > refe.transform.position.x + 15 && positiony < -6));
                //i.transform.gameObject.tag = "destroy";
                //i.GetComponent<SpriteRenderer>().enabled = false;
                //i.GetComponent<player>().enabled = false;

                //Instantiate(Des, i.transform.position, Quaternion.identity);
            }

            if ((positiony < refe.transform.position.y - 10) || (positiony > refe.transform.position.y + 10))
            {
                Debug.Log("Je suis morrrrrrrrrrrrrtttttttttttttttttttttt 22222222222222222");
                //i.transform.gameObject.tag = "destroy";
                //i.GetComponent<SpriteRenderer>().enabled = false;
                //i.GetComponent<player>().enabled = false;

                //Instantiate(Des, i.transform.position, Quaternion.identity);
            }

        }
        cmptScore = 0;
        if (refe != null)
        {
            transform.position = refe.transform.position + new Vector3(0, 0, -15);
        }

    }
}
