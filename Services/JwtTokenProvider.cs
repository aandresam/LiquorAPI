using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using liquorApi.Context.Entities;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace liquorApi.Services;

    public class JwtTokenProvider
    {
        private IConfiguration config;

        public JwtTokenProvider(IConfiguration config){
            this.config = config;
        }

        public String GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
