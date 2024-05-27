using Microsoft.AspNetCore.Mvc;
using Microsoft.BugTracker.Dtos;
using Microsoft.BugTracker.Entities;
using Microsoft.BugTracker.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.BugTracker.Controllers
{
    [ApiController]
    [Route("api/projects/{projectId}/tickets/{ticketId}/comments")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IProjectService _projectService;

        public CommentController(ICommentService commentService, IProjectService projectService)
        {
            _commentService = commentService;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments(string ticketId){
            var ticketComments = await _commentService.GetAllCommentsAsync(ticketId);
            return Ok(ticketComments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(
            string projectId,
            string ticketId,
            [FromBody] CreateCommentDto commentInformation
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            var userIsPartOfProject2 = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, commentInformation.UserName);
            if (userIsPartOfProject && userIsPartOfProject2)
            {
                Comment comment = new(
                    ticketId,
                    commentInformation.UserName,
                    commentInformation.Content,
                    DateTime.Now.ToUniversalTime(),
                    DateTime.Now.ToUniversalTime()
                );
                await _commentService.AddCommentAsync(comment);
                return Ok("Comment Created");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }
        
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(
            string commentId
        )
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            return Ok(comment);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment(
            string projectId,
            [FromBody] CommentDto commentInformation
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                Comment comment = new(
                    commentInformation.TicketId,
                    commentInformation.UserName,
                    commentInformation.Content,
                    commentInformation.CreatedAt,
                    commentInformation.UpdatedAt
                )
                {
                    Id = commentInformation.Id
                };
                await _commentService.UpdateCommentAsync(comment);
                return Ok("Comment Updated");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(
            string commentId,
            string projectId
        )
        {
            var loggedInUsername = User.Identity.Name;
            var userIsPartOfProject = await _projectService.CheckIfUserIsPartOfProjectAsync(projectId, loggedInUsername);
            if (userIsPartOfProject)
            {
                await _commentService.DeleteCommentByIdAsync(commentId);
                return Ok("Comment deleted");
            }
            return Unauthorized("You are not allowed to access this resource.");
        }
    }
}
