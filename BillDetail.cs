using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HondaVN.Models
{
    public class BillDetail
    {
		public int BillDetailId { get; set; }

		[Display(Name = "Địa chỉ khách hàng")]
		public int BillId { get; set; }

		[Display(Name = "Tên sản phẩm")]
		public int ProductId { get; set; }

		[Display(Name = "Giá")]
		public int Price { get; set; }
		[Display(Name = "Số lượng")]
		public int Quantity { get; set; }

		[Display(Name = "Tổng tiền")]
		public int Amount { get; set; }

		[Display(Name = "Quê quán")]
		public Bill Bill { get; set; }

		[Display(Name = "Sản phẩm")]
		public Product Product { get; set; }
	}
}
