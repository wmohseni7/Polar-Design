using UnityEngine;

public class ScoreResetter : MonoBehaviour
{
    [Header("Données à nettoyer")]
    public FloorData floorData;

    private void Start()
    {
        ResetScoreNow();
    }

    // Cette fonction est publique pour pouvoir être liée à un Bouton Unity
    public void ResetScoreNow()
    {
        if (floorData != null)
        {
            floorData.ResetAll();
        }
        else
        {
            Debug.LogWarning("[ScoreResetter] Oubli : Il manque le fichier FloorData !");
        }
    }
}