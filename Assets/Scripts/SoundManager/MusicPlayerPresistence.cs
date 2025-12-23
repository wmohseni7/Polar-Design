using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Assure la persistance de l'objet musical à travers les chargements de scènes.
/// Empêche la duplication de la musique de fond lors du retour au menu principal.
/// </summary>
public class MusicPlayerPresistence : MonoBehaviour
{
    /// <summary> Instance statique permettant de vérifier l'existence du manager dans la session. </summary>
    private static MusicPlayerPresistence instance;

    /// <summary>
    /// Initialise le pattern Singleton au réveil de l'objet.
    /// Si une instance existe déjà, le nouvel objet est détruit pour éviter les doublons sonores.
    /// </summary>
    void Awake()
    {
        // 1. Vérifie si une instance de ce script existe déjà
        if (instance != null && instance != this)
        {
            // 2. Si une instance existe (et que ce n'est pas celle-ci), on détruit le doublon.
            Destroy(this.gameObject);
            return;
        }

        // 3. C'est la première et unique instance : on l'assigne et on la rend persistante.
        instance = this;
        
        // DontDestroyOnLoad permet à l'objet de survivre au chargement de nouvelles scènes.
        DontDestroyOnLoad(this.gameObject);
    }
}