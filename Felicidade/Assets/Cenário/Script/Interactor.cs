using UnityEngine;

interface IInteractable
{
    public void Interact();


}

public class Interactor : MonoBehaviour
{
    private Transform InteractorSource;
    private float InteractRange;

    public static GameObject LEGENDA;

    void Start()
    {
        InteractorSource = GameObject.Find("Camera").transform;
        InteractRange = 2;
        LEGENDA = GameObject.Find("E-PARA-INTERAGIR");
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf == false && PROPRIEDADES_JOGADOR.FAIXAJ.activeSelf == false)//
                {
                    LEGENDA.SetActive(true);
                }else if (PROPRIEDADES_JOGADOR.FAIXAJ.activeSelf)
                {
                    LEGENDA.SetActive(false);
                }
               

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
        }
        else
        {
            LEGENDA.SetActive(false);
            if (PROPRIEDADES_JOGADOR.FAIXAJ.activeSelf)
            { 
                FAIXA.PILHA = null;
                PROPRIEDADES_JOGADOR.FAIXAJ.SetActive(false);
            }
        }
        
    }
}
