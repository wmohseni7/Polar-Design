using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Gère la session de jeu globale, incluant la progression du joueur, 
/// la satisfaction par pièce et le calcul des moyennes par étage.
/// Ce composant est persistant entre les scènes (Singleton).
/// </summary>
public class GameSessionManager : MonoBehaviour
{
    /// <summary> Instance unique du GameSessionManager (Singleton). </summary>
    public static GameSessionManager Instance { get; private set; }

    [Header("Progression")]
    /// <summary> Liste des identifiants des pièces déjà complétées par le joueur. </summary>
    public List<string> completedRooms = new List<string>();

    /// <summary> 
    /// Dictionnaire stockant les scores de satisfaction. 
    /// Clé : Identifiant de la pièce | Valeur : Score de satisfaction (0 à 1).
    /// </summary>
    public Dictionary<string, float> roomsSatisfaction = new Dictionary<string, float>();

    /// <summary> Référence aux données de l'étage actuellement sélectionné par le joueur. </summary>
    public FloorData currentSelectedFloor;

    /// <summary>
    /// Initialise le Singleton et garantit que l'objet n'est pas détruit au changement de scène.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enregistre une pièce comme étant terminée pour empêcher toute modification ultérieure.
    /// </summary>
    /// <param name="roomID">L'identifiant unique de la pièce.</param>
    public void MarkRoomAsCompleted(string roomID)
    {
        if (!completedRooms.Contains(roomID))
        {
            completedRooms.Add(roomID);
            Debug.Log($"[Progression] La pièce {roomID} est désormais verrouillée.");
        }
    }

    /// <summary>
    /// Enregistre ou met à jour le score de satisfaction pour une pièce spécifique.
    /// </summary>
    /// <param name="roomID">L'identifiant unique de la pièce.</param>
    /// <param name="score">Valeur de satisfaction (float entre 0 et 1).</param>
    public void SaveRoomSatisfaction(string roomID, float score)
    {
        if (roomsSatisfaction.ContainsKey(roomID))
            roomsSatisfaction[roomID] = score;
        else
            roomsSatisfaction.Add(roomID, score);
            
        Debug.Log($"[Satisfaction] {roomID} enregistrée avec un score de {score * 100}%");
    }

    /// <summary>
    /// Renvoie la SOMME TOTALE des points de toutes les pièces filtrées (sans faire de moyenne).
    /// </summary>
    public float GetTotalFloorScore(string filter)
    {
        float totalScore = 0f;
        
        // (Je suppose que vous avez une boucle similaire dans votre GetAverage actuel)
        foreach (var entry in roomsSatisfaction) 
        {
            if (entry.Key.Contains(filter))
            {
                totalScore += entry.Value;
            }
        }
        
        return totalScore;
    }
}