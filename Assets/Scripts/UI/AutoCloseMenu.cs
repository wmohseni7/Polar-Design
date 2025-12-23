using UnityEngine;

public class AutoCloseMenu : MonoBehaviour
{
    [Header("Les boutons à surveiller")]
    public GameObject lightButton; // Le bouton Lumiere
    public GameObject colorButton; // Le bouton Couleur

    void Update()
    {
        // Sécurité : on vérifie si les variables sont bien assignées pour éviter les erreurs
        if (lightButton == null || colorButton == null) return;

        // Si le bouton Lumière est éteint ET que le bouton Couleur est éteint
        if (!lightButton.activeSelf && !colorButton.activeSelf)
        {
            // On désactive ce panneau (MenuPanel)
            Debug.Log("Plus aucun bouton disponible : fermeture du Menu.");
            gameObject.SetActive(false);
        }
    }
}