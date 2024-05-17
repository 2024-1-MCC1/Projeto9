using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CAMMIN : MonoBehaviour, IInteractable
{
    public GameObject CAMINHAO;
    public TMP_Text TEXTOC;
    public float LIXOC = 0f;

    public static bool volta = false;

    public int START = 0; //TESTA SE O JOGADOR TENTOU INTERAGIR COM O CAMINHÃO
    // Start is called before the first frame update
    public static bool HISTORIA = true;

    public static GameObject PRANCHA;
    public void Interact()
    {
        LIXOC += PROPRIEDADES_JOGADOR.PESOATUAL;
        PROPRIEDADES_JOGADOR.PESOATUAL = LIXOC > 100 ? LIXOC - 100 : 0;
        LIXOC = LIXOC > 100 ? 100 : LIXOC;
        TEXTOC.text = ((int)((LIXOC / 100) * 100)) + "%";
        

        if (START == 0)
        {
            START = 1;
        }
        
    }

    void Start()
    {
        CAMINHAO = this.gameObject;
        TEXTOC = GameObject.Find("TEXTOC").GetComponent<TMP_Text>();
        PRANCHA = GameObject.Find("PRANCHA");
        PRANCHA.GetComponent<BoxCollider>().enabled = false;
        TEXTOC.text = "0%";
    }

    // Update is called once per frame
    void Update()
    {
        if (HISTORIA)
        {

            
            if (START == 1)
            {
                START += ORQUESTRA.DICAS("Deposite o lixo acumulado do lixão dentro do caminhão. Você pode ver o nível de carga dele na lateral esquerda do veículo.");
            }else if(START == 0)
            {
                PROPRIEDADES_JOGADOR.ATRAIR_SETA(CAMINHAO);
            }

            if (volta == false)
            {
                if (LIXOC >= 100) //CAMINHÃO VAI EMBORA
                {
                    CAMINHAO.transform.Translate(-2f, 0f, 0f);
                    if (CAMINHAO.transform.position.x < -400f)
                    {
                        REINICIAR();
                        volta = true;
                    }
                }
            }
            else
            {
                CAMINHAO.transform.Translate(2f, 0f, 0f);
                if (START == 2)
                { START++; }
                if (CAMINHAO.transform.position.x > 122f)
                {
                    volta = false;
                }
            }
            if (START == 3)
            {
                START += ORQUESTRA.DICAS("O caminhão voltará em breve. Continue acumulando lixo.");
            }
            if (PROPRIEDADES_JOGADOR.PESOATUAL >= PROPRIEDADES_JOGADOR.PESOLIMITE)
            {
                PROPRIEDADES_JOGADOR.ATRAIR_SETA(CAMINHAO);
            }
        }
        else
        {
            if (PROPRIEDADES_JOGADOR.PESOATUAL >= PROPRIEDADES_JOGADOR.PESOLIMITE)
            {
                PROPRIEDADES_JOGADOR.ATRAIR_SETA(CAMINHAO);
            }

            if (CAMINHAO.transform.position.x < -460f)
            {
                REINICIAR();
                volta = true;
            }else if(CAMINHAO.transform.position.x > -122f)
            {
                volta = false;
            }

            if (volta)
            {
                CAMINHAO.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
                CAMINHAO.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                CAMINHAO.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
                CAMINHAO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
    public void REINICIAR()
    {
        LIXOC = 0;
        TEXTOC.text = "0%";
    }
}
