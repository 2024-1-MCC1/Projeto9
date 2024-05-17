using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRANSITION_CAMERA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x < -4.2)
        {
            this.gameObject.transform.Translate(0, 0, 0.0001f);
            if (Input.GetMouseButtonDown(0))
            {
                this.gameObject.transform.position = new Vector3(-4.2f, 0.48f, -7.38f);
                GameObject.Find("Jogar_Button").transform.position = new Vector3(0.324f, 1.25f, -4.25f);
                GameObject.Find("Creditos_Button").transform.position = new Vector3(0.324f, 0.35f, -4.25f);
                GameObject.Find("LUZGIRA").transform.Rotate(0, 90f, 0);
            }
        }
        
    }
}
