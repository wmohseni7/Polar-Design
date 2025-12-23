using UnityEngine;

/// <summary>
/// Gère l'interface de sélection des couleurs.
/// Calcule les scores en fonction du contexte de la pièce actuelle et diffuse 
/// les changements via l'EventManager.
/// </summary>
public class ColorPanelManager : MonoBehaviour
{
    [Header("Profils de Score (ScriptableObjects)")]
    public ContextualScoreProfile whiteProfile;
    public ContextualScoreProfile yellowOrangeProfile;
    public ContextualScoreProfile blueGreenProfile;
    public ContextualScoreProfile redGreyProfile;

    [Header("Configuration")]
    /// <summary> Référence partagée permettant de connaître la pièce en cours d'édition. </summary>
    public CurrentRoomRef currentRoomRef;
    public GameObject panelRoot;
    public GameObject mainColorButton;

    #region Méthodes de Sélection (Appelées par les boutons UI)

    /// <summary> Palette Blanche : Neutre et propre, idéale pour les laboratoires. </summary>
    public void OnWhitePaletteSelected()
    {
        ScoreMetrics scoreToSend = GetContextualScore(whiteProfile);
        EventManager.NotifyPaletteChanged(Color.white, Color.white, scoreToSend);
    }

    /// <summary> Palette Jaune/Orange : Chaude, idéale pour compenser les nuits polaires. </summary>
    public void OnYellowOrangePaletteSelected()
    {
        ScoreMetrics scoreToSend = GetContextualScore(yellowOrangeProfile);
        EventManager.NotifyPaletteChanged(
            new Color(1.0f, 0.9f, 0.6f),
            new Color(1.0f, 0.6f, 0.2f),
            scoreToSend
        );
    }

    /// <summary> Palette Bleu/Vert : Apaisante, adaptée au jour continu. </summary>
    public void OnBlueGreenPaletteSelected()
    {
        ScoreMetrics scoreToSend = GetContextualScore(blueGreenProfile);
        EventManager.NotifyPaletteChanged(
            new Color(0.5f, 0.7f, 0.9f),
            new Color(0.6f, 0.8f, 0.6f),
            scoreToSend
        );
    }

    /// <summary> Palette Rouge/Gris : Stimulante mais potentiellement oppressante. </summary>
    public void OnRedGreyPaletteSelected()
    {
        ScoreMetrics scoreToSend = GetContextualScore(redGreyProfile);
        EventManager.NotifyPaletteChanged(
            new Color(0.753f, 0f, 0f),
            new Color(0.45f, 0.45f, 0.45f),
            scoreToSend
        );
    }

    #endregion

    /// <summary>
    /// Détermine le score approprié en croisant le profil de couleur choisi 
    /// avec le type de la pièce actuellement active (Chambre, Cuisine, etc.).
    /// </summary>
    /// <param name="profile">Le profil de score lié à la couleur sélectionnée.</param>
    /// <returns>Les métriques de score adaptées au contexte.</returns>
    private ScoreMetrics GetContextualScore(ContextualScoreProfile profile)
    {
        // Vérifie si une pièce est bien enregistrée comme "active" dans le ScriptableObject
        if (currentRoomRef.activeRoom != null)
        {
            // On demande au profil de renvoyer le score spécifique au type de la pièce active
            return profile.GetScore(currentRoomRef.activeRoom.roomType);
        }

        // Valeur de repli (fallback) si aucune pièce n'est sélectionnée
        return profile.defaultScore;
    }

    /// <summary>
    /// Fonction du bouton CONFIRMER
    /// </summary>
    public void OnConfirmClicked()
    {
        // 1. On trouve le ColorManager de la pièce active et on lui dit "Valide !"
        if (currentRoomRef.activeRoom != null && currentRoomRef.activeRoom.colorManager != null)
        {
            currentRoomRef.activeRoom.colorManager.ConfirmChanges();
        }

        // On fait disparaître le bouton du menu principal
        if (mainColorButton != null)
        {
            mainColorButton.SetActive(false);
        }
        
        // 2. On ferme le panneau
        ClosePanel();
    }

    /// <summary>
    /// Fonction du bouton ANNULER / FERMER (la croix)
    /// </summary>
    public void OnCancelClicked()
    {
        // 1. On dit au ColorManager "Annule tout, remets comme avant !"
        if (currentRoomRef.activeRoom != null && currentRoomRef.activeRoom.colorManager != null)
        {
            currentRoomRef.activeRoom.colorManager.CancelChanges();
        }

        // 2. On ferme le panneau
        ClosePanel();
    }

    private void ClosePanel()
    {
        if (panelRoot != null) panelRoot.SetActive(false);
    }
}