using UnityEngine;
using UnityEngine.UI;

public class SatisfactionBar : MonoBehaviour
{
    [Header("Réglages")]
    public Slider satisfactionSlider;
    
    [Tooltip("Le score de départ (ex: 75 pour commencer au milieu)")]
    public int baseScore = 75; 
    public int maxScore = 150;

    void Start()
    {
        // Initialisation propre
        satisfactionSlider.minValue = 0;
        satisfactionSlider.maxValue = maxScore;
        
        // Au démarrage, on se met à la position neutre (75) en attendant les données
        UpdateVisuals(0); 
    }

    // Cette fonction sera appelée par ton UI Manager ou RoomManager
    public void UpdateVisuals(float scoreDeLaPiece)
    {
        float finalValue = baseScore + scoreDeLaPiece;

        // --- AJOUTE CECI ---
        Debug.Log($"[SatisfactionBar] Reçu: {scoreDeLaPiece} | Calculé: {finalValue} | Slider assigné ? {(satisfactionSlider != null)}");
        // -------------------

        if (satisfactionSlider != null)
        {
            satisfactionSlider.value = Mathf.Clamp(finalValue, 0, maxScore);
        }
    }
}