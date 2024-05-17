using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DESPACHA_LIXO : MonoBehaviour, IInteractable
{

    private float PREENCHIMENTO = 0;
    private GameObject TEXTO;
    public void Interact()
    {
        PREENCHIMENTO += PROPRIEDADES_JOGADOR.PESOATUAL;
        PREENCHIMENTO = PREENCHIMENTO > 100 ? 100 : PREENCHIMENTO;
        PROPRIEDADES_JOGADOR.PESOATUAL = PREENCHIMENTO > 100 ? PREENCHIMENTO - 100 : 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        TEXTO = GameObject.Find("TEXTO_" + this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(PROPRIEDADES_JOGADOR.PESOATUAL > PROPRIEDADES_JOGADOR.PESOLIMITE && PREENCHIMENTO < 80)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }

        if(PREENCHIMENTO > 0)
        {
            PREENCHIMENTO -= 0.5f*Time.deltaTime;
        }
        TEXTO.GetComponent<TMP_Text>().text = (int)PREENCHIMENTO + "%";
    }
}
