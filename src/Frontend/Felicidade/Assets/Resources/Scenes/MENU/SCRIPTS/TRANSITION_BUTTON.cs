using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TRANSITION_BUTTON : MonoBehaviour
{


    public AudioSource MusicaMenu;
    public bool jogar = false;
    public static bool creditos = false;
    public bool voltar = false;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

        if ((this.gameObject.transform.position.y < 1.25 && this.gameObject.name == "Jogar_Button") || (this.gameObject.transform.position.y < 0.35 && this.gameObject.name == "Creditos_Button"))
        {
            this.gameObject.transform.Translate(0f, 0.0001f, 0f);
        }
        if (jogar == true)
        {     
            MusicaMenu.volume -= 0.0001f;
            GameObject.Find("Main Camera").transform.Translate(0f, 0f, 0.001f);

            GameObject.Find("Voltar_Button").transform.position = new Vector3(1000f, 0f, 0f);
            GameObject.Find("CREDITOS").transform.position = new Vector3(1000f, 0f, 0f);

            if (GameObject.Find("Main Camera").transform.position.x > 10)
            {
                SceneManager.LoadScene("MapaPrincipal");
            }
        }else if(creditos == true)
        {
            if (GameObject.Find("Main Camera").transform.position.x < 5 && voltar == false)
            {

                GameObject.Find("LUZGIRA").transform.rotation = Quaternion.Euler(0f, -90f, 0f);

                GameObject.Find("Main Camera").transform.Translate(0f, 0f, 0.001f);
            }
            else if(voltar)
            {
                GameObject.Find("Main Camera").transform.Translate(0f, 0f, -0.01f);
                if(GameObject.Find("Main Camera").transform.position.x < -4.2)
                {
                    voltar = false;
                    creditos = false;
                }
            }

        }

    }

    void OnMouseDown()
    {
        if (this.gameObject.name == "Jogar_Button") { jogar = true; } else if (this.gameObject.name == "Creditos_Button") { creditos = true; } else{voltar = true; }
        
        
    }
}
