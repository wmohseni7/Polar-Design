namespace StartMenu.SettingsMenu
{
    /// <summary>
    /// Interface de base pour le patron de conception 'Command'.
    /// Elle permet d'encapsuler une action de paramétrage (ex: changer la langue, 
    /// modifier la résolution) dans un objet unique.
    /// </summary>
    public interface ISettingsCommand
    {
        /// <summary>
        /// Exécute l'action associée à la commande de paramétrage.
        /// </summary>
        void Execute();
    }
}