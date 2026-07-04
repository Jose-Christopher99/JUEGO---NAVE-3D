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

    void Start()
    {
        // Llama repetidamente al método SpawnEnemigo
        InvokeRepeating("SpawnEnemigo", tiempoInicial, tiempoEntreEnemigos);
    }

    void SpawnEnemigo()
    {
        if (enemigoPrefab != null)
        {
            // Calcula una coordenada X aleatoria
            float xAleatorio = Random.Range(-limiteX, limiteX);
            Vector3 posicionSpawn = new Vector3(xAleatorio, spawnY, 0f);

            // Instancia el clon del enemigo
            Instantiate(enemigoPrefab, posicionSpawn, Quaternion.identity);
        }
    }

}
