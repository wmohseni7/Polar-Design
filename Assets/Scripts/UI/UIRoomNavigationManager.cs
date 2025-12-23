using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIRoomNavigationManager : MonoBehaviour
{
    // =========================================================
    // 1. PARTIE VISUELLE (ANCIEN UIManager)
    // =========================================================
    [Header("--- PANNEAUX (Drag & Drop ici) ---")]
    public GameObject mainMenuPanel;   // Le panneau principal (MenuPanel)
    public GameObject lightPanel;      // Le sous-panneau LightSubPanel
    public GameObject colorPanel;      // Le sous-panneau ColorSubPanel
    public GameObject furniturePanel;  // Le sous-panneau FurniturePanel (si tu en as un)
    
    [Header("--- POPUPS ---")]
    public GameObject confirmationPanel;   
    public GameObject finalizeFloorPanel; 

    // =========================================================
    // 2. PARTIE LOGIQUE & SCORE
    // =========================================================
    [Header("--- SCORES & BARRES ---")]
    public SatisfactionBar roomSatisfactionBar; // Ta nouvelle barre custom
    public Slider floorSlider;                  // Le slider d'étage standard

    // Constante
    private const int MAX_SCORE_PER_ROOM = 100; 
    private float _lastReceivedRoomScore = 0f;

    private void Start()
    {
        // A. Initialisation des Scores
        SetupFloorSliderMax();
        UpdateFloorBar();
        UpdateRoomSatisfaction(0);

        // B. Initialisation de l'affichage (On cache les sous-menus au début)
        OpenMainMenu();
    }

    // ---------------------------------------------------------
    // FONCTIONS D'OUVERTURE DE MENUS (Recopiées de UIManager)
    // ---------------------------------------------------------
    
    public void OpenMainMenu()
    {
        // Affiche le menu principal, cache les sous-menus
        if(mainMenuPanel) mainMenuPanel.SetActive(true);
        if(lightPanel) lightPanel.SetActive(false);
        if(colorPanel) colorPanel.SetActive(false);
        if(furniturePanel) furniturePanel.SetActive(false);
    }

    public void OpenLightPanel()
    {
        // Cache le menu principal, affiche la Lumière
        SwitchPanel(lightPanel);
    }

    public void OpenColorPanel()
    {
        // Cache le menu principal, affiche la Couleur
        SwitchPanel(colorPanel);
    }

    public void OpenFurniturePanel()
    {
        SwitchPanel(furniturePanel);
    }

    public void BackToMenu()
    {
        OpenMainMenu();
    }

    /// <summary>
    /// Fonction utilitaire pour n'afficher qu'un seul panneau à la fois
    /// </summary>
    private void SwitchPanel(GameObject targetPanel)
    {
        // On cache tout d'abord (sauf le MenuPanel si on veut qu'il reste en fond, 
        // mais selon ton ancien script, tu cachais tout).
        // Adapte ici selon si tu veux voir le MenuPanel en fond ou pas.
        
        // Comportement "Ancien Script" : On ferme tout et on ouvre le cible
        if(mainMenuPanel) mainMenuPanel.SetActive(true); // On garde souvent le main actif en fond ? 
        // Sinon : mainMenuPanel.SetActive(false); 

        if(lightPanel) lightPanel.SetActive(false);
        if(colorPanel) colorPanel.SetActive(false);
        if(furniturePanel) furniturePanel.SetActive(false);

        // On active le cible
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
        }
    }

    // ---------------------------------------------------------
    // FONCTIONS DE LOGIQUE (Le reste du code ne change pas)
    // ---------------------------------------------------------

    public void UpdateRoomSatisfaction(float currentRoomScore)
    {
        _lastReceivedRoomScore = currentRoomScore;
        if (roomSatisfactionBar != null) roomSatisfactionBar.UpdateVisuals(currentRoomScore);
        
        if (GameSessionManager.Instance != null)
        {
            string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
            string uniqueID = GameData.SelectedTower + floorTag + GameData.SelectedPart;
            GameSessionManager.Instance.SaveRoomSatisfaction(uniqueID, currentRoomScore);
        }
        UpdateFloorBar();
    }

    private void SetupFloorSliderMax()
    {
        if (floorSlider == null) return;
        var strategy = SceneLoaderFactory.GetStrategy(GameData.SelectedTower);
        if (strategy != null)
        {
            string[] roomsInFloor = strategy.GetAllPartsForFloor(GameData.SelectedFloor);
            int calculatedMax = roomsInFloor.Length * MAX_SCORE_PER_ROOM;
            floorSlider.minValue = 0;
            floorSlider.maxValue = calculatedMax;
        }
        else floorSlider.maxValue = 300; 
    }

    public void UpdateFloorBar()
    {
        if (floorSlider == null) return;
        if (GameSessionManager.Instance != null)
        {
            string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
            string currentFloorFilter = GameData.SelectedTower + floorTag;
            floorSlider.value = GameSessionManager.Instance.GetTotalFloorScore(currentFloorFilter);
        }
    }

    #region Gestion des Panneaux Confirmation
    public void OpenConfirmation() { if (confirmationPanel != null) confirmationPanel.SetActive(true); }
    public void CloseConfirmation() { if (confirmationPanel != null) confirmationPanel.SetActive(false); }
    #endregion

    public void ConfirmAndExit()
    {
        string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
        string uniqueID = GameData.SelectedTower + floorTag + GameData.SelectedPart;
        
        if (GameSessionManager.Instance != null)
        {
            GameSessionManager.Instance.SaveRoomSatisfaction(uniqueID, _lastReceivedRoomScore);
            GameSessionManager.Instance.MarkRoomAsCompleted(uniqueID);
        }

        string nextPart = GetNextAvailablePart();
        if (!string.IsNullOrEmpty(nextPart)) { LoadNextRoom(nextPart); confirmationPanel.SetActive(false); }
        else { confirmationPanel.SetActive(false); ShowFinalPanel(); }
    }

    private void ShowFinalPanel()
    {
        if (finalizeFloorPanel != null) finalizeFloorPanel.SetActive(true);
        else ReturnToFloorSelect();
    }
    
    // --- NAVIGATION (Inchangé) ---
    private string GetNextAvailablePart()
    {
        var strategy = SceneLoaderFactory.GetStrategy(GameData.SelectedTower);
        if (strategy == null) return null;
        string[] allParts = strategy.GetAllPartsForFloor(GameData.SelectedFloor);
        foreach (string part in allParts)
        {
            string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
            string uniqueID = GameData.SelectedTower + floorTag + part;
            if (!GameSessionManager.Instance.completedRooms.Contains(uniqueID)) return part;
        }
        return null; 
    }

    private void LoadNextRoom(string partName)
    {
        GameData.SelectedPart = partName;
        var strategy = SceneLoaderFactory.GetStrategy(GameData.SelectedTower);
        string sceneName = strategy?.GetSceneName(GameData.SelectedFloor, partName);
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (SceneManager.GetSceneByName("UIScene").isLoaded) SceneManager.UnloadSceneAsync("UIScene");
            NavigationManager.Push(sceneName);
            SceneManager.sceneLoaded += OnRoomLoaded; 
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnRoomLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnRoomLoaded;
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    public void ReturnToFloorSelect()
    {
        NavigationManager.Clear();
        string sceneToLoad = (GameData.SelectedTower == "CalmTower") ? "CT-FloorSelectScene" : "NT-FloorSelectScene";
        if (SceneManager.GetSceneByName("UIScene").isLoaded) SceneManager.UnloadSceneAsync("UIScene");
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReturnToGame() { if (finalizeFloorPanel != null) finalizeFloorPanel.SetActive(false); }

    public void GoToEndScreen()
    {
        string floorID = (GameData.SelectedTower == "CalmTower") ? ((GameData.SelectedFloor == 0) ? "Calm_GF" : "Calm_1F") : ((GameData.SelectedFloor == 0) ? "Noisy_GF" : "Noisy_1F");
        string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
        string currentFloorFilter = GameData.SelectedTower + floorTag;
        float totalScore = GameSessionManager.Instance.GetTotalFloorScore(currentFloorFilter);
        float maxScore = floorSlider.maxValue;
        float finalPercentage = (maxScore > 0) ? (totalScore / maxScore) * 100f : 0f;

        PlayerPrefs.SetString("EtageActuel", floorID);
        PlayerPrefs.SetFloat("ScoreFinalPercent", finalPercentage);
        PlayerPrefs.Save();

        NavigationManager.Clear();
        if (SceneManager.GetSceneByName("UIScene").isLoaded) SceneManager.UnloadSceneAsync("UIScene");
        SceneManager.LoadScene("EndScene"); 
    }
}