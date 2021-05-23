using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string LoginProviderId { get; set; }
        public string Email { get; set; }
    }
}
