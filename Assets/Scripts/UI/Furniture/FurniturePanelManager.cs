using UnityEngine;

/// <summary>
/// Gère l'affichage et le masquage du panneau d'interface utilisateur pour les meubles.
/// Utilise le modèle Singleton pour permettre un accès rapide depuis les déclencheurs de la scène.
/// </summary>
public class FurniturePanelManager : MonoBehaviour
{
    /// <summary> Instance unique accessible globalement dans la scène. </summary>
    public static FurniturePanelManager Instance { get; private set; }

    [Header("Panel du menu (UI)")]
    /// <summary> Référence vers l'objet racine du panneau de mobilier (CanvasGroup ou Panel). </summary>
    public GameObject furniturePanel; 

    private void Awake()
    {
        // Implémentation du Singleton : s'assure qu'il n'y a qu'un seul gestionnaire de panneau
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Active le panneau de mobilier pour le rendre visible à l'utilisateur.
    /// </summary>
    public void OpenPanel()
    {
        if (furniturePanel != null)
            furniturePanel.SetActive(true);
    }

    /// <summary>
    /// Désactive le panneau de mobilier.
    /// </summary>
    public void ClosePanel()
    {
        if (furniturePanel != null)
            furniturePanel.SetActive(false);
    }

    /// <summary>
    /// Méthode de haut niveau appelée lors de l'interaction avec un meuble.
    /// Elle coordonne la mise à jour des données et l'affichage de l'interface.
    /// </summary>
    /// <param name="p1">Option de meuble 1</param>
    /// <param name="p2">Option de meuble 2</param>
    /// <param name="p3">Option de meuble 3</param>
    /// <param name="p4">Option de meuble 4</param>
    public void ShowFurnitureMenu(GameObject p1, GameObject p2, GameObject p3, GameObject p4)
    {
        // 1. Délègue la mise à jour des données au MenuManager
        if (FurnitureMenuManager.Instance != null)
        {
            FurnitureMenuManager.Instance.ShowFurnitureOptions(p1, p2, p3, p4);
        }

        // 2. Déclenche l'ouverture visuelle
        OpenPanel();
    }
}