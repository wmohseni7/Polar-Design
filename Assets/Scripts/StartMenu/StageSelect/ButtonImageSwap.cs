using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Gère une transition fluide par fondu (fade) entre deux images lors du survol de la souris.
/// Idéal pour des boutons de menu élégants et interactifs.
/// </summary>
public class ButtonImageSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Configuration des Images")]
    /// <summary> L'image visible par défaut (au-dessus). </summary>
    public Image imageDefault; 
    
    /// <summary> L'image qui apparaît lors du survol (en-dessous). </summary>
    public Image imageHover;   

    [Header("Paramètres d'Animation")]
    /// <summary> Vitesse de la transition alpha. </summary>
    public float fadeSpeed = 10f;

    /// <summary> État interne pour savoir si la souris survole l'objet. </summary>
    private bool hovered = false;

    /// <summary>
    /// Met à jour l'opacité de l'image par défaut à chaque frame pour créer l'effet de fondu.
    /// </summary>
    void Update()
    {
        // Si survolé, on veut que l'image par défaut soit invisible (0), sinon visible (1)
        float targetAlpha = hovered ? 0f : 1f;

        // Récupération de la couleur actuelle
        Color c = imageDefault.color;
        
        // Modification progressive de l'alpha vers la cible
        c.a = Mathf.MoveTowards(c.a, targetAlpha, fadeSpeed * Time.deltaTime);
        
        // Réapplication de la couleur modifiée
        imageDefault.color = c;
    }

    /// <summary>
    /// Détecte quand la souris entre dans la zone du bouton.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    /// <summary>
    /// Détecte quand la souris quitte la zone du bouton.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}