using UnityEngine;
using UnityEngine.UI;

public class LightPanelManager : MonoBehaviour
{
    [Header("Composants UI")]
    public Slider intensitySlider;
    public Toggle warmToggle;
    public Toggle coldToggle;
    public GameObject panelRoot;
    
    // AJOUTS POUR LE BOUTON CONFIRMER
    public Button[] interactableButtons; // Liste des boutons à bloquer (Toggles, Slider handle...)
    public GameObject mainLightButton; // Le bouton du menu principal à cacher

    [Header("Profils")]
    public ContextualScoreProfile warmProfile; 
    public ContextualScoreProfile coldProfile; 
    public CurrentRoomRef currentRoomRef;
    
    private bool isChangingToggle = false;

    void Start()
    {
        // ... Vos listeners (inchangés) ...
        if (intensitySlider != null) intensitySlider.onValueChanged.AddListener(OnIntensityChanged);
        if (warmToggle != null) warmToggle.onValueChanged.AddListener(OnWarmToggleChanged);
        if (coldToggle != null) coldToggle.onValueChanged.AddListener(OnColdToggleChanged);
    }

    void OnEnable()
    {
        SetInteractable(true);
        // Ici, il faudrait idéalement mettre à jour l'UI (slider/toggles) pour refléter 
        // les valeurs actuelles de la lumière si on rouvre le menu.
    }

    // ... Vos méthodes OnIntensityChanged, OnWarmToggleChanged... (INCHANGÉES) ...
    // Elles appellent EventManager.Notify... qui déclenche la PREVIEW dans LightManager.

    // --- LE NOUVEAU CODE ---

    public void OnConfirmClicked()
    {
        // 1. Bloquer l'UI
        SetInteractable(false);

        // 2. Valider dans le LightManager
        if (currentRoomRef.activeRoom != null && currentRoomRef.activeRoom.lightManager != null)
        {
            currentRoomRef.activeRoom.lightManager.ConfirmChanges();
        }

        // 3. Cacher le bouton du menu principal
        if (mainLightButton != null) mainLightButton.SetActive(false);

        // 4. Fermer
        ClosePanel();
    }

    public void OnCancelClicked()
    {
        // Annuler dans le LightManager
        if (currentRoomRef.activeRoom != null && currentRoomRef.activeRoom.lightManager != null)
        {
            currentRoomRef.activeRoom.lightManager.CancelChanges();
        }
        ClosePanel();
    }

    private void ClosePanel()
    {
        if (panelRoot != null) panelRoot.SetActive(false);
    }

    private void SetInteractable(bool state)
    {
        if (interactableButtons != null)
        {
            foreach (var btn in interactableButtons) if (btn) btn.interactable = state;
        }
        if (intensitySlider != null) intensitySlider.interactable = state;
        if (warmToggle != null) warmToggle.interactable = state;
        if (coldToggle != null) coldToggle.interactable = state;
    }
    
    // ... vos getters GetContextualScore ...
    private ScoreMetrics GetContextualScore(ContextualScoreProfile profile)
    {
        if (currentRoomRef.activeRoom != null) return profile.GetScore(currentRoomRef.activeRoom.roomType);
        return profile.defaultScore;
    }
    
    void OnIntensityChanged(float value) { EventManager.NotifyLightIntensityChanged(value); }

    void OnWarmToggleChanged(bool isOn)
    {
        if (isChangingToggle) return;
        isChangingToggle = true;
        if(coldToggle) coldToggle.isOn = !isOn;
        
        if (isOn)
        {
             EventManager.NotifyLightColorChanged(new Color(1f, 0.84f, 0.66f), GetContextualScore(warmProfile));
        }
        else
        {
             // Si on décoche chaud, on passe en froid (logique binaire)
             EventManager.NotifyLightColorChanged(new Color(0.66f, 0.84f, 1f), GetContextualScore(coldProfile));
        }
        isChangingToggle = false;
    }

    void OnColdToggleChanged(bool isOn)
    {
        if (isChangingToggle) return;
        isChangingToggle = true;
        if(warmToggle) warmToggle.isOn = !isOn;

        if (isOn)
             EventManager.NotifyLightColorChanged(new Color(0.66f, 0.84f, 1f), GetContextualScore(coldProfile));
        else
             EventManager.NotifyLightColorChanged(new Color(1f, 0.84f, 0.66f), GetContextualScore(warmProfile));
             
        isChangingToggle = false;
    }
}