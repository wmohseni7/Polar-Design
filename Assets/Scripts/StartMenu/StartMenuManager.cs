using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Point d'entrée principal du jeu.
/// Gère les interactions du menu d'accueil, l'initialisation de la navigation
/// et la fermeture de l'application.
/// </summary>
public class StartMenuManager : MonoBehaviour
{
    [Header("Boutons de l'Interface")]
    public Button newButton;
    public Button continueButton;
    public Button quitButton;

    /// <summary>
    /// Initialise les écouteurs d'événements (Listeners) pour chaque bouton.
    /// </summary>
    void Start()
    {
        newButton.onClick.AddListener(StartNewDesign);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    /// <summary>
    /// Lance une nouvelle session de jeu.
    /// Réinitialise l'historique de navigation pour partir sur une base propre.
    /// </summary>
    public void StartNewDesign()
    {
        Debug.Log("Lancement d'un nouveau design...");
        
        // Nettoyage de l'historique pour éviter les retours en arrière vers le menu vide
        NavigationManager.Clear();
        
        // Préparation du fil d'Ariane pour la scène suivante
        NavigationManager.Push("TowerSelectScene");
        
        SceneManager.LoadScene("TowerSelectScene");
    }

    /// <summary>
    /// Prévu pour charger une sauvegarde existante.
    /// </summary>
    public void ContinueGame()
    {
        Debug.Log("Continuer : Système de sauvegarde à implémenter.");
    }

    /// <summary>
    /// Quitte l'application proprement.
    /// Gère la fermeture différemment si le jeu est lancé dans l'Éditeur Unity ou en build.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Fermeture du jeu.");

        #if UNITY_EDITOR
        // Arrête le mode Lecture dans l'éditeur
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Ferme l'application compilée
        Application.Quit();
        #endif
    }
}