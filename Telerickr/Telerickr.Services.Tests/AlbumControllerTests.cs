namespace Telerickr.Services.Tests
{
    using System.Collections.Generic;
    using System.Web.Http.Results;

    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models.Albums;
    using Telerickr.Models;
    using TestObjects;

    /// <summary>
    /// Album Controller CRUD Tests
    /// </summary>
    [TestClass]
    public class AlbumControllerTests
    {
        private InMemoryRepository<Album> albums;
        private InMemoryRepository<User> users;
        private InMemoryRepository<Photo> photos;

        [TestInitialize]
        public void Init()
        {
            this.albums = ObjectsFactory.GetAlbumRepository();
            this.users = ObjectsFactory.GetUserRepository();
            this.photos = ObjectsFactory.GetEmptyPhotoRepository();
        }

        [TestMethod]
        public void GetShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var result = controller.Get();

            var expected = TestConstants.DefaultNumberOfModels;
            var actual = result as OkNegotiatedContentResult<List<AlbumResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);
        }

        [TestMethod]
        public void GetWithProvidedValidIdShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var result = controller.Get(1);

            var expectedCount = 1;
            var actual = result as OkNegotiatedContentResult<List<AlbumResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCount, actual.Content.Count);
        }

        [TestMethod]
        public void GetWithProvidedInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var result = controller.Get(TestConstants.DefaultNonExistingModelId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutWithValidDataProvidedShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var correctedTitleAlbum = new AlbumRequestModel() { Title = "CoolTitle" };
            var result = controller.Put(0, correctedTitleAlbum);

            var actual = result as OkNegotiatedContentResult<string>;
            var expectedContent = "Album updated";
            var expectedUpdates = 1;
            var expectedSaveChangesCalls = 1;

            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedUpdates, this.albums.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChangesCalls, this.albums.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidModelStateShouldReturnInvalidModelStateResultWithContent()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);
            controller.ModelState.AddModelError("title", "Title cannot be null.");

            var emptyAlbum = new AlbumRequestModel();
            var result = controller.Put(0, emptyAlbum);

            var expectedUpdates = 0;
            var expectedSaveChangesCalls = 0;

            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            Assert.AreEqual(expectedUpdates, this.albums.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChangesCalls, this.albums.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidUserShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos, true, false);

            var correctedTitleAlbum = new AlbumRequestModel() { Title = TestConstants.ValidTitle };
            var result = controller.Put(1, correctedTitleAlbum);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void PutWithBadAlbumIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var correctedTitleAlbum = new AlbumRequestModel() { Title = TestConstants.ValidTitle };
            var result = controller.Put(TestConstants.DefaultNonExistingModelId, correctedTitleAlbum);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var albumToBeAdded = new AlbumRequestModel() { Title = TestConstants.ValidTitle };
            var result = controller.Post(albumToBeAdded);

            var expectedSaveChanges = 1;
            var expectedAlbumsAfterChange = TestConstants.DefaultNumberOfModels + 1;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<int>));
            Assert.AreEqual(expectedAlbumsAfterChange, this.albums.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.albums.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithInvalidModelShouldReturnBadRequestResultWithContent()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);
            controller.ModelState.AddModelError("title", "Title cannot be null.");

            var emptyAlbum = new AlbumRequestModel();
            var result = controller.Post(emptyAlbum);

            var expectedUpdates = 0;
            var expectedSaveChangesCalls = 0;

            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            Assert.AreEqual(expectedUpdates, this.albums.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChangesCalls, this.albums.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithFakeAccountShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(
                this.albums, this.users, this.photos, true, false);

            var albumToBeAdded = new AlbumRequestModel() { Title = TestConstants.ValidTitle };
            var result = controller.Post(albumToBeAdded);

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void DeleteWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var result = controller.Delete(0);

            var actual = result as OkNegotiatedContentResult<string>;
            var expectedSaveChanges = 1;
            var expectedCount = TestConstants.DefaultNumberOfModels - 1;
            var expectedContent = "Album deleted with all pictures inside.";

            // TODO: Add Assert to check if all photos are removed.
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedSaveChanges, this.albums.NumberOfSaves);
            Assert.AreEqual(expectedCount, this.albums.NumberOfModels);
            Assert.AreEqual(expectedContent, actual.Content);
        }

        [TestMethod]
        public void DeleteWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(this.albums, this.users, this.photos);

            var result = controller.Delete(TestConstants.DefaultNonExistingModelId);

            var expectedSaveChanges = 0;
            var expectedCount = TestConstants.DefaultNumberOfModels;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedSaveChanges, this.albums.NumberOfSaves);
            Assert.AreEqual(expectedCount, this.albums.NumberOfModels);
        }

        [TestMethod]
        public void DeleteWithWrongUserShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMockFactory.GetAlbumsController(
                this.albums, this.users, this.photos, true, false);

            var result = controller.Delete(0);

            var expectedSaveChanges = 0;
            var expectedCount = TestConstants.DefaultNumberOfModels;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
            Assert.AreEqual(expectedSaveChanges, this.albums.NumberOfSaves);
            Assert.AreEqual(expectedCount, this.albums.NumberOfModels);
        }
    }
}
