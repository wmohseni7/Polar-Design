using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gère l'apparence du curseur de la souris au sein du jeu.
/// Permet de basculer entre un curseur par défaut et un curseur de survol personnalisé.
/// </summary>
public class CursorManager : MonoBehaviour
{
    /// <summary> Instance unique du CursorManager (Singleton) accessible depuis n'importe quel script. </summary>
    public static CursorManager Instance { get; private set; }

    [Header("Configuration du Curseur")]
    /// <summary> Texture personnalisée à afficher lors du survol d'un objet interactif. </summary>
    public Texture2D hoverCursorTexture; 
    
    /// <summary> Point d'ancrage précis de la texture du curseur (généralement le bout du pointeur). </summary>
    public Vector2 hotSpot = Vector2.zero; 

    /// <summary>
    /// Initialise le Singleton et détruit toute instance supplémentaire pour garantir l'unicité du manager.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Modifie l'apparence du curseur pour utiliser la texture de survol (hover).
    /// </summary>
    public void SetHoverCursor()
    {
        // CursorMode.Auto permet au système de gérer le rendu (plus rapide)
        Cursor.SetCursor(hoverCursorTexture, hotSpot, CursorMode.Auto);
    }

    /// <summary>
    /// Réinitialise le curseur vers l'apparence par défaut du système d'exploitation.
    /// </summary>
    public void SetDefaultCursor()
    {
        // Mettre null remet le curseur par défaut du système (Windows/Mac)
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}