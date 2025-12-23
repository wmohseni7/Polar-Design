using UnityEngine;

namespace Observer
{
    /// <summary>
    /// Interface définissant le contrat pour les objets souhaitant observer les changements de couleurs.
    /// Elle permet de notifier les abonnés lorsqu'une nouvelle palette (murs, sol, score) est appliquée.
    /// </summary>
    public interface ColorObserver
    {
        /// <summary>
        /// Méthode appelée automatiquement par le sujet (Subject) lorsqu'une modification de palette survient.
        /// </summary>
        /// <param name="wallColor">La nouvelle couleur à appliquer aux murs.</param>
        /// <param name="floorColor">La nouvelle couleur à appliquer au sol.</param>
        /// <param name="score">Les métriques de score (Design, Gout, Usure) associées à cette palette.</param>
        void OnPaletteChanged(Color wallColor, Color floorColor, ScoreMetrics score);
    }
}