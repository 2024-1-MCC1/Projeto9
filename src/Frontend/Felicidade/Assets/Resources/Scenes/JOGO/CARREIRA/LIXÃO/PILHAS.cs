using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PILHAS : MonoBehaviour, IInteractable
{

    private GameObject PILHA;
    public float PREENCHIMENTO = 0f;
    public float PREENCHIMENTOMAX;

    private bool TRABALHANDO = false;

    public float TAMANHOX;
    public float TAMANHOY;
    public float TAMANHOZ;

    public bool LIXAO = true;

    private int teste;
    public void Interact()
    {
        if(PROPRIEDADES_JOGADOR.FAIXAJ.activeSelf == false && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE && CICLICIDADE.ATIVO)
        {
            PROPRIEDADES_JOGADOR.FAIXAJ.SetActive(true);
            this.TRABALHANDO = true;
        }
    }

    void Start()
    {
        PILHA = this.gameObject;

        TAMANHOX = PILHA.transform.localScale.x;
        TAMANHOY = PILHA.transform.localScale.y;
        TAMANHOZ = PILHA.transform.localScale.z;
        this.PREENCHIMENTOMAX = this.PREENCHIMENTO;
    }

    // Update is called once per frame
    void Update()
    {
        if(PREENCHIMENTO > PREENCHIMENTOMAX)
        {
            PREENCHIMENTO = PREENCHIMENTOMAX;
        }

        if (CICLICIDADE.ATIVO && CICLICIDADE.auxESCOLHA == 1 && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }

        if (TRABALHANDO)
        {
            FAIXA.PILHA = PILHA;
            if(PROPRIEDADES_JOGADOR.FAIXAJ == false)
            {
                TRABALHANDO = false;
            }
        }

        if(this.PREENCHIMENTO <= 20 && this.LIXAO)
        {
            Destroy(PILHA);
        }else if(this.PREENCHIMENTO <= 20)
        {
            CICLICIDADE.Progresso++;
            PROPRIEDADES_JOGADOR.FAIXAJ.SetActive(false);
            TRABALHANDO = false;
            RESPAWN();
        }
    }
    void RESPAWN()
    {
        PREENCHIMENTO = PREENCHIMENTOMAX;
        if(Random.Range(0,100) < 24)
        {
            PROPRIEDADES_JOGADOR.RECICLAGE();
        }
        teste = Random.Range(0, 3);
        if (teste == 0)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -320), 71.84f, Random.Range(0, 95) - 42f);
        }
        else if (teste == 1)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -122), 71.84f, Random.Range(0, 20) - 0.219f);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-202, -218), 71.84f, Random.Range(0, 82.5f) - 22.5f);
        }

    }

}
