namespace Telerickr.Services.Tests
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
        private IRepository<Photo> photos;
        private IRepository<Comment> comments;

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
    }
}
