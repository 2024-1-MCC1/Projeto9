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

    public void Interact()
    {
        if(PROPRIEDADES_JOGADOR.FAIXAJ.activeSelf == false && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
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
        if (TRABALHANDO)
        {
            FAIXA.PILHA = PILHA;
            if(PROPRIEDADES_JOGADOR.FAIXAJ == false)
            {
                TRABALHANDO = false;
            }
        }

        if(this.PREENCHIMENTO <= 20)
        {
            Destroy(PILHA);
        }
    }
}
