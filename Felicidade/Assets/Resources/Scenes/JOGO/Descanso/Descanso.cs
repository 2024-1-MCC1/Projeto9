using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descanso : MonoBehaviour, IInteractable
{
    public static GameObject telaDescanso;
    private static float delayinseconds = 2;
    public static PROPRIEDADES_JOGADOR PROPRIEDADES_JOGADOR;
    void Start()
    {
        //telaDescanso = GameObject.Find("TelaDescanso");
        //telaDescanso.SetActive(false);
        PROPRIEDADES_JOGADOR = GameObject.Find("JOGADOR").GetComponent<PROPRIEDADES_JOGADOR>();
    }

    public void Interact()
    {
        telaDescanso.SetActive(true);
        Invoke("Nanar", delayinseconds);
    }

    public void Nanar()
    {
        PROPRIEDADES_JOGADOR.energia = 100f;
        telaDescanso.SetActive(false);
    }
}
