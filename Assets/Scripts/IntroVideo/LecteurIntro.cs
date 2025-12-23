using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la lecture des vidéos d'introduction selon la saison choisie et assure la transition vers la scène suivante.
/// </summary>
public class LecteurIntro : MonoBehaviour
{
    /// <summary> Nom de la saison sélectionnée pour déterminer le clip vidéo à jouer (Hiver/Été). </summary>
    public static string saisonChoisie = "Hiver"; 
    /// <summary> Nom de la scène vers laquelle basculer une fois la vidéo terminée. </summary>
    public static string nomSceneSuivante = ""; 

    [Header("Configuration")]
    /// <summary> Référence au composant VideoPlayer d'Unity. </summary>
    public VideoPlayer videoPlayer;
    /// <summary> Clip vidéo correspondant à l'ambiance hivernale. </summary>
    public VideoClip clipHiver;
    /// <summary> Clip vidéo correspondant à l'ambiance estivale. </summary>
    public VideoClip clipEte;

    /// <summary>
    /// Initialise la scène, coupe les musiques d'ambiance globales et lance la lecture de la vidéo appropriée.
    /// </summary>
    void Start()
    {
        // --- 1. SILENCE TOTAL ---
        // On cherche TOUTES les sources audio actives dans le jeu pour éviter les superpositions.
        AudioSource[] toutesLesMusiques = FindObjectsOfType<AudioSource>();
        
        foreach (AudioSource musique in toutesLesMusiques)
        {
            // On coupe toutes les sources sauf celle attachée à cet objet (le son de la vidéo).
            if (musique.gameObject != this.gameObject)
            {
                musique.Stop(); 
            }
        }

        // Sélection du clip selon la saison statique
        if (saisonChoisie == "Hiver")
        {
            videoPlayer.clip = clipHiver;
        }
        else
        {
            videoPlayer.clip = clipEte;
        }

        videoPlayer.Play();
        
        // Abonnement à l'événement de fin de vidéo
        videoPlayer.loopPointReached += FinVideo;
    }

    /// <summary>
    /// Appelé automatiquement à la fin du clip vidéo ou manuellement pour charger la scène suivante.
    /// </summary>
    /// <param name="vp">Référence au VideoPlayer concerné.</param>
    void FinVideo(VideoPlayer vp)
    {
        if (!string.IsNullOrEmpty(nomSceneSuivante))
        {
            SceneManager.LoadScene(nomSceneSuivante);
        }
        else
        {
            Debug.LogError("Oups ! Pas de scène suivante définie.");
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    /// <summary>
    /// Permet à l'utilisateur de passer l'introduction (Skip) via un bouton UI par exemple.
    /// </summary>
    public void PasserIntro()
    {
        // On simule la fin de la vidéo
        FinVideo(videoPlayer);
    }
}