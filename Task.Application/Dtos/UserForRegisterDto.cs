using System;
using System.ComponentModel.DataAnnotations;

namespace Task.Application.Dtos
{
    public class UserForRegisterDto
    {
        [Required]

        public string UserName { get; set; }
        [StringLength(8,MinimumLength=4,ErrorMessage="يجب أن لا تقل كلمة السر عن أربعة أحرف ولا تزيد عن ثمانية")]
        [Required]
        public string Password { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
       
        public string Phone { get; set; }
        public Boolean isActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int []OrganizationsId   { get; set; }
       public string  Role { get; set; }


        public UserForRegisterDto()
        {
            isActive = true;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;



        }
    }
}