using Domain;
using System;

namespace Kwetter.Services.AuthService.Application.Common.Models
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }

        public static implicit operator UserDTO(User v)
        {
            UserDTO user = new UserDTO()
            {
                Name = v.Name,
                Avatar = v.Avatar,
                Email = v.Email
            };
            return user;
        }
    }
}
