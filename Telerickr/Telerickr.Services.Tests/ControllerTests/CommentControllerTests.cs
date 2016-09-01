namespace Telerickr.Services.Tests.ControllerTests
{
    using System.Collections.Generic;


    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Data;
    using Telerickr.Models;
    using Mocks;
    using TestObjects;
    using Common;
    using System.Web.Http.Results;
    using Models.Comments;

    /// <summary>
    /// Summary description for CommentControllerTests
    /// </summary>
    [TestClass]
    public class CommentControllerTests
    {
        private InMemoryRepository<Photo> photos;
        private InMemoryRepository<Comment> comments;

        [TestInitialize]
        public void Init()
        {
            this.photos = ObjectsFactory.GetPhotoRepository();
            this.comments = ObjectsFactory.GetCommentRepository();
        }

        [TestMethod]
        public void GetShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var result = controller.Get();
            var actual = result as OkNegotiatedContentResult<List<CommentResponseModel>>;
            var expectedNumberOfObjects = TestConstants.DefaultPageSize;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedNumberOfObjects, actual.Content.Count);
        }

        [TestMethod]
        public void GetWithValidIdShouldReturnOkResultWithData()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var id = 0;
            var result = controller.Get(id);
            var actual = result as OkNegotiatedContentResult<CommentResponseModel>;

            Assert.IsNotNull(result);
            Assert.AreEqual(id, actual.Content.Id);
        }

        [TestMethod]
        public void GetWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var result = controller.Get(TestConstants.DefaultNonExistingModelId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var updatedComment = new CommentRequestModel()
            {
                Content = "Edited Content"
            };
            var result = controller.Put(0, updatedComment);
            var actual = result as OkNegotiatedContentResult<string>;
            var expectedContent = "Comment updated";
            var expectedUpdatedCount = 1;
            var expectedSaveChanges = 1;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedUpdatedCount, this.comments.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidModelShouldReturnInvalidModelStateResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);
            controller.ModelState.AddModelError("content", "Invalid content!");

            var updatedComment = new CommentRequestModel();
            var result = controller.Put(0, updatedComment);
            var expectedUpdatedCount =0;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            Assert.AreEqual(expectedUpdatedCount, this.comments.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var updatedComment = new CommentRequestModel()
            {
                Content = "Edited Content"
            };
            var result = controller.Put(TestConstants.DefaultNonExistingModelId, updatedComment);
            var expectedUpdatedCount = 0;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedUpdatedCount, this.comments.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void PutWithInvalidUserShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos, false);

            var updatedComment = new CommentRequestModel()
            {
                Content = "Edited Content"
            };
            var result = controller.Put(TestConstants.DefaultNonExistingModelId, updatedComment);
            var expectedUpdatedCount = 0;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedUpdatedCount, this.comments.UpdatedEntities.Count);
            Assert.AreEqual(expectedSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithValidDataShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var commentToBeAdded = new CommentRequestModel()
            {
                Content = TestConstants.ValidContent,
                UserId = TestConstants.ValidUserId
            };
            var result = controller.Post(0, commentToBeAdded);
            var actual = result as OkNegotiatedContentResult<string>;
            var expectedContent = "Comment successfully added.";
            var expectedSaveChanges = 1;
            var expectedNumberOfCommentsAdded = 1;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedNumberOfCommentsAdded, this.photos.GetById(0).Comments.Count);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithInvalidModelShouldReturnInvalidModelStateResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);
            controller.ModelState.AddModelError("content", "Invalid content!");

            var commentToBeAdded = new CommentRequestModel();
            var result = controller.Post(0, commentToBeAdded);
            var expectedNumberOfCommentsAdded = 0;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
            Assert.AreEqual(expectedNumberOfCommentsAdded, this.photos.GetById(0).Comments.Count);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void PostWithInvalidPhotoIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var commentToBeAdded = new CommentRequestModel()
            {
                Content = TestConstants.ValidContent,
                UserId = TestConstants.ValidUserId
            };
            var result = controller.Post(TestConstants.DefaultNonExistingModelId, commentToBeAdded);
            var expectedNumberOfCommentsAdded = 0;
            var expectedSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedNumberOfCommentsAdded, this.photos.GetById(0).Comments.Count);
            Assert.AreEqual(expectedSaveChanges, this.photos.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteWithValidIdShouldReturnOkResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var result = controller.Delete(0);
            var actual = result as OkNegotiatedContentResult<string>;
            var expectedContent = "Comment deleted";
            var expectedNumberOfComments = TestConstants.DefaultNumberOfModels - 1;
            var expectedNumberOfSaveChanges = 1;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedContent, actual.Content);
            Assert.AreEqual(expectedNumberOfComments, this.comments.NumberOfModels);
            Assert.AreEqual(expectedNumberOfSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteWithInvalidIdShouldReturnNotFoundResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos);

            var result = controller.Delete(TestConstants.DefaultNonExistingModelId);
            var expectedNumberOfComments = TestConstants.DefaultNumberOfModels;
            var expectedNumberOfSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(expectedNumberOfComments, this.comments.NumberOfModels);
            Assert.AreEqual(expectedNumberOfSaveChanges, this.comments.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteWithInvalidUserShouldReturnUnauthorisedResult()
        {
            var controller = ControllerMockFactory.GetCommentsController(this.comments, this.photos, false);

            var result = controller.Delete(0);
            var expectedNumberOfComments = TestConstants.DefaultNumberOfModels;
            var expectedNumberOfSaveChanges = 0;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
            Assert.AreEqual(expectedNumberOfComments, this.comments.NumberOfModels);
            Assert.AreEqual(expectedNumberOfSaveChanges, this.comments.NumberOfSaves);
        }
    }
}
