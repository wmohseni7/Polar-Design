using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gère les données d'un personnage, ses statistiques de satisfaction et ses préférences.
/// </summary>
public class Character : MonoBehaviour
{
    [Header("Infos")]
    /// <summary> Nom du personnage. </summary>
    public string characterName;
    /// <summary> Genre du personnage. </summary>
    public string gender;
    /// <summary> Métier du personnage. </summary>
    public string job;
    /// <summary> Nationalité du personnage. </summary>
    public string nationality;

    public System.Action OnStatsChanged;

    [Header("Notes du personnage (0-100)")]
    /// <summary> Niveau de confort actuel. </summary>
    [Range(0, 100)] public int comfort;
    /// <summary> Niveau d'usure actuel. </summary>
    [Range(0, 100)] public int wear;
    /// <summary> Niveau de goût/esthétique actuel. </summary>
    [Range(0, 100)] public int taste;

    [Header("Preferences")]
    public List<string> likes = new List<string>();
    public List<string> dislikes = new List<string>();

    [Header("UI & Feedback")]
    // MODIFICATION ICI : On utilise ton nouveau script FloatingIcon
    public FloatingIcon reactionIcon;

    /// <summary>
    /// Applique des modifications aux statistiques de satisfaction.
    /// </summary>
    /// <param name="comfortDelta">Variation de la valeur de confort.</param>
    /// <param name="wearDelta">Variation de la valeur d'usure.</param>
    /// <param name="tasteDelta">Variation de la valeur de goût.</param>
    public void ApplyChange(int comfortDelta, int wearDelta, int tasteDelta)
    {
        // On calcule le score total du changement
        int totalChange = comfortDelta + wearDelta + tasteDelta;

        // On applique les modifs
        comfort += comfortDelta;
        wear += wearDelta;
        taste += tasteDelta;

        // On previent l'UI que les chiffres ont changé
        OnStatsChanged?.Invoke();

        // Si il y a eu un changement (positif ou négatif), on affiche l'icône
        if (totalChange != 0)
        {
            ShowReaction(totalChange);
        }
    }

    /// <summary>
    /// Restreint les statistiques dans la plage autorisée (0-100).
    /// </summary>
    void ClampStats()
    {
        comfort = Mathf.Clamp(comfort, 0, 100);
        wear = Mathf.Clamp(wear, 0, 100);
        taste = Mathf.Clamp(taste, 0, 100);
    }
    /// <summary>
    /// Calcule le score moyen et déclenche l'affichage de la réaction sur l'UI.
    /// </summary>
    void ShowReaction(int changeAmount)
    {
        if (reactionIcon != null)
        {
            // Si le changement est positif (> 0), isPositive devient VRAI (Vert/Sourire)
            // Sinon, c'est FAUX (Rouge/Triste)
            bool isPositive = (changeAmount > 0);
            
            reactionIcon.Show(isPositive);
        }
    }
}
