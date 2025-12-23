using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Gère le comportement du bouton "Retour" universel dans l'interface.
/// Utilise le NavigationManager pour retourner à la scène précédente ou au menu principal.
/// </summary>
public class BackButtonHandler : MonoBehaviour
{
    /// <summary> Référence au composant Bouton de l'interface utilisateur. </summary>
    public Button backButton;

    /// <summary>
    /// Initialise le bouton en ajoutant un écouteur d'événement au démarrage.
    /// </summary>
    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoBack);
        }
    }

    /// <summary>
    /// Logique de retour : consulte l'historique du NavigationManager.
    /// Si une scène précédente existe, elle est chargée, sinon le joueur est renvoyé au menu principal.
    /// </summary>
    public void GoBack()
    {
        // Vérifie s'il y a un historique de navigation disponible
        if (NavigationManager.HasPrevious())
        {
            // Retire la scène actuelle de la pile
            NavigationManager.Pop(); 
            
            // Récupère le nom de la scène précédente sans la supprimer
            string previousScene = NavigationManager.Peek(); 
            
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            // Sécurité : si la pile est vide, retour forcé au menu principal
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}