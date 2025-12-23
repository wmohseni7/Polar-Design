using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la lecture des effets sonores lors de l'interaction avec les boutons de l'interface.
/// </summary>
public class ButtonSoundPlayer : MonoBehaviour
{
    /// <summary> Référence au composant AudioSource qui diffusera le son. </summary>
    public AudioSource audioSource;
    
    /// <summary> Clip audio à jouer lors du clic (ex: un son bref de bouton). </summary>
    public AudioClip clickSound;

    /// <summary>
    /// Déclenche la lecture du son de clic une seule fois. 
    /// Cette méthode est conçue pour être appelée par l'événement "OnClick" d'un bouton UI.
    /// </summary>
    public void playClickSound()
    {
        // PlayOneShot permet de superposer les sons sans couper la lecture précédente
        audioSource.PlayOneShot(clickSound);
    }
}