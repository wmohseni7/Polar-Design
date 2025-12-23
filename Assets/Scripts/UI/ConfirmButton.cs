using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    private Button myButton;

    // Variable statique pour savoir quel est le meuble actif en ce moment
    public static FurnitureManager CurrentSelectedManager;

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnConfirmClicked);
    }

    void OnConfirmClicked()
    {
        if (CurrentSelectedManager != null)
        {
            // On valide le meuble
            CurrentSelectedManager.ValidateFurniture();

            // On dit au meuble : "C'est bon, tu comptes comme modifié !"
            CurrentSelectedManager.MarkAsModified();

            // On ferme le menu UI (Lumière/Meuble)
            EventManager.NotifyDeselectAll();
            
            // On oublie la sélection
            CurrentSelectedManager = null;
        }
        else
        {
            Debug.Log("Aucun meuble sélectionné à valider !");
        }
    }
}