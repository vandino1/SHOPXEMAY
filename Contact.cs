using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HondaVN.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="Phải nhập {0}")]
        [Display(Name = "Họ Tên")]

        public string FullName { get; set; }
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage ="Phải là địa chỉ email")]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Display(Name = "Ngày gửi")]
        public DateTime DateSent { get; set; }

        [Display(Name = "Nội dung")]
        public string Message { get; set; }
        [StringLength(50)]
        [Phone(ErrorMessage ="Phải là số điện thoại")]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        public string Phone { get; set; }

    }
}
