using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PROPRIEDADES_JOGADOR : MonoBehaviour
{
    public static float fome = 100f;
    public static float sede = 100f;
    public static float energia = 100f;
    public static float respeito = 0f;
    public static float pessoasNoMovimento = 0f;

    public static float dinheiro = 1500f;
	public static GameObject desentopi;
	public static GameObject fertilizar;

    public static GameObject TELA;
    public static GameObject TELA_CONVERSA;
	public static GameObject MenuPausa;
    private static bool auxConversa = true;

    private static float imposto = 100;
    private static float tempoParaPagarImposto = 200;
	private static bool taLento = false;
	private bool entrouNoMenu = true;

    public static Missoes MISSAO;

    //---------------------CHUVA----------------------
    public static GameObject CHUVA;
    public static float TEMPO;
    public static bool CHOVEU = false;

    //---------------------RECURSOS DA SETA DOS OBJETIVOS-------------------------
    public static GameObject SETA;
    public static GameObject JOGADOR;
    public static float DISTANCIA = 9999999999999999;
    public static float DISTANCIA1 = 0f;
    public static GameObject OBJETIVO;

    //---------------------RECURSOS DO PESO--------------------------------------

    public static float PESOATUAL = 0f;
    public static float PESOLIMITE = 20f;
    public static float constPeso = 30;

    //-------------------BARRA_FAIXAS-----------------------------

    public static GameObject FAIXAJ;

    //----------------------------------------CONVENCER O NPC A ENTRAR NO MOVIMENTO----------------------------------
    public static GameObject CONV;

    //private Vector3 posicaoJogador = new Vector3(-222.397598f, 70.8499985f, 9.6088028f);
    // Update is called once per frame
    void Start()
    {
		fertilizar = GameObject.Find("Fertilizar");
		fertilizar.GetComponent<Image>().fillAmount = 0f;
		fertilizar.SetActive(false);
        SETA = GameObject.Find("SETA");
		desentopi = GameObject.Find("Desentupir");
		desentopi.SetActive(false);
        TELA = GameObject.Find("ATRIBUTOS");
        TELA_CONVERSA = GameObject.Find("DIALOGO");
        JOGADOR = GameObject.Find("Scavenger Variant");
		MenuPausa = GameObject.Find("MenuPausa");
        MenuPausa.SetActive(false);
        TELA.SetActive(false);

        MISSAO = GameObject.Find("MissaoGameObject").GetComponent<Missoes>();

        FAIXAJ = GameObject.Find("BARRA_FAIXAS");
        FAIXAJ.SetActive(false);
        CONV = GameObject.Find("CONVENCER");
        CHUVA = GameObject.Find("Rain");
    }

    void Update()
    {
        PESOLIMITE = constPeso * (fome / 100) + 2*(constPeso/3);

        if (SC_FPSController.MODOCARREIRA == false)
        {
            if (auxConversa)
            {
                TELA_CONVERSA.SetActive(false);
                auxConversa = false;
            }

            if (CONV.activeSelf == false && JOGADOR.GetComponent<CharacterController>().enabled == false)
            {
                JOGADOR.GetComponent<CharacterController>().enabled = true;
                JOGADOR.GetComponent<SC_FPSController>().enabled = true;
            }
		if(Input.GetKey(KeyCode.Escape) || entrouNoMenu == false)
		{
			entrouNoMenu = false;
			MenuPausa.SetActive(true);
			Time.timeScale = 0;
			MenuPausado();
		}

        ATRIBUTOS_TEMPO();
		Imposto();

            if(SC_FPSController.animator.GetInteger("andando") == 0 && fome > 50 && sede > 50 && energia < 100)
            {
                energia += ((fome / 100) * 0.5f + (sede / 100) * 0.5f) * Time.deltaTime * 5;
            }

            if (Input.GetKeyDown(KeyCode.Q) && TELA_CONVERSA.activeSelf == false)
            {
      
                if (TELA.activeSelf)
                {
                    TELA.SetActive(false);
                }
                else
                {
                    TELA.SetActive(true);
                }
           
            }

            if (FAIXAJ.activeSelf)
            {
                TELA.SetActive(true);
            }

            if (TELA.activeSelf)
            {

                ATRIBUTOSNATELA();
              
                if (MISSAO.missaoLayout[0].activeSelf || MISSAO.missaoLayout[1].activeSelf || PESOATUAL > PESOLIMITE) { SETA.SetActive(true); } else { SETA.SetActive(false); }

            }
            else if (SETA.activeSelf)
            {
                SETA.SetActive(false);
            }

            if (OBJETIVO != null)
            {

                SETA.transform.LookAt(new Vector3(OBJETIVO.transform.position.x, OBJETIVO.transform.position.y, OBJETIVO.transform.position.z));
                REINICIAR_SETA();

             }   
        }
    }
    public static void CICLO_CHUVA()
    {
        if(TEMPO > 0)
        {
            TEMPO -= Time.deltaTime;
        }
        else
        {
            if (CHOVEU)
            {
                CHOVEU = false;
                CHUVA.SetActive(false);
                TEMPO = 120f;
            }
            else
            {
                if(UnityEngine.Random.Range(0,101) < 14)
                {
                    TEMPO = 120f;
                    CHOVEU = true;
                    CHUVA.SetActive(true);
                }
                else
                {
                    TEMPO = 120f;
                }
            }
        }
        if (CHOVEU)
        {
            SC_FPSController.walkingSpeed = SC_FPSController.walkingSpeed - 0.1f;
            PESOLIMITE = PESOLIMITE - 5f;
        }
    }
    public static void ATRAIR_SETA(GameObject OBJETO1)
    {
        DISTANCIA1 = Mathf.Sqrt(((OBJETO1.transform.position.x - JOGADOR.transform.position.x) * (OBJETO1.transform.position.x - JOGADOR.transform.position.x)) + ((OBJETO1.transform.position.z - JOGADOR.transform.position.z) * (OBJETO1.transform.position.z - JOGADOR.transform.position.z))) / 14;
        
        if (DISTANCIA > DISTANCIA1)
        {
            OBJETIVO = OBJETO1;
            DISTANCIA = DISTANCIA1;

        }
    }

    public static void REINICIAR_SETA()
    {
        OBJETIVO = null;
        DISTANCIA = 9999999999999999;
    }

    private void Imposto ()
    {

        if(Time.time > tempoParaPagarImposto)
        {
            tempoParaPagarImposto += Time.time;
            dinheiro -= imposto;
            Debug.Log($"O tempo passou e tá na hora de pagar 100 Reals para o prefeito Naconta, você ficou com {dinheiro}");
        }

    }
    public void ATRIBUTOS_TEMPO()
    {

        fome -= fome > 0 ? Time.deltaTime * 0.1f : 0;
        sede -= sede > 0 ? Time.deltaTime * 0.1f : 0;
        energia -= energia > 0 ? Time.deltaTime * 0.1f * (100 / (sede * 0.5f)) : 0;

        fome = fome > 100 ? 100 : fome;
        sede = sede > 100 ? 100 : sede;
    }

    public void ATRIBUTOSNATELA()
    {

        GameObject.Find("PESO").GetComponent<TMP_Text>().text = "PESO     " + (int)PESOATUAL + "/" + (int) PESOLIMITE + "KG";
        GameObject.Find("DINHEIROTEXT").GetComponent<TMP_Text>().text = dinheiro.ToString();
        GameObject.Find("PREENCHIMENTOFOME").GetComponent<RectTransform>().localPosition = new Vector3(0f, (fome / 100.0f) * 100 - 100, 0f);
        GameObject.Find("PREENCHIMENTO_SEDE").GetComponent<RectTransform>().localPosition = new Vector3(0f, (sede / 100.0f) * 100 - 100, 0f);
        GameObject.Find("PREENCHIMENTO_ENERGIA").GetComponent<RectTransform>().localPosition = new Vector3(0f, (energia / 100.0f) * 100 - 100, 0f);
        GameObject.Find("PREENCHIMENTO_RESPEITO").GetComponent<RectTransform>().localPosition = new Vector3((respeito / 100) * 1750 - 1750, 0f, 0f);
        GameObject.Find("NumeroPessoasMovimento").GetComponent<TMP_Text>().text = pessoasNoMovimento.ToString();

    }
	public void MenuPausado()
	{
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
		if(Input.GetButtonDown("Fire1"))
		{
			entrouNoMenu = true;
			MenuPausa.SetActive(false);
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}else if(Input.GetButtonDown("Fire1") && EventSystem.current.currentSelectedGameObject.name == "VoltaMenu")
		{
			entrouNoMenu = true;
			MenuPausa.SetActive(false);
			Time.timeScale = 1;
			SceneManager.LoadScene("Menu");
		}
	}


}
