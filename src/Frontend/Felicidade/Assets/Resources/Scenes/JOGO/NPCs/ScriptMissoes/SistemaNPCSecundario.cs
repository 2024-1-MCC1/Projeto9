using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaNPCSecundario : MonoBehaviour
{

    private Missoes quest; //Pega o gameobject que contém o script das missões
    public string tituloMissao;
    public string textoObjetivoMissao = ""; //String para inserir o texto do objetivo da missão
    public int objetivoFinalMissaoInte = 0;//Inserir a quantidade de tarefas que o jogador tem que alcançar para concluir a missão
    public float dinheiroMissao;
	public float respeitoGanho;
	public float respeitoNecessario;
    public int idMissao; //Usado para identificar a missão 
    public int selecionarOqueFazer;//"Coleta de Lixo"(0), "Arborização"(1), "Varrer as ruas"(0)
	public float tempoParaOFinal;
	public bool eAleatoria = false;

	public int tipoMissao = 1; //(0) é para missões principais e (1) é para missões secundarias
    private SistemaNPCPrincipal sistemaNPCPrincipal;

    void Start()
    {
        quest = GameObject.Find("MissaoGameObject").GetComponent<Missoes>();
        sistemaNPCPrincipal = this.gameObject.GetComponent<SistemaNPCPrincipal>();
    }

    void Update()
    {
        if (idMissao == quest.idMissaoAtual[quest.tipoMissaoAtual] && quest.acabouAMissao[quest.tipoMissaoAtual])//Verifica se o ID da missão desse script é igual ao ID da missão no script "Missoes", também verifica se a bool missão acabou é verdadeira
        {
            this.gameObject.GetComponent<SistemaNPCSecundario>().enabled = false;//Desativa o Script no gameobject em que ele está inserido
        }
		if(eAleatoria && respeitoNecessario == PROPRIEDADES_JOGADOR.respeito)
		{
			InteragiuSecundario();
		}
    }

    public void InteragiuSecundario()
    {
		Debug.Log("O 2 funcionou");
        if (sistemaNPCPrincipal.comecouPrincipal)
        {
			quest.respeito[tipoMissao] = respeitoGanho;
			quest.tempoParaFinal = tempoParaOFinal;
            quest.dinheiro[tipoMissao] = dinheiroMissao;
            quest.textoTituloMissao[tipoMissao] = tituloMissao;
            quest.tipoMissaoAtual = tipoMissao;
            quest.acabouAMissao[quest.tipoMissaoAtual] = false;
            quest.textoObjetivoMissao[tipoMissao] = textoObjetivoMissao;
            quest.progressoMissao[tipoMissao] = 0;
            quest.objetivoFinalMissao[tipoMissao] = objetivoFinalMissaoInte;
            quest.idMissaoAtual[tipoMissao] = idMissao;
            quest.Missao();
        }
    }

    public void Coletou()
    {
        quest.progrediuObjetivo[tipoMissao] = true;
        quest.CheckDaMissao();
    }
}
