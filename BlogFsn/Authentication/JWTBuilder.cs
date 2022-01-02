using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Infrastructure;
using Fsn.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlogFsn.Authentication
{
    public class JWTBuilder : IJWTBuilder
    {
        private readonly IUserApplication _userApplication;
        private readonly IRoleApplication _roleApplication;
        public JWTBuilder(IUserApplication userApplication, IRoleApplication roleApplication)
        {
            _userApplication = userApplication;
            _roleApplication = roleApplication;
        }

        public async Task<string> CreateTokenAsync(string userid)
        {
            var _userDetails = await _userApplication.GetAllUserDetailsAsync(userid);

            if (_userDetails == null)
                throw new Exception();

            var _user = await _userApplication.GetUserById(userid);
            var _userRoles = await _roleApplication.GetRolesByUserAsync(_user);

            if (_userRoles == null)
                throw new Exception();

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,_userDetails.Id), //userid
                new Claim(ClaimTypes.Name,_userDetails.UserName),
                new Claim(ClaimTypes.Email,_userDetails.Email),
                new Claim(ClaimTypes.GivenName,_userDetails.FullName??""),
                new Claim("AccessLevel",_userDetails.AccessLevelTile),
                new Claim("Date",_userDetails.Data.ToString("yyyy-MM-dd HH:mm")),
            };

            Claims.AddRange(_userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));


            var _Key = Encoding.ASCII.GetBytes(AuthConst.SecretCode);
            var TokenDescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_Key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = AuthConst.Issuer,
                Audience = AuthConst.Audience,
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddHours(50)
            };

            var _SecurityToken = new JwtSecurityTokenHandler().CreateToken(TokenDescreptor);
            string _GeneratedToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(_SecurityToken);

            return _GeneratedToken.AesEncrypt(AuthConst.SecretKey);
        }

    }
}