using UnityEngine;
using UnityEngine.UI;

public class FloatingIcon : MonoBehaviour
{
    [Header("Réglages Visuels")]
    public Image targetImage; // Glisse le composant Image ici
    
    [Header("Les Images (Sprites)")]
    public Sprite happySprite; // Image du sourire
    public Sprite sadSprite;   // Image triste

    [Header("Les Couleurs")]
    public Color happyColor = Color.green;
    public Color sadColor = Color.red;

    [Header("Animation")]
    public float displayDuration = 2.0f;

    // Affiche l'icône avec le bon smiley et la bonne couleur
    public void Show(bool isPositive)
    {
        // 1. On change l'image et la couleur
        if (targetImage != null)
        {
            if (isPositive)
            {
                targetImage.sprite = happySprite;
                targetImage.color = happyColor;
            }
            else
            {
                targetImage.sprite = sadSprite;
                targetImage.color = sadColor;
            }
        }

        // 2. On active l'objet
        gameObject.SetActive(true);

        // 3. On programme la disparition
        CancelInvoke("Hide");
        Invoke("Hide", displayDuration);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}