using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Gère les réglages du volume sonore en faisant le lien entre un Slider UI et un AudioMixer.
/// Permet de contrôler le volume global (Master) du jeu.
/// </summary>
public class VolumeSettings : MonoBehaviour
{   
    [Header("Configuration Audio")]
    /// <summary> Référence au Master Mixer d'Unity. </summary>
    public AudioMixer masterMixer;
    
    /// <summary> Référence au composant Slider de l'interface utilisateur. </summary>
    public Slider volumeSlider;
    
    /// <summary> 
    /// Nom exact du paramètre exposé dans l'AudioMixer (ex: "Master_Volume").
    /// Ce nom est sensible à la casse.
    /// </summary>
    [Tooltip("Le nom du paramètre doit correspondre à celui exposé dans l'Audio Mixer.")]
    public string exposedParameterName = "Master_Volume";

    /// <summary>
    /// Initialise le slider avec la valeur actuelle du Mixer au démarrage.
    /// Configure également l'écouteur d'événement pour le changement de valeur.
    /// </summary>
    void Start()
    {
        // 1. Récupère le volume actuel depuis le Mixer (en dB)
        float currentVolume;
        if (masterMixer.GetFloat(exposedParameterName, out currentVolume))
        {
            // 2. Applique cette valeur au slider pour que sa position soit synchronisée
            volumeSlider.value = currentVolume;
        }

        // 3. Ajoute un écouteur : appelle SetVolume chaque fois que l'utilisateur déplace le slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    /// <summary>
    /// Applique la valeur du slider au paramètre de l'AudioMixer.
    /// </summary>
    /// <param name="sliderValue">Valeur flottante provenant du slider UI.</param>
    public void SetVolume(float sliderValue)
    {
        // Applique le volume au paramètre exposé de l'Audio Mixer
        // Note : Si vous utilisez une échelle logarithmique, une conversion vers les dB serait nécessaire ici.
        masterMixer.SetFloat(exposedParameterName, sliderValue);
    }
}