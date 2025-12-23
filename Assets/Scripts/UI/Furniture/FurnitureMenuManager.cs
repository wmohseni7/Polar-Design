using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère le menu de sélection des meubles.
/// Reçoit les données des meubles cliqués et configure dynamiquement les boutons de l'interface.
/// Implémente FurnitureSelectionObserver pour savoir quand un objet est sélectionné en scène.
/// </summary>
public class FurnitureMenuManager : MonoBehaviour, FurnitureSelectionObserver
{
    /// <summary> Singleton de scène pour un accès facile depuis les sélecteurs. </summary>
    public static FurnitureMenuManager Instance { get; private set; }

    [Header("Références UI")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    // Références temporaires aux prefabs liés aux boutons actuels
    private GameObject currentPrefab1;
    private GameObject currentPrefab2;
    private GameObject currentPrefab3;
    private GameObject currentPrefab4;

    private void Awake()
    {
        // Implémentation du Singleton de scène
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // S'inscrit auprès de l'EventManager pour être notifié des clics sur les meubles
        EventManager.AddSelectionObserver(this); 
    }

    /// <summary>
    /// Configure les boutons du menu avec les modèles 3D disponibles pour le meuble sélectionné.
    /// Utilise des listeners dynamiques pour lier chaque bouton au bon prefab.
    /// </summary>
    public void ShowFurnitureOptions(GameObject p1, GameObject p2, GameObject p3, GameObject p4)
    {
        currentPrefab1 = p1;
        currentPrefab2 = p2;
        currentPrefab3 = p3;
        currentPrefab4 = p4;

        // Nettoyage des anciens événements pour éviter les appels multiples
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        // Assignation des nouveaux comportements via des expressions lambda () => ...
        button1.onClick.AddListener(() => OnFurnitureButtonClicked(currentPrefab1));
        button2.onClick.AddListener(() => OnFurnitureButtonClicked(currentPrefab2));
        button3.onClick.AddListener(() => OnFurnitureButtonClicked(currentPrefab3));
        button4.onClick.AddListener(() => OnFurnitureButtonClicked(currentPrefab4));
    }

    /// <summary>
    /// Déclenche l'événement global de changement de meuble lorsqu'un bouton est cliqué.
    /// </summary>
    private void OnFurnitureButtonClicked(GameObject prefab)
    {
        EventManager.NotifyFurnitureChanged(prefab);
    }

    /// <summary>
    /// Implémentation de l'interface FurnitureSelectionObserver.
    /// Prépare le menu et les abonnements lorsqu'un meuble est cliqué dans le monde 3D.
    /// </summary>
    /// <param name="selector">Le composant FurnitureSelector de l'objet cliqué.</param>
    public void OnFurnitureSelected(FurnitureSelector selector)
    {
        // 1. Liaison : On récupère le FurnitureManager parent du meuble cliqué
        FurnitureManager newActiveManager = selector.GetComponentInParent<FurnitureManager>();
        if (newActiveManager != null)
        {
            // On abonne cet emplacement spécifique pour qu'il soit le seul à recevoir le changement
            EventManager.AddObserver(newActiveManager); 
        }

        // 2. Configuration : On prépare les 4 options visuelles sur les boutons
        ShowFurnitureOptions(selector.prefab1, selector.prefab2, selector.prefab3, selector.prefab4);
        
        // 3. Affichage : On demande au gestionnaire de panneau d'ouvrir l'UI
        if (FurniturePanelManager.Instance != null)
        {
            FurniturePanelManager.Instance.OpenPanel();
        }
    }
}