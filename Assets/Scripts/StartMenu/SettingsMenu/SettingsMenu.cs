using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Contrôleur de l'interface utilisateur pour le menu des paramètres.
    /// Agit comme l'émetteur (Invoker) dans le patron de conception 'Command'.
    /// </summary>
    public class SettingsMenu : MonoBehaviour
    {
        [Header("Références Audio & UI")]
        public AudioMixer audioMixer;
        public TMP_Dropdown resolutionDropdown;

        /// <summary> Liste des résolutions supportées par l'écran actuel. </summary>
        Resolution[] resolutions;

        /// <summary>
        /// Initialise le menu en détectant les résolutions disponibles 
        /// et en configurant le menu déroulant (Dropdown).
        /// </summary>
        void Start()
        {
            // Récupère toutes les résolutions possibles du moniteur
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                // Formate l'affichage (ex: "1920 x 1080")
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                // Détecte la résolution actuelle pour l'afficher par défaut
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            // Remplit le composant UI avec les options générées
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        #region Actions de l'Interface (Commandes)

        /// <summary> Crée et exécute la commande de changement de volume. </summary>
        public void SetVolume(float volume)
        {
            var command = new SetVolumeCommand(audioMixer, volume);
            command.Execute();
        }

        /// <summary> Crée et exécute la commande de changement de qualité graphique. </summary>
        public void SetQuality(int qualityIndex)
        {
            var command = new SetQualityCommand(qualityIndex);
            command.Execute();
        }

        /// <summary> Crée et exécute la commande de bascule plein écran. </summary>
        public void SetFullscreen(bool isFullScreen)
        {
            var command = new SetFullscreenCommand(isFullScreen);
            command.Execute();
        }

        /// <summary> Crée et exécute la commande de changement de résolution. </summary>
        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            var command = new SetResolutionCommand(resolution, Screen.fullScreen);
            command.Execute();
        }

        #endregion
    }
}