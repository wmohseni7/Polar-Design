using UnityEngine;

/// <summary>
/// Contrôleur de déplacement du joueur.
/// Permet de déplacer le personnage en fonction de l'orientation de la caméra (Pivot)
/// et de faire pivoter la vue à la souris.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Paramètres de Déplacement")]
    /// <summary> Vitesse de translation du joueur. </summary>
    public float moveSpeed = 5f;

    [Header("Paramètres de Caméra")]
    /// <summary> Référence vers le transform servant de pivot à la caméra. </summary>
    public Transform cameraPivot;
    /// <summary> Sensibilité de la rotation horizontale à la souris. </summary>
    public float mouseSensitivity = 2f;

    /// <summary> Accumulateur pour la rotation sur l'axe Y (Haut/Bas). </summary>
    private float rotationY;

    void Update()
    {
        // --- ROTATION (REGARD) ---
        // On récupère le mouvement horizontal de la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += mouseX;

        // On applique la rotation au pivot de la caméra
        // Cela permet d'orienter la direction du mouvement futur
        cameraPivot.rotation = Quaternion.Euler(0, rotationY, 0);

        // --- MOUVEMENT (TRANSLATION) ---
        // Récupération des entrées clavier (ZQSD / Flèches)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // Création d'un vecteur de direction normalisé pour éviter de courir plus vite en diagonale
        Vector3 direction = new Vector3(h, 0, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // On calcule la direction du mouvement par rapport à l'avant et la droite de la caméra
            Vector3 moveDir = cameraPivot.forward * v + cameraPivot.right * h;
            
            // On force l'axe Y à 0 pour éviter que le joueur ne s'envole en regardant vers le haut
            moveDir.y = 0;

            // Application du mouvement à la position du transform
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        }
    }
}