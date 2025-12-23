using UnityEngine;

/// <summary>
/// Interface définissant le contrat pour les stratégies de navigation par étage.
/// Permet de standardiser la récupération des noms de scènes pour n'importe quelle tour du jeu.
/// </summary>
public interface IFloorSceneStrategy
{
    /// <summary>
    /// Récupère le nom de la scène principale correspondant à un étage spécifique.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage concerné.</param>
    /// <returns>Une chaîne de caractères représentant le nom de la scène Unity.</returns>
    string GetFloorScene(int floor);
 
    /// <summary>
    /// Récupère le nom d'une scène spécifique pour une pièce donnée au sein d'un étage.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage.</param>
    /// <param name="part">Le nom ou l'identifiant de la pièce (ex: "Cuisine").</param>
    /// <returns>Le nom exact de la scène à charger.</returns>
    string GetSceneName(int floor, string part);
}