using System.Collections.Generic;

namespace SelfieAWookie.Core.Domain
{
    public class Wookie
    {
        #region Properties

        public int Id { get; set; }

        public List<Selfie> Selfies { get; set; }

        #endregion
    }
}