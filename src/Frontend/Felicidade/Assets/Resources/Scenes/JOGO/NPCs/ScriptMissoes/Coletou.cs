using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Coletou : MonoBehaviour, IInteractable
{
	private AudioSource Coleta;
    private Missoes missao;
	private GameObject Vassoura;
	private float speed = 2f;
	private float velocidadePreenchimento = 1f;
	private float sucesso = 0.05f;
	private bool taMovendo = false;
	private bool comecarEncher = false;
	private GameObject desentopi;
	private GameObject fertilizar;
	private bool terminou;
	private Material materiais;
	private bool instalou = false;
    public SistemaNPCPrincipal SistemaNPC;
    public SistemaNPCSecundario SistemaNPCSecundario;
    public int idMissao; //Usado para que o ID desse script seja igual ao ID do script "SistemaNPC", isso impede de que, ao realizar essas ações, o jogador não irá concluir objetivos de outras missões
    

    void Start()
    {
		materiais = GameObject.Find("CuboFertil").GetComponent<MeshRenderer>().material;
        Vassoura = GameObject.Find("Vassoura");
        if (this.gameObject.tag == "Arvore")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
        missao = GameObject.Find("MissaoGameObject").GetComponent<Missoes>();
		Coleta = GameObject.Find("Coletar").GetComponent<AudioSource>();
        
    }
	
	void Update()
	{

		if(this.gameObject.tag == "Arvore")
		{
		if((SistemaNPC.selecionarOqueFazer == 1 || SistemaNPCSecundario.selecionarOqueFazer == 1) && (missao.idMissaoAtual[SistemaNPC.tipoMissao] == idMissao || missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] == idMissao))
		{
			this.gameObject.GetComponent<BoxCollider>().enabled = true;
		}
		}
		
		if(comecarEncher && Input.GetKey(KeyCode.E))
		{
			fertilizar.SetActive(true);
			GameObject.Find("Fertilizar").GetComponent<Image>().fillAmount += velocidadePreenchimento * Time.deltaTime;
			if(GameObject.Find("Fertilizar").GetComponent<Image>().fillAmount >=1f)
			{
				comecarEncher = false;
				GameObject.Find("Fertilizar").GetComponent<Image>().fillAmount = 0f;
				GameObject.Find("Fertilizar").SetActive(false);
				Fertilizacao();
			}
		}else if(comecarEncher)
		{
			comecarEncher = false;
			GameObject.Find("Fertilizar").GetComponent<Image>().fillAmount = 0f;
			GameObject.Find("Fertilizar").SetActive(false);
		}
		
		if(taMovendo)
		{
			desentopi.SetActive(true);
			GameObject.Find("SliderBueiro").GetComponent<Slider>().value += speed * Time.deltaTime;
			if(GameObject.Find("SliderBueiro").GetComponent<Slider>().value >= 1f)
			{
				GameObject.Find("SliderBueiro").GetComponent<Slider>().value = 0;
			}
			if(Input.GetButtonDown("Fire1"))
			{
				taMovendo = false;
				ChecarResultado();
			}
		}
	}
    public void Interact()
    {
        //Coleta de Lixo
        if (missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 0)
        {
			PROPRIEDADES_JOGADOR.PESOATUAL += Random.Range(1,5);
            missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
            this.gameObject.SetActive(false);
			Coleta.Play(0);
            SistemaNPC.Coletou();
        }else
        if (missao.objetivoFinalMissao[1] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 0)
        {
            PROPRIEDADES_JOGADOR.PESOATUAL += Random.Range(1, 5);
            missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
            this.gameObject.SetActive(false);
			Coleta.Play(0);
            SistemaNPCSecundario.Coletou();
        }
		else
        //Arborização =1
        if (missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 1 && instalou == false)
        {
            missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
			this.gameObject.GetComponent<BoxCollider>().enabled = true;
			instalou = true;
			Coleta.Play(0);
            SistemaNPC.Coletou();
        }
		else
        if (missao.objetivoFinalMissao[1] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 1 && instalou == false)
        {
            missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
			instalou = true;
			Coleta.Play(0);
            SistemaNPCSecundario.Coletou();
        }else 
			//Instalação de filtros nas chamines =2
		if(missao.objetivoFinalMissao[1] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 2 && instalou == false)
		{
			missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
			instalou = true;
			Coleta.Play(0);
            SistemaNPCSecundario.Coletou();
		}else
		if (missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 2 && instalou == false)
		{
			missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
			instalou = true;
			Coleta.Play(0);
            SistemaNPC.Coletou();
		}else
		//Conversar com NPCS = 3
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 3)
		{
			missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
            SistemaNPC.Coletou();
			this.gameObject.GetComponent<Rigidbody>().useGravity = false;
			this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
			this.gameObject.GetComponent<NPC_IA>().Interact();
		}else
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 3)
		{
			missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
            SistemaNPCSecundario.Coletou();
			this.gameObject.GetComponent<Rigidbody>().useGravity = false;
			this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
			this.gameObject.GetComponent<NPC_IA>().Interact();
		}else
		//Desentupir bueiros = 5
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 5)
		{
			desentopi = PROPRIEDADES_JOGADOR.desentopi;
			taMovendo = true;
		}else
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 5)
		{
			desentopi = PROPRIEDADES_JOGADOR.desentopi;
			taMovendo = true;
		}else
			//Fertilizar o solo = 6
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 6 && terminou == false)
		{
			fertilizar = PROPRIEDADES_JOGADOR.fertilizar;
			comecarEncher = true;
		}else
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 6 && terminou == false)
		{
			fertilizar = PROPRIEDADES_JOGADOR.fertilizar;
			comecarEncher = true;
		}
		
	}
		//Varrer as ruas = 4
	void OnTriggerEnter(Collider colisao)
	{
		if(colisao.gameObject.name == "ObjetivoVarrer" && missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 4)
		{
			missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
			Coleta.Play(0);
            SistemaNPC.Coletou();
			this.gameObject.SetActive(false);
		}else
			if(colisao.gameObject.name == "ObjetivoVarrer" && missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 4)
			{
				missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
				Coleta.Play(0);
				SistemaNPCSecundario.Coletou();
				this.gameObject.SetActive(false);
			}
	}
	
	//Desentupir bueiros
	void ChecarResultado()
	{
		float valorSlider = GameObject.Find("SliderBueiro").GetComponent<Slider>().normalizedValue;
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 5)
		{
			
			if(valorSlider >= (0.50f - sucesso) && valorSlider <= (0.50f + sucesso))//TestarMuitaCoisa
			{
				Debug.Log(valorSlider);
				missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
                Coleta.Play(0);
				SistemaNPC.Coletou();
                this.gameObject.SetActive(false);
                desentopi.SetActive(false);
				taMovendo = false;
            }
			
		}
	}
	void Fertilizacao()
	{
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPC.tipoMissao] && SistemaNPC.selecionarOqueFazer == 6)
		{
		this.gameObject.GetComponent<MeshRenderer>().material = materiais ;
		terminou = true;
		missao.tipoMissaoAtual = SistemaNPC.tipoMissao;
		Coleta.Play(0);
        SistemaNPC.Coletou();
		}else
		if(missao.objetivoFinalMissao[0] > 0 && idMissao == missao.idMissaoAtual[SistemaNPCSecundario.tipoMissao] && SistemaNPCSecundario.selecionarOqueFazer == 6)
		{
		this.gameObject.GetComponent<MeshRenderer>().material = materiais ;
		terminou = true;
		missao.tipoMissaoAtual = SistemaNPCSecundario.tipoMissao;
		Coleta.Play(0);
        SistemaNPCSecundario.Coletou();
		}
	}
}
