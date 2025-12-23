using UnityEngine;

/// <summary>
/// Gère l'affichage et la transition entre les différents panneaux de l'interface utilisateur.
/// Assure qu'un seul panneau est actif à la fois pour une navigation claire.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Références des Panneaux")]
    public GameObject mainMenuPanel;
    public GameObject lightPanel;
    public GameObject colorPanel;
    public GameObject furniturePanel;

    /// <summary> Garde une trace du panneau actuellement affiché. </summary>
    private GameObject currentPanel;

    /// <summary>
    /// Initialise l'interface en affichant le menu principal au démarrage.
    /// </summary>
    void Start()
    {
        OpenMainMenu();
    }

    #region Méthodes d'Ouverture des Panneaux

    public void OpenMainMenu()
    {
        SwitchPanel(mainMenuPanel);
    }

    public void OpenLightPanel()
    {
        SwitchPanel(lightPanel);
    }

    public void OpenColorPanel()
    {
        SwitchPanel(colorPanel);
    }

    public void OpenFurniturePanel()
    {
        SwitchPanel(furniturePanel);
    }

    #endregion

    /// <summary>
    /// Méthode utilitaire pour fermer tous les panneaux avant d'en activer un nouveau.
    /// </summary>
    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        lightPanel.SetActive(false);
        colorPanel.SetActive(false);
        furniturePanel.SetActive(false);
    }

    /// <summary>
    /// Centralise la logique de basculement pour éviter la répétition de code.
    /// </summary>
    /// <param name="targetPanel">Le panneau GameObject à activer.</param>
    private void SwitchPanel(GameObject targetPanel)
    {
        CloseAllPanels();
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
            currentPanel = targetPanel;
        }
    }

    /// <summary>
    /// Fonction de retour rapide vers le menu principal.
    /// </summary>
    public void BackToMenu()
    {
        OpenMainMenu();
    }
}