using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la logique du menu de pause, incluant la navigation entre les panneaux UI, 
/// le réglage du temps de jeu (Time.timeScale) et la redirection vers les scènes de sélection ou le menu principal.
/// </summary>
public class PauseMenuHandler : MonoBehaviour
{
    // --- Public References (Link in Inspector) ---
    [Header("UI Panels")]
    /// <summary> Bouton permettant de passer à la pièce suivante. </summary>
    [Tooltip("The next room button")]
    public GameObject NextRoomButton;
    
    [Header("UI Panels")]
    /// <summary> Panneau principal contenant les éléments du menu de pause. </summary>
    [Tooltip("The main panel that contains all pause menu elements.")]
    public GameObject pausePanel;

    /// <summary> Panneau contenant les menus de navigation généraux. </summary>
    [Tooltip("The panel that holds the game settings (audio, controls, etc.).")]
    public GameObject menuPanel;
    
    /// <summary> Panneau dédié aux réglages du jeu (audio, contrôles, etc.). </summary>
    [Tooltip("The panel that holds the game settings (audio, controls, etc.).")]
    public GameObject settingsPanel;

    [Header("Manager Dependency")]
    /// <summary> Référence au gestionnaire de sélection d'étage pour les changements de scène. </summary>
    [Tooltip("Reference to your StageSelectManager for scene changes.")]
    public StageSelectManager stageSelectManager;
    
    /// <summary> Panneau permettant de basculer entre les caméras. </summary>
    public GameObject switchCameraPanel;
    
    // --- Initial Setup ---
    
    /// <summary>
    /// Initialise l'état des panneaux au démarrage et tente de trouver le StageSelectManager s'il n'est pas lié.
    /// </summary>
    void Start()
    {
        // On s'assure que les panneaux sont masqués au début
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        
        // Vérification de sécurité pour trouver le manager si non lié dans l'Inspecteur
        if (stageSelectManager == null)
        {
            stageSelectManager = FindObjectOfType<StageSelectManager>();
            if (stageSelectManager == null)
            {
                Debug.LogError("PauseMenuHandler: StageSelectManager not found in the scene.");
            }
        }
    }

    /// <summary>
    /// Active le menu de pause, désactive l'interface de jeu et fige le temps.
    /// </summary>
    public void OnPauseButtonPressed()
    {
        if (pausePanel == null) return;
        if (menuPanel == null) return;
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Met le jeu en pause
        
        // S'assure que le panneau des paramètres est fermé
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        switchCameraPanel.SetActive(false);
        NextRoomButton.SetActive(false);
    }

    /// <summary>
    /// Désactive les menus de pause et relance le temps de jeu pour retourner en partie.
    /// </summary>
    public void OnBackToGameButtonPressed()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Reprise du temps de jeu
        switchCameraPanel.SetActive(true);
        NextRoomButton.SetActive(true);
    }

    /// <summary>
    /// Bascule l'affichage vers le panneau des paramètres.
    /// </summary>
    public void OnSettingsButtonPressed()
    {
        if (settingsPanel != null)
        {
            // Ferme d'abord le panneau de pause pour un rendu plus propre
            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            } 
            
            // Ouvre le panneau des paramètres
            settingsPanel.SetActive(true);
        }
    }
    
    /// <summary>
    /// Ferme le panneau des paramètres et revient au menu de pause principal.
    /// </summary>
    public void OnCloseSettingsButtonPressed()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    /// <summary>
    /// Quitte la pièce actuelle et redirige vers la scène de sélection d'étage correspondante.
    /// Utilise une stratégie basée sur les données de la tour et de l'étage.
    /// </summary>
    public void OnChangeRoomButtonPressed()
    {
        // --- VÉRIFICATION CRITIQUE ---
        if (string.IsNullOrEmpty(GameData.SelectedTower) || GameData.SelectedFloor < 0)
        {
            Debug.LogError("Contexte de navigation manquant. Redirection vers le Menu Principal.");
            OnMainMenuButtonPressed(); 
            return; 
        }

        // Relance le temps avant de quitter la scène
        Time.timeScale = 1f; 

        // Utilise la Factory pour déterminer la scène de sélection d'étage correcte
        var strategy = FloorSceneStrategyFactory.GetStrategy(GameData.SelectedTower);
        string stageSelectSceneName = strategy?.GetFloorScene(GameData.SelectedFloor);

        if (!string.IsNullOrEmpty(stageSelectSceneName))
        {
            NavigationManager.Pop(); 
            SceneManager.LoadScene(stageSelectSceneName);
        }
        else
        {
            Debug.LogError($"Scène de sélection introuvable pour la Tour:'{GameData.SelectedTower}' et l'Étage:'{GameData.SelectedFloor}'.");
        }
    }

    /// <summary>
    /// Relance le temps de jeu, vide l'historique de navigation et charge la scène du menu principal.
    /// </summary>
    public void OnMainMenuButtonPressed()
    {
        // Relance le temps avant de quitter
        Time.timeScale = 1f; 

        // Vide la pile d'historique de navigation
        NavigationManager.Clear();
        
        // Charge la scène du menu principal
        SceneManager.LoadScene("MainMenuScene");
    }
}