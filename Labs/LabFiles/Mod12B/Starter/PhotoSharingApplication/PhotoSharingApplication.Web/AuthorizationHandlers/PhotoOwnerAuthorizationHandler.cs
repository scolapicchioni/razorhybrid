using Microsoft.AspNetCore.Authorization;
using PhotoSharingApplication.Shared.Entities;

namespace PhotoSharingApplication.Web.AuthorizationHandlers {
    public class PhotoOwnerAuthorizationHandler : AuthorizationHandler<PhotoOwnerRequirement, Photo> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       PhotoOwnerRequirement requirement,
                                                       Photo photo) {
            if (context.User.Identity?.Name == photo.SubmittedBy) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class PhotoOwnerRequirement : IAuthorizationRequirement { }
}
