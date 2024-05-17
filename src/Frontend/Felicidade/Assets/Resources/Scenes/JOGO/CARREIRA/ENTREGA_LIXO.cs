using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENTREGA_LIXO : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    public void Interact()
    {
        PROPRIEDADES_JOGADOR.PESOATUAL = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(PROPRIEDADES_JOGADOR.PESOATUAL > PROPRIEDADES_JOGADOR.PESOLIMITE)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }
    }
}
