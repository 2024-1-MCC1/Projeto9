using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Missoes : MonoBehaviour
{
    public GameObject[] missaoLayout;//Layout para fazer call out
    public GameObject missaoConcluida;
	public GameObject missaoFalhou;
    //public GameObject desentupirLayout;

    public TMP_Text[] tituloMissao;//Titulo da Missão

    public string[] textoTituloMissao;//Texto do titulo da missão
    public TMP_Text[] textoMissao;//TMP_Text da Missão
    public string[] textoObjetivoMissao = {"",""};//Inserir objetivo da missão
    public float[] dinheiro = {0,0};
	public float[] respeito ={0,0};
    public int[] progressoMissao = {0,0};//Inserir o progresso do jogador na missão
    public int[] objetivoFinalMissao = {0,0};//Objetivo que o jogador tem que alcançar na missão
    public bool[] progrediuObjetivo = {false, false};//Pergunta se o jogador avamçou no objetivo
    private float delayInSeconds = 2f;
    public int[] idMissaoAtual = {1,0};
    public int tipoMissaoAtual; //(0) é para missões principais e (1) é para missões secundarias
    public bool[] acabouAMissao = {false, false};
	public bool taNaHoraPro = true;
	public float tempoParaFinal;
    public TMP_Text tempoTexto;
	
	private GameObject Tempo;
	private bool temTempo = false;
	private AudioSource MissaoConcluida;
	private AudioSource GameOver;

    public int[] tipoDeMissao = new int[2]; //(1) para missões de conversa

    private int sorteiaSOM;

    public void Missao()
    {
		taNaHoraPro = false;
        missaoLayout[tipoMissaoAtual].SetActive(true);
        textoMissao[tipoMissaoAtual].SetText(textoObjetivoMissao[tipoMissaoAtual] + $"({progressoMissao[tipoMissaoAtual]}/{objetivoFinalMissao[tipoMissaoAtual]})");
        tituloMissao[tipoMissaoAtual].SetText(textoTituloMissao[tipoMissaoAtual]);
        sorteiaSOM = Random.Range(0, 2) + 1;
        ORQUESTRA.SONG.clip = Resources.Load<AudioClip>("Scenes/JOGO/AUDIO/MISSAO_SONG" + sorteiaSOM.ToString());
        ORQUESTRA.SONG.Play();
    }
    void Start()
    {
		Tempo = GameObject.Find("Tempo");
		MissaoConcluida = GameObject.Find("MissaoConcluidaSound").GetComponent<AudioSource>();
		GameOver = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
        Tempo.SetActive(false);
        //desentupirLayout.SetActive(false);
        missaoLayout[0].SetActive(false);
        missaoLayout[1].SetActive(false);
        missaoConcluida.SetActive(false);
		missaoFalhou.SetActive(false);
    }

    void Update()
    {
		if (acabouAMissao[1] == false && tempoParaFinal > 0)
        {
            Tempo.SetActive (true);
            tempoParaFinal -= Time.deltaTime;
            tempoTexto.SetText($"Tempo restante {(int)tempoParaFinal}");
            temTempo = true;
        }else if (acabouAMissao[1] == false && tempoParaFinal <= 0 && temTempo)
        {
			PROPRIEDADES_JOGADOR.respeito -= respeito[tipoMissaoAtual];
            Tempo.SetActive(false);
            missaoFalhou.SetActive(true);
            missaoLayout[1].SetActive(false);
            Invoke("MissaoFalhou", delayInSeconds);
            temTempo=false;
            tempoParaFinal = 0;
        }
    }

    public void CheckDaMissao()
    {
        if (progrediuObjetivo[tipoMissaoAtual] && progressoMissao[tipoMissaoAtual] <= objetivoFinalMissao[tipoMissaoAtual])
        {
            progressoMissao[tipoMissaoAtual]++;
            progrediuObjetivo[tipoMissaoAtual] = false;
            textoMissao[tipoMissaoAtual].SetText(textoObjetivoMissao[tipoMissaoAtual] + $"({progressoMissao[tipoMissaoAtual]}/{objetivoFinalMissao[tipoMissaoAtual]})");
        }
        if (progressoMissao[tipoMissaoAtual] == objetivoFinalMissao[tipoMissaoAtual])
        {
			MissaoConcluida.Play(0);
			taNaHoraPro = true;
			temTempo=false;
			Tempo.SetActive(false);
            Invoke("EsconderConclusao", delayInSeconds);
            missaoLayout[tipoMissaoAtual].SetActive(false);
            missaoConcluida.SetActive(true);
            PROPRIEDADES_JOGADOR.dinheiro += dinheiro[tipoMissaoAtual];
			PROPRIEDADES_JOGADOR.respeito += respeito[tipoMissaoAtual];
			PROPRIEDADES_JOGADOR.pessoasNoMovimento ++;
        }
        PROPRIEDADES_JOGADOR.REINICIAR_SETA();
    }

    private void EsconderConclusao()
    {
        missaoConcluida.SetActive(false);
        acabouAMissao[tipoMissaoAtual] = true;
		Tempo.SetActive(false);
    }

	private void MissaoFalhou()
    {
		GameOver.Play(0);
        missaoFalhou.SetActive(false);
        acabouAMissao[1] = true;
    }
}
