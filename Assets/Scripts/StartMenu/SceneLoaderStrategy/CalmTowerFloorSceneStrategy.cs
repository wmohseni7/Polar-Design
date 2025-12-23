using UnityEngine;

/// <summary>
/// Stratégie spécifique pour la tour "Calm Tower".
/// Détermine quelle scène de sélection d'étage charger en fonction du numéro de l'étage.
/// </summary>
public class CalmTowerFloorSceneStrategy : IFloorSceneStrategy
{
    /// <summary>
    /// Retourne le nom de la scène de sélection de niveau pour un étage donné de la Calm Tower.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage (0 pour RDC, 1 pour le 1er, etc.).</param>
    /// <returns>Le nom de la scène correspondante ou null si l'étage n'est pas reconnu.</returns>
    public string GetFloorScene(int floor)
    {
        // Utilisation d'une expression switch pour une syntaxe concise et lisible
        return floor switch
        {
            0 => "CT-GF-StageSelectScene", // Rez-de-chaussée
            1 => "CT-1F-StageSelectScene", // 1er étage
            _ => null                      // Cas par défaut si l'étage n'existe pas
        };
    }

    /// <summary>
    /// (Non implémenté) Prévu pour récupérer le nom d'une scène spécifique à une partie d'un étage.
    /// </summary>
    public string GetSceneName(int floor, string part)
    {
        throw new System.NotImplementedException();
    }
}