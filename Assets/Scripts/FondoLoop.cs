using UnityEngine;

public class FondoLoop : MonoBehaviour
{
    [Header("Configuración del Desplazamiento")]
    [Tooltip("Dirección y velocidad del scroll. X para horizontal, Y para vertical.")]
    /*DEFINE HACIA DONDE Y QUE TAN RAPIDO SE MOVERA EL FONDO. COMO ESTA CONFIGURADO
    SIGNIFICA QUE NO SE MUEVE HACIA LOS LADOS (X=0) Y SE MUEVE HACIA ABAJO (Y<0)
    A UNA VELOCIDAD DE 0.2*/
    public Vector2 direccionScroll = new Vector2(0f, -0.2f);
    //GUARDA LA REFERENCIA AL MATERIAL DEL OBJETO QUAD PARA PODER MODIFICRLO EN TIEMPO REAL
    private Material miMaterial;
    //ES EL ACUMULADOR QUE GUARDA LA POSICION ACTUAL DEL DESPLAZAMIENTO DEL FONDO, SE INICIALIZA EN CERO PARA EMPEZAR DESDE EL PRINCIPIO
    private Vector2 offsetActual = Vector2.zero;

    void Start()
    {
        // 1. Obtenemos el componente Renderer del Quad para modificar su material.
        Renderer miRenderer = GetComponent<Renderer>();
        
        if (miRenderer != null)
        {
            // .material crea una instancia única del material para este objeto.
            miMaterial = miRenderer.material;
        }
        else
        {
            Debug.LogError("¡Coloca este script en un Quad que tenga un Renderer!");
        }
    }

    void Update()
    {
        if (miMaterial != null)
        {
            // 2. Calculamos el nuevo desplazamiento basándonos en el tiempo transcurrido (Time.deltaTime)
            offsetActual += direccionScroll * Time.deltaTime;

            // 3. Aplicamos el desplazamiento a la propiedad de offset del material.
            miMaterial.mainTextureOffset = offsetActual;
        }
    }
}
