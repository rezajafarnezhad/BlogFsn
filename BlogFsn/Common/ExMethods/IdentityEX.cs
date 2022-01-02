using System;
using System.Linq;
using System.Security.Claims;
using BlogFsn.Models;

namespace BlogFsn.Common.ExMethods
{
    public static class IdentityEX
    {
        public static GetUserDetailsViewModel GetUserDetails(this ClaimsPrincipal user)
        {

            var userDetails = new GetUserDetailsViewModel();

            userDetails.UserId = user.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault() ?? "";
            userDetails.UserName = user.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault() ?? "";
            userDetails.Email = user.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() ?? "";
            userDetails.fullName = user.Claims.Where(c => c.Type == ClaimTypes.GivenName).Select(c => c.Value).SingleOrDefault() ?? "";
            userDetails.AccessLevel = user.Claims.Where(c => c.Type == "AccessLevel").Select(c => c.Value).SingleOrDefault() ?? "";
            userDetails.Data = DateTime.Parse(user.Claims.Where(c => c.Type == "Date").Select(c => c.Value).SingleOrDefault() ?? DateTime.MinValue.ToString());

            return userDetails;
        }

    }
}