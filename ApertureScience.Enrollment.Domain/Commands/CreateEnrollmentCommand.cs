using ApertureScience.Library.Cqrs.Command.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Enrollment.Domain.Commands
{
  public  class CreateEnrollmentCommand:ICommand
    {
        public string FullName { get;  }
        public string Email { get;  }
        public string Password { get; }
        public int Code { get;}
        public bool IsAdmin { get; }

        public CreateEnrollmentCommand(string fullname,string email,string password,int code,bool isAdmin)
        {
            FullName = fullname;
            Email = email;
            Password = password;
            Code = code;
            IsAdmin = IsAdmin;
        }
    }
}
