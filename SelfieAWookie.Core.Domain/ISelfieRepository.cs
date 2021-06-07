using System.Collections.Generic;
using SelfieAWookie.core.Framework;

namespace SelfieAWookie.Core.Domain
{
    /// <summary>
    ///     Repository to manage Selfies
    /// </summary>
    public interface ISelfieRepository : IRepository
    {
        /// <summary>
        ///     Get all Selfies
        /// </summary>
        /// <param name="wookieId"></param>
        /// <returns></returns>
        ICollection<Selfie> GetAll(int wookieId);


        /// <summary>
        ///     Add one Selfie in database
        /// </summary>
        /// <param name="selfie"></param>
        /// <returns></returns>
        Selfie AddOne(Selfie selfie);

        /// <summary>
        ///     Add One Photo
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Photo AddOnePhoto(string url);
    }
}