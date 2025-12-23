using UnityEngine;

/// <summary>
/// Gère la logique globale d'une pièce et communique avec l'UI.
/// </summary>
public class RoomManager : MonoBehaviour
{
    private UIRoomNavigationManager uiManager;

    [Header("Configuration")]
    public string roomID;
    public RoomType roomType;

    [Header("Wiring (Données)")]
    public CurrentRoomRef currentRoomRef; 
    public Character activeCharacter;
    public FloorData floorData;

    [Header("Modules")]
    public ColorManager colorManager;
    public LightManager lightManager;

    private ScoreMetrics currentRoomScore = new ScoreMetrics(0,0,0);

    private void OnEnable() 
    {
        // On essaie de trouver l'UI au démarrage (peut échouer si l'UI charge après)
        uiManager = FindObjectOfType<UIRoomNavigationManager>(true);

        if (currentRoomRef != null)
            currentRoomRef.SetActiveRoom(this);
        
        currentRoomScore = new ScoreMetrics(0,0,0);
        UpdateRoomUI();
    }

    private void OnDisable()
    {
        if (currentRoomRef != null && currentRoomRef.activeRoom == this)
            currentRoomRef.activeRoom = null;
    }

    public void AddPointsToRoom(ScoreMetrics pointsToAdd)
    {
        if (floorData == null) return;
        
        currentRoomScore += pointsToAdd;
        floorData.UpdateRoomScore(roomID, currentRoomScore);

        if (activeCharacter != null)
            activeCharacter.ApplyChange(pointsToAdd.design, pointsToAdd.usure, pointsToAdd.gout);

        UpdateRoomUI();
    }

    // --- C'EST ICI QUE LA CORRECTION EST IMPORTANTE ---
    private void UpdateRoomUI()
    {
        // 1. Si on a perdu l'UI ou qu'on ne l'a pas trouvée au démarrage, on re-cherche maintenant
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIRoomNavigationManager>(true);
        }

        // 2. Si on l'a trouvée, on envoie le score
        if (uiManager != null)
        {
            float totalScore = currentRoomScore.design + currentRoomScore.usure + currentRoomScore.gout;
            uiManager.UpdateRoomSatisfaction(totalScore);
        }
        // Pas de message d'erreur rouge ici : si l'UI n'est pas là (changement de scène), on ignore juste.
    }
    // --------------------------------------------------
}