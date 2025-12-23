using UnityEngine;
using Observer;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour, ColorObserver
{
    [Header("Configuration")]
    public CurrentRoomRef currentRoomRef;
    private Renderer[] wallRenderers;
    private Renderer floorRenderer;
    private RoomManager roomManager;
   
    // SCORE : Celui qui est validé et enregistré
    private ScoreMetrics currentConfirmedScore;
    
    // SCORE : Celui qu'on est en train de tester (Preview)
    private ScoreMetrics pendingScore;

    // SAUVEGARDE : Pour pouvoir annuler
    private Color originalWallColor;
    private Color originalFloorColor;
    private bool isPreviewing = false; // Est-ce qu'on a commencé à modifier ?

    [Header("Paramètres de Départ")]
    public ContextualScoreProfile initialScoreProfile;

    void Start()
    {
        roomManager = GetComponentInParent<RoomManager>();
        
        // --- Récupération des Renderers (Code inchangé) ---
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>();
        List<Renderer> wallsFound = new List<Renderer>();
        foreach (Renderer r in allRenderers)
        {
            if (r.gameObject.CompareTag("Wall"))
            {
                r.material = Instantiate(r.sharedMaterial);
                wallsFound.Add(r);
            }
            else if (r.gameObject.CompareTag("Floor"))
            {
                r.material = Instantiate(r.sharedMaterial);
                floorRenderer = r;
            }
        }
        wallRenderers = wallsFound.ToArray();
        
        // Initialisation des scores
        currentConfirmedScore = new ScoreMetrics(0,0,0);
        
        if (initialScoreProfile != null && roomManager != null)
        {
            ScoreMetrics startScore = initialScoreProfile.GetScore(roomManager.roomType);
            roomManager.AddPointsToRoom(startScore);
            currentConfirmedScore = startScore;
        }

        // On sauvegarde l'état initial des couleurs
        SaveOriginalColors();
    }

    void SaveOriginalColors()
    {
        if (wallRenderers.Length > 0 && wallRenderers[0] != null)
            originalWallColor = wallRenderers[0].material.color;
            
        if (floorRenderer != null)
            originalFloorColor = floorRenderer.material.color;
    }

    // ... OnEnable / OnDisable inchangés ...
    void OnEnable() { EventManager.AddObserver(this); }
    void OnDisable() { EventManager.RemoveObserver(this); }


    /// <summary>
    /// MODE PREVIEW : Change le visuel mais PAS le score réel du RoomManager.
    /// </summary>
    public void OnPaletteChanged(Color wallColor, Color floorColor, ScoreMetrics newScore)
    {
        if (currentRoomRef.activeRoom != roomManager) return;

        // Si c'est la première fois qu'on touche à la palette depuis l'ouverture du menu,
        // on sauvegarde l'état actuel comme "Original" pour pouvoir annuler.
        if (!isPreviewing)
        {
            SaveOriginalColors();
            isPreviewing = true;
        }

        // 1. On stocke le futur score, mais ON NE L'ENVOIE PAS ENCORE
        pendingScore = newScore;

        // 2. Mise à jour Visuelle (Preview)
        ApplyColorVisuals(wallColor, floorColor);
    }

    /// <summary>
    /// Appelé quand le joueur clique sur CONFIRMER.
    /// Valide le pendingScore et l'envoie au RoomManager.
    /// </summary>
    public void ConfirmChanges()
    {
        if (!isPreviewing || roomManager == null) return;

        // 1. On retire l'ANCIEN score validé
        ScoreMetrics pointsToRemove = new ScoreMetrics(
            -currentConfirmedScore.design,
            -currentConfirmedScore.usure,
            -currentConfirmedScore.gout
        );
        roomManager.AddPointsToRoom(pointsToRemove);

        // 2. On ajoute le NOUVEAU score (celui qui était en attente)
        roomManager.AddPointsToRoom(pendingScore);

        // 3. Le score en attente devient le score officiel
        currentConfirmedScore = pendingScore;

        // On n'est plus en preview, c'est validé
        isPreviewing = false; 
        Debug.Log("Couleurs confirmées !");
    }

    /// <summary>
    /// Appelé quand le joueur ferme sans confirmer.
    /// Remet les anciennes couleurs.
    /// </summary>
    public void CancelChanges()
    {
        if (!isPreviewing) return;

        // On remet les visuels d'avant
        ApplyColorVisuals(originalWallColor, originalFloorColor);

        // On oublie le score en attente
        isPreviewing = false;
        Debug.Log("Changement de couleur annulé.");
    }

    // Petite fonction utilitaire pour éviter de dupliquer le code visuel
    private void ApplyColorVisuals(Color wCol, Color fCol)
    {
        if (wallRenderers != null)
        {
            foreach (Renderer r in wallRenderers)
            {
                if (r != null)
                {
                    r.material.mainTexture = null;
                    r.material.color = wCol;
                    if (r.material.HasProperty("_BaseColor")) r.material.SetColor("_BaseColor", wCol);
                }
            }
        }
        if (floorRenderer != null) floorRenderer.material.color = fCol;
    }
}