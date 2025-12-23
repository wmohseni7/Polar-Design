using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

/// <summary>
/// Gère le basculement entre plusieurs caméras au sein d'une scène.
/// Inclut le tri automatique des caméras, la gestion de l'affichage UI et 
/// la synchronisation de l'AudioListener pour éviter les avertissements de la console.
/// </summary>
public class CameraSwitcher : MonoBehaviour
{
    // Liste des caméras disponibles dans la scène actuelle
    private List<Camera> activeSceneCameras = new List<Camera>(); 
    private int currentCameraIndex = 0;

    [Header("Références UI")]
    /// <summary> Élément texte affichant le nom de la caméra active. </summary>
    public TMP_Text cameraNameDisplay;

    void Awake()
    {
        // S'abonner à l'événement de chargement de scène pour actualiser la liste des caméras
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Se désabonner pour éviter les fuites de mémoire
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindCamerasInScene();
    }

    void Start()
    {
        if (activeSceneCameras.Count == 0)
        {
            FindCamerasInScene();
        }
    }

    /// <summary>
    /// Identifie toutes les caméras de la scène, les trie et exclut les caméras d'interface.
    /// </summary>
    private void FindCamerasInScene()
    {
        activeSceneCameras.Clear();
        currentCameraIndex = 0;

        // Récupération de toutes les caméras présentes
        Camera[] allCameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);

        // Filtrage par LINQ : exclut la caméra UI et trie par nom (alphabétique)
        activeSceneCameras = allCameras
            .Where(c => c.gameObject.tag != "UICamera")
            .OrderBy(c => c.name) 
            .ToList();

        Debug.Log($"CameraSwitcher : {activeSceneCameras.Count} caméras trouvées.");

        InitializeCameras();
    }
    
    /// <summary>
    /// Active la première caméra et désactive toutes les autres ainsi que leurs AudioListeners.
    /// </summary>
    private void InitializeCameras()
    {
        if (activeSceneCameras.Count > 0)
        {
            for (int i = 0; i < activeSceneCameras.Count; i++)
            {
                bool isActive = (i == 0);
                ToggleCamera(i, isActive);
            }

            UpdateCameraNameDisplay();
        }
    }

    /// <summary>
    /// Met à jour le texte à l'écran avec le nom de la caméra sélectionnée.
    /// </summary>
    private void UpdateCameraNameDisplay()
    {
        if (cameraNameDisplay != null && activeSceneCameras.Count > 0)
        {
            cameraNameDisplay.text = activeSceneCameras[currentCameraIndex].name;
        }
    }

    /// <summary> Passe à la caméra suivante dans la liste (bouclage automatique). </summary>
    public void SwitchNextCamera()
    {
        if (activeSceneCameras.Count <= 1) return;

        ToggleCamera(currentCameraIndex, false);
        currentCameraIndex = (currentCameraIndex + 1) % activeSceneCameras.Count; 
        ToggleCamera(currentCameraIndex, true);
    
        UpdateCameraNameDisplay();
    }

    /// <summary> Passe à la caméra précédente dans la liste. </summary>
    public void SwitchPreviousCamera()
    {
        if (activeSceneCameras.Count <= 1) return;

        ToggleCamera(currentCameraIndex, false);
        currentCameraIndex = (currentCameraIndex + activeSceneCameras.Count - 1) % activeSceneCameras.Count; 
        ToggleCamera(currentCameraIndex, true);
    
        UpdateCameraNameDisplay(); 
    }

    /// <summary>
    /// Active ou désactive proprement une caméra et son AudioListener associé.
    /// </summary>
    private void ToggleCamera(int index, bool state)
    {
        activeSceneCameras[index].gameObject.SetActive(state);
        
        var listener = activeSceneCameras[index].GetComponent<AudioListener>();
        if (listener != null) listener.enabled = state;
    }
}