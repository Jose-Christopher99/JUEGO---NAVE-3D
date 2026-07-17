using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [Header("Configuración de Vidas")]
    public int vidasIniciales = 3;
    private int vidasActuales;

    [Header("Configuración de Puntaje")]
    private int puntajeActual = 0;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoVidas;
    public TextMeshProUGUI textoPuntaje;
    
    public TextMeshProUGUI textoNivel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        vidasActuales = vidasIniciales;
        ActualizarUI();
        OcultarMensajeNivel();
    }

    public void RestarVida()
    {
        if (vidasActuales <= 0) return;

        vidasActuales--;
        ActualizarUI();

        if (vidasActuales <= 0)
        {
            GameOver();
        }
    }

    public void SumarPuntos(int cantidad)
    {
        puntajeActual += cantidad;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas x " + vidasActuales;
        }

        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Score: " + puntajeActual;
        }
    }

    public void MostrarMensajeNivel()
    {
        Debug.Log("SE LLAMÓ MostrarMensajeNivel");
        if (textoNivel != null)
        {
            Debug.Log("Texto asignado: " + textoNivel.name);
            textoNivel.text = "MISIÓN COMPLETADA\n\n↑ Avanza al siguiente nivel ↑";
            textoNivel.gameObject.SetActive(true);
            textoNivel.fontSize = 40;
        }
        else
        {
            Debug.Log("textoNivel es NULL");
        }
    }

    public void OcultarMensajeNivel()
    {
        if (textoNivel != null)
        {
            textoNivel.gameObject.SetActive(false);
        }
    }

    void GameOver()
    {
        Debug.Log("¡Game Over! Score final: " + puntajeActual);

        NaveController nave = FindAnyObjectByType<NaveController>();
        if (nave != null)
        {
            nave.Morir();
            Destroy(nave.gameObject);
        }
    }
}
