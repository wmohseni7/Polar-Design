using UnityEngine;

namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Commande concrète permettant de modifier le niveau de qualité graphique du jeu.
    /// Utilise les niveaux prédéfinis dans les 'Project Settings > Quality' d'Unity.
    /// </summary>
    public class SetQualityCommand : ISettingsCommand
    {
        /// <summary> Index correspondant au niveau de qualité (ex: 0 = Low, 1 = Medium, etc.). </summary>
        private int qualityIndex;

        /// <summary>
        /// Initialise une nouvelle instance de la commande avec l'index de qualité souhaité.
        /// </summary>
        /// <param name="qualityIndex">L'index du niveau de qualité à appliquer.</param>
        public SetQualityCommand(int qualityIndex)
        {
            this.qualityIndex = qualityIndex;
        }

        /// <summary>
        /// Exécute le changement de qualité graphique via l'API QualitySettings d'Unity.
        /// </summary>
        public void Execute()
        {
            // Applique le niveau de qualité global
            QualitySettings.SetQualityLevel(qualityIndex);
            
            Debug.Log($"[Settings] Niveau de qualité défini sur : {qualityIndex}");
        }
    }
}