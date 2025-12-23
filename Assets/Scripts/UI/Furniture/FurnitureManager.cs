using System.Collections.Generic;
using UnityEngine;
using Observer;

/// <summary>
/// Gère un emplacement de meuble spécifique (Spawn Point).
/// Responsable du remplacement visuel des objets et du calcul des points de satisfaction associés.
/// Implémente FurnitureObserver pour réagir aux sélections de l'UI.
/// </summary>
public class FurnitureManager : MonoBehaviour, FurnitureObserver
{
    public Transform spawnPoint;         // Point de spawn
    private GameObject currentFurniture; // Meuble actif dans la scène

    // Variable privée pour retenir les points de ce meuble
    private RoomManager roomManager;

    // On distingue le score validé (déjà donné) du score en attente (prévisualisation)
    private ScoreMetrics confirmedScore = new ScoreMetrics(0, 0, 0);
    private ScoreMetrics pendingScore = new ScoreMetrics(0, 0, 0);

    private bool isLocked = false; // Est-ce que le meuble est validé ?

    private bool isModified = false;

    /// <summary>
    /// Initialise le point de spawn et cherche le RoomManager parent.
    /// Détecte également si un meuble est déjà présent par défaut.
    /// </summary>
    void Start()
    {
        // Si aucun spawn point -> utilise l'objet où est placé le script
        if (spawnPoint == null)
            spawnPoint = transform;

        // On cherche le RoomManager dans les parents
        roomManager = GetComponentInParent<RoomManager>();

        if (roomManager == null) 
            Debug.LogError("FurnitureManager : Pas de RoomManager trouvé dans les parents !");
        DetectExistingFurniture();
    }
    /// <summary>
    /// Active l'écoute des événements du menu de meubles pour cet emplacement précis.
    /// Appelé généralement lors du clic sur le point de spawn.
    /// </summary>
    public void SelectFurniture()
    {
        // On ne peut sélectionner que si ce n'est pas verrouillé
        if (!isLocked)
        {
            ConfirmButton.CurrentSelectedManager = this;
            EventManager.AddObserver(this);
        }
    }

    /// <summary>
    /// Détecte un meuble déjà présent dans la scène.
    /// </summary>
    private void DetectExistingFurniture()
    {
        // 1. Si un enfant existe → utilisé comme meuble
        if (spawnPoint.childCount > 0)
        {
            currentFurniture = spawnPoint.GetChild(0).gameObject;
            SetupOutline(currentFurniture, null);
        }
    }

    /// <summary>
    /// Appelé lorsque l’utilisateur choisit un nouveau prefab depuis le menu.
    /// Appelé via le Pattern Observer lorsqu'un nouveau meuble est choisi dans l'UI.
    /// </summary>
    public void OnFurnitureChanged(GameObject newPrefab)
    {
        if (newPrefab == null || isLocked) return;

        // On récupère le sélecteur et le collider de l'objet actuel
        FurnitureSelector oldSelector = null;
        BoxCollider oldCollider = null;
        Outline oldOutline = null;

        if (currentFurniture != null)
        {
            oldSelector = currentFurniture.GetComponent<FurnitureSelector>();
            oldCollider = currentFurniture.GetComponent<BoxCollider>();
            oldOutline = currentFurniture.GetComponent<Outline>();

            Destroy(currentFurniture);
            currentFurniture = null;
        }

        // Instancie le nouveau
        currentFurniture = Instantiate(newPrefab);

        // Définit son parent sur spawnPoint, en conservant la position/rotation/échelle locale par défaut
        // (c est ce que le paramètre 'worldPositionStays' à 'false' fait)
        currentFurniture.transform.SetParent(spawnPoint, false);
        
        // Réinitialisation des Transformations Locales
        
        // La position locale doit être (0, 0, 0) par rapport au SpawnPoint
        // currentFurniture.transform.localPosition = Vector3.zero;
        
        // La rotation locale doit être (0, 0, 0) par rapport au SpawnPoint
        // currentFurniture.transform.localRotation = Quaternion.identity;

        // L'échelle locale est forcée à (1, 1, 1). 
        // L'échelle réelle sera (1, 1, 1) * (spawnPoint.localScale).
        // currentFurniture.transform.localScale = Vector3.one;

        // Copie des composants
        
        FurnitureSelector newSelector = null;

        // On remet le selector et collider pour pouvoir recliquer dessus plus tard
        if (oldSelector != null)
        {
            // 1. Ajouter le composant au nouveau meuble
            newSelector = currentFurniture.AddComponent<FurnitureSelector>();
            
            // 2. Copier les valeurs des prefabs disponibles
            newSelector.prefab1 = oldSelector.prefab1;
            newSelector.prefab2 = oldSelector.prefab2;
            newSelector.prefab3 = oldSelector.prefab3;
            newSelector.prefab4 = oldSelector.prefab4;

            // 3. Copie des Scores
            newSelector.score1 = oldSelector.score1;
            newSelector.score2 = oldSelector.score2;
            newSelector.score3 = oldSelector.score3;
            newSelector.score4 = oldSelector.score4;
        }
        
        // Ajout et copie du BoxCollider
        if (oldCollider != null)
        {
            // Ajout du BoxCollider
            BoxCollider newCollider = currentFurniture.AddComponent<BoxCollider>();

            // Copier les propriétés de l ancien BoxCollider
            newCollider.center = oldCollider.center;
            newCollider.size = oldCollider.size;
            newCollider.isTrigger = oldCollider.isTrigger;
        }

        //   Configuration de l'Outline et du Hover
        // On passe 'oldOutline' pour copier la couleur si elle existait déjà
        SetupOutline(currentFurniture, oldOutline);

        // On récupère le script SmartOutline qu'on vient d'ajouter via SetupOutline
        SmartOutline smartOutline = currentFurniture.GetComponent<SmartOutline>();
        
        if (smartOutline != null)
        {
            // Pour allumer les objets qui viennent d etre cree via le menu
            smartOutline.ForceSelect();
        }

        // Mise à jour du score
        if (newSelector != null)
        {
            pendingScore = newSelector.GetScoreForPrefab(newPrefab);
        }
    }

    // Cette fonction est appelée par le script ConfirmButton quand on clique sur "Confirmer"
    public void ValidateFurniture()
    {
        if (isLocked) return;

        if (roomManager != null)
        {
            // On ajoute le score en attente
            roomManager.AddPointsToRoom(pendingScore);
            
            // Ce score devient le score officiel
            confirmedScore = pendingScore;
        }

        // VERROUILLAGE DEFINITIF
        isLocked = true;
        
        // On arrête d'écouter les changements du menu
        EventManager.RemoveObserver(this);

        // On désactive l'interactivité pour que le joueur ne puisse plus cliquer dessus
        if (currentFurniture != null)
        {
            // On éteint l'outline
            Outline outL = currentFurniture.GetComponent<Outline>();
            if (outL != null) outL.enabled = false;

            // On retire le script de sélection (SmartOutline)
            SmartOutline smart = currentFurniture.GetComponent<SmartOutline>();
            if (smart != null) Destroy(smart);

            // On retire le collider
            Collider col = currentFurniture.GetComponent<Collider>();
            if (col != null) Destroy(col);
        }
    }

    // Cette fonction ajoute l'Outline et le script Hover proprement
    private void SetupOutline(GameObject targetObj, Outline sourceSettings)
    {
        // On ajoute le composant Outline
        Outline outline = targetObj.GetComponent<Outline>();
        if (outline == null)
            outline = targetObj.AddComponent<Outline>();

        // On configure les paramètres
        if (sourceSettings != null)
        {
            // On copie les réglages de l'ancien meuble
            outline.OutlineColor = sourceSettings.OutlineColor;
            outline.OutlineWidth = sourceSettings.OutlineWidth;
            outline.OutlineMode = sourceSettings.OutlineMode;
        }
        else
        {
            // Valeurs par défaut (si c'est le premier meuble)
            outline.OutlineColor = new Color(0.180f, 0.992f, 0.765f); 
            outline.OutlineWidth = 6f;
            outline.OutlineMode = Outline.Mode.OutlineAll;
        }

        // Important : On désactive l'outline par défaut (pour qu'il ne s'allume qu'au survol)
        outline.enabled = false;

        // On ajoute le script de détection de souris (HoverOutline)
        if (targetObj.GetComponent<SmartOutline>() == null)
        {
            targetObj.AddComponent<SmartOutline>();
        }
    }

    public void MarkAsModified()
    {
        // 1. On cherche le script FurnitureSelector qui est sur le même objet
        FurnitureSelector mySelector = GetComponent<FurnitureSelector>();

        // 2. Si on le trouve, on le met à jour LUI (car c'est lui que le compteur regarde)
        if (mySelector != null)
        {
            mySelector.MarkAsModified(); 
        }
        else
        {
            // Sécurité : Si pas de selector, on essaie quand même de mettre à jour le compteur
            if (ModificationCounterUI.Instance != null)
            {
                ModificationCounterUI.Instance.UpdateCount();
            }
        }
    }
}