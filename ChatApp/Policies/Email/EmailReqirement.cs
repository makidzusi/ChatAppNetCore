using Microsoft.AspNetCore.Authorization;

namespace ChatApp.Policies.Email
{
    public class EmailReqirement : IAuthorizationRequirement
    {
        protected internal string Email { get; set; }
        public EmailReqirement(string email) => Email = email;
    }
}
