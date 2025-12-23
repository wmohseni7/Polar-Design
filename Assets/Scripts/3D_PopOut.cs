using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gère un effet visuel de grossissement (smooth scale) lorsqu'un élément de l'UI 
/// est survolé par la souris. Implémente les interfaces du EventSystem de Unity.
/// </summary>
public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Paramètres d'Échelle")]
    /// <summary> Taille normale du bouton. </summary>
    public Vector3 normalScale = Vector3.one;
    /// <summary> Taille du bouton lors du survol. </summary>
    public Vector3 hoverScale = new Vector3(2f, 2f, 2f);

    [Header("Animation")]
    /// <summary> Vitesse de la transition (interpolation). </summary>
    public float speed = 10f;

    /// <summary> État interne pour savoir si la souris est sur l'objet. </summary>
    private bool isHovering = false;

    /// <summary>
    /// Appelé par le EventSystem de Unity lorsque la souris entre dans la zone du collider/image.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    /// <summary>
    /// Appelé à chaque frame pour animer l'échelle de l'objet de manière fluide.
    /// </summary>
    void Update()
    {
        // Utilise Lerp pour passer d'une échelle à l'autre sans saccade
        transform.localScale = Vector3.Lerp(
            transform.localScale, 
            isHovering ? hoverScale : normalScale, 
            Time.deltaTime * speed
        );
    }

    /// <summary>
    /// Appelé par le EventSystem de Unity lorsque la souris quitte la zone.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}