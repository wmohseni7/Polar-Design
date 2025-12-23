using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestionnaire central de la sélection d'étapes et de la navigation entre les tours.
/// Coordonne le chargement des scènes, la gestion des saisons et les restrictions d'accès.
/// </summary>
public class StageSelectManager : MonoBehaviour
{
    /// <summary>
    /// Charge une pièce spécifique en vérifiant si elle est accessible.
    /// Utilise SceneLoaderFactory pour obtenir le nom de la scène de jeu.
    /// </summary>
    /// <param name="partname">Nom de la pièce à charger (ex: "Kitchen").</param>
    public void LoadStage(string partname)
    {
        // Construction de l'identifiant unique pour la vérification de progression
        string floorTag = (GameData.SelectedFloor == 0) ? "-GF-" : "-1F-";
        string uniqueID = GameData.SelectedTower + floorTag + partname;

        // 1. Vérifie si la pièce a déjà été terminée pour empêcher d'y retourner
        if (GameSessionManager.Instance != null && GameSessionManager.Instance.completedRooms.Contains(uniqueID))
        {
            Debug.LogWarning($"Accès refusé: La pièce {uniqueID} est déjà terminée!");
            return;
        }

        GameData.SelectedPart = partname;

        // Utilisation de la Factory pour récupérer la stratégie de chargement de la tour actuelle
        var strategy = SceneLoaderFactory.GetStrategy(GameData.SelectedTower);
        string sceneName = strategy?.GetSceneName(GameData.SelectedFloor, GameData.SelectedPart);

        if (!string.IsNullOrEmpty(sceneName))
        {
            NavigationManager.Push(sceneName);
            SceneManager.LoadScene(sceneName);
            // Chargement de l'UI en mode Additive pour qu'elle se superpose au jeu
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }
    }

    /// <summary> Initialise la session pour la 'Calm Tower' et charge la sélection d'étage. </summary>
    public void LoadCalmTower(string towerName)
    {
        GameData.SelectedTower = "CalmTower";
        NavigationManager.Push("CT-FloorSelectScene");
        SceneManager.LoadScene("CT-FloorSelectScene");
    }

    /// <summary> Initialise la session pour la 'Noisy Tower' et charge la sélection d'étage. </summary>
    public void LoadNoisyTower(string towerName)
    {
        GameData.SelectedTower = "NoisyTower";
        NavigationManager.Push("NT-FloorSelectScene");
        SceneManager.LoadScene("NT-FloorSelectScene");
    }
    
    /// <summary>
    /// Sélectionne l'étage, définit la saison associée et lance la vidéo d'introduction.
    /// </summary>
    /// <param name="floorNumber">Index de l'étage (0 ou 1).</param>
    public void SelectFloor(int floorNumber)
    {
        GameData.SelectedFloor = floorNumber;

        var strategy = FloorSceneStrategyFactory.GetStrategy(GameData.SelectedTower);
        string sceneName = strategy?.GetFloorScene(GameData.SelectedFloor);

        if (!string.IsNullOrEmpty(sceneName))
        {
            // Transmission des données au lecteur d'intro
            LecteurIntro.nomSceneSuivante = sceneName;
            
            // Logique de saison : Hiver pour le RDC, Été pour le 1er étage
            LecteurIntro.saisonChoisie = (floorNumber == 0) ? "Hiver" : "Ete";

            NavigationManager.Push(sceneName);
            SceneManager.LoadScene("IntroVideoScene");
        }
    }

    /// <summary>
    /// Gère le retour en arrière intelligent.
    /// Décharge l'UI si nécessaire et remonte la pile de navigation.
    /// </summary>
    public void GoBack()
    {
        // Nettoyage de l'interface superposée
        if (SceneManager.GetSceneByName("UIScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("UIScene");
        }

        if (NavigationManager.HasPrevious())
        {
            NavigationManager.Pop(); // Retire la scène actuelle
            string previousScene = NavigationManager.Peek(); // Récupère la précédente
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}