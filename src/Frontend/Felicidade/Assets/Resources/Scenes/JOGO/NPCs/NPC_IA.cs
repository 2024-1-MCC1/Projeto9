using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPC_IA : MonoBehaviour, IInteractable
{

    private GameObject NPC;
    //------------------VARIÁVEIS PARA O DIÁLOGO---------------------

    public string[] dialogo = new string[3];
    public string[] falante = new string[3];
	public bool temMissao = false;
	public bool anda = true;
	public int ultimaMissao1ou2 = 1;

    private TMP_Text Falante;
    private TMP_Text Conversa;

    private int andamentoDaConversa = 0; //POSIÇÃO DO DIÁLOGO ATUAL
    private int pausaRegistroDialogo = -10; //SERVE PARA INTERROMPER AS REPETIDAS EXECUÇÕES DE ATUALIZAÇÃO DOS TEXTOS DO | OBS.: -10 É UM VALOR QUALQUER E DIFERENTE DE 0

    private GameObject JOGADOR;
    //-------------VARIÁVEIS PARA O MOVIMENTO DA IA---------------------------
    public float velocidade = 1f;
	private float cont = 0f;
    private int direction = 0;
    public float cansaco = 0f; //DEFINE O TEMPO DE CAMINHADA DO NPC
    private float recuperacao = 0f; //TEMPO DE RECUPERAÇÃO PÓS-CAMINHADA
    private bool cicloAndar = false; //Auxilia na execução do ciclo de caminhada do objeto em função do tempo
    public bool andando = true; //Define se o objeto pode andar
	private SistemaNPCPrincipal sistemaNPCPrincipal;

	private float rastroX;
    private float rastroZ;

    public bool objetivo = false;

    private bool conversando = false;

    private Missoes MissaoObjeto;

    //--------------------CONVENCER NPC--------------------------
    public int CHANCE = 25;
    public int GATILHO;
    public bool TENTATIVA = false;

    //--------------------DOENÇAS-------------------
    private bool LIBERADOENTE = false;
    private bool DOENTE = false;
    public void Interact()
    {

        if (PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf == false && this.andamentoDaConversa != -10)
        {
            if(Random.Range(0,101) < CHANCE - 1)
            {
                CHANCE -= 10;
                GATILHO = Random.Range(0, falante.Length-1);
                TENTATIVA = true;
            }
            if(LIBERADOENTE)
            {
                if (Random.Range(0, 101) < 24)
                {
                    DOENTE = true;
                }
                else
                {
                    DOENTE = false;
                }
            }

            PROPRIEDADES_JOGADOR.TELA_CONVERSA.SetActive(true);
            PROPRIEDADES_JOGADOR.TELA.SetActive(false);
            Interactor.LEGENDA.SetActive(false);
            this.conversando = true;
            this.andando = false;

            SC_FPSController.canMove = false;
            if (NPC.gameObject.name != "PREFEITO")
            { NPC.transform.forward = JOGADOR.transform.forward * (-1); } //VIRA O NPC EM DIREÇÃO AO JOGADOR}

            if (PROPRIEDADES_JOGADOR.MISSAO.tipoDeMissao[0] == 1 && objetivo)
            {
                PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[0]++;
                PROPRIEDADES_JOGADOR.MISSAO.textoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual].SetText(PROPRIEDADES_JOGADOR.MISSAO.textoObjetivoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual] + $"({PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual]}/{PROPRIEDADES_JOGADOR.MISSAO.objetivoFinalMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual]})");
                PROPRIEDADES_JOGADOR.MISSAO.CheckDaMissao();
                objetivo = false;
            }

        }
        else if(this.andamentoDaConversa == -10)
        {
            this.andamentoDaConversa = 0;
        }
        
    }

    private void Start()
    {
        MissaoObjeto = GameObject.Find("MissaoGameObject").GetComponent<Missoes>();

        JOGADOR = GameObject.Find("Scavenger Variant");
        NPC = this.gameObject;
		
		if(anda == false)
		{
			velocidade = 0;
		}
		
		sistemaNPCPrincipal = this.gameObject.GetComponent<SistemaNPCPrincipal>();

        JOGADOR = GameObject.Find("Scavenger Variant");

        Falante = GameObject.Find("FALANTE").GetComponent<TMP_Text>();
        Conversa = GameObject.Find("CONVERSA").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        LIBERADOENTE = LIXAO.LIBERADOENCA;
        if (DOENTE)
        {
            PROPRIEDADES_JOGADOR.fome -= 50;
            PROPRIEDADES_JOGADOR.sede -= 50;
            ORQUESTRA.SONG.clip = Resources.Load<AudioClip>("Scenes/JOGO/AUDIO/TOSSE 1");
            ORQUESTRA.SONG.Play();
            DOENTE = false;
        }
        //Debug.Log(andando);
        if (andando)
        {
            ANDAR();
        }
        if (PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf && andando == false && this.conversando)
        {
            //Debug.Log(this.gameObject.name);
            CONVERSANDO();
        }

        if(objetivo)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }

    }
    //-----------------------------------------------------------
    void ANDAR()
    {
		if (cont == 0f) // REINICIA A VERIFICAÇÃO DE COLISÃO DO NPC
        {
            rastroX = NPC.transform.position.x;
            rastroZ = NPC.transform.position.z;
        }

        if(cicloAndar == false)
        {
            cansaco = UnityEngine.Random.Range(1, 11);
            recuperacao = UnityEngine.Random.Range(1, 11);
            direction = UnityEngine.Random.Range(0, 361);
            cicloAndar = true;
        }

        cansaco -= Time.deltaTime;

        if(cansaco < 0)
        { 
            recuperacao -= Time.deltaTime;
            if(recuperacao < 0)
            {
                cicloAndar = false;
            }
        }
        else
        {
            NPC.transform.rotation = Quaternion.Euler(0f, direction, 0f);

            NPC.transform.Translate(transform.TransformDirection(Vector3.forward) * velocidade * Time.deltaTime);
			cont += Time.deltaTime;						
        }
        if(cont >= 0.5f) // VERIFICA SE O NPC FICOU EMPERRADO
        {

            cont = 0f;

            if ((Mathf.Sqrt(Mathf.Pow(NPC.transform.position.x - rastroX, 2) + Mathf.Pow(NPC.transform.position.z - rastroZ, 2))) < velocidade * 0.375f)
            {
                direction = UnityEngine.Random.Range(0, 361);
            }

        }

    }

    public static void ATRAIR()
    {
        
    }


    private void CONVERSANDO()
    {
        if(this.andamentoDaConversa == -10)
        {
            this.andamentoDaConversa = 0;
        }
        if (this.andamentoDaConversa != this.pausaRegistroDialogo)
        {
			Falante = GameObject.Find("FALANTE").GetComponent<TMP_Text>();
			Conversa = GameObject.Find("CONVERSA").GetComponent<TMP_Text>();
            Falante.text = this.falante[this.andamentoDaConversa];
            Conversa.text = this.dialogo[this.andamentoDaConversa];
            this.pausaRegistroDialogo = this.andamentoDaConversa;
        }
        else
        {
            if (this.andamentoDaConversa > 0 && Input.GetKeyDown(KeyCode.Q))
            {
                this.andamentoDaConversa--;
            }
            else if (this.andamentoDaConversa < (this.dialogo.Length - 1) && Input.GetKeyDown(KeyCode.E))
            {
                this.andamentoDaConversa++;
            }
            else if (this.andamentoDaConversa == (this.dialogo.Length - 1) && Input.GetKeyDown(KeyCode.E))
            {
                if (temMissao)
                {
                    if (MissaoObjeto.idMissaoAtual[0] == this.gameObject.GetComponent<SistemaNPCPrincipal>().idMissao - ultimaMissao1ou2 && MissaoObjeto.taNaHoraPro == true)
                    {
                        Debug.Log("foimeo");
                        sistemaNPCPrincipal.Interacao();
                        temMissao = false;
                    }
                }
                SC_FPSController.canMove = true;
                if (this.anda) { this.andando = true; }
                this.conversando = false;
                this.andamentoDaConversa = -10;
                this.pausaRegistroDialogo = -10; //UM VALOR QUALQUER
                PROPRIEDADES_JOGADOR.TELA_CONVERSA.SetActive(false);
            }
        }
        if(this.andamentoDaConversa == GATILHO && TENTATIVA == true)
        {
            PROPRIEDADES_JOGADOR.CONV.SetActive(true);
            CONVENCER.REINICIAR();
            TENTATIVA = false;
        }
    }
}
