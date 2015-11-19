namespace Telerickr.Services.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestObjects;
    using Telerickr.Models;
    using Mocks;
    using System.Net.Http;
    using Models.Albums;
    using System.Net;
    using System.Web.Http.Results;
    using Common;

    /// <summary>
    /// Summary description for AlbumControllerTests
    /// </summary>
    [TestClass]
    public class AlbumControllerTests
    {
        private InMemoryRepository<Album> albums;
        private InMemoryRepository<User> users;
        private InMemoryRepository<Photo> photos;

        [TestInitialize()]
        public void Init()
        {
            this.albums = ObjectsFactory.GetAlbumRepository();
            this.users = ObjectsFactory.GetEmptyUserRepository();
            this.photos = ObjectsFactory.GetEmptyPhotoRepository();
        }

        [TestMethod]
        public void GetShouldReturnOkResultWithData()
        {
            var controller = ControllerMocks.GetAlbumsController(albums, users, photos, "test user");

            var result = controller.Get();

            var expected = TestConstants.DefaultNumberOfModels;
            var actual = result as OkNegotiatedContentResult<List<AlbumResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);
        }

        [TestMethod]
        public void GetWithProvidedIdShouldReturnOkResultWithData()
        {
            var controller = ControllerMocks.GetAlbumsController(albums, users, photos, "Test User");

            var result = controller.Get(1);

            var expected = 1;
            var actual = result as OkNegotiatedContentResult<List<AlbumResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);
        }

        [TestMethod]
        public void PutWithValidDataProvidedShouldReturnOkResult()
        {
            var controller = ControllerMocks.GetAlbumsController(albums, users, photos, ObjectsFactory.GetValidUser().UserName);

            var correctedTitleAlbum = new AlbumRequestModel() { Title = "CoolTitle" };
            var result = controller.Put(0, correctedTitleAlbum);

            var actual = result as OkNegotiatedContentResult<string>;
            var expectedContent = "Album updated";
            var expectedUpdates = 1;
            var expectedSaveChangesCalls = 1;

            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedUpdates, albums.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChangesCalls, albums.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidUserShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMocks.GetAlbumsController(albums, users, photos, ObjectsFactory.GetValidUser().UserName);

            var correctedTitleAlbum = new AlbumRequestModel() { Title = "CoolTitle" };
            var result = controller.Put(1, correctedTitleAlbum);
            
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        
    }
}
