using UnityEngine;

/// <summary>
/// Fabrique statique permettant de générer la stratégie de navigation appropriée.
/// Centralise la création des objets de stratégie en fonction du nom de la tour sélectionnée.
/// </summary>
public static class FloorSceneStrategyFactory
{
    /// <summary>
    /// Retourne une instance de stratégie correspondant à la tour spécifiée.
    /// </summary>
    /// <param name="tower">Le nom technique de la tour (ex: "CalmTower").</param>
    /// <returns>
    /// Une instance implémentant IFloorSceneStrategy ou null si la tour n'est pas reconnue.
    /// </returns>
    public static IFloorSceneStrategy GetStrategy(string tower)
    {
        // Utilisation d'une expression switch pour instancier la bonne classe de stratégie.
        // Cela permet d'ajouter de nouvelles tours simplement en ajoutant une ligne ici.
        return tower switch
        {
            "CalmTower" => new CalmTowerFloorSceneStrategy(),
            "NoisyTower" => new NoisyTowerFloorSceneStrategy(),
            _ => null // Sécurité : retourne null si le nom de la tour est invalide.
        };
    }
}