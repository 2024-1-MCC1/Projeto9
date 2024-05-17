using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUZROTORICA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TRANSITION_BUTTON.creditos == false) { this.gameObject.transform.Rotate(0, 0.03f, 0); }
        
    }
}
