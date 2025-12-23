using UnityEngine;
using System.Collections;

/// <summary>
/// Gère l'affichage visuel des réactions d'un personnage (icônes de satisfaction ou de mécontentement).
/// </summary>
public class CharacterReactionUI : MonoBehaviour
{
    /// <summary> Objet visuel représentant une réaction positive (ex: icône joyeuse). </summary>
    public GameObject happyIcon;
    /// <summary> Objet visuel représentant une réaction négative (ex: icône triste). </summary>
    public GameObject sadIcon;

    /// <summary>
    /// Affiche l'icône appropriée en fonction d'un score donné et lance un minuteur pour la masquer.
    /// </summary>
    /// <param name="score">Le score moyen utilisé pour déterminer la réaction (seuil à 50).</param>
    public void ShowReaction(int val)
    {
        bool good = (val >= 0); 

        if(happyIcon) happyIcon.SetActive(good);
        if(sadIcon) sadIcon.SetActive(!good);

        StopAllCoroutines();
        StartCoroutine(HideLater());
    }

    /// <summary>
    /// Coroutine qui désactive toutes les icônes de réaction après un délai de 1,5 seconde.
    /// </summary>
    /// <returns>Attend un délai en temps réel avant de masquer les objets.</returns>
    IEnumerator HideLater()
    {
        yield return new WaitForSeconds(1.5f);
        happyIcon.SetActive(false);
        sadIcon.SetActive(false);
    }
}
