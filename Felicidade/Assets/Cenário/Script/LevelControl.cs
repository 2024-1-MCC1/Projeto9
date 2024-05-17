using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int index;
    void OnTriggerEnter3D(Collision batida) 
    {
        if (batida.gameObject.name == "Jogador")
        {
            //Loading level with build index
            SceneManager.LoadScene(index);
        }
    }
}
