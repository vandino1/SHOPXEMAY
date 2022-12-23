using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HondaVN.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "Trường Tên sản phẩm phải là một chuỗi có độ dài tối thiểu là 3 và độ dài tối đa là 60", MinimumLength = 3)]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm.")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hãng SX")]
        public string Manufacturer { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0} đ")]
        //Hiển thị $: [DataType(DataType.Currency)] 
        [Display(Name = "Giá")]
        public int Price { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        [Display(Name = "Mô tả")]
        
        public string Desciption { get; set; }
        [Display(Name = "Hình ảnh")]

        public string Image { get; set; }
        
    }
}
