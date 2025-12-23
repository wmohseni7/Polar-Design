using UnityEngine;

/// <summary>
/// Interface définissant la stratégie de chargement des scènes pour les pièces d'un étage.
/// Elle permet d'abstraire la structure des étages pour faciliter la navigation automatique.
/// </summary>
public interface ISceneLoaderStrategy
{
    /// <summary>
    /// Récupère la liste exhaustive de toutes les pièces disponibles pour un étage donné.
    /// Indispensable pour calculer la progression ou identifier la pièce suivante.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage concerné.</param>
    /// <returns>Un tableau de chaînes de caractères contenant les identifiants des pièces.</returns>
    string[] GetAllPartsForFloor(int floor);

    /// <summary>
    /// Résout le nom exact de la scène Unity à charger en fonction de l'étage et de la pièce.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage.</param>
    /// <param name="part">L'identifiant de la pièce (ex: "Dorm", "Library").</param>
    /// <returns>Le nom de la scène tel qu'il apparaît dans les Build Settings.</returns>
    string GetSceneName(int floor, string part);
}