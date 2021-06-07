using System.Collections.Generic;

namespace SelfieAWookie.Core.Domain
{
    public class Photo
    {
        #region Properties

        public int Id { get; set; }

        public string Url { get; set; }

        public List<Selfie> Selfies { get; set; }

        #endregion
    }
}