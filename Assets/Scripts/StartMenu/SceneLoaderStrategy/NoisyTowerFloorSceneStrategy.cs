using UnityEngine;

/// <summary>
/// Stratégie de navigation spécifique pour la tour "Noisy Tower".
/// Gère la correspondance entre les index d'étages et les scènes de sélection de niveaux.
/// </summary>
public class NoisyTowerFloorSceneStrategy : IFloorSceneStrategy
{
    /// <summary>
    /// Retourne le nom de la scène de sélection d'étage pour la Noisy Tower.
    /// </summary>
    /// <param name="floor">L'index de l'étage (0 pour le RDC, 1 pour le 1er étage).</param>
    /// <returns>Le nom de la scène Unity correspondante ou null si l'étage n'existe pas.</returns>
    public string GetFloorScene(int floor)
    {
        // Utilisation du switch expression pour mapper les étages aux scènes NT (Noisy Tower)
        return floor switch
        {
            0 => "NT-GF-StageSelectScene",
            1 => "NT-1F-StageSelectScene",
            _ => null
        };
    }

    /// <summary>
    /// (Non implémenté) Prévu pour retourner le nom d'une scène spécifique à une partie de la tour.
    /// </summary>
    public string GetSceneName(int floor, string part)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// (Non implémenté) Prévu pour lister toutes les pièces disponibles par étage.
    /// </summary>
    public string[] GetAllPartsForFloor(int floor)
    {
        throw new System.NotImplementedException();
    }
}