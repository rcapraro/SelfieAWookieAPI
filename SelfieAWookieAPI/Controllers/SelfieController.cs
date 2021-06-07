using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Domain;
using SelfieAWookieAPI.Application.Dto;

namespace SelfieAWookieAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfieController : ControllerBase
    {
        #region Constructors

        public SelfieController(ISelfieRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        #endregion

        #region fields

        private readonly ISelfieRepository _repository;
        private readonly IWebHostEnvironment _environment;

        #endregion

        #region Public methods

        [HttpGet]
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            var model = _repository
                .GetAll(wookieId)
                .Select(item => new
                    SelfieResumeDto
                    {
                        Title = item.Title,
                        WookieId = item.Wookie.Id,
                        NbSelfies = (item.Wookie.Selfies?.Count).GetValueOrDefault(0)
                    }
                ).ToList();

            return Ok(model);
        }


        [HttpPost]
        public IActionResult AddOne(SelfieDto selfieDto)
        {
            IActionResult result = BadRequest();

            var addedSelfie = _repository.AddOne(new Selfie
            {
                ImagePath = selfieDto.ImagePath,
                Title = selfieDto.Title
            });
            _repository.UnitOfWork.SaveChanges();

            if (addedSelfie == null) return result;
            selfieDto.Id = addedSelfie.Id;
            result = Ok(selfieDto);

            return result;
        }

        [Route("photos")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile photo)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, @"images\selfies");
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, photo.FileName);

            await using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await photo.CopyToAsync(stream);

            var addedPhoto = _repository.AddOnePhoto(filePath);
            _repository.UnitOfWork.SaveChanges();

            return Ok(addedPhoto);
        }

        #endregion
    }
}