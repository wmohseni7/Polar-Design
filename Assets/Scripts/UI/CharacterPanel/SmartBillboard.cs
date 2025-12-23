using UnityEngine;

public class SmartBillboard : MonoBehaviour
{
    [Header("Réglages de Position (Standard)")]
    // Position par défaut : Un peu à droite, Au-dessus de la tête
    public Vector3 defaultOffset = new Vector3(0.8f, 2.2f, 0f);

    [Header("Réglages de Repli (Si ça dépasse)")]
    // Si ça dépasse en haut, on le met où ? (Ex: à 1m de hauteur, niveau torse)
    public float loweredHeight = 1.0f; 

    [Header("Limites d'écran (Marges de sécurité 0-1)")]
    // Si le perso est dans les 15% à droite de l'écran -> On inverse X
    public float rightMargin = 0.85f; 
    // Si le perso est dans les 10% en haut de l'écran -> On baisse Y
    public float topMargin = 0.9f;

    private Canvas myCanvas;
    private Transform parentCharacter;

    void Start()
    {
        myCanvas = GetComponent<Canvas>();
        // On récupère le parent (le Character)
        if (transform.parent != null)
        {
            parentCharacter = transform.parent;
        }
    }

    void LateUpdate()
    {
        // 1. Trouver la caméra active (celle qui a le tag MainCamera ou n'importe laquelle)
        Camera cam = Camera.main;
        if (cam == null) cam = FindObjectOfType<Camera>();

        if (cam != null && parentCharacter != null)
        {
            // --- GESTION DE L'EVENT CAMERA (Pour les clics) ---
            if (myCanvas != null && myCanvas.worldCamera != cam)
            {
                myCanvas.worldCamera = cam;
            }

            // --- CALCUL DE LA POSITION INTELLIGENTE ---
            
            // On regarde où est le perso sur l'écran (entre 0 et 1)
            Vector3 viewportPos = cam.WorldToViewportPoint(parentCharacter.position);

            // On part de l'offset par défaut
            Vector3 finalOffset = defaultOffset;

            // A. EST-CE QU'ON DÉPASSE À DROITE ?
            // Si le perso est trop à droite (> 0.85), on met le panel à sa GAUCHE (-X)
            if (viewportPos.x > rightMargin) 
            {
                finalOffset.x = -Mathf.Abs(defaultOffset.x);
            }
            else 
            {
                finalOffset.x = Mathf.Abs(defaultOffset.x);
            }

            // B. EST-CE QU'ON DÉPASSE EN HAUT ?
            // Si la tête du perso touche le haut de l'écran (> 0.9), on FORCE le panel plus bas
            if (viewportPos.y > topMargin)
            {
                finalOffset.y = loweredHeight; // Hop, on le met au niveau du torse
            }
            else
            {
                finalOffset.y = defaultOffset.y; // Sinon, on le laisse au-dessus
            }

            // --- APPLICATION ---
            
            // On calcule la position finale dans le monde 3D
            // On utilise cam.transform.right pour que "Droite" veuille dire "Droite de l'écran"
            Vector3 targetPosition = parentCharacter.position 
                                     + cam.transform.right * finalOffset.x 
                                     + Vector3.up * finalOffset.y;

            transform.position = targetPosition;

            // --- ROTATION (Toujours face caméra) ---
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                             cam.transform.rotation * Vector3.up);
        }
    }
}