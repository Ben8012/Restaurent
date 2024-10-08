﻿using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Forms
{
    public class UpdateUserFormBll
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Phone { get; set; }
        public int AdressId { get; set; }
    }
}
