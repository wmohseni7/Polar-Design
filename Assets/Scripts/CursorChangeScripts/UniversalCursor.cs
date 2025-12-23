using UnityEngine;
using UnityEngine.EventSystems; // INDISPENSABLE pour l'UI

public class UniversalCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Configuration")]
    public Texture2D hoverCursor; // L'image du curseur
    public Vector2 hotSpot = Vector2.zero; // Le point de clic

    // --- PARTIE 1 : POUR L'INTERFACE UTILISATEUR (UI / BOUTONS) ---
    
    // Détecte le survol de la souris sur un élément UI
    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeCursor();
    }

    // Détecte la sortie de la souris d'un élément UI
    public void OnPointerExit(PointerEventData eventData)
    {
        ResetCursor();
    }

    // --- PARTIE 2 : POUR LES OBJETS 3D (AVEC COLLIDER) ---

    // Détecte le survol sur un objet 3D
    private void OnMouseEnter()
    {
        ChangeCursor();
    }

    // Détecte la sortie sur un objet 3D
    private void OnMouseExit()
    {
        ResetCursor();
    }

    // --- LOGIQUE COMMUNE ---

    private void ChangeCursor()
    {
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
        }
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    
    // Sécurité : Si l'objet est désactivé, on reset
    private void OnDisable()
    {
        ResetCursor();
    }
}