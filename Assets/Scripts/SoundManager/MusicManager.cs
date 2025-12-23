using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections; // Requis pour IEnumerator

/// <summary>
/// Gère la musique de fond du jeu de manière persistante (Singleton).
/// Alterne automatiquement entre la musique du menu et celle du jeu en fonction de la scène chargée.
/// </summary>
public class MusicManager : MonoBehaviour
{
    /// <summary> Instance unique du MusicManager accessible globalement. </summary>
    public static MusicManager Instance;

    /// <summary> Composant audio utilisé pour la lecture. </summary>
    private AudioSource audioSource;
    
    /// <summary> Liste des noms de scènes considérées comme des niveaux de jeu (pour jouer la musique 'Game'). </summary>
    [Tooltip("Ajoutez ici les noms exacts des scènes de jeu.")]
    public List<string> gameSceneNames = new List<string>(); 
    
    /// <summary> Clip audio pour les menus. </summary>
    public AudioClip menuMusic; 
    /// <summary> Clip audio pour les phases de gameplay. </summary>
    public AudioClip gameMusic; 

    /// <summary>
    /// Initialise le Singleton, configure l'AudioSource et s'abonne aux événements de chargement de scène.
    /// </summary>
    void Awake()
    {
        Debug.Log("MusicManager Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            audioSource = GetComponent<AudioSource>();
            
            if (audioSource == null)
            {
                Debug.LogError("MusicManager nécessite un composant AudioSource sur le même GameObject !");
                Destroy(gameObject);
                return;
            }
            
            audioSource.loop = true;

            // Abonnement à l'événement de chargement de scène
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Vérifie la scène active dès le lancement du gestionnaire.
    /// </summary>
    void Start()
    {
        Debug.Log("MusicManager Start — Scène Active: " + SceneManager.GetActiveScene().name);
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Se désabonne de l'événement de chargement de scène pour éviter les erreurs de référence.
    /// </summary>
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    /// <summary>
    /// Détecte le changement de scène et lance la transition musicale appropriée.
    /// </summary>
    /// <param name="scene">La scène qui vient d'être chargée.</param>
    /// <param name="mode">Le mode de chargement de la scène.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded déclenché pour: " + scene.name);

        // Arrête les transitions en cours pour éviter les conflits
        StopAllCoroutines();
        
        // Détermine quelle musique jouer selon la liste gameSceneNames
        if (gameSceneNames.Contains(scene.name)) 
        {
            StartCoroutine(SwitchMusic(gameMusic));
        }
        else 
        {
            StartCoroutine(SwitchMusic(menuMusic));
        }
    }

    /// <summary>
    /// Coroutine gérant la transition propre entre deux pistes audio.
    /// </summary>
    /// <param name="newTrack">Le nouveau clip audio à jouer.</param>
    /// <returns>Attend la fin de la frame pour garantir l'arrêt du moteur audio avant de relancer.</returns>
    public IEnumerator SwitchMusic(AudioClip newTrack)
    {
        // Si la piste est déjà en cours de lecture, on ne fait rien
        if (audioSource.isPlaying && audioSource.clip == newTrack)
        {
            yield break; 
        }
    
        // Sécurité : si aucune piste n'est fournie, on arrête tout
        if (newTrack == null)
        {
            audioSource.Stop();
            audioSource.clip = null;
            yield break;
        }
        
        // 1. ARRÊT DE L'ANCIENNE PISTE
        audioSource.Stop();
        audioSource.clip = null; 
        
        // 2. PAUSE TECHNIQUE : Attend la fin de la frame pour laisser Unity traiter l'arrêt
        yield return new WaitForEndOfFrame(); 

        // 3. LANCEMENT DE LA NOUVELLE PISTE
        audioSource.clip = newTrack;
        audioSource.Play();
    
        Debug.Log($"Nouvelle piste lancée : {audioSource.clip.name}");
    }
}