using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAUCHEIRO : MonoBehaviour
{
    public float velocidade = 5f;
	private float cont = 0f;
    private int direction = 0;
    public float cansaco = 0f; //DEFINE O TEMPO DE CAMINHADA DO NPC
    private float recuperacao = 0f; //TEMPO DE RECUPERAÇÃO PÓS-CAMINHADA
    private bool cicloAndar = false; //Auxilia na execução do ciclo de caminhada do objeto em função do tempo
    public bool andando = true; //Define se o objeto pode andar
    private GameObject NPC;

	private float rastroX;
    private float rastroZ;

    public bool RESPAWN = true;

    private float TEMPOrespawn = 120f;
    // Start is called before the first frame update
    void Start()
    {
        
        NPC = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ANDAR();

        //-------------RESPAWN------------
        
        if (TEMPOrespawn > 0)
        {
            TEMPOrespawn -= Time.deltaTime;
        }
        else
        {
            if (RESPAWN)
            {
                TEMPOrespawn = 10;
                NPC.transform.position = new Vector3(Random.Range(-380, -122), 71.84f, Random.Range(0, 95) - 42f);
            }
            else{
                NPC.transform.position = new Vector3(Random.Range(250, 287), 71.84f, Random.Range(-30, 0));
            }
        }
        
    }
    void ANDAR()
    {
        if (cont == 0f) // REINICIA A VERIFICAÇÃO DE COLISÃO DO NPC
        {
            rastroX = NPC.transform.position.x;
            rastroZ = NPC.transform.position.z;
        }

        if (cicloAndar == false)
        {
            cansaco = UnityEngine.Random.Range(1, 11);
            recuperacao = UnityEngine.Random.Range(1, 11);
            direction = UnityEngine.Random.Range(0, 361);
            cicloAndar = true;
        }

        cansaco -= Time.deltaTime;

        if (cansaco < 0)
        {
            recuperacao -= Time.deltaTime;
            if (recuperacao < 0)
            {
                cicloAndar = false;
            }
        }
        else if(Mathf.Sqrt(Mathf.Pow(GameObject.Find("Scavenger Variant").transform.position.x - NPC.transform.position.x, 2) + Mathf.Pow(GameObject.Find("Scavenger Variant").transform.position.z - NPC.transform.position.z, 2)) > 14f)
        {
           
            NPC.transform.rotation = Quaternion.Euler(0f, direction, 0f);
            NPC.transform.Translate(transform.TransformDirection(Vector3.forward) * velocidade * Time.deltaTime);
            cont += Time.deltaTime;
            
        }

        if(Mathf.Sqrt(Mathf.Pow(GameObject.Find("Scavenger Variant").transform.position.x - NPC.transform.position.x, 2) + Mathf.Pow(GameObject.Find("Scavenger Variant").transform.position.z - NPC.transform.position.z, 2)) < 14f && PROPRIEDADES_JOGADOR.fome > 25 && PROPRIEDADES_JOGADOR.sede > 25)
        {
            NPC.transform.LookAt(new Vector3(GameObject.Find("Scavenger Variant").transform.position.x, NPC.transform.position.y, GameObject.Find("Scavenger Variant").transform.position.z));
            NPC.transform.Translate(transform.TransformDirection(Vector3.forward) * SC_FPSController.runningSpeed * Time.deltaTime);
        }

        if (cont >= 0.5f) // VERIFICA SE O NPC FICOU EMPERRADO
        {

            cont = 0f;

            if ((Mathf.Sqrt(Mathf.Pow(NPC.transform.position.x - rastroX, 2) + Mathf.Pow(NPC.transform.position.z - rastroZ, 2))) < velocidade * 0.375f)
            {
                direction = UnityEngine.Random.Range(0, 361);
            }

        }

    }
    public void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.name == "Scavenger Variant")
        {
            PROPRIEDADES_JOGADOR.fome -= 150 * Time.deltaTime;
            PROPRIEDADES_JOGADOR.sede -= 150 * Time.deltaTime;
            ORQUESTRA.SONG.clip = Resources.Load<AudioClip>("Scenes/JOGO/AUDIO/TOSSE 1");
            ORQUESTRA.SONG.Play();
        }
    }
}
