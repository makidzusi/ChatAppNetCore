using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ChatApp.Policies.Email
{
    public class EmailHandler : AuthorizationHandler<EmailReqirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
    EmailReqirement requirement)
        {
            // получаем claim с типом ClaimTypes.DateOfBirth - год рождения
            var email = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (email is not null)
            {

                    // и разница между текущим годом и годом рождения больше требуемого возраста
                    if (email  == requirement.Email)
                    {
                        context.Succeed(requirement); // сигнализируем, что claim соответствует ограничению
                    }
              
            }
            return Task.CompletedTask;
        }
    }
}
