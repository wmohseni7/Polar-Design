using UnityEngine;

/// <summary>
/// Stratégie de chargement des scènes spécifique à la "Calm Tower".
/// Définit la liste des pièces disponibles par étage et fait correspondre les identifiants aux noms de scènes réels.
/// </summary>
public class CalmTowerSceneLoader : ISceneLoaderStrategy
{
    /// <summary>
    /// Retourne la liste des identifiants de pièces disponibles pour un étage spécifique.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage (0 pour RDC, 1 pour le 1er étage).</param>
    /// <returns>Un tableau de chaînes de caractères contenant les noms des pièces.</returns>
    public string[] GetAllPartsForFloor(int floor)
    {
        return floor switch
        {
            // Liste des pièces pour le Rez-de-chaussée (Ground Floor)
            0 => new string[] { "Dorm", "Washroom", "Bedroom1", "Bedroom2" },
            // Liste des pièces pour le 1er étage (1st Floor)
            1 => new string[] { "Library", "Dorm", "Washroom", "Bedroom1", "Bedroom2" },
            // Retourne un tableau vide si l'étage n'est pas reconnu
            _ => new string[0]
        };
    }

    /// <summary>
    /// Associe un étage et une pièce au nom de la scène Unity correspondante.
    /// Utilise le pattern matching sur tuple pour une résolution précise et rapide.
    /// </summary>
    /// <param name="floor">Le numéro de l'étage.</param>
    /// <param name="part">L'identifiant de la pièce (ex: "Dorm").</param>
    /// <returns>Le nom exact de la scène dans Unity ou null si aucune correspondance n'est trouvée.</returns>
    public string GetSceneName(int floor, string part)
    {
        // Utilisation du Tuple Pattern Matching pour gérer deux variables simultanément
        return (floor, part) switch
        {
            // Étage 0 (GF)
            (0, "Dorm")     => "CT-GF-Dorm",
            (0, "Washroom") => "CT-GF-Washroom",
            (0, "Bedroom1") => "CT-GF-Bedroom1",
            (0, "Bedroom2") => "CT-GF-Bedroom2",
            
            // Étage 1 (1F)
            (1, "Library")  => "CT-1F-Library",
            (1, "Dorm")     => "CT-1F-Dorm",
            (1, "Washroom") => "CT-1F-Washroom",
            (1, "Bedroom1") => "CT-1F-Bedroom1",
            (1, "Bedroom2") => "CT-1F-Bedroom2",
            
            // Cas par défaut : aucune scène trouvée
            _ => null
        };
    }
}