using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Définit les catégories de pièces pour adapter les scores de décoration.
/// Permet de distinguer les besoins spécifiques selon l'usage ou la saison.
/// </summary>
public enum RoomType
{
    White,      // Espaces cliniques/neutres (ex: Salles de bain, infirmerie)
    Day,        // Espaces conçus pour la saison d'été (luminosité, fraîcheur)
    Night       // Espaces conçus pour la saison d'hiver (chaleur, cocooning)
}

/// <summary>
/// Structure associant un type de pièce à un score spécifique.
/// Utilisée pour définir des exceptions dans les profils de score.
/// </summary>
[System.Serializable]
public struct RoomScoreRule
{
    /// <summary> Le type de pièce concerné par cette règle. </summary>
    public RoomType type;      
    /// <summary> Le score à appliquer si la pièce correspond au type. </summary>
    public ScoreMetrics score; 
}

/// <summary>
/// Profil de score intelligent qui s'adapte au contexte.
/// Contient un score de base et une liste de règles d'exception.
/// </summary>
[System.Serializable]
public class ContextualScoreProfile
{
    [Header("Score par défaut")]
    [Tooltip("Score appliqué si le type de la pièce n'est pas dans la liste des règles spécifiques.")]
    public ScoreMetrics defaultScore; 

    [Header("Exceptions (Règles Spécifiques)")]
    [Tooltip("Liste des bonus/malus selon le type de pièce.")]
    public List<RoomScoreRule> rules; 

    /// <summary>
    /// Recherche et retourne le score approprié pour un type de pièce donné.
    /// </summary>
    /// <param name="typeToCheck">Le type de la pièce actuelle.</param>
    /// <returns>Le score spécifique si une règle existe, sinon le score par défaut.</returns>
    public ScoreMetrics GetScore(RoomType typeToCheck)
    {
        // On parcourt la liste des exceptions pour trouver une correspondance
        if (rules != null)
        {
            foreach (var rule in rules)
            {
                if (rule.type == typeToCheck)
                {
                    return rule.score; // Règle spécifique trouvée
                }
            }
        }

        // Si aucune règle n'a été définie pour ce type, on utilise la valeur par défaut
        return defaultScore;
    }
}