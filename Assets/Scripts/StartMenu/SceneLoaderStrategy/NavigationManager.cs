using System.Collections.Generic;

/// <summary>
/// Gestionnaire statique de l'historique de navigation.
/// Utilise une structure de données en "Pile" (Last-In, First-Out) pour mémoriser 
/// l'ordre de passage dans les scènes et permettre un retour en arrière fluide.
/// </summary>
public static class NavigationManager
{
    /// <summary> Pile stockant les noms des scènes visitées. </summary>
    private static Stack<string> sceneHistory = new Stack<string>();

    /// <summary>
    /// Ajoute une nouvelle scène au sommet de l'historique.
    /// Appelez ceci juste avant de charger une nouvelle scène.
    /// </summary>
    /// <param name="sceneName">Le nom de la scène à mémoriser.</param>
    public static void Push(string sceneName)
    {
        sceneHistory.Push(sceneName);
    }

    /// <summary>
    /// Retire et retourne la scène actuelle (le sommet de la pile).
    /// </summary>
    /// <returns>Le nom de la scène retirée, ou null si l'historique est vide.</returns>
    public static string Pop()
    {
        return sceneHistory.Count > 0 ? sceneHistory.Pop() : null;
    }

    /// <summary>
    /// Consulte le nom de la scène au sommet de la pile sans la retirer.
    /// Utile pour savoir où le bouton "Retour" va renvoyer le joueur.
    /// </summary>
    /// <returns>Le nom de la scène précédente, ou null si aucune n'est stockée.</returns>
    public static string Peek()
    {
        return sceneHistory.Count > 0 ? sceneHistory.Peek() : null;
    }

    /// <summary>
    /// Efface l'intégralité de l'historique de navigation.
    /// Généralement utilisé lors d'un retour au menu principal ou d'une déconnexion.
    /// </summary>
    public static void Clear()
    {
        sceneHistory.Clear();
    }

    /// <summary>
    /// Vérifie s'il existe une scène précédente dans l'historique.
    /// On vérifie si le compte est > 1 car la scène actuelle est toujours au sommet.
    /// </summary>
    /// <returns>Vrai si un retour en arrière est possible.</returns>
    public static bool HasPrevious()
    {
        return sceneHistory.Count > 1;
    }
}