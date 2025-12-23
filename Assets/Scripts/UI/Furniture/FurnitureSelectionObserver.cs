/// <summary>
/// Interface définissant le contrat pour les objets observant la sélection d'un meuble.
/// Permet de faire le pont entre l'interaction 3D (clic sur un objet) 
/// et la réaction de l'interface utilisateur (ouverture du menu).
/// </summary>
public interface FurnitureSelectionObserver
{
    /// <summary>
    /// Méthode appelée lorsqu'un utilisateur clique sur un objet interactif 
    /// possédant un composant FurnitureSelector.
    /// </summary>
    /// <param name="selector">L'instance du sélecteur attaché à l'objet cliqué, 
    /// contenant les données des prefabs et des scores.</param>
    void OnFurnitureSelected(FurnitureSelector selector);
}