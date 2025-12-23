using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gère la détection du survol de la souris pour afficher ou masquer les informations d'un personnage.
/// </summary>
public class CharacterHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary> Référence vers les données du personnage concerné. </summary>
    public Character character;
    /// <summary> Référence vers le panneau d'interface qui affiche les informations. </summary>
    public CharacterInfoPanel panel;

    /// <summary>
    /// Déclenché lorsque la souris entre dans la zone de l'objet. Affiche le panneau d'infos.
    /// </summary>
    /// <param name="eventData">Données de l'événement de pointage.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.Show(character);
    }

    /// <summary>
    /// Déclenché lorsque la souris sort de la zone de l'objet. Masque le panneau d'infos.
    /// </summary>
    /// <param name="eventData">Données de l'événement de pointage.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        panel.Hide();
    }
}