using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RECICLAGEM : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    public void Interact()
    {
        if (PROPRIEDADES_JOGADOR.RECICLAR != 0f)
        {
            if (PROPRIEDADES_JOGADOR.TESTE == 0 && this.gameObject.name == "METAIS")
            {
                PROPRIEDADES_JOGADOR.RECICLAR = 0f;
            }
            else if (this.gameObject.name == "METAIS")
            {
                PROPRIEDADES_JOGADOR.respeito--;
            }

            if (PROPRIEDADES_JOGADOR.TESTE == 1 && this.gameObject.name == "PLASTICOS")
            {
                PROPRIEDADES_JOGADOR.RECICLAR = 0f;
            }
            else if (this.gameObject.name == "PLASTICOS")
            {
                PROPRIEDADES_JOGADOR.respeito--;
            }

            if (PROPRIEDADES_JOGADOR.TESTE == 2 && this.gameObject.name == "PAPEIS")
            {
                PROPRIEDADES_JOGADOR.RECICLAR = 0f;
            }
            else if (this.gameObject.name == "PAPEIS")
            {
                PROPRIEDADES_JOGADOR.respeito--;
            }

            if (PROPRIEDADES_JOGADOR.TESTE == 3 && this.gameObject.name == "VIDROS")
            {
                PROPRIEDADES_JOGADOR.RECICLAR = 0f;
            }
            else if (this.gameObject.name == "VIDROS")
            {
                PROPRIEDADES_JOGADOR.respeito--;
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PROPRIEDADES_JOGADOR.RECICLAR != 0)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }
    }
}
