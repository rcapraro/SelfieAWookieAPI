namespace SelfieAWookie.Core.Domain
{
    /// <summary>
    ///     Représente un Selfie avec un wookie lié
    /// </summary>
    public class Selfie
    {
        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }


        public string Description { get; set; }

        public string ImagePath { get; set; }

        public Wookie Wookie { get; set; }

        public Photo Photo { get; set; }

        #endregion
    }
}