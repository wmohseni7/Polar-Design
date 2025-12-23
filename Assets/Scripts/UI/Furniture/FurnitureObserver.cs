using UnityEngine;

namespace Observer
{
    /// <summary>
    /// Interface définissant le contrat pour les objets qui observent le changement de mobilier.
    /// Tout script implémentant cette interface pourra être notifié lorsqu'un nouveau 
    /// modèle 3D (prefab) doit être installé.
    /// </summary>
    public interface FurnitureObserver
    {
        /// <summary>
        /// Méthode appelée lorsqu'un utilisateur sélectionne un nouveau meuble dans le menu.
        /// </summary>
        /// <param name="newPrefab">Le nouveau modèle 3D à instancier dans la scène.</param>
        void OnFurnitureChanged(GameObject newPrefab);
    }
}