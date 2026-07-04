using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Configuración del Enemigo")]
    public float velocidad = 4f;
    public float limiteYInferior = -6f;

    [Header("Audio")]
    [Tooltip("Sonido que se reproduce al destruirse el enemigo (por bala).")]
    public AudioClip sonidoExplosion;

    void Update()
    {
        //Mueve el enemigo hacia abajo constantemente
        transform.Translate(Vector3.down * velocidad * Time.deltaTime, Space.World);

        //destrucción automática al salir de la pantalla por debajo
        if (transform.position.y < limiteYInferior)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Proyectil>() != null)
    {
        if (sonidoExplosion != null)
        {
            AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
        //sumamos puntos al destruir un meteorito con una bala
        GameManager.Instance.SumarPuntos(10);
    }
        /*colisión con un Proyectil (bala)
        if (other.GetComponent<Proyectil>() != null)
        {
            if (sonidoExplosion != null)
            {
                AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }*/
    }

}
