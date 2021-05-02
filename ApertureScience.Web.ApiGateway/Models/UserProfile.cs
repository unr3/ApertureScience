using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApertureScience.Web.ApiGateway.Models
{
    [Table("UserProfile", Schema = "Enrollment")]
    public class UserProfile
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string FullName { get; private set; }
        [Required]
        [EmailAddress]
        public string Email { get; private set; }
        [Required]
        public string Password { get; private set; }
        [Required]
        public int Code { get; private set; }
        [Required]
        public bool IsAdmin { get; private set; }

        private UserProfile()
        {

        }

        public UserProfile(Guid id, string fullName,string email,string password,int code,bool isAdmin)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Password = password;
            Code = code;
            IsAdmin = isAdmin;
        }
    }
}
