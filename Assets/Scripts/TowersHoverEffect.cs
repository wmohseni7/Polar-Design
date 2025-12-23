using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Gère l'effet de survol des tours 3D.
/// Ce script est attaché aux Colliders 3D dans la scène. Il utilise le EventSystem 
/// pour détecter la souris et modifier l'opacité d'une image UI correspondante.
/// </summary>
public class TowersHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Référence UI")]
    /// <summary> L'image UI (Overlay) qui doit s'afficher au survol de cette tour. </summary>
    public Image targetOverlayImage; 
    
    [Header("Paramètres de l'effet")]
    /// <summary> Durée du fondu (fade in/out). </summary>
    public float fadeDuration = 0.15f; 
    
    private Coroutine fadeCoroutine;

    void Start()
    {
        if (targetOverlayImage == null)
        {
            Debug.LogError($"Target Overlay Image non assignée sur {gameObject.name} !");
            return;
        }

        // Initialise l'overlay en toute transparence (Alpha = 0)
        Color startColor = targetOverlayImage.color;
        targetOverlayImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    /// <summary>
    /// Déclenché quand la souris entre dans le Sphere Collider.
    /// Lance le fondu vers l'opacité maximale.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(1f));
    }

    /// <summary>
    /// Déclenché quand la souris quitte le Sphere Collider.
    /// Lance le fondu vers la transparence totale.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(0f));
    }

    /// <summary>
    /// Coroutine gérant la transition fluide de l'opacité (Alpha).
    /// </summary>
    /// <param name="targetAlpha">Opacité cible (0 pour transparent, 1 pour opaque).</param>
    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = targetOverlayImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            
            Color currentColor = targetOverlayImage.color;
            targetOverlayImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            yield return null; 
        }

        // Assure que la valeur finale est exacte
        Color finalColor = targetOverlayImage.color;
        targetOverlayImage.color = new Color(finalColor.r, finalColor.g, finalColor.b, targetAlpha);
    }
}