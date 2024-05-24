using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VARRIVEIS : MonoBehaviour
{

    private int vida = 5;
    private int teste;
    
    // Update is called once per frame
    void Update()
    {
        if(vida < 0)
        {
            PROPRIEDADES_JOGADOR.PESOATUAL++;
            RESPAWN();
        }
        if(CICLICIDADE.auxESCOLHA == 2 && PROPRIEDADES_JOGADOR.PESOATUAL < PROPRIEDADES_JOGADOR.PESOLIMITE)
        {
            PROPRIEDADES_JOGADOR.ATRAIR_SETA(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "vassoura" && CICLICIDADE.auxESCOLHA == 2)
        {
            vida--;
            CICLICIDADE.Progresso++;
        }
    }
    void RESPAWN()
    {
        vida = 5;
        teste = Random.Range(0, 3);
        if (teste == 0)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -320), 85f, Random.Range(0, 95) - 42f);
        }
        else if (teste == 1)
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-380, -122), 85f, Random.Range(0, 20) - 0.219f);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(Random.Range(-202, -218), 85f, Random.Range(0, 82.5f) - 22.5f);
        }

    }
}
