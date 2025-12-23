using UnityEngine;

/// <summary>
/// Classe statique servant de conteneur de données global pour la session de jeu.
/// Permet de stocker et de transmettre les sélections de l'utilisateur (Tour, Étage, Pièce) 
/// entre les différentes scènes du projet.
/// </summary>
public static class GameData
{
    /// <summary> Nom de la tour actuellement sélectionnée (ex: "CalmTower"). </summary>
    public static string SelectedTower;

    /// <summary> Index ou numéro de l'étage choisi par le joueur. </summary>
    public static int SelectedFloor;

    /// <summary> Identifiant de la pièce ou de la partie spécifique à charger (ex: "Bedroom1"). </summary>
    public static string SelectedPart;
}