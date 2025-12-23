using UnityEngine;

/// <summary>
/// Objet de données (ScriptableObject) servant de référence globale pour la pièce actuellement active.
/// Permet de partager l'instance du RoomManager entre plusieurs systèmes sans couplage direct.
/// </summary>
[CreateAssetMenu(menuName = "Game/Current Room Ref")]
public class CurrentRoomRef : ScriptableObject
{
    /// <summary> 
    /// Référence à l'instance active de RoomManager dans la scène actuelle.
    /// L'attribut NonSerialized garantit que la référence est réinitialisée à chaque lancement du jeu.
    /// </summary>
    [System.NonSerialized] 
    public RoomManager activeRoom;

    /// <summary>
    /// Met à jour la référence de la pièce active.
    /// </summary>
    /// <param name="room">L'instance du RoomManager à mémoriser.</param>
    public void SetActiveRoom(RoomManager room)
    {
        activeRoom = room;
    }
}