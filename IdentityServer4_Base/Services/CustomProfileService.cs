using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4_Base.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4_Base.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly ICustomUserRepository _customUserRepository;
        public CustomProfileService(ICustomUserRepository customUserRepository)
        {
            _customUserRepository = customUserRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _customUserRepository.FindById(int.Parse(userId));

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("name",user.UserName),
                new Claim("city",user.City),
            };

            if (user.Id == 1)
            {
                claims.Add(new Claim("role", "admin"));
            }
            else
            {
                claims.Add(new Claim("role", "user"));
            }

            context.AddRequestedClaims(claims);
            //context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await _customUserRepository.FindById(int.Parse(userId));
            context.IsActive = user != null ? true : false;
        }
    }
}
