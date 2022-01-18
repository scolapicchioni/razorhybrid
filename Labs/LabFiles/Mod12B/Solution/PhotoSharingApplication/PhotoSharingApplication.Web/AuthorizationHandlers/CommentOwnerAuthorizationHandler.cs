using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Web.AuthorizationHandlers;

public class CommentOwnerAuthorizationHandler : AuthorizationHandler<CommentOwnerRequirement, Comment> {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   CommentOwnerRequirement requirement,
                                                   Comment comment) {
        if (context.User.Identity?.Name == comment.SubmittedBy) {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class CommentOwnerRequirement : IAuthorizationRequirement { }
