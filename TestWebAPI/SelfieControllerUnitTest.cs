using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.Core.Domain;
using SelfieAWookie.core.Framework;
using SelfieAWookieAPI.Application.Dto;
using SelfieAWookieAPI.Controllers;
using Xunit;

namespace TestWebAPI
{
    public class SelfieControllerUnitTest
    {
        #region Public methods

        [Fact]
        public void ShouldReturnListOfSelfies()
        {
            //ARRANGE
            var repositoryMock = new Mock<ISelfieRepository>();
            var configurationMock = new Mock<IWebHostEnvironment>();
            var controller = new SelfieController(repositoryMock.Object, configurationMock.Object);
            var expectedList = new List<Selfie>
            {
                new() {Wookie = new Wookie()},
                new() {Wookie = new Wookie()}
            };
            repositoryMock
                .Setup(item => item.GetAll(It.IsAny<int>()))
                .Returns(expectedList);

            //ACT
            var result = controller.GetAll();

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<List<SelfieResumeDto>>(okResult?.Value);

            var list = okResult.Value as List<SelfieResumeDto>;
            Assert.True(list?.Count == expectedList.Count);
        }

        [Fact]
        public void ShouldAddOneSelfie()
        {
            //ARRANGE
            var selfieDto = new SelfieDto();
            var repositoryMock = new Mock<ISelfieRepository>();
            var configurationMock = new Mock<IWebHostEnvironment>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repositoryMock
                .Setup(item => item.UnitOfWork)
                .Returns(unitOfWork.Object);
            repositoryMock
                .Setup(item => item.AddOne(It.IsAny<Selfie>()))
                .Returns(new Selfie {Id = 42});

            //ACT
            var controller = new SelfieController(repositoryMock.Object, configurationMock.Object);
            var result = controller.AddOne(selfieDto);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var addedSelfie = okResult.Value as SelfieDto;
            Assert.NotNull(addedSelfie);
            Assert.True(addedSelfie.Id > 0);
        }

        #endregion
    }
}