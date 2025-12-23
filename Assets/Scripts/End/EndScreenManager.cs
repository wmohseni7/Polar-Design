using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;    
    public TextMeshProUGUI reportText;   
    public Image[] stars;                
    
    [Header("Graphismes")]
    public Sprite starFilled; // Glisse l'image JAUNE ici
    public Sprite starEmpty;  // Glisse l'image BLANCHE ici

    [Header("Textes des Rapports")]
    [TextArea] public string reportCalmGround = "Tour Calme (RDC) : ...";
    [TextArea] public string reportCalmFirst = "Tour Calme (1er) : ...";
    [TextArea] public string reportNoisyGround = "Tour Sociale (RDC) : ...";
    [TextArea] public string reportNoisyFirst = "Tour Sociale (1er) : ...";

    void Start()
    {
        string currentFloor = PlayerPrefs.GetString("EtageActuel", "Inconnu");
        float finalPercentage = PlayerPrefs.GetFloat("ScoreFinalPercent", 0f);

        DisplayScore(finalPercentage);
        DisplayReport(currentFloor, finalPercentage);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DisplayScore(float percentage)
    {
        scoreText.text = Mathf.RoundToInt(percentage).ToString() + "%";

        int starCount = 0;
        if (percentage >= 33) starCount = 1;
        if (percentage >= 66) starCount = 2;
        if (percentage >= 85) starCount = 3;

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < starCount)
            {
                // --- ETOILE GAGNÉE (JAUNE) ---
                if (starFilled != null) 
                {
                    stars[i].sprite = starFilled;
                    // IMPORTANT : On met la couleur à BLANC.
                    // Dans Unity, Blanc = "Pas de filtre", donc on verra ton image Jaune originale.
                    stars[i].color = new Color(1f, 0.84f, 0f, 1f);
                }
            }
            else
            {
                // --- ETOILE PERDUE (BLANCHE) ---
                if (starEmpty != null) 
                {
                    stars[i].sprite = starEmpty;
                    // Pareil ici, on laisse l'image originale
                    stars[i].color = Color.white; 
                }
            }
        }
    }

    void DisplayReport(string floorID, float percentage)
    {
        string message = "";
        switch (floorID)
        {
            case "Calm_GF": message = reportCalmGround; break;
            case "Calm_1F": message = reportCalmFirst; break;
            case "Noisy_GF": message = reportNoisyGround; break;
            case "Noisy_1F": message = reportNoisyFirst; break;
            default: message = "Rapport généré."; break;
        }

        if (percentage >= 80) message += "\n\n<color=green>Excellent travail architecte !</color>";
        else if (percentage >= 50) message += "\n\n<color=orange>Résultat acceptable.</color>";
        else message += "\n\n<color=red>Attention, le moral de l'équipe est en danger.</color>";

        reportText.text = message;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }
}