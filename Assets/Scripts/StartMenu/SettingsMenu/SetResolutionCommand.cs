using UnityEngine;

namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Commande concrète permettant de modifier la résolution de l'écran.
    /// Encapsule les données de largeur, hauteur et l'état du mode plein écran.
    /// </summary>
    public class SetResolutionCommand : ISettingsCommand
    {
        private Resolution resolution;
        private bool isFullScreen;

        /// <summary>
        /// Initialise une nouvelle commande de résolution.
        /// </summary>
        /// <param name="resolution">Structure contenant la largeur, la hauteur et le taux de rafraîchissement.</param>
        /// <param name="isFullScreen">État souhaité du mode plein écran.</param>
        public SetResolutionCommand(Resolution resolution, bool isFullScreen)
        {
            this.resolution = resolution;
            this.isFullScreen = isFullScreen;
        }

        /// <summary>
        /// Applique les paramètres de résolution au moteur Unity.
        /// </summary>
        public void Execute()
        {
            // Application de la résolution via l'API Screen de Unity
            Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
            
            Debug.Log($"[Settings] Résolution définie sur : {resolution.width}x{resolution.height}, Plein écran : {isFullScreen}");
        }
    }
}