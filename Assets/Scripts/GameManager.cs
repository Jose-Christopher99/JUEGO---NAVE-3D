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
            textoVidas.text = "Vidas: " + vidasActuales;
        }

        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntaje: " + puntajeActual;
        }
    }

    void GameOver()
    {
        Debug.Log("¡Game Over! Puntaje final: " + puntajeActual);

        NaveController nave = FindAnyObjectByType<NaveController>();
        if (nave != null)
        {
            nave.Morir();
            Destroy(nave.gameObject);
        }
    }
}
