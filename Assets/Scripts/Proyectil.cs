using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    [Tooltip("Velocidad de avance del disparo.")]
    public float velocidad = 15f;
    [Tooltip("Tiempo en segundos antes de autodestruirse.")]
    public float tiempoVida = 3f;

    void Start()
    {
        // Destruye el clon después de X segundos para no saturar la memoria
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        // Mueve el proyectil constantemente hacia arriba (Y positivo) en coordenadas del mundo
        transform.Translate(Vector3.up * velocidad * Time.deltaTime, Space.World);
    }

}
