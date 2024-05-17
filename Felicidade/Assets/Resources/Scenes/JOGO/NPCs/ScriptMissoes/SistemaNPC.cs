using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SistemaNPCPrincipal : MonoBehaviour
{

    private Missoes quest; //Pega o gameobject que contém o script das missões
    public string tituloMissao;
    public string textoObjetivoMissao = ""; //String para inserir o texto do objetivo da missão
    public int objetivoFinalMissaoInte = 0;//Inserir a quantidade de tarefas que o jogador tem que alcançar para concluir a missão
    public float dinheiroMissao;
	public float respeitoGanho;
    public int idMissao; //Usado para identificar e diferenciar missões 
    public int selecionarOqueFazer;  //"Coleta de Lixo"(0), "Arborização"(1), "Varrer as ruas"(0)
    public bool comecouPrincipal = false;
	public int tipoMissao = 0; //(0) é para missões principais e (1) é para missões secundarias
	
	public bool[] missaoAcabou = new bool [20];

    void Start()
    {
        quest = GameObject.Find("MissaoGameObject").GetComponent<Missoes>();
		missaoAcabou[idMissao] = false;
    }

    void Update()
    {
		if(quest.idMissaoAtual[tipoMissao] == idMissao && quest.acabouAMissao[0] == true)
		{
			missaoAcabou[idMissao] = true;
		}
    }

    public void Interacao()
    {

		Debug.Log("foiNPC");
		if(missaoAcabou[idMissao] == false && this.gameObject.GetComponent<SistemaNPCPrincipal>().objetivoFinalMissaoInte >0)
		{
			Interagiu();
		}
        if(this.gameObject.GetComponent<SistemaNPCSecundario>().objetivoFinalMissaoInte > 0)
        {
            this.gameObject.GetComponent<SistemaNPCSecundario>().InteragiuSecundario();
        }

    }
    
    public void Coletou()
    {
       // if (desentupir)
        //{
           // quest.DesentupirMissao();
        //}
        quest.progrediuObjetivo[tipoMissao] = true;
        quest.CheckDaMissao();
    }

    private void Interagiu()
    {
		quest.respeito[tipoMissao] = respeitoGanho;
        quest.dinheiro[tipoMissao] = dinheiroMissao;
        quest.textoTituloMissao[tipoMissao] = tituloMissao;
        quest.tipoMissaoAtual = tipoMissao;
        quest.acabouAMissao[quest.tipoMissaoAtual] = false;
        quest.textoObjetivoMissao[tipoMissao] = textoObjetivoMissao;
        quest.progressoMissao[tipoMissao] = 0;
        quest.objetivoFinalMissao[tipoMissao] = objetivoFinalMissaoInte;
        quest.idMissaoAtual[tipoMissao] = idMissao;
        comecouPrincipal = true;
        quest.Missao();
    }

}
