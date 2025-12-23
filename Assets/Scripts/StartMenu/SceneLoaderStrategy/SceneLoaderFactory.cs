using UnityEngine;

/// <summary>
/// Fabrique statique responsable de la création des stratégies de chargement de scènes.
/// Permet d'obtenir la logique de structure d'étage appropriée selon la tour choisie.
/// </summary>
public static class SceneLoaderFactory
{
    /// <summary>
    /// Retourne l'instance de la stratégie de chargement correspondant à la tour spécifiée.
    /// </summary>
    /// <param name="tower">Le nom technique de la tour (ex: "CalmTower").</param>
    /// <returns>
    /// Une instance implémentant ISceneLoaderStrategy, ou null si la tour n'est pas répertoriée.
    /// </returns>
    public static ISceneLoaderStrategy GetStrategy(string tower)
    {
        // Utilisation d'une expression switch pour instancier dynamiquement la bonne stratégie.
        // Cette approche facilite l'ajout de nouvelles tours (ex: "AncientTower") 
        // avec un impact minimal sur le reste du code.
        return tower switch
        {
            "CalmTower" => new CalmTowerSceneLoader(),
            "NoisyTower" => new NoisyTowerSceneLoader(),
            _ => null
        };
    }
}