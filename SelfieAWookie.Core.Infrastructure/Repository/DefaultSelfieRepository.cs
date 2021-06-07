using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Domain;
using SelfieAWookie.core.Framework;
using SelfieAWookie.Core.Infrastructure.Data;

namespace SelfieAWookie.Core.Infrastructure.Repository
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        #region Fields

        private readonly SelfieContext _context;

        #endregion

        #region Constructors

        public DefaultSelfieRepository(SelfieContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork => _context;

        #endregion

        #region Public methods

        public ICollection<Selfie> GetAll(int wookieId)
        {
            var query = _context
                .Selfies
                .Include(item => item.Wookie)
                .AsQueryable();

            if (wookieId > 0) query = query.Where(item => item.Wookie.Id == wookieId);
            return query.ToList();
        }

        public Selfie AddOne(Selfie selfie)
        {
            return _context.Selfies.Add(selfie).Entity;
        }

        public Photo AddOnePhoto(string url)
        {
            return _context.Photos.Add(new Photo
            {
                Url = url
            }).Entity;
        }

        #endregion
    }
}