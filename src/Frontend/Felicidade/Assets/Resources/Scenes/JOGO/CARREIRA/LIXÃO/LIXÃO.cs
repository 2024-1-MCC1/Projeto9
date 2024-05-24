using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIXAO : MonoBehaviour
{
    private bool INICIAR = false;
    public static bool LIBERADOENCA = false;
    private GameObject PESSOAS;
    private NPC_IA PREFEITO;
    private GameObject CAMERA;

    private int escur = 0; 
    public static bool comecaAux = false;

    private GameObject CAMINHAO;
    private GameObject MAUCHE;

    public static GameObject NPCs;

    // Start is called before the first frame update
    void Start()
    {
        PESSOAS = GameObject.Find("PESSOAS");
        PREFEITO = GameObject.Find("PREFEITO").GetComponent<NPC_IA>();
        PESSOAS.SetActive(false);
        CAMERA = GameObject.Find("Camera");
        CAMINHAO = GameObject.Find("CAMINHÃO");
        CAMINHAO.SetActive(false);
        MAUCHE = GameObject.Find("MAUCHEIRO");
        MAUCHE.SetActive(false);
        NPCs = GameObject.Find("NPCs");
    }

    // Update is called once per frame
    void Update()
    {
        INICIAR = ORQUESTRA.LIXAOf;

        if (INICIAR)
        {
            if(escur == 1000)
            {
                escur = 1;
                comecaAux = true;
            }
            if (comecaAux)
            {
                if (ORQUESTRA.SEQUENCIA[0] == false)
                {
                    CAMERA.SetActive(false);
                    ORQUESTRA.SEQUENCIA[0] = true;
                    SC_FPSController.MODOCARREIRA = true;
                    SC_FPSController.animator.SetInteger("andando", 0);
                    GameObject.Find("ENTRADA_LIXÃO").SetActive(false);
                    MAUCHE.SetActive(true);
                    PESSOAS.SetActive(true);

                }
                else
                {
                    if (escur < 2)
                    {
                        if (escur == 1)
                        {
                            PROPRIEDADES_JOGADOR.TELA_CONVERSA.SetActive(false);
                            PREFEITO.Interact();
                            escur = 2;
                        }
                    }
                    else if (escur == 2)
                    {
                        
                        escur += ORQUESTRA.ESCURECER(false);

                    }
                    if (PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf == false && escur == 3)
                    {
                        escur += ORQUESTRA.ESCURECER(true);
                    }
                    else if (escur == 4)
                    {
                        PESSOAS.SetActive(false);
                        CAMERA.SetActive(true);
                        ORQUESTRA.REINICIAR();
                        SC_FPSController.MODOCARREIRA = false;

                        ORQUESTRA.LIXAOf = false;
                        escur = 5;
                    }

                }

            }
            
            else
            {
                escur += ORQUESTRA.ESCURECER(true) * 1000;
            }
        }
        else if (escur == 5)
        {
            NPCs.SetActive(false);
            escur += ORQUESTRA.ESCURECER(false);
        }else if(escur == 6)
        {
            escur += ORQUESTRA.DICAS("Por participar do movimento do JOVEM, automaticamente você está responsável por limpar o lixão.");
        }else if(escur == 7)
        {
            PROPRIEDADES_JOGADOR.MISSAO.progressoMissao[0] = 0;
            PROPRIEDADES_JOGADOR.MISSAO.tipoMissaoAtual = 0;
            PROPRIEDADES_JOGADOR.MISSAO.textoTituloMissao[0] = "Ajude a limpar o LIXÃO da cidade!";
            PROPRIEDADES_JOGADOR.MISSAO.textoObjetivoMissao[0] = "Carregue o caminhão de lixo";
            PROPRIEDADES_JOGADOR.MISSAO.objetivoFinalMissao[0] = 7;
            PROPRIEDADES_JOGADOR.MISSAO.tipoDeMissao[0] = 1;
            PROPRIEDADES_JOGADOR.MISSAO.Missao();
            CAMINHAO.SetActive(true);

            //CANCELAR MISSÕES EM EXECUÇÃO NESTE PONTO

            escur = 8;
        }else if(escur == 8) {
            escur += ORQUESTRA.DICAS("As pessoas da cidade estão adoecendo. Certifique-se de manter-se longe de pessoas doentes e de maus odores.");
        }else if(escur == 9)
        {
            if(PROPRIEDADES_JOGADOR.MISSAO.missaoLayout[0].activeSelf == false)
            {
                NPCs.SetActive(true);
                escur += ORQUESTRA.DICAS("Parabéns! Você esvaziou o lixão da cidade! A partir de agora, o caminhão será responsável por descartar todo o lixo recolhido.");
                LIBERADOENCA = true;
                CAMMIN.HISTORIA = false;
                CAMMIN.PRANCHA.GetComponent<BoxCollider>().enabled = true;
            }
        }
        
    }
}
