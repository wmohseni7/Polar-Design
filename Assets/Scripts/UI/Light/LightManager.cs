using UnityEngine;
using Observer;

public class LightManager : MonoBehaviour, LightObserver
{
    [Header("Configuration")]
    public CurrentRoomRef currentRoomRef;
    public ContextualScoreProfile colorProfile; 
    public LightIntensityProfile intensityProfile; 

    private Light[] targetLights;
    private RoomManager roomManager;
    
    // SCORES CONFIRMÉS (Sauvegardés)
    private ScoreMetrics confirmedColorScore;
    private ScoreMetrics confirmedIntensityScore;

    // SCORES EN ATTENTE (Preview)
    private ScoreMetrics pendingColorScore;
    private ScoreMetrics pendingIntensityScore;

    // SAUVEGARDE (Pour Annuler)
    private float originalIntensity;
    private Color originalColor;
    private bool isPreviewing = false;

    void Start()
    {
        roomManager = GetComponentInParent<RoomManager>();
        targetLights = GetComponentsInChildren<Light>();

        confirmedColorScore = new ScoreMetrics(0, 0, 0);
        confirmedIntensityScore = new ScoreMetrics(0, 0, 0);

        // --- Initialisation des scores ---
        if (colorProfile != null && roomManager != null)
        {
            ScoreMetrics startScore = colorProfile.GetScore(roomManager.roomType);
            roomManager.AddPointsToRoom(startScore);
            confirmedColorScore = startScore;
            pendingColorScore = startScore; 
        }

        if (intensityProfile != null && targetLights != null && targetLights.Length > 0)
        {
            float startIntensity = targetLights[0].intensity;
            ScoreMetrics intScore = intensityProfile.GetScoreForIntensity(roomManager.roomType, startIntensity);
            
            if (roomManager != null)
            {
                roomManager.AddPointsToRoom(intScore);
                confirmedIntensityScore = intScore;
                pendingIntensityScore = intScore;
            }
        }
        
        // On mémorise l'état initial au démarrage
        SaveOriginalState();
    }

    void SaveOriginalState()
    {
        if (targetLights != null && targetLights.Length > 0)
        {
            originalIntensity = targetLights[0].intensity;
            originalColor = targetLights[0].color;
        }
    }

    void OnEnable() => EventManager.AddObserver(this);
    void OnDisable() => EventManager.RemoveObserver(this);


    // --- 1. MODIFICATIONS VISUELLES (PREVIEW) ---

    public void OnLightIntensityChanged(float sliderValue)
    {
        if (currentRoomRef.activeRoom != roomManager) return;

        if (!isPreviewing)
        {
            SaveOriginalState();
            isPreviewing = true;
        }

        // Si votre formule est slider * 2, ajustez ici :
        float realIntensity = sliderValue * 2; 

        // Calcul du score potentiel (sans l'envoyer au RoomManager)
        if (intensityProfile != null)
        {
            pendingIntensityScore = intensityProfile.GetScoreForIntensity(roomManager.roomType, realIntensity);
        }

        // Mise à jour visuelle : SEULEMENT l'intensité
        UpdateIntensityOnly(realIntensity);
    }

    public void OnLightColorChanged(Color color, ScoreMetrics newScore)
    {
        if (currentRoomRef.activeRoom != roomManager) return;

        if (!isPreviewing)
        {
            SaveOriginalState();
            isPreviewing = true;
        }

        pendingColorScore = newScore;

        // Mise à jour visuelle : SEULEMENT la couleur
        UpdateColorOnly(color);
    }


    // --- 2. CONFIRMATION & ANNULATION ---

    public void ConfirmChanges()
    {
        if (!isPreviewing || roomManager == null) return;

        // A. On retire les ANCIENS scores validés
        ScoreMetrics toRemove = new ScoreMetrics(
            -(confirmedColorScore.design + confirmedIntensityScore.design),
            -(confirmedColorScore.usure + confirmedIntensityScore.usure),
            -(confirmedColorScore.gout + confirmedIntensityScore.gout)
        );
        roomManager.AddPointsToRoom(toRemove);

        // B. On ajoute les NOUVEAUX scores
        ScoreMetrics toAdd = pendingColorScore + pendingIntensityScore;
        roomManager.AddPointsToRoom(toAdd);

        // C. On valide
        confirmedColorScore = pendingColorScore;
        confirmedIntensityScore = pendingIntensityScore;
        
        isPreviewing = false;
        
        // On sauvegarde le nouvel état comme étant la nouvelle référence
        SaveOriginalState();
        
        Debug.Log("Lumières confirmées !");
    }

    public void CancelChanges()
    {
        if (!isPreviewing) return;

        // On remet tout comme avant (Intensité ET Couleur)
        RestoreAll(originalIntensity, originalColor);

        // On réinitialise les scores en attente
        pendingColorScore = confirmedColorScore;
        pendingIntensityScore = confirmedIntensityScore;

        isPreviewing = false;
        Debug.Log("Lumières annulées.");
    }

    // --- 3. FONCTIONS UTILITAIRES (Pour éviter l'erreur de surcharge) ---

    private void UpdateIntensityOnly(float intensity)
    {
        if (targetLights == null) return;
        foreach (Light l in targetLights) 
        {
            if (l != null) l.intensity = intensity;
        }
    }

    private void UpdateColorOnly(Color color)
    {
        if (targetLights == null) return;
        foreach (Light l in targetLights) 
        {
            if (l != null) l.color = color;
        }
    }

    private void RestoreAll(float intensity, Color color)
    {
        if (targetLights == null) return;
        foreach (Light l in targetLights)
        {
            if (l != null)
            {
                l.intensity = intensity;
                l.color = color;
            }
        }
    }
}