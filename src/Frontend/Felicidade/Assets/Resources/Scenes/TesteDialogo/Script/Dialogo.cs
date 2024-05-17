using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour, IInteractable
{
    public GameObject dialogoBox;
    public TMP_Text dialogo;
    public string nomeNPC ="Paulo";
    public TMP_Text nome;
    public string[] dialogoatual= {"Olá","Tudo bem?" };
    public int numeroDeDialogos=0;
    public bool dialogoFinal = false;
    void Start()
    {
        nome.SetText("Paulo");
        dialogoBox.SetActive(false);
    }

    public void Interact()
    {
        if (numeroDeDialogos == dialogoatual.Length)
        {
            dialogoBox.SetActive(false);
        }
        else
        {
            dialogoBox.SetActive(true);
            nome.SetText("Paulo");
            dialogo.SetText(dialogoatual[numeroDeDialogos]);
            numeroDeDialogos++;
        }
        
    }
    void DialogoComecou()
    {
        while (numeroDeDialogos != dialogoatual.Length)
        {
            if (Input.GetButton("Fire1"))
            {
                numeroDeDialogos++;
                dialogo.SetText(dialogoatual[numeroDeDialogos]);
            }
        }
        dialogoBox.SetActive(false);
    }
}
