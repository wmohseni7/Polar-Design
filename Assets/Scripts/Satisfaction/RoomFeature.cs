// using UnityEngine;

// public class RoomFeature : MonoBehaviour
// {
//     [Header("Configuration des points")]
//     public float currentPoints; // Les points actuels (ex: 20 pour mur bleu)
//     public float maxPoints;     // Le maximum possible (ex: 50 pour le mur le plus beau)

//     private RoomManager roomManager;

//     void Start()
//     {
//         roomManager = GetComponentInParent<RoomManager>();
//     }

//     // Appelle cette fonction dès que tu changes la couleur ou l'intensité
//     public void NotifyChange(float newPoints)
//     {
//         currentPoints = newPoints;
//         if (roomManager != null)
//             roomManager.UpdateRoomGlobalSatisfaction();
//     }
// }