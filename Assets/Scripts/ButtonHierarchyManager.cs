using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère la transition entre deux panneaux (panels) au sein d'une même interface.
/// Utile pour naviguer dans des sous-menus ou des hiérarchies de boutons.
/// </summary>
public class ButtonHierarchyManager : MonoBehaviour
{
    [Header("Gestion des Panneaux")]
    [Tooltip("Le panneau actuel qui sera masqué lors de la transition.")]
    public GameObject currentPanel; 

    [Tooltip("Le nouveau panneau qui sera affiché.")]
    public GameObject nextPanel;    

    /// <summary>
    /// Exécute la transition : désactive le panneau actuel et active le suivant.
    /// Cette méthode est conçue pour être liée à l'événement 'OnClick' d'un bouton Unity.
    /// </summary>
    public void OpenSubMenu()
    {
        // Désactive le panneau source s'il est renseigné
        if (currentPanel != null)
            currentPanel.SetActive(false);

        // Active le panneau cible s'il est renseigné
        if (nextPanel != null)
            nextPanel.SetActive(true);
    }
}