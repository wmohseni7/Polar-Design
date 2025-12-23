using UnityEngine;
using TMPro; // N'oublie pas ça pour TextMeshPro

public class ModificationCounterUI : MonoBehaviour
{
    public static ModificationCounterUI Instance; // Singleton pour accès facile

    [Header("UI")]
    public TextMeshProUGUI counterText; // Glisse ton texte ici (ex: "0 / 5")

    private FurnitureSelector[] allModifiables;
    private int totalCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 1. On trouve TOUS les objets modifiables de la scène (Ton idée !)
        allModifiables = FindObjectsOfType<FurnitureSelector>();
        
        // 2. On calcule le total
        totalCount = allModifiables.Length;

        // 3. On affiche l'état initial (0 / Total)
        UpdateUI(0);
    }

    // Cette fonction recalculera combien sont modifiés
    public void UpdateCount()
    {
        int currentModified = 0;

        foreach (var furniture in allModifiables)
        {
            if (furniture.IsModified)
            {
                currentModified++;
            }
        }

        UpdateUI(currentModified);
    }

    void UpdateUI(int current)
    {
        if (counterText != null)
        {
            counterText.text = $"Il y a {totalCount} meubles à modifier";
            
            // Affiche sous forme "3 / 10"
            // counterText.text = $"{current} / {totalCount}";
        }
    }
}