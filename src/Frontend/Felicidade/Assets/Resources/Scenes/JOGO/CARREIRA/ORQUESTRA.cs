using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ORQUESTRA : MonoBehaviour
{
    public static bool COMECO = false;
    public static bool COMECO2 = false;
    public static bool LIXAO = false;


    public static TMP_Text TEXTO;

    public static GameObject JOGADOR;
    public static Image escuro;
    public static float escurecer = 0f;

    public static bool[] SEQUENCIA = new bool[8];

    public static List<string> PALAVRAS = new List<string>();

    private static TMP_Text DICA;
    public static float tempoT = 0f; //TEMPO DA MENSAGEM DO TUTORIAL
    private static bool auxtempoT = true; //AUXILIA O CICLO DE TEMPO DA MENSAGEM DO TUTORIAL

    public static float tempoPALAVRAS = 5f;
    public static float tempoLIMITEPALAVRAS = 5f; //DEFINE O TEMPO MÁXIMO PARA CADA FRASE APARECER NA TELA
    public static float tempoSAIRPALAVRAS = 5f; //DEFINE O TEMPO PARA A FRASE SAIR DA TELA

    public static int guiaPALAVRAS = 0;
    public static bool auxESMAECER = false; //ANIMAÇÃO DE MUDANÇA DE TRANSPARÊNCIA

    public static AudioSource SONG;
    
    
    // Start is called before the first frame update
    void Start()
    {
        escuro = GameObject.Find("ESCURO").GetComponent<Image>();
        JOGADOR = GameObject.Find("Scavenger Variant");
        TEXTO = GameObject.Find("CARREIRA_TEXTO").GetComponent<TMP_Text>();

        DICA = GameObject.Find("DICA").GetComponent<TMP_Text>();

        GameObject.Find("I-INFORMACAO").GetComponent<Image>().color = new Color(GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.r, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.g, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.b, 0);
        GameObject.Find("FundoTutorial").GetComponent<Image>().color = new Color(GameObject.Find("FundoTutorial").GetComponent<Image>().color.r, GameObject.Find("FundoTutorial").GetComponent<Image>().color.g, GameObject.Find("FundoTutorial").GetComponent<Image>().color.b, 0);

        SONG = GameObject.Find("SOM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void REINICIAR()
    {

        for(int P = 0; P < SEQUENCIA.Length; P++)
        {
            SEQUENCIA[P] = false;
        }

        PALAVRAS.Clear();
        tempoPALAVRAS = 5f;
        tempoLIMITEPALAVRAS = 5f;
        tempoSAIRPALAVRAS = 5f;
        guiaPALAVRAS = 0;
        auxESMAECER = false;

        SC_FPSController.runningSpeed = 2f;
        SC_FPSController.walkingSpeed = 1f;
        TEXTO.alpha = 0;

    }

    public static int ESCURECER(bool VALOR)
    {
        if (VALOR && escurecer < 1)
        {
            escurecer += Time.deltaTime*0.1f;
            escuro.color = new Color(0f,0f,0f,escurecer);
            return 0;
        }
        else if(VALOR == false && escurecer > 0)
        {
            escurecer -= Time.deltaTime*0.1f;
            escuro.color = new Color(0f, 0f, 0f, escurecer);
            return 0;
        }
        else
        {
            if(escurecer <= 0 || escurecer >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public static void CICLO_TEXTO()
    {

        //------------ARTICULA tempoPALAVRAS---------------
        if (auxESMAECER)
        {
            tempoPALAVRAS += Time.deltaTime;
        }
        else
        {
            tempoPALAVRAS -= Time.deltaTime;

        }
        //--------------------------------------------------

        TEXTO.alpha = (tempoPALAVRAS / tempoLIMITEPALAVRAS) * 5;


        if (tempoPALAVRAS < 0)
        {
            tempoPALAVRAS = 0;
            if (guiaPALAVRAS == PALAVRAS.Count) //EVITA CONFLITOS COM O TAMANHO DA LISTA 
            {
                guiaPALAVRAS = 0;
                auxESMAECER = false;
            }
            TEXTO.text = PALAVRAS[guiaPALAVRAS];
            guiaPALAVRAS++;
            tempoLIMITEPALAVRAS = 5f;
            auxESMAECER = true;
        }
        else if (tempoPALAVRAS > tempoLIMITEPALAVRAS && guiaPALAVRAS != 0)
        {

            tempoPALAVRAS = tempoSAIRPALAVRAS;
            auxESMAECER = false;
        }


    }

    public static int DICAS(string TEXTO)
    {
        if(DICA.text != TEXTO)
        {
            auxtempoT = true;
            DICA.text = TEXTO;
        }

        if (auxtempoT)
        {
            tempoT += Time.deltaTime;
        }
        else
        {
            tempoT -= Time.deltaTime;
        }

        if(tempoT < 0)
        {
            tempoT = 0;
            DICA.text = "";
            DICA.alpha = 0;
            GameObject.Find("I-INFORMACAO").GetComponent<Image>().color = new Color(GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.r, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.g, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.b, 0);
            GameObject.Find("FundoTutorial").GetComponent<Image>().color = new Color(GameObject.Find("FundoTutorial").GetComponent<Image>().color.r, GameObject.Find("FundoTutorial").GetComponent<Image>().color.g, GameObject.Find("FundoTutorial").GetComponent<Image>().color.b, 0);
            return 1;
        }else if(tempoT > 10)
        {
            auxtempoT = false;
        }

        GameObject.Find("I-INFORMACAO").GetComponent<Image>().color = new Color(GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.r, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.g, GameObject.Find("I-INFORMACAO").GetComponent<Image>().color.b,tempoT);
        GameObject.Find("FundoTutorial").GetComponent<Image>().color = new Color(GameObject.Find("FundoTutorial").GetComponent<Image>().color.r, GameObject.Find("FundoTutorial").GetComponent<Image>().color.g, GameObject.Find("FundoTutorial").GetComponent<Image>().color.b, tempoT - 0.2f);
        DICA.alpha = tempoT;
        return 0;
        
    }
}
