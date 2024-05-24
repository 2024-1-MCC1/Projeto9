using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CICLICIDADE : MonoBehaviour
{
    public static bool ATIVO = false;

    public static int ESCOLHA = -10;
    public static int auxESCOLHA = -10;
    public static int ObjetivoFinal = 0;
    public static int Progresso = 0;

    public static string TITULO;
    public static string ENUNCIADO;

    public static GameObject LAYOUT;

    private float tempo;

    private int DICA = 0;

    private float tempAux = 1f;

    // Start is called before the first frame update
    void Start()
    {
        LAYOUT = GameObject.Find("MissaoLayoutSecundario");
    }

    // Update is called once per frame
    void Update()
    {
        if (LAYOUT == null)
        {
            if (tempAux > 0)
            {
                tempAux -= Time.deltaTime;
            }
            else if (LAYOUT.activeSelf)
            {
                PROPRIEDADES_JOGADOR.TELA.SetActive(true);
                LAYOUT = GameObject.Find("MissaoLayoutSecundario");
                LAYOUT.SetActive(false);
                PROPRIEDADES_JOGADOR.TELA.SetActive(false);
                  
            }
        }

        if (ATIVO)
        {

            if(auxESCOLHA == 2 && DICA == 0)
            {
                DICA += ORQUESTRA.DICAS("Para varrer objetos pequenos, PRESSIONE 'V' para pegar a vassoura.");
            }

            if (ESCOLHA == -10)
            {
                LAYOUT.SetActive(true);
                ESCOLHA = Random.Range(0,3);
                auxESCOLHA = ESCOLHA;
            }
            
            if(ESCOLHA == 0)
            {
                TITULO = "Mais que bagunça!";
                ENUNCIADO = "Colete Sacos de Lixo";
                ObjetivoFinal = Random.Range(3,11);
                Progresso = 0;
                ESCOLHA = -5;
            }else if(ESCOLHA == 1)
            {
                TITULO = "Cheio de Entulho!";
                ENUNCIADO = "Acabe com as Pilhas de Lixo";
                ObjetivoFinal = Random.Range(1, 3);
                Progresso = 0;
                ESCOLHA = -5;
            }
            else if(ESCOLHA == 2)
            {
                TITULO = "Eu vou varrendo!";
                ENUNCIADO = "Varra as ruas da cidade";
                ObjetivoFinal = Random.Range(200, 300);
                Progresso = 0;
                ESCOLHA = -5;
            }

            //----------------------------------------------

            if(Progresso >= ObjetivoFinal && Progresso != 0)
            {
                Missoes.MissaoConcluida.Play(0);

                Invoke("EsconderConclusao", 2);
                PROPRIEDADES_JOGADOR.dinheiro += 30;
                PROPRIEDADES_JOGADOR.respeito += 0.5f;
                ESCOLHA = -10;
            }
            if (LAYOUT.activeSelf && PROPRIEDADES_JOGADOR.TELA.activeSelf)
            {
                GameObject.Find("TituloSecundario").GetComponent<TMP_Text>().text = TITULO;
                GameObject.Find("textoMissaoSecundario").GetComponent<TMP_Text>().text = ENUNCIADO + "(" + Progresso + "/" + ObjetivoFinal + ")";
            }
            
            if(tempo > 0)
            {
                tempo -= Time.deltaTime;
            }
            else if(tempo < 0 && ESCOLHA == -5)
            {
                ESCOLHA = -10;
            }
        }
        else
        {
            LAYOUT.SetActive(false);
        }
    }
    private void EsconderConclusao()
    {
        Missoes.missaoConcluida.SetActive(false);
    }
}
