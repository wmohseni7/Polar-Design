using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Définit une règle de score basée sur une plage d'intensité lumineuse.
/// Permet de récompenser le joueur si l'éclairage d'une pièce est optimal.
/// </summary>
[System.Serializable]
public struct IntensityRule
{
    /// <summary> Le type de pièce concerné par cette règle (ex: Infirmerie). </summary>
    public RoomType roomType;       
    
    /// <summary> Seuil d'intensité minimale pour valider la règle. </summary>
    public float minIntensity;      
    
    /// <summary> Seuil d'intensité maximale pour valider la règle. </summary>
    public float maxIntensity;      
    
    /// <summary> Points de score accordés si l'intensité est dans la plage définie. </summary>
    public ScoreMetrics bonusScore; 
}

/// <summary>
/// Asset de type ScriptableObject contenant les règles d'intensité lumineuse.
/// Centralise la logique de calcul des bonus liés à l'éclairage.
/// </summary>
[CreateAssetMenu(fileName = "NewIntensityProfile", menuName = "Game/Light Intensity Profile")]
public class LightIntensityProfile : ScriptableObject
{
    /// <summary> Liste de toutes les règles d'intensité configurées pour ce profil. </summary>
    public List<IntensityRule> rules;

    /// <summary>
    /// Analyse l'intensité actuelle et retourne le score correspondant selon le type de pièce.
    /// </summary>
    /// <param name="type">Le type de la pièce à tester.</param>
    /// <param name="currentIntensity">L'intensité lumineuse mesurée.</param>
    /// <returns>Les ScoreMetrics bonus si une règle est satisfaite, sinon un score nul.</returns>
    public ScoreMetrics GetScoreForIntensity(RoomType type, float currentIntensity)
    {
        if (rules != null)
        {
            foreach (IntensityRule rule in rules)
            {
                // Vérification du type de pièce
                if (rule.roomType == type)
                {
                    // Vérification si l'intensité est comprise dans la fourchette (inclusive)
                    if (currentIntensity >= rule.minIntensity && currentIntensity <= rule.maxIntensity)
                    {
                        return rule.bonusScore;
                    }
                }
            }
        }
        
        // Retourne un score neutre (0,0,0) si aucune condition n'est remplie
        return new ScoreMetrics(0, 0, 0);
    }
}