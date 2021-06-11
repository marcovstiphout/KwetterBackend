﻿using System;

namespace Kwetter.Services.AuthService.Application.Common.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
