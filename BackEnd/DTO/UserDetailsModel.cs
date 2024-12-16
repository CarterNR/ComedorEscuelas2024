﻿namespace BackEnd.DTO
{
    public class UserDetailsModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; }
    }
}