using System;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using Unity.Collections.LowLevel.Unsafe;


/// <summary>
/// Gestionnaire central des événements du jeu.
/// Agit comme le "Sujet" dans le Pattern Observer, permettant de diffuser
/// des messages entre des systèmes déconnectés (ex: UI vers Objets 3D).
/// </summary>
public class EventManager : MonoBehaviour
{
    // Listes des abonnés pour chaque type d'événement
    private static List<LightObserver> lightObservers = new List<LightObserver>();
    private static List<ColorObserver> colorObservers = new List<ColorObserver>();

    // Observateurs uniques pour la gestion du mobilier (Focus sur l'objet sélectionné)
    private static FurnitureObserver furnitureObserver;

    // Pour l'événement de selection de meuble
    private static FurnitureSelectionObserver furnitureSelectionObserver; 

    #region Gestion des Inscriptions (Observers)

    public static void AddObserver(LightObserver observer)
    {
        if (!lightObservers.Contains(observer)) lightObservers.Add(observer);
    }

    public static void RemoveObserver(LightObserver observer)
    {
        lightObservers.Remove(observer);
    }

    public static void AddObserver(ColorObserver observer)
    {
        if (!colorObservers.Contains(observer)) colorObservers.Add(observer);
    }

    public static void RemoveObserver(ColorObserver observer)
    {
        colorObservers.Remove(observer);
    }

    /// <summary> Définit quel emplacement de meuble écoute actuellement les changements. </summary>
    public static void AddObserver(FurnitureObserver observer)
    {
        furnitureObserver = observer;
    }

    public static void RemoveObserver(FurnitureObserver observer)
    {
        // On vérifie si celui qu'on veut retirer est bien celui qui est enregistré
        if (furnitureObserver == observer)
        {
            furnitureObserver = null; // On vide la variable
        }
    }

    /// <summary> Enregistre le menu UI comme observateur de la sélection physique. </summary>
    public static void AddSelectionObserver(FurnitureSelectionObserver observer)
    {
        furnitureSelectionObserver = observer; // Écrase l'ancien observateur de sélection
    }

    #endregion

    #region Notifications (Diffusion des événements)

    /// <summary> Notifie les lampes que l'intensité doit changer. </summary>
    public static void NotifyLightIntensityChanged(float intensity)
    {
        foreach (LightObserver obs in lightObservers)
            obs.OnLightIntensityChanged(intensity);
    }

    /// <summary> Notifie les lampes qu'une nouvelle couleur/score est appliquée. </summary>
    public static void NotifyLightColorChanged(Color color, ScoreMetrics score)
    {
        foreach (LightObserver obs in lightObservers)
            obs.OnLightColorChanged(color, score);
    }

    /// <summary> Notifie les murs et sols qu'une nouvelle palette est choisie. </summary>
    public static void NotifyPaletteChanged(Color wallColor, Color floorColor, ScoreMetrics score)
{
    foreach (ColorObserver observer in colorObservers)
    {
        observer.OnPaletteChanged(wallColor, floorColor, score);
    }
}

    /// <summary> Notifie l'emplacement de meuble actif qu'il doit changer son modèle 3D. </summary>
    public static void NotifyFurnitureChanged(GameObject newPrefab)
    {
        // On fait une copie pour éviter la modification pendant l’itération
        if (furnitureObserver != null)
            furnitureObserver.OnFurnitureChanged(newPrefab);
    }

    /// <summary> Notifie le système d'UI qu'un meuble a été cliqué dans la scène 3D. </summary>
    public static void NotifyFurnitureSelected(FurnitureSelector selector)
    {
        // Notifie le menu UI qu'un nouveau meuble a été cliqué
        if (furnitureSelectionObserver != null)
            furnitureSelectionObserver.OnFurnitureSelected(selector);
    }

    // Action qui envoie quel objet a été sélectionné
    public static event Action<GameObject> OnObjectSelected;

    // Action simple pour tout désélectionner
    public static event Action OnDeselectAll;

    public static void NotifyObjectSelected(GameObject selectedObj)
    {
        if (OnObjectSelected != null) // Est-ce qu'il y a au moins une personne qui écoute ?
        {
            OnObjectSelected(selectedObj);
        }
        // = OnObjectSelected?.Invoke(selectedObj); (meme chose que ce que j ai ecrit)
    }

    public static void NotifyDeselectAll()
    {
        if (OnDeselectAll != null)
        {
            OnDeselectAll();
        }
    }

    // Unity a besoin d une fonction non static pour l'utiliser dans l'Inspecteur
    public void DeselectAllFromUI()
    {
        NotifyDeselectAll();
    }
    #endregion
}
