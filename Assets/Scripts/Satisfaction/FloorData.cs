using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ScriptableObject stockant les scores de chaque pièce d'un étage.
/// Permet de calculer le score global de l'étage et de notifier les autres systèmes des changements.
/// </summary>
[CreateAssetMenu(menuName = "Game/Floor Data")]
public class FloorData : ScriptableObject
{
    /// <summary> Dictionnaire associant le nom d'une pièce à ses mesures de score respectives. </summary>
    private Dictionary<string, ScoreMetrics> roomScores = new Dictionary<string, ScoreMetrics>();

    /// <summary> Événement déclenché lorsque le score total de l'étage change. Envoie le nouveau total. </summary>
    public System.Action<ScoreMetrics> OnFloorScoreChanged;

    /// <summary>
    /// Met à jour ou ajoute le score d'une pièce spécifique et recalcule le total de l'étage.
    /// </summary>
    /// <param name="roomName">Nom de la pièce à mettre à jour.</param>
    /// <param name="newScore">Nouvelles valeurs de score (ScoreMetrics) pour cette pièce.</param>
    public void UpdateRoomScore(string roomName, ScoreMetrics newScore)
    {
        // Initialisation de sécurité du dictionnaire
        if (roomScores == null) roomScores = new Dictionary<string, ScoreMetrics>();
        
        // Mise à jour ou ajout des données dans le dictionnaire
        if (roomScores.ContainsKey(roomName))
            roomScores[roomName] = newScore;
        else
            roomScores.Add(roomName, newScore);

        // Recalculer le score total de l'étage et notifier les abonnés
        ScoreMetrics total = GetTotalFloorScore();
        OnFloorScoreChanged?.Invoke(total);
    }

    /// <summary>
    /// Parcourt toutes les pièces enregistrées pour calculer la somme des scores de l'étage.
    /// </summary>
    /// <returns>Une instance de ScoreMetrics contenant les totaux cumulés.</returns>
    public ScoreMetrics GetTotalFloorScore()
    {
        ScoreMetrics total = new ScoreMetrics(0,0,0);
        
        if (roomScores == null) return total;
        
        foreach (ScoreMetrics score in roomScores.Values)
        {
            // Utilise la surcharge de l'opérateur + définie dans la classe ScoreMetrics
            total += score; 
        }
        return total;
    }
    
    /// <summary>
    /// Récupère le score enregistré pour une pièce donnée.
    /// </summary>
    /// <param name="roomName">Nom de la pièce recherchée.</param>
    /// <returns>Le score de la pièce, ou un score nul (0,0,0) si non trouvée.</returns>
    public ScoreMetrics GetScoreForRoom(string roomName)
    {
        if (roomScores != null && roomScores.ContainsKey(roomName))
            return roomScores[roomName];
            
        return new ScoreMetrics(0,0,0);
    }
    
    /// <summary>
    /// Réinitialise toutes les données de score enregistrées pour cet étage.
    /// </summary>
    public void ResetAll() 
    { 
        if (roomScores != null)
        {
            roomScores.Clear();
        }
    }
}