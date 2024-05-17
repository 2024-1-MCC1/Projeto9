using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CONVENCER : MonoBehaviour
{

    public static GameObject BOLA;
    public TMP_Text TEXTO;

    public static float TEMPO = 5;

    public static int DirectionX = 0;
    public static int DirectionY = 0;

    public static float X;
    public static float Y;

    public float velocidade = 100f;

    public bool TESTE = false;

    private float resgAux;
    // Start is called before the first frame update
    void Start()
    {

        BOLA = this.gameObject;
        TEXTO = GameObject.Find("TEMPO_CONV").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        velocidade = 1000 * Time.deltaTime;
        if (TESTE)
        {
            REINICIAR();
            TESTE = false;
        }
        if(TEMPO > 0)
        {
            TEMPO -= Time.deltaTime;
        }
        else
        {
            BOLA.SetActive(false);
        }

        X = BOLA.GetComponent<RectTransform>().transform.localPosition.x;
        Y = BOLA.GetComponent<RectTransform>().transform.localPosition.y;

        DirectionX = X > 935 || X < -935 ? DirectionX * (-1) : DirectionX;
        DirectionY = Y > 420 || Y < -420 ? DirectionY * (-1) : DirectionY;

        BOLA.GetComponent<RectTransform>().transform.localPosition = new Vector2(X + DirectionX * velocidade, Y + DirectionY * velocidade);
        TEXTO.text = ((int)TEMPO).ToString();

        if(PROPRIEDADES_JOGADOR.TELA_CONVERSA.activeSelf == false)
        {
            BOLA.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (Mathf.Sqrt(Mathf.Pow(BOLA.transform.position.x - Input.mousePosition.x, 2) + Mathf.Pow(BOLA.transform.position.y - Input.mousePosition.y, 2)) <= 45f && Input.GetMouseButtonDown(0))
        {
            BOLA.GetComponent<RectTransform>().transform.localScale = new Vector2(BOLA.GetComponent<RectTransform>().transform.localScale.x - 0.5f, BOLA.GetComponent<RectTransform>().transform.localScale.y - 0.5f);
        }

        if (BOLA.GetComponent<RectTransform>().transform.localScale.x <= 0.5f)
        {
            PROPRIEDADES_JOGADOR.pessoasNoMovimento++;
            BOLA.SetActive(false);

        }

    }

    public static void REINICIAR()
    {
        TEMPO = Random.Range(3,6);
        
        BOLA.GetComponent<RectTransform>().transform.localScale = new Vector2(2f, 2f);
        DirectionX = Random.Range(-1, 2);
        DirectionY = Random.Range(-1, 2);
        PROPRIEDADES_JOGADOR.JOGADOR.GetComponent<CharacterController>().enabled = false;
        PROPRIEDADES_JOGADOR.JOGADOR.GetComponent<SC_FPSController>().enabled = false;
        BOLA.GetComponent<RectTransform>().transform.localPosition = new Vector2(Random.Range(-935,935), Random.Range(-420, 120));
    }
}
