using UnityEngine;

/// <summary>
/// Structure regroupant les trois axes de notation du jeu : Design, Usure et Goût.
/// Permet de manipuler ces scores comme un bloc unique grâce à la surcharge d'opérateurs.
/// </summary>
[System.Serializable] 
public struct ScoreMetrics
{
    /// <summary> Score esthétique lié à la qualité visuelle de l'aménagement. </summary>
    public int design;
    /// <summary> Score représentant l'état de dégradation ou de vieillissement. </summary>
    public int usure;
    /// <summary> Score basé sur la correspondance avec les préférences du personnage. </summary>
    public int gout;

    /// <summary>
    /// Constructeur permettant d'initialiser rapidement les trois mesures de score.
    /// </summary>
    /// <param name="d">Valeur initiale du Design.</param>
    /// <param name="u">Valeur initiale de l'Usure.</param>
    /// <param name="g">Valeur initiale du Goût.</param>
    public ScoreMetrics(int d, int u, int g)
    {
        design = d;
        usure = u;
        gout = g;
    }

    /// <summary>
    /// Définit le comportement de l'addition (+) entre deux instances de ScoreMetrics.
    /// Permet d'ajouter simultanément les trois axes de score.
    /// </summary>
    /// <param name="a">Premier ensemble de scores.</param>
    /// <param name="b">Deuxième ensemble de scores à ajouter.</param>
    /// <returns>Une nouvelle instance contenant la somme des scores axe par axe.</returns>
    public static ScoreMetrics operator +(ScoreMetrics a, ScoreMetrics b)
    {
        return new ScoreMetrics(a.design + b.design, a.usure + b.usure, a.gout + b.gout);
    }

    /// <summary>
    /// Définit le comportement de la soustraction (-) entre deux instances de ScoreMetrics.
    /// Utile pour retirer les points d'un ancien meuble avant d'ajouter le nouveau.
    /// </summary>
    /// <param name="a">Score actuel.</param>
    /// <param name="b">Score à soustraire.</param>
    /// <returns>Une nouvelle instance contenant la différence des scores axe par axe.</returns>
    public static ScoreMetrics operator -(ScoreMetrics a, ScoreMetrics b)
    {
        return new ScoreMetrics(a.design - b.design, a.usure - b.usure, a.gout - b.gout);
    }
}