using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PRINCIPIO : MonoBehaviour
{
    public static bool INICIAR = false;
    private GameObject JOGADOR;

    private float ESCURECER = 0f;
    private bool escurecer = false;

    private GameObject JOVEM;
    private bool auxJOVEM = false;

    private int MENCIONATUTORIAL = 0;
    private string auxMensagem = "";
	
	private GameObject Seta;
	
	void Start()
	{
		Seta = GameObject.Find("SETA");
		//Seta.SetActive(false);
	}

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        INICIAR = ORQUESTRA.COMECO; 
        if (INICIAR) {
        if (JOGADOR == null)
        {
            SC_FPSController.runningSpeed = 1f;
            SC_FPSController.walkingSpeed = 0.5f;
            SC_FPSController.BLOQUEIACORRIDA = false;

                JOGADOR = ORQUESTRA.JOGADOR.gameObject;
                JOVEM = GameObject.Find("JOVEM");
                JOVEM.SetActive(false);
            ORQUESTRA.PALAVRAS.Insert(0, "Como é bom voltar pra casa");
            ORQUESTRA.PALAVRAS.Insert(1,"após tantos anos que se passaram desde...");
            ORQUESTRA.PALAVRAS.Insert(2,"[...] desde aquela tragédia!");
            ORQUESTRA.PALAVRAS.Insert(3, "Passaram-se tempos difíceis para todos nós desde que abandonamos a cidade.");
            ORQUESTRA.PALAVRAS.Insert(4, "Gostaria de sentir ao menos um último suspiro da grandeza desse lugar.");
            ORQUESTRA.PALAVRAS.Insert(5, "Antes que..."); //COLOCAR ÁUDIO DE TOSSE 
            ORQUESTRA.PALAVRAS.Insert(6, "Anoiteceu rápido. Nem me dei conta da passagem do tempo.");  
            ORQUESTRA.PALAVRAS.Insert(7, "Talvez tenha sido pela anciedade de chegar logo.");
            ORQUESTRA.PALAVRAS.Insert(8, "");
                ORQUESTRA.PALAVRAS.Insert(9, "");
                ORQUESTRA.PALAVRAS.Insert(10, "");
                ORQUESTRA.PALAVRAS.Insert(11, "");

                GameObject.Find("NPCs").SetActive(false);

                //ORQUESTRA.guiaPALAVRAS = 5;

            }
            
            //--------------------PRIMEIRA PARTE DAS FALAS DA PERSONAGEM-----------------------
            if (ORQUESTRA.SEQUENCIA[2] != true)
            {
                if (ORQUESTRA.SEQUENCIA[0] != true)
                {

                    JOGADOR.GetComponent<CharacterController>().enabled = false;
                    JOGADOR.transform.position = new Vector3(-577f, 70f, 10.19f);
                    JOGADOR.GetComponent<CharacterController>().enabled = true;
                    ORQUESTRA.SEQUENCIA[0] = true;

                }
                if (ORQUESTRA.guiaPALAVRAS == 2 || ORQUESTRA.guiaPALAVRAS == 3)
                {
                    ORQUESTRA.tempoSAIRPALAVRAS = 1f;
                }
                else if (ORQUESTRA.guiaPALAVRAS == 7 && ORQUESTRA.SEQUENCIA[1] != true)
                {
                    ORQUESTRA.TEXTO.text = "";
                    ORQUESTRA.guiaPALAVRAS--;
                    ORQUESTRA.tempoLIMITEPALAVRAS = 15f;
                    ORQUESTRA.SEQUENCIA[1] = true;
                }
                else
                {
                    ORQUESTRA.tempoSAIRPALAVRAS = 5f;
                }

                if ((ORQUESTRA.guiaPALAVRAS != 12 && JOGADOR.transform.position.x < -410f))
                {
                    ORQUESTRA.CICLO_TEXTO();

                    if (ORQUESTRA.guiaPALAVRAS == 6 && ORQUESTRA.SONG.clip == null)
                    {
                        ORQUESTRA.SONG.clip = Resources.Load<AudioClip>("Scenes/JOGO/AUDIO/TOSSE 1");
                        ORQUESTRA.SONG.Play();
                    }
                    else if(ORQUESTRA.SONG.clip != null && ORQUESTRA.guiaPALAVRAS > 6)
                    {
                        ORQUESTRA.SONG.clip = null;
                    }

                    if (ORQUESTRA.guiaPALAVRAS > 6)
                    { 
                        if (MENCIONATUTORIAL == 0)
                        {
                            MENCIONATUTORIAL += ORQUESTRA.DICAS("PRESSIONE 'Q' para exibir os RECURSOS DA TELA.");
                        }
                        else if(MENCIONATUTORIAL == 1)
                        {
                            SC_FPSController.BLOQUEIACORRIDA = true;
                            if (auxMensagem == "")
                            {
                                auxMensagem = "PRESSIONE 'SHIFT' para CORRER.";
                                
                            }

                            MENCIONATUTORIAL += ORQUESTRA.DICAS(auxMensagem);

                            if (Input.GetKey(KeyCode.LeftShift))
                            {
                                auxMensagem = "Enquanto você corre, você perde energia. E sua velocidade é função da sua energia.";
                           
                            }
                        }
                    }

                }
                else if (JOGADOR.transform.position.x > -410f && ORQUESTRA.SEQUENCIA[2] != true)
                {
                    
                    ORQUESTRA.guiaPALAVRAS = 0;
                    ORQUESTRA.auxESMAECER = false;
                    ORQUESTRA.tempoPALAVRAS = 0;

                    ORQUESTRA.PALAVRAS.Clear();
                    ORQUESTRA.PALAVRAS.Insert(0, "Nossa! Mas que lixaiada é essa?!");
                    ORQUESTRA.PALAVRAS.Insert(1, "Na minha época o próprio bom-senso das pessoas as levava a descartarem o lixo no lugar certo!");
                    ORQUESTRA.PALAVRAS.Insert(2, "A repovoação da cidade deve ter trazido outro tipo de gente para cá.");
                    ORQUESTRA.PALAVRAS.Insert(3, "Eram outros tempos... A disciplina era outra.");
                    ORQUESTRA.PALAVRAS.Insert(4, "Bem me lembro de como antes mesmo do assunto de sustentabilidade vir à tona, nossa cidade já era exemplo.");
                    ORQUESTRA.PALAVRAS.Insert(5, "Até o conceito de 'autossustentabilidade' surgiu aqui.");
                    ORQUESTRA.PALAVRAS.Insert(6, "");
                    ORQUESTRA.PALAVRAS.Insert(7, "Nossa, que cheiro pesado!");
                    ORQUESTRA.PALAVRAS.Insert(8, "Eu não...");
                    ORQUESTRA.PALAVRAS.Insert(9, "Eu não consigo respirar!");
                    ORQUESTRA.PALAVRAS.Insert(10, "");
                    ORQUESTRA.PALAVRAS.Insert(11, "");

                    ORQUESTRA.SEQUENCIA[2] = true;
                }

                
            }
            if (ORQUESTRA.SEQUENCIA[2] && ORQUESTRA.SEQUENCIA[3] != true)
            {
                ORQUESTRA.CICLO_TEXTO();
                if (ORQUESTRA.guiaPALAVRAS == 8 && JOGADOR.GetComponent<CharacterController>().enabled)
                {
                    JOGADOR.GetComponent<SC_FPSController>().enabled = false;
                    JOGADOR.GetComponent<Animator>().SetInteger("andando", 0);
                    escurecer = true;

                }else if (ORQUESTRA.guiaPALAVRAS == 11 && ORQUESTRA.escuro.color.a > 0.6f && ORQUESTRA.escuro.color.a < 1f)
                {
                    JOGADOR.GetComponent<CapsuleCollider>().enabled = false;
                    JOGADOR.GetComponent<BoxCollider>().enabled = true;

                    JOGADOR.GetComponent<Rigidbody>().freezeRotation = false;
                    JOGADOR.GetComponent<Rigidbody>().AddForce(0.1f, 0f, 0.1f);
                    JOGADOR.GetComponent<Rigidbody>().useGravity = true;
                    ESCURECER += Time.deltaTime * 0.08f;

                }
                if (escurecer && ORQUESTRA.escuro.color.a < 1f)
                {
                    ESCURECER += Time.deltaTime * 0.02f;
                    ORQUESTRA.escuro.color = new Color(0f, 0f, 0f, ESCURECER);
                }
                if(ORQUESTRA.escuro.color.a >= 1f)
                {
                    ORQUESTRA.SEQUENCIA[3] = true;
             
                }

            }else if (ORQUESTRA.SEQUENCIA[3] && ORQUESTRA.SEQUENCIA[5] != true)
            {
                if(ORQUESTRA.guiaPALAVRAS <= 11 && ORQUESTRA.SEQUENCIA[4] != true)
                {
                    ORQUESTRA.CICLO_TEXTO();
                }
                
                
                if (ORQUESTRA.escuro.color.a > 0 && ORQUESTRA.guiaPALAVRAS > 11)
                {
                    ESCURECER -= Time.deltaTime * 0.04f;
                    ORQUESTRA.escuro.color = new Color(0f, 0f, 0f, ESCURECER);
                    if (ORQUESTRA.SEQUENCIA[4] != true)
                    {
                        ORQUESTRA.SEQUENCIA[4] = true;
                        ORQUESTRA.PALAVRAS.Clear();

                        //ORQUESTRA.guiaPALAVRAS = 0;
                        ORQUESTRA.auxESMAECER = false;
                        ORQUESTRA.tempoPALAVRAS = 0;
                        JOVEM.SetActive(true);
                        JOVEM.transform.position = new Vector3(JOGADOR.transform.position.x, JOGADOR.transform.position.y, JOGADOR.transform.position.z);
                        JOVEM.GetComponent<NPC_IA>().andando = true;
                        JOVEM.GetComponent<NPC_IA>().velocidade = 0.5f;
                    }
                }
                else if(ORQUESTRA.escuro.color.a <= 0)
                {
                    
                    JOGADOR.GetComponent<CapsuleCollider>().enabled = true;
                    JOGADOR.GetComponent<BoxCollider>().enabled = false;

                    JOGADOR.GetComponent<Rigidbody>().freezeRotation = true;
                    JOGADOR.transform.rotation = Quaternion.Euler(0f, 156.07f, 0f);
                    JOGADOR.GetComponent<Rigidbody>().useGravity = false;

                    JOGADOR.GetComponent<SC_FPSController>().enabled = true;
                    escurecer = false;
                    ORQUESTRA.SEQUENCIA[5] = true;
                }

                
            }
           
            if (ORQUESTRA.SEQUENCIA[5])
            {
     
                if (ORQUESTRA.SEQUENCIA[6] != true)
                {

                    JOGADOR.transform.LookAt(new Vector3(JOVEM.transform.position.x, JOGADOR.transform.position.y, JOVEM.transform.position.z));
                    JOVEM.GetComponent<NPC_IA>().Interact();
                    
                    ORQUESTRA.SEQUENCIA[6] = true;

                } 
                
                if (PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf == false)
                {
                    //MANDAR O JOVEM EMBORA
                    Debug.Log(JOGADOR);

                    if (auxJOVEM == false)
                    {

                        if (Mathf.Sqrt(((JOVEM.transform.position.x - JOGADOR.transform.position.x) * (JOVEM.transform.position.x - JOGADOR.transform.position.x)) + ((JOVEM.transform.position.z - JOGADOR.transform.position.z) * (JOVEM.transform.position.z - JOGADOR.transform.position.z))) > 14f)
                        {
                            JOVEM.SetActive(false);
                            JOGADOR = null;
                            auxJOVEM = true;
                            MENCIONATUTORIAL = 3;
							Seta.SetActive(true);
                        }
                        else
                        {
                            JOVEM.GetComponent<NPC_IA>().cansaco = 10f;
                            JOVEM.GetComponent<NPC_IA>().velocidade = 3f;
                        }
                    }
                    
                }
            }
            //TUTORIAL
            if(MENCIONATUTORIAL == 3)
            {
                MENCIONATUTORIAL += ORQUESTRA.DICAS("Quanto maior for a sua sede, mais rápido você ficará cansado. Certifique-se de se manter bem hidratado!");
            }
            else if(MENCIONATUTORIAL == 4)
            {
                MENCIONATUTORIAL += ORQUESTRA.DICAS("Lojas estão dispostas por todo o mapa para você poder comprar consumíveis e aprimorar suas habilidades.");
            }else if(MENCIONATUTORIAL == 5)
            {
                ORQUESTRA.COMECO = false;
                ORQUESTRA.REINICIAR();
                ORQUESTRA.COMECO2 = true; //INICIA A SEGUNDA PARTE DO INICIO DA HISTÓRIA!
            }
            
        }
    }

    //ORQUESTRA.PALAVRAS.Clear();
}
