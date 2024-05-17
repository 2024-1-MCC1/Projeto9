using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FAIXA : MonoBehaviour
{
    private GameObject FAIXAS;
    private GameObject FAIXAP;
    private bool Direction = false;
    private float velocidade = 5f;

    public bool INTERSECCAO = false;

    public static GameObject PILHA;
    public float calc;

    // Start is called before the first frame update
    void Start()
    {
        FAIXAS = this.gameObject;
        FAIXAP = GameObject.Find("FAIXA");
    }

    // Update is called once per frame
    void Update()
    {
        if(FAIXAS.GetComponent<RectTransform>().localPosition.x < -450)
        {
            Direction = true;
            velocidade = Mathf.Abs(velocidade) + 0.5f;
        }else if(FAIXAS.GetComponent<RectTransform>().localPosition.x > 450)
        {
            Direction = false;
            velocidade = Mathf.Abs(velocidade) + 0.5f;
        }
        velocidade = Direction ? Mathf.Abs(velocidade) : Mathf.Abs(velocidade) * (-1);
        FAIXAS.GetComponent<RectTransform>().Translate(velocidade,0f,0f);

        if(FAIXAS.GetComponent<RectTransform>().localPosition.x > FAIXAP.GetComponent<RectTransform>().localPosition.x - 55 && FAIXAS.GetComponent<RectTransform>().localPosition.x < FAIXAP.GetComponent<RectTransform>().localPosition.x + 55)
        {
            FAIXAS.GetComponent<Image>().color = new Color(0.48f,0.37f,0.63f);
            INTERSECCAO = true;
        }
        else
        {
            FAIXAS.GetComponent<Image>().color = new Color(1f, 0f, 0f);
            INTERSECCAO = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (INTERSECCAO)
            {
                calc = Random.Range(1, 11);
                if(PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
                { PROPRIEDADES_JOGADOR.PESOATUAL += calc; }
                PILHA.GetComponent<PILHAS>().PREENCHIMENTO -= calc;
                calc = PILHA.GetComponent<PILHAS>().PREENCHIMENTO / PILHA.GetComponent<PILHAS>().PREENCHIMENTOMAX;
                PILHA.transform.localScale = new Vector3(PILHA.GetComponent<PILHAS>().TAMANHOX * calc, PILHA.GetComponent<PILHAS>().TAMANHOY * calc, PILHA.GetComponent<PILHAS>().TAMANHOZ * calc);
                REINICIAR();
                if (PROPRIEDADES_JOGADOR.PESOATUAL >= PROPRIEDADES_JOGADOR.PESOLIMITE)
                {
                    PROPRIEDADES_JOGADOR.FAIXAJ.SetActive(false);

                    PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[0] = 7;
                    PROPRIEDADES_JOGADOR.MISSAO.textoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual].SetText(PROPRIEDADES_JOGADOR.MISSAO.textoObjetivoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual] + $"({PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual]}/{PROPRIEDADES_JOGADOR.MISSAO.objetivoFinalMissao[PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual]})");
                    PROPRIEDADES_JOGADOR.MISSAO.CheckDaMissao();
                }
                
            }
            else if(INTERSECCAO == false)
            {
                calc = Random.Range(1, 11);
                if(PROPRIEDADES_JOGADOR.PESOATUAL > 0)
                { PROPRIEDADES_JOGADOR.PESOATUAL -= calc; }
                PILHA.GetComponent<PILHAS>().PREENCHIMENTO += calc;
                calc = PILHA.GetComponent<PILHAS>().PREENCHIMENTO / PILHA.GetComponent<PILHAS>().PREENCHIMENTOMAX;
                PILHA.transform.localScale = new Vector3(PILHA.GetComponent<PILHAS>().TAMANHOX * calc, PILHA.GetComponent<PILHAS>().TAMANHOY * calc, PILHA.GetComponent<PILHAS>().TAMANHOZ * calc);
                REINICIAR();
            }
        }
    }
    public void REINICIAR()
    {
        velocidade = Random.Range(5, 11);
        Direction = Random.Range(1, 3) == 2 ? true : false;

        FAIXAS.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(0,400) * Random.Range(-1,2),-212f,0f);

        FAIXAP.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(0, 400) * Random.Range(-1, 2), -212f, 0f);
        PILHA = null;
    }

}
