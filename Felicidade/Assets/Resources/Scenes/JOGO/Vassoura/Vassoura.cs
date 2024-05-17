using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vassoura : MonoBehaviour
{
	private Animator animator;
	private bool varreu = false;
	
    void Start()
    {
        GameObject.Find("MMarket_WizardBroomstick_LOD0").GetComponent<MeshRenderer>().enabled = false;
		GameObject.Find("MMarket_WizardBroomstick_Collision_08").GetComponent<BoxCollider>().enabled = false;
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Varrendo();
    }
	
	void Varrendo()
	{
		if(Input.GetKeyDown(KeyCode.V) && varreu== false)
		{
			GameObject.Find("MMarket_WizardBroomstick_LOD0").GetComponent<MeshRenderer>().enabled = true;
			GameObject.Find("MMarket_WizardBroomstick_Collision_08").GetComponent<BoxCollider>().enabled = true;
			animator.SetBool("Varre", true);
			varreu = true;
		}else if(Input.GetKeyDown(KeyCode.V) && varreu == true)
			{
				GameObject.Find("MMarket_WizardBroomstick_LOD0").GetComponent<MeshRenderer>().enabled = false;
				GameObject.Find("MMarket_WizardBroomstick_Collision_08").GetComponent<BoxCollider>().enabled = false;
				animator.SetBool("Varre", false);
				varreu = false;
			}
	}
}
