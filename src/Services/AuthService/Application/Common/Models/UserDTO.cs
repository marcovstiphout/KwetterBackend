using Domain;
using System;

namespace Kwetter.Services.AuthService.Application.Common.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public static implicit operator UserDTO(User v)
        {
            UserDTO user = new UserDTO()
            {
                Id = v.Id,
                Name = v.Name,
                Avatar = v.Avatar,
                Email = v.Email
            };
            return user;
        }
    }
}
