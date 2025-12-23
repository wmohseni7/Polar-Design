using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SmartOutline : MonoBehaviour
{
    private Outline outlineScript;
    private bool isSelected = false;

    void Awake()
    {
        // On prépare l'outline
        outlineScript = GetComponent<Outline>();
        if (outlineScript == null)
        {
            outlineScript = gameObject.AddComponent<Outline>();
            outlineScript.OutlineMode = Outline.Mode.OutlineAll;
            outlineScript.OutlineColor = new Color(0.247f, 0.259f, 0.275f); // Ton gris
            outlineScript.OutlineWidth = 5f;
        }
        outlineScript.enabled = false;
    }

    // --- ABONNEMENT AUX EVENEMENTS ---
    void OnEnable()
    {
        EventManager.OnObjectSelected += OnGlobalSelectionChanged;
        EventManager.OnDeselectAll += OnDeselectOrder;
    }

    void OnDisable()
    {
        EventManager.OnObjectSelected -= OnGlobalSelectionChanged;
        EventManager.OnDeselectAll -= OnDeselectOrder;
    }

    // --- SOURIS (HOVER) ---
    void OnMouseEnter()
    {
        // On allume toujours quand on passe dessus
        if (outlineScript != null) outlineScript.enabled = true;
    }

    void OnMouseExit()
    {
        // On éteint seulement si on n'est PAS sélectionné
        if (!isSelected && outlineScript != null)
        {
            outlineScript.enabled = false;
        }
    }

    // --- CLIC (SELECTION) ---
    void OnMouseDown()
    {
        // On devient sélectionné
        isSelected = true;
        
        // On allume
        if (outlineScript != null) outlineScript.enabled = true;

        // On previens tout le monde
        EventManager.NotifyObjectSelected(this.gameObject);
    }

    // --- REPONSE AUX EVENEMENTS ---
    
    // Appelé quand nímporte qui est cliqué
    private void OnGlobalSelectionChanged(GameObject newSelection)
    {
        // Si l'objet cliqué n'est pas moi
        if (newSelection != this.gameObject)
        {
            // alors je ne suis plus sélectionné
            isSelected = false;
            // et je m'éteins
            if (outlineScript != null) outlineScript.enabled = false;
        }
    }

    // Appelé quand on clique sur l'UI (Bouton Mur/Lumière)
    private void OnDeselectOrder()
    {
        isSelected = false;
        if (outlineScript != null) outlineScript.enabled = false;
    }

    // Pour pouvoir selectionner un meuble depuis le code
    public void ForceSelect()
    {
        //  On devient sélectionné
        isSelected = true;

        // On allume la l outline
        if (outlineScript != null) outlineScript.enabled = true;

        // (Optionnel mais c est pas tres propre sans) Je préviens le système que c'est moi le chef
        // Comme ça si un autre meuble était sélectionné ailleurs, il s'éteint
        EventManager.NotifyObjectSelected(this.gameObject);
    }
}