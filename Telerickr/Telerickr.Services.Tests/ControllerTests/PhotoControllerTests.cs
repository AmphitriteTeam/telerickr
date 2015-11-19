namespace Telerickr.Services.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http.Results;

    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Photos;
    using Telerickr.Models;
    using Mocks;
    using TestObjects;

    /// <summary>
    /// Summary description for PhotoControllerTests
    /// </summary>
    [TestClass]
    public class PhotoControllerTests
    {
        private InMemoryRepository<Album> albums;
        private InMemoryRepository<Photo> photos;

        [TestInitialize]
        public void Init()
        {
            this.albums = ObjectsFactory.GetAlbumRepository();
            this.photos = ObjectsFactory.GetPhotoRepository();
        }

        [TestMethod]
        public void GetShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Get();

            var expected = TestConstants.DefaultPageSize;
            var actual = result as OkNegotiatedContentResult<List<PhotoResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);
        }


        [TestMethod]
        public void GetWithIdShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Get(0);

            var actual = result as OkNegotiatedContentResult<PhotoResponseModel>;
            var expectedTitle = TestConstants.ValidTitle;


            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedTitle, actual.Content.Title);
        }

        [TestMethod]
        public void GetWithPagingShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Get(2, TestConstants.DefaultPageSize);
            var expected = TestConstants.DefaultNumberOfModels - TestConstants.DefaultPageSize;
            var actual = result as OkNegotiatedContentResult<List<PhotoResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);

            result = controller.Get(0, TestConstants.DefaultPageSize);
            expected = TestConstants.DefaultPageSize;
            actual = result as OkNegotiatedContentResult<List<PhotoResponseModel>>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content.Count);
        }

        [TestMethod]
        public void PutWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Put(0, true);
            var expectedContent = "Sucessfully liked picture!";
            var expectedLikes = 1;
            var actual = result as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedLikes, this.photos.GetById(0).Likes);

            result = controller.Put(0, false);
            expectedContent = "Sucessfully disliked picture!";
            expectedLikes = 0;
            actual = result as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedLikes, this.photos.GetById(0).Likes);
        }

        [TestMethod]
        public void PutWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Put(TestConstants.DefaultNonExistingModelId, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var photo = new PhotoRequestModel
            {
                Title = TestConstants.ValidTitle,
                ImageUrl = TestConstants.ValidUrl,
                FileExtension = TestConstants.ValidFileExtension,
                AlbumId = 0

            };
            var result = controller.Post(photo);
            var expectedNumberOfPhotos = 16;
            var expectedSaveChanges = 1;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<int>));
            Assert.AreEqual(expectedNumberOfPhotos, this.photos.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithInvalidModelStateShouldReturnInvalidModelStateResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);
            controller.ModelState.AddModelError("url", "Invalid url!");

            var photo = new PhotoRequestModel();
            var result = controller.Post(photo);
            var expectedSaveChanges = 0;
            var expectedNumberOfPhotos = TestConstants.DefaultNumberOfModels;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            Assert.AreEqual(expectedNumberOfPhotos, this.photos.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithInvalidAlbumIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var photo = new PhotoRequestModel
            {
                Title = TestConstants.ValidTitle,
                ImageUrl = TestConstants.ValidUrl,
                FileExtension = TestConstants.ValidFileExtension,
                AlbumId = TestConstants.DefaultNonExistingModelId
            };
            var result = controller.Post(photo);
            var expectedSaveChanges = 0;
            var expectedNumberOfPhotos = TestConstants.DefaultNumberOfModels;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedNumberOfPhotos, this.photos.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteWithValidIdShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Delete(0);
            var actual = result as OkNegotiatedContentResult<string>;
            var expectedNumberOfPhotos = TestConstants.DefaultNumberOfModels - 1;
            var expectedSaveChanges = 1;
            var expectedMessage = "Photo deleted";

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMessage, actual.Content);
            Assert.AreEqual(expectedNumberOfPhotos, this.photos.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetPhotosController(this.photos, this.albums);

            var result = controller.Delete(TestConstants.DefaultNonExistingModelId);
            var expectedNumberOfPhotos = TestConstants.DefaultNumberOfModels;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedNumberOfPhotos, this.photos.NumberOfModels);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }
    }
}
