using UnityEngine;

public class GenerarEnemigo : MonoBehaviour
{
    [Header("Configuración del Generador")]
    public GameObject enemigoPrefab;
    public float tiempoInicial = 1f;
    public float tiempoEntreEnemigos = 1.5f;

    [Header("Límites de Aparición")]
    public float limiteX = 8f;
    public float spawnY = 6f;

    [Header("Cantidad de Meteoritos")]
    public int maxMeteoritos = 10;

    private int meteoritosGenerados = 0;

    public bool NivelCompletado => meteoritosGenerados >= maxMeteoritos;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemigo), tiempoInicial, tiempoEntreEnemigos);
    }

    void SpawnEnemigo()
    {
        // Si ya se generó el máximo, detener el generador
        if (meteoritosGenerados >= maxMeteoritos)
        {
            CancelInvoke(nameof(SpawnEnemigo));
            GameManager.Instance.MostrarMensajeNivel();
            return;
        }

        if (enemigoPrefab != null)
        {
            float xAleatorio = Random.Range(-limiteX, limiteX);
            Vector3 posicionSpawn = new Vector3(xAleatorio, spawnY, 0f);

            Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);

            meteoritosGenerados++;
        }
    }
}