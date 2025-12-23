using UnityEngine;
using UnityEngine.Audio;

namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Commande concrète permettant de modifier le volume d'un AudioMixer.
    /// Encapsule la référence au mixer et la valeur cible pour isoler la logique du son.
    /// </summary>
    public class SetVolumeCommand : ISettingsCommand
    {
        private AudioMixer mixer;
        private float volume;

        /// <summary>
        /// Initialise une nouvelle commande de réglage de volume.
        /// </summary>
        /// <param name="mixer">L'AudioMixer cible (ex: MasterMixer).</param>
        /// <param name="volume">La valeur du volume à appliquer (généralement en décibels).</param>
        public SetVolumeCommand(AudioMixer mixer, float volume)
        {
            this.mixer = mixer;
            this.volume = volume;
        }

        /// <summary>
        /// Applique la modification du paramètre exposé dans l'AudioMixer.
        /// </summary>
        public void Execute()
        {
            // Modifie le paramètre "volume" exposé dans l'AudioMixer Unity
            mixer.SetFloat("volume", volume);
            
            Debug.Log($"[Settings] Volume défini sur : {volume}");
        }
    }
}