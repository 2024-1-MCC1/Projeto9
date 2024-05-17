using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CREDITOS_SCRIPT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TRANSITION_BUTTON.creditos == true && GameObject.Find("Main Camera").transform.position.x >= 5)
        {
            this.gameObject.transform.Translate(0f,Input.mouseScrollDelta.y * (-0.1f),0f);
        }
    }
}
