using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestionnaire central du jeu (GameManager).
/// Utilise le pattern Singleton pour assurer une instance unique et persistante entre les scènes.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> Instance unique du GameManager accessible globalement. </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Initialise le Singleton au réveil du script.
    /// Garantit que l'objet n'est pas détruit lors du chargement de nouvelles scènes.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Empêche la destruction de cet objet lors du changement de scène
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Détruit toute copie supplémentaire pour maintenir l'unicité
            Destroy(gameObject);
        }
    }

    // Vous pouvez déplacer les méthodes de chargement ici si vous préférez, 
    // mais les laisser dans StageSelectManager convient également.
}