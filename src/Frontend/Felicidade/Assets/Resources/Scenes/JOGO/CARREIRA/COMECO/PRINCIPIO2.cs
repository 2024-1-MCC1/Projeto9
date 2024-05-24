using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRINCIPIO2 : MonoBehaviour
{
    private bool INICIAR = false;
    

    private GameObject NPCsFamiliar;
    private GameObject JOVEM;
    private int AVANCADICAS = 0;

    // Start is called before the first frame update
    void Start()
    {

        NPCsFamiliar = GameObject.Find("NPCsFamiliar");
        NPCsFamiliar.SetActive(false);

        JOVEM = GameObject.Find("JOVEM_ENTRAR");
        JOVEM.SetActive(false);
        ORQUESTRA.REINICIAR();
    }

    // Update is called once per frame
    void Update()
    {
        INICIAR = ORQUESTRA.COMECO2;
        if (INICIAR)
        {
            if (ORQUESTRA.SEQUENCIA[1] == false)
            {
                if (ORQUESTRA.SEQUENCIA[0] == false)
                {
                    ORQUESTRA.PALAVRAS.Insert(0, "...");
                    ORQUESTRA.PALAVRAS.Insert(1, "Mas que sujeito inconveniente.");
                    ORQUESTRA.PALAVRAS.Insert(2, "Mal me oferece ajuda para me levantar.");
                    ORQUESTRA.PALAVRAS.Insert(3, "");
                    ORQUESTRA.PALAVRAS.Insert(4, "Mas tinha boas inten��es...");
                    ORQUESTRA.PALAVRAS.Insert(5, "As pessoas est�o adoecendo, as ruas est�o imundas, esse mau-cheiro, um ar quente e pesado...");
                    ORQUESTRA.PALAVRAS.Insert(6, "Nunca fui t�o bem recebido em toda minha vida...");
                    ORQUESTRA.PALAVRAS.Insert(7, "Cruel ironia...");
                    ORQUESTRA.PALAVRAS.Insert(8, "");
                    ORQUESTRA.PALAVRAS.Insert(9, "� bom saber que ao menos existe algu�m aqui se mobilizando para fazer diferente.");
                    ORQUESTRA.PALAVRAS.Insert(10, "Inclusive, isso daqui t� parecendo um filme de terror! Cad� o povo daqui?");
                    ORQUESTRA.PALAVRAS.Insert(11, "Talvez eu encontre por a� algum conterr�neo com quem possa conversar.");
                    ORQUESTRA.PALAVRAS.Insert(12, "");
                    ORQUESTRA.SEQUENCIA[0] = true;

                    ORQUESTRA.guiaPALAVRAS = 0;
                }
                else
                {
                    
                    Debug.Log(ORQUESTRA.guiaPALAVRAS);
                    if (ORQUESTRA.guiaPALAVRAS == 13)
                    {
                        PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual = 0;
                        PROPRIEDADES_JOGADOR.MISSAO.textoTituloMissao[0] = "Familiarize-se";
                        PROPRIEDADES_JOGADOR.MISSAO.textoObjetivoMissao[0] = "Converse com 5 habitantes";
                        PROPRIEDADES_JOGADOR.MISSAO.objetivoFinalMissao[0] = 5;
                        PROPRIEDADES_JOGADOR.MISSAO.tipoDeMissao[0] = 1;
                        PROPRIEDADES_JOGADOR.MISSAO.Missao();
                        NPCsFamiliar.SetActive(true);
                        ORQUESTRA.REINICIAR();
                        ORQUESTRA.SEQUENCIA[1] = true;

                    }
                    else
                    {
                        ORQUESTRA.CICLO_TEXTO();
                    }
                }
            }
            else if (PROPRIEDADES_JOGADOR.MISSAO.missaoLayout[0].activeSelf == false && ORQUESTRA.SEQUENCIA[2] == false)
            {
                if(AVANCADICAS == 0)
                {
                    AVANCADICAS += ORQUESTRA.DICAS("Parab�ns! Voc� concluiu sua primeira miss�o! Atrav�s delas voc� pode adquirir recompensas e avan�ar no jogo.");
                }else if(AVANCADICAS == 1)
                {
                    ORQUESTRA.PALAVRAS.Insert(0, "�...");
                    ORQUESTRA.PALAVRAS.Insert(1, "A coisa est� feia mesmo nesse lugar.");
                    ORQUESTRA.PALAVRAS.Insert(2, "Talvez fosse uma ideia interessante terminar minha vida aqui nessa 'terra dos sonhos',");
                    ORQUESTRA.PALAVRAS.Insert(3, "ou quem sabe ajudar aquele jovem a torn�-la de fato um lugar mais humano.");
                    ORQUESTRA.PALAVRAS.Insert(4, "Ele disse que tinha 'colegas' ajudando-o, ent�o talvez n�s consigamos fazer as coisas mudarem por aqui.");
                    ORQUESTRA.PALAVRAS.Insert(5, "� isso ent�o. Vou ver se encontro ele por a�.");

                    ORQUESTRA.guiaPALAVRAS = 0;
                    ORQUESTRA.SEQUENCIA[2] = true;
                }else if(AVANCADICAS == 2)
                {
                    AVANCADICAS = 0;
                }

            }
            else
            {
                if(AVANCADICAS == 0)
                {
                    AVANCADICAS += ORQUESTRA.DICAS("Voc� recebeu uma miss�o! PRESSIONE 'Q' para visualizar seu objetivo.")*2;
                }
                
            }

            if(ORQUESTRA.SEQUENCIA[2] == true && (ORQUESTRA.SEQUENCIA[3] == false || ORQUESTRA.guiaPALAVRAS == 6))
            {
                ORQUESTRA.CICLO_TEXTO();
                if (ORQUESTRA.guiaPALAVRAS == 6 && ORQUESTRA.SEQUENCIA[3] == false)
                {

                    PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual = 0;
                    PROPRIEDADES_JOGADOR.MISSAO.textoTituloMissao[0] = "Entre no Movimento";
                    PROPRIEDADES_JOGADOR.MISSAO.textoObjetivoMissao[0] = "Fale com O JOVEM";
                    PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[0] = 0;
                    PROPRIEDADES_JOGADOR.MISSAO.objetivoFinalMissao[0] = 1;
                    PROPRIEDADES_JOGADOR.MISSAO.tipoDeMissao[0] = 1;
                    PROPRIEDADES_JOGADOR.MISSAO.Missao();

                    JOVEM.SetActive(true);

                    ORQUESTRA.SEQUENCIA[3] = true; 
                }
            }
            if (ORQUESTRA.SEQUENCIA[3] == true)
            {
                LIXAO.NPCs.SetActive(true);
                ORQUESTRA.COMECO2 = false;
                ORQUESTRA.REINICIAR();
            }

        }
    }
}
