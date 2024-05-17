using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Economia : MonoBehaviour, IInteractable
{
    public string nomeProduto; //Insere o nome do produto no texto
    public string descricaoProduto; //Insere a descrição do produto no texto
    public float preco; //Quanto o produto vai custar
    public float fomeRestaura; //Quanto da barra de fome o produto vai encher
    public float sedeRestaura; //Quanto da barra de sede o produto vai encher
    public TMP_Text titulo;
    public TMP_Text Descricao;

    public GameObject Compra;
    private bool estaNoMenu = false;
    private AudioSource Comer;

    void Start()
    {
        Compra.SetActive(false);
        Comer = GameObject.Find("ComerSom").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (estaNoMenu == true && Input.GetKey(KeyCode.F) && PROPRIEDADES_JOGADOR.dinheiro > preco)
        {
            if (PROPRIEDADES_JOGADOR.fome + fomeRestaura >= 100)
            {
                PROPRIEDADES_JOGADOR.fome += (100 - PROPRIEDADES_JOGADOR.fome);
            }else
            PROPRIEDADES_JOGADOR.fome += fomeRestaura;
            if(PROPRIEDADES_JOGADOR.sede + sedeRestaura >= 100)
            {
                PROPRIEDADES_JOGADOR.sede += (100 - PROPRIEDADES_JOGADOR.sede);
            }else
            PROPRIEDADES_JOGADOR.sede += sedeRestaura;

            if(this.gameObject.name == "BottleLOD0")
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
            PROPRIEDADES_JOGADOR.dinheiro -= preco;
            Compra.SetActive(false);
            estaNoMenu = false;
        }
        else if (estaNoMenu == true && Input.GetKey(KeyCode.R) || PROPRIEDADES_JOGADOR.dinheiro < preco)
        {
            Compra.SetActive(false);
            estaNoMenu = false;
        }
    }

    public void Interact()
    {
        Compra.SetActive(true);
        titulo.SetText(nomeProduto);
        Descricao.SetText(descricaoProduto);
        estaNoMenu = true;
    }
}
