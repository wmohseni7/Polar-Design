using UnityEngine;

public class ImageToggler : MonoBehaviour
{
    [Header("Réglages")]
    // C'est ici que tu glisseras ton objet QRCODE
    public GameObject imageToToggle; 

    public void ToggleImage()
    {
        if (imageToToggle != null)
        {
            // Inverse l'état : si allumé -> éteint, si éteint -> allumé
            bool estActif = imageToToggle.activeSelf;
            imageToToggle.SetActive(!estActif);
        }
    }
}