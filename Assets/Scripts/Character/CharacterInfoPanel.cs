using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CharacterInfoPanel : MonoBehaviour
{
    [Header("Textes Généraux")]
    public TMP_Text nameText;
    public TMP_Text infosText;
    
    [Header("Stats (Mise à jour en temps réel)")]
    public TMP_Text statsText; 
    
    [Header("Goûts (Listes)")]
    public TMP_Text likesText;
    public TMP_Text dislikesText;

    [Header("Configuration")]
    public GameObject panelRoot; 

    // Variable pour se souvenir quel personnage on est en train d'afficher
    private Character currentCharacter;

    void Start()
    {
        Hide();
    }

    /// <summary>
    /// Active le panneau, remplit les infos et S'ABONNE aux mises à jour.
    /// </summary>
    public void Show(Character c)
    {
        // 1. Si on affichait déjà quelqu'un d'autre avant, on se désabonne de lui
        if (currentCharacter != null)
        {
            currentCharacter.OnStatsChanged -= UpdateLiveStats;
        }

        // 2. On définit le nouveau personnage
        currentCharacter = c;
        panelRoot.SetActive(true);

        // 3. On affiche les infos qui ne bougent jamais (Nom, Métier, Goûts)
        nameText.text = c.characterName;
        infosText.text = $"{c.gender} | {c.job} ({c.nationality})";
        
        likesText.text = "Aime : " + FormatList(c.likes);
        dislikesText.text = "N'aime pas : " + FormatList(c.dislikes);

        // 4. On s'abonne pour recevoir les futurs changements de stats
        if (currentCharacter != null)
        {
            currentCharacter.OnStatsChanged += UpdateLiveStats;
            
            // 5. On force une première mise à jour immédiate
            UpdateLiveStats();
        }
    }

    /// <summary>
    /// Cette fonction est appelée automatiquement par le Character quand ses stats changent.
    /// </summary>
    private void UpdateLiveStats()
    {
        if (currentCharacter == null) return;

        statsText.text = 
            $"Confort : {currentCharacter.comfort}/100\n" +
            $"État : {currentCharacter.wear}/100\n" +
            $"Goût : {currentCharacter.taste}/100";
            
        // Si tu veux ajouter un petit effet visuel (flash) quand ça change, c'est ici !
    }

    string FormatList(List<string> items)
    {
        if (items == null || items.Count == 0) return "Rien de spécial";
        return string.Join(", ", items);
    }

    public void Hide()
    {
        // TRES IMPORTANT : On se désabonne quand on ferme le panneau
        // Sinon le personnage continuera d'essayer de mettre à jour un panneau fermé !
        if (currentCharacter != null)
        {
            currentCharacter.OnStatsChanged -= UpdateLiveStats;
            currentCharacter = null; // On oublie la référence
        }

        panelRoot.SetActive(false);
    }
    
    // Sécurité supplémentaire : si l'objet est détruit brutalement
    private void OnDestroy()
    {
        if (currentCharacter != null)
            currentCharacter.OnStatsChanged -= UpdateLiveStats;
    }
}