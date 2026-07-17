using UnityEngine;

public class NaveController : MonoBehaviour
{
   [Header("Configuración de Movimiento")]
    public float velocidad = 10f;
    public float limiteX = 8.5f;
    public float limiteY = 4.5f;
    [SerializeField] private SimpleJoystick joystick;

    [Header("Configuración de Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float cadenciaDisparo = 0.25f;

    [Header("Audio")]
    public AudioClip sonidoDisparo;
    public AudioClip sonidoExplosionNave;

    [Header("Invulnerabilidad")]
    public float tiempoInvulnerabilidad = 2f;
    public float velocidadParpadeo = 0.15f;

    private float tiempoSiguienteDisparo = 0f;
    private AudioSource audioSource;
    private bool esInvulnerable = false;
    private bool estaViva = true;
    private MeshRenderer[] renderers;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        //Movimiento de la Nave
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        if (joystick != null && joystick.InputDirection != Vector2.zero)
        {
            movimientoHorizontal = joystick.InputDirection.x;
            movimientoVertical = joystick.InputDirection.y;
        }
        Vector3 direccion = new Vector3(movimientoHorizontal, movimientoVertical, 0f);
        Vector3 nuevaPosicion = transform.position + direccion * velocidad * Time.deltaTime;

        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -limiteX, limiteX);
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, -limiteY, limiteY);
        transform.position = nuevaPosicion;

        //Logica de Disparo
        if ((Input.GetKey(KeyCode.Space)) && Time.time >= tiempoSiguienteDisparo)
        {
            tiempoSiguienteDisparo = Time.time + cadenciaDisparo;
            Disparar();
        }
    }

    public void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            if (sonidoDisparo != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoDisparo);
            }
        }
        else
        {
            Debug.LogWarning("¡Atención! Asigna el 'Proyectil Prefab' y el 'Punto Disparo' en el Inspector de la Nave.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!estaViva) return; // Si ya "murió", ignora cualquier colisión

        if (other.GetComponent<Enemigo>() != null)
        {
            if (esInvulnerable)
            {
                Destroy(other.gameObject);
                return;
            }

            if (sonidoExplosionNave != null)
            {
                AudioSource.PlayClipAtPoint(sonidoExplosionNave, transform.position);
            }

            Destroy(other.gameObject);
            GameManager.Instance.RestarVida();

            StartCoroutine(RutinaInvulnerabilidad());
        }
    }

    private System.Collections.IEnumerator RutinaInvulnerabilidad()
    {
        esInvulnerable = true;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < tiempoInvulnerabilidad)
        {
            foreach (MeshRenderer rend in renderers)
            {
                rend.enabled = !rend.enabled;
            }

            yield return new WaitForSeconds(velocidadParpadeo);
            tiempoTranscurrido += velocidadParpadeo;
        }

        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = true;
        }

        esInvulnerable = false;
    }

    public void Morir()
    {
        estaViva = false;
    }
}