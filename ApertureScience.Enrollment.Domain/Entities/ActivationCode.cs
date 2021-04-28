using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApertureScience.Enrollment.Domain.Entities
{
    [Table("ActivationCode",Schema ="Codes")]
    public class ActivationCode
    {

        [Key]
        public Guid Id { get; private set; }
        [Required]
        [Range(100000, 999999)]
        public int Code { get; private set; }
        [Required]
        public DateTime CreatedDateUtc { get; private set; }
        [Required]
        public DateTime ExpirationDateUtc { get; private set; }
        [Required]
        public bool IsUsed { get; private set; }
        [Required]
        public bool IsValid { get; private set; }
        [Required]
        public bool IsAdmin { get; private set; }

        private ActivationCode()
        {
            // for ef
        }
        public ActivationCode(int code,bool isAdmin)
        {
            Id = Guid.NewGuid();
            Code = code;
            CreatedDateUtc = DateTime.UtcNow;
            ExpirationDateUtc = CreatedDateUtc.AddMinutes(60);
            IsUsed = false;
            IsValid = true;
            IsAdmin = isAdmin;
            
        }

        public void UseCode()
        {
            this.IsUsed = true;
        }
        public void SetNotValid()
        {
            IsValid = false;
        }




    }
}
