using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script para un Joystick Virtual básico en Unity.
/// Ideal para aprender el uso de interfaces de eventos de interfaz de usuario (UI).
/// </summary>
public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Referencias UI")]
    [SerializeField] private RectTransform joystickBackground; // Imagen de fondo del joystick
    [SerializeField] private RectTransform joystickHandle;     // Imagen de la palanca central

    [Header("Configuración")]
    [Tooltip("Rango máximo de movimiento de la palanca (en porcentaje del fondo)")]
    [SerializeField] private float dragRange = 100f;

    // Vector de dirección de entrada (-1 a 1 en X e Y)
    private Vector2 inputVector = Vector2.zero;

    // Propiedad pública para leer la dirección de movimiento desde otros scripts (como PlayerController)
    public Vector2 InputDirection => inputVector;

    private void Start()
    {
        // Si no se asignan en el inspector, buscamos referencias por defecto
        if (joystickBackground == null)
        {
            joystickBackground = GetComponent<RectTransform>();
        }
        
        if (joystickHandle == null && transform.childCount > 0)
        {
            joystickHandle = transform.GetChild(0).GetComponent<RectTransform>();
        }
            
        // Asegurar que la palanca comience en el centro absoluto
        if (joystickHandle != null)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
        }
    }

    /// <summary>
    /// Evento llamado continuamente mientras se arrastra el dedo/mouse sobre el joystick.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        
        // Convertir la posición de pantalla del toque/mouse a posición local dentro del RectTransform del fondo
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground, 
            eventData.position, 
            eventData.pressEventCamera, 
            out position))
        {
            // Normalizar la posición en base al tamaño del fondo
            position.x = (position.x / joystickBackground.sizeDelta.x) * 2;
            position.y = (position.y / joystickBackground.sizeDelta.y) * 2;

            // Crear el vector de entrada con la posición relativa
            inputVector = new Vector2(position.x, position.y);
            
            // Si el arrastre supera el círculo (magnitud > 1), lo limitamos (normalizar)
            if (inputVector.magnitude > 1.0f)
            {
                inputVector = inputVector.normalized;
            }

            // Desplazar visualmente el Handle (la palanca)
            if (joystickHandle != null)
            {
                joystickHandle.anchoredPosition = new Vector2(
                    inputVector.x * (joystickBackground.sizeDelta.x / 2f) * (dragRange / 100f),
                    inputVector.y * (joystickBackground.sizeDelta.y / 2f) * (dragRange / 100f)
                );
            }
        }
    }

    /// <summary>
    /// Evento llamado inmediatamente cuando el usuario presiona el joystick por primera vez.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // Forzar el comportamiento de arrastre inmediatamente al presionar
        OnDrag(eventData);
    }

    /// <summary>
    /// Evento llamado cuando el usuario levanta el dedo o suelta el clic.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // Restablecer valores a cero para detener el movimiento del jugador
        inputVector = Vector2.zero;
        
        if (joystickHandle != null)
        {
            joystickHandle.anchoredPosition = Vector2.zero;
        }
    }
}