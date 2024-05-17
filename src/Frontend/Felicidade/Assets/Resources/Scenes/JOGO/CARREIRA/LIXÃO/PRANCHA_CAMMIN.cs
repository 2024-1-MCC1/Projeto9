using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PRANCHA_CAMMIN : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool PRANCHA = false;
    public static GameObject JOGADOR;

    private float X = -5.2f;
    private float Y = 2.15f;
    private float Z = -1;
    void Start()
    {
        JOGADOR = GameObject.Find("Scavenger Variant");
    }

    // Update is called once per frame
    void Update()
    {
        if (PRANCHA)
        {
            JOGADOR.GetComponent<CharacterController>().enabled = false;
            if (CAMMIN.volta)
            {
                X = Mathf.Abs(X) * (-1);
                Z = Mathf.Abs(X) * (-1);
            }
            else
            {
                X = Mathf.Abs(X);
                Z = Mathf.Abs(X);
            }
            JOGADOR.transform.position = new Vector3(GameObject.Find("PRANCHA").transform.position.x + X, GameObject.Find("PRANCHA").transform.position.y + Y, GameObject.Find("PRANCHA").transform.position.z + Z);
        }
        if (PRANCHA && Input.GetKeyDown(KeyCode.Space))
        {
            PRANCHA = false;
            JOGADOR.GetComponent<CharacterController>().enabled = true;
            
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Scavenger Variant")
        {
            PRANCHA = true;
        }
    }
}
