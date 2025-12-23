using UnityEngine;

/// <summary>
/// Stratégie de chargement des scènes spécifique à la "Noisy Tower".
/// Gère la structure des étages et les correspondances de scènes pour cette tour.
/// </summary>
public class NoisyTowerSceneLoader : ISceneLoaderStrategy
{
    /// <summary>
    /// Récupère la liste des pièces disponibles pour un étage donné de la Noisy Tower.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage (0 pour le RDC, 1 pour le 1er étage).</param>
    /// <returns>Un tableau contenant les identifiants techniques des pièces.</returns>
    public string[] GetAllPartsForFloor(int floor)
    {
        return floor switch
        {
            // Étage 0 : Thématiques communes (Hôpital, Cuisine, Social)
            0 => new string[] { "Hospital", "Kitchen", "Social" },
            // Étage 1 : Thématiques techniques (Gym, Atelier, Laboratoire)
            1 => new string[] { "Gym", "Workshop", "Laboratory" },
            // Cas de sécurité : retourne un tableau vide
            _ => new string[0]
        };
    }

    /// <summary>
    /// Associe un étage et une pièce au nom de la scène Unity correspondante pour la Noisy Tower.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage.</param>
    /// <param name="part">L'identifiant de la pièce.</param>
    /// <returns>Le nom exact de la scène à charger ou null si aucune correspondance n'est trouvée.</returns>
    public string GetSceneName(int floor, string part)
    {
        // Utilisation du Tuple Pattern Matching pour une lecture claire et efficace des conditions doubles
        return (floor, part) switch
        {
            // Scènes du Rez-de-chaussée (GF)
            (0, "Hospital")   => "NT-GF-Hospital",
            (0, "Kitchen")    => "NT-GF-Kitchen",
            (0, "Social")     => "NT-GF-Social",
            
            // Scènes du 1er étage (1F)
            (1, "Gym")        => "NT-1F-Gym",
            (1, "Workshop")   => "NT-1F-Workshop",
            (1, "Laboratory") => "NT-1F-Laboratory",
            
            // Sécurité pour les valeurs non répertoriées
            _ => null
        };
    }
}