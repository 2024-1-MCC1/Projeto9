using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SACO_LIXO : MonoBehaviour, IInteractable
{
    private int teste;
    public void Interact()
    {
        if(CICLICIDADE.auxESCOLHA == 0 && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
        {
            CICLICIDADE.Progresso++;
            PROPRIEDADES_JOGADOR.PESOATUAL += Random.Range(3, 11);
            RESPAWN();
        }
        if (Random.Range(0, 100) < 6)
        {
            PROPRIEDADES_JOGADOR.RECICLAGE();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CICLICIDADE.auxESCOLHA == 0 && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }
        
    }

    void RESPAWN()
    {
        teste = Random.Range(0, 3);
        if(teste == 0)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -320), 85f, Random.Range(0, 95) -42f);
        }
        else if(teste == 1)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -122), 85f, Random.Range(0, 20) - 0.219f);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-202, -218), 85f, Random.Range(0, 82.5f) - 22.5f);
        }
        
    }
}
