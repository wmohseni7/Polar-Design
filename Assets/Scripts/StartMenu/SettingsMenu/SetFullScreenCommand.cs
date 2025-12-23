using UnityEngine;

namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Commande concrète permettant de modifier l'état du mode plein écran du jeu.
    /// Implémente l'interface ISettingsCommand pour être utilisée dans le menu des paramètres.
    /// </summary>
    public class SetFullscreenCommand : ISettingsCommand
    {
        /// <summary> État cible du plein écran (vrai pour activer, faux pour désactiver). </summary>
        private bool isFullScreen;

        /// <summary>
        /// Initialise une nouvelle instance de la commande avec l'état souhaité.
        /// </summary>
        /// <param name="isFullScreen">Définit si le jeu doit passer en plein écran.</param>
        public SetFullscreenCommand(bool isFullScreen)
        {
            this.isFullScreen = isFullScreen;
        }

        /// <summary>
        /// Exécute l'action système pour modifier l'affichage de l'écran.
        /// </summary>
        public void Execute()
        {
            // Modification de la propriété système de Unity pour l'affichage
            Screen.fullScreen = isFullScreen;
            
            Debug.Log($"[Settings] Plein écran défini sur : {isFullScreen}");
        }
    }
}