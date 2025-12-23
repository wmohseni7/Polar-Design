using UnityEditor;
using UnityEngine;

/// <summary>
/// Outil d'édition pour automatiser la création de la hiérarchie de meubles.
/// Permet de transformer un objet sélectionné en un système compatible avec le FurnitureManager.
/// </summary>
public class WrapFurnitureEditor
{
    /// <summary>
    /// Crée une structure composée d'un Wrapper (parent) et d'un SpawnPoint pour l'objet sélectionné.
    /// Ajoute automatiquement les composants FurnitureManager, FurnitureSelector et un Collider.
    /// accessible via le menu "Tools/Wrap Selected As Furniture".
    /// </summary>
    [MenuItem("Tools/Wrap Selected As Furniture")]
    public static void WrapSelected()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("Aucun objet sélectionné !");
            return;
        }

        GameObject selected = Selection.activeGameObject;

        // Étape 1 : ENREGISTRER les transformations actuelles du meuble (en coordonnées Mondiales)
        Vector3 worldPosition = selected.transform.position;
        Quaternion worldRotation = selected.transform.rotation;
        Vector3 worldScale = selected.transform.lossyScale; // Échelle globale réelle

        // --- Création de la Hiérarchie ---

        // Étape 2 : Créer un nouveau parent vide (le Wrapper)
        GameObject wrapper = new GameObject(selected.name + "_Furniture");
        // Le wrapper prend la position/rotation/échelle du meuble pour être logique.
        wrapper.transform.position = worldPosition; 

        // On ajoute le FurnitureManager dessus
        FurnitureManager fm = wrapper.AddComponent<FurnitureManager>();

        // Étape 3 : Créer le spawn point vide (le Conteneur)
        GameObject spawn = new GameObject("SpawnPoint");
        spawn.transform.SetParent(wrapper.transform);
        
        // --- Transfert des Transformations ---

        // Étape 4 : Appliquer les transformations enregistrées au SpawnPoint.
        // C'est le SpawnPoint qui doit hériter de la position/rotation/échelle de l'ancien meuble.
        spawn.transform.position = worldPosition;
        spawn.transform.rotation = worldRotation;
        
        // On définit l'échelle locale du spawnPoint sur l'échelle mondiale enregistrée.
        spawn.transform.localScale = worldScale; 
        
        fm.spawnPoint = spawn.transform;

        // --- Finalisation du Parentage ---

        // Étape 5 : Le meuble sélectionné devient enfant du SpawnPoint
        selected.transform.SetParent(spawn.transform);

        // Étape 6 : RÉINITIALISER les transformations locales du meuble à l'identité.
        // Le meuble est maintenant positionné par son parent (SpawnPoint).
        selected.transform.localPosition = Vector3.zero;
        selected.transform.localRotation = Quaternion.identity;
        selected.transform.localScale = Vector3.one;

        // --- Étape 7 : Ajouter le FurnitureSelector et un Collider ---

        // 1. Ajouter le script FurnitureSelector s'il n'existe pas déjà
        if (selected.GetComponent<FurnitureSelector>() == null)
        {
            selected.AddComponent<FurnitureSelector>();
        }

        // 2. Ajouter un Collider s'il n'existe pas déjà
        if (selected.GetComponent<BoxCollider>() == null)
        {
            selected.AddComponent<BoxCollider>();
            Debug.Log("BoxCollider ajouté automatiquement à " + selected.name + " pour permettre le clic.");
        }
        // --- Étape 8 : Ajouter et Configurer l'Outline ---
        
        Outline outline = selected.GetComponent<Outline>();
        if (outline == null)
            outline = selected.AddComponent<Outline>();

        // Configuration de la couleur Hexadécimale #2EFDC3
        Color myHexColor;
        if (ColorUtility.TryParseHtmlString("#2EFDC3", out myHexColor))
        {
            outline.OutlineColor = myHexColor;
        }

        outline.OutlineWidth = 6f;                  // Largeur de 6
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.enabled = false;                    // Désactivé par défaut (s'allume au survol)

        // --- Étape 9 (NOUVEAU) : Ajouter SmartOutline ---

        if (selected.GetComponent<SmartOutline>() == null)
        {
            selected.AddComponent<SmartOutline>();
        }

        // --- Fin ---

        Debug.Log("Furniture créé : " + wrapper.name);
    }
}