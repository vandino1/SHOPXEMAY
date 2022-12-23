using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HondaVN.Models
{
    public class CreateRoleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Phải nhập tên role")]
        [Display(Name = "Tên của Role (vai trò)")]
        [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
        public string RoleName { get; set; }
    }
}
