using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desapareca : MonoBehaviour
{
    public bool spawnarOutroNPC;
	public GameObject proximoNPC;
	public GameObject meshRendererPersonagem;

    void Start()
    {
			meshRendererPersonagem.GetComponent<SkinnedMeshRenderer>().enabled = false;
			proximoNPC.GetComponent<CapsuleCollider>().enabled = false;
			proximoNPC.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<SistemaNPCPrincipal>().missaoAcabou[this.gameObject.GetComponent<SistemaNPCPrincipal>().idMissao] == true)
		{
			this.gameObject.SetActive(false);
			meshRendererPersonagem.GetComponent<SkinnedMeshRenderer>().enabled = true;
			proximoNPC.GetComponent<CapsuleCollider>().enabled = true;
			proximoNPC.GetComponent<Rigidbody>().useGravity = true;
		}
    }
}
