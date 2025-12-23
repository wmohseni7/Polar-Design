using UnityEngine;

namespace Observer
{
    /// <summary>
    /// Interface définissant le contrat pour les objets qui réagissent aux changements de lumière.
    /// Elle permet de synchroniser l'ambiance visuelle et les métriques de score associées.
    /// </summary>
    public interface LightObserver
    {
        /// <summary>
        /// Appelé lorsque le curseur d'intensité lumineuse est manipulé.
        /// </summary>
        /// <param name="intensity">La nouvelle valeur d'intensité à appliquer aux sources lumineuses.</param>
        void OnLightIntensityChanged(float intensity);
        
        /// <summary>
        /// Appelé lorsqu'une nouvelle couleur de lumière est sélectionnée.
        /// </summary>
        /// <param name="color">La couleur (RGB) choisie.</param>
        /// <param name="score">Les métriques de score associées à cette nuance spécifique.</param>
        void OnLightColorChanged(Color color, ScoreMetrics score);
    }
}