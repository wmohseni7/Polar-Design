using System;
using UnityEngine;

/// <summary>
/// Gère l'interactivité 3D d'un meuble et stocke les données des variantes disponibles.
/// Lorsqu'il est cliqué, il transmet ses informations au système d'interface utilisateur.
/// </summary>
public class FurnitureSelector : MonoBehaviour
{
    [Header("Variantes de Modèles (Prefabs)")]
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public GameObject prefab4;

    [Header("Métriques de Score par Variante")]
    public ScoreMetrics score1; 
    public ScoreMetrics score2;
    public ScoreMetrics score3;
    public ScoreMetrics score4;

    /// <summary> Référence vers le gestionnaire parent qui gère le point de spawn. </summary>
    private FurnitureManager parentManager;
    public bool IsModified { get; private set; } = false;

    // Appelle cette fonction quand le joueur confirme un changement sur ce meuble
    public void MarkAsModified()
    {
        if (!IsModified)
        {
            IsModified = true; // C'est cette variable que le compteur vérifie !
            
            // On demande au compteur de se rafraîchir
            if (ModificationCounterUI.Instance != null)
            {
                ModificationCounterUI.Instance.UpdateCount();
            }
        }
    }
    void Awake()
    {
        // On récupère le gestionnaire parent pour assurer la liaison logique
        parentManager = GetComponentInParent<FurnitureManager>();
        
        if (parentManager == null)
        {
            Debug.LogError($"[FurnitureSelector] sur {gameObject.name} : Aucun FurnitureManager trouvé dans les parents !");
        }
    }

    /// <summary>
    /// Détecte le clic de la souris sur le collider de l'objet.
    /// Déclenche la notification système pour ouvrir le menu de sélection.
    /// </summary>
    void OnMouseDown()
    {
        
        
        // Sécurité : on ne fait rien si le système d'UI ou le manager sont absents
        if (FurniturePanelManager.Instance == null || parentManager == null) 
            return;

        parentManager.SelectFurniture();
        
        // Publie l'événement de sélection, passant 'this' (le sélecteur) comme information
        // Le FurnitureMenuManager écoutera et s'occupera du reste.
        EventManager.NotifyFurnitureSelected(this);
    }

    /// <summary>
    /// Utilitaire permettant de récupérer le score associé à un modèle spécifique.
    /// </summary>
    /// <param name="prefab">Le prefab dont on veut connaître le score.</param>
    /// <returns>Les métriques de score correspondantes.</returns>
    public ScoreMetrics GetScoreForPrefab(GameObject prefab)
    {
        if (prefab == prefab1) return score1;
        if (prefab == prefab2) return score2;
        if (prefab == prefab3) return score3;
        if (prefab == prefab4) return score4;
        
        // Retourne (0,0,0) par défaut
        return new ScoreMetrics(0,0,0); 
    }

    
}