using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaVN.Models
{
    public class ProductService
    { 
        public List<Product> GetProducts()
        {
           
                    var products = new List<Product>
            {
        new Product{Id=1,Name="Xe mô tô CB500X 2022 Đỏ",Manufacturer="Honda",Price=193790000,Quantity=5,Desciption="Thiết kế thể thao. Uy lực tốc độ.",Image="1.jpg"},
        new Product{Id=2,Name="Xe mô tô CB500X 2022 Đen",Manufacturer="Honda",Price=193890000,Quantity=3,Desciption="Thiết kế thể thao. Uy lực tốc độ.",Image="2.jpg"},
        new Product{Id=3,Name="Xe mô tô CB500X 2022 Xám Xanh",Manufacturer="Honda",Price=194000000,Quantity=6,Desciption="Thiết kế thể thao. Uy lực tốc độ.",Image="3.jpg"},
        new Product{Id=4,Name="Xe mô tô CBR500R Đen",Manufacturer="Honda",Price=192490000,Quantity=3,Desciption="Thiết kế thể thao. Động cơ phản hồi nhanh nhạy.",Image="4.jpg"},
        new Product{Id=5,Name="Xe mô tô CBR500R Đỏ",Manufacturer="Honda",Price=192000000,Quantity=3,Desciption="Thiết kế thể thao. Động cơ phản hồi nhanh nhạy.",Image="5.jpg"},
        new Product{Id=6,Name="Honda Future 125 FI Đen",Manufacturer="Honda",Price=30000000,Quantity=10,Desciption="Thiết kế trẻ trung, lịch lãm và hiện đại. Thu hút mọi ánh nhìn.",Image="6.jpg"},
        new Product{Id=7,Name="Honda Future 125 FI Đỏ",Manufacturer="Honda",Price=31000000,Quantity=8,Desciption="Thiết kế trẻ trung, lịch lãm và hiện đại. Thu hút mọi ánh nhìn.",Image="7.jpg"},
        new Product{Id=8,Name="Honda Future 125 FI Xanh",Manufacturer="Honda",Price=30500000,Quantity=6,Desciption="Thiết kế trẻ trung, lịch lãm và hiện đại. Thu hút mọi ánh nhìn.",Image="8.jpg"},
        new Product{Id=9,Name="Honda Future 125 FI Xám Đen",Manufacturer="Honda",Price=30300000,Quantity=2,Desciption="Thiết kế trẻ trung, lịch lãm và hiện đại. Thu hút mọi ánh nhìn.",Image="9.jpg"},
        new Product{Id=10,Name="Honda Future 125 FI Trắng",Manufacturer="Honda",Price=30400050,Quantity=5,Desciption="Thiết kế trẻ trung, lịch lãm và hiện đại. Thu hút mọi ánh nhìn.",Image="10.jpg"},
        new Product{Id=11,Name="Air-Blade Đen",Manufacturer="Honda",Price=45000000,Quantity=5,Desciption="Tự do tạo dấu ấn của riêng mình",Image="11.jpg"},
        new Product{Id=12,Name="Air-Blade Đỏ",Manufacturer="Honda",Price=45500000,Quantity=6,Desciption="Tự do tạo dấu ấn của riêng mìnhh.",Image="12.jpg"},
        new Product{Id=13,Name="Air-Blade Xám",Manufacturer="Honda",Price=46000000,Quantity=2,Desciption="Tự do tạo dấu ấn của riêng mình",Image="13.jpg"},
        new Product{Id=14,Name="Air-Blade Xanh",Manufacturer="Honda",Price=44000000,Quantity=1,Desciption="Tự do tạo dấu ấn của riêng mình.",Image="14.jpg"},
        new Product{Id=15,Name="SH MODE 2022",Manufacturer="Honda",Price=77000000,Quantity=3,Desciption="Xe được ưa chuộng trong giới trẻ",Image="15.jpg"},
        new Product{Id=16,Name="Honda LEAD 2022 - 125cc - Đen",Manufacturer="Honda",Price=44000000,Quantity=1,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="16.jpg"},
        new Product{Id=17,Name="Honda LEAD 2022 - 125cc - Xám",Manufacturer="Honda",Price=44500000,Quantity=2,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="17.jpg"},
        new Product{Id=18,Name="Honda WinnerX 2022 - Bạc",Manufacturer="Honda",Price=36500000,Quantity=5,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="18.jpg"},
        new Product{Id=19,Name="Honda WinnerX 2022 - Đen",Manufacturer="Honda",Price=36500000,Quantity=2,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="19.jpg"},
        new Product{Id=20,Name="Honda WinnerX 2022 - Đỏ",Manufacturer="Honda",Price=36000000,Quantity=1,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="20.jpg"},
        new Product{Id=21,Name="Honda WinnerX 2022 - Trắng",Manufacturer="Honda",Price=37000000,Quantity=1,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="21.png"},
        new Product{Id=22,Name="Honda RSX 2022 - Đỏ",Manufacturer="Honda",Price=21000000,Quantity=3,Desciption="Xe được ưa chuộng trong giới trẻ.",Image="22.jpg"},
        new Product{Id=23,Name="Honda RSX 2022 - Trắng",Manufacturer="Honda",Price=21100000,Quantity=2,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="23.jpg"},
        new Product{Id=24,Name="Honda RSX 2022 - Xám",Manufacturer="Honda",Price=22000000,Quantity=2,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="24.jpg"},
        new Product{Id=25,Name="Honda RSX 2022 - Xanh",Manufacturer="Honda",Price=20500000,Quantity=1,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="25.jpg"},
        new Product{Id=26,Name="Honda RSX 2022 - Đen",Manufacturer="Honda",Price=21000000,Quantity=1,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="26.jpg"},
        new Product{Id=27,Name="SYM ANGEL FI Đen",Manufacturer="Honda",Price=26000000,Quantity=2,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="27.jpg"},
        new Product{Id=28,Name="SYM ANGEL FI Đỏ",Manufacturer="Honda",Price=26500000,Quantity=2,Desciption="Có số lượng bán ra lớn nhất tại thị trường Việt Nam.",Image="28.jpg"},
        new Product{Id=29,Name="Yamaha Mio M3 Đen",Manufacturer="Honda",Price=31000000,Quantity=5,Desciption="Phong cách sang trọng cùng những tiện ích và công nghê hiện đại",Image="29.jpg"},
        new Product{Id=30,Name="Yamaha Mio M3 Đỏ",Manufacturer="Honda",Price=32000000,Quantity=2,Desciption="Phong cách sang trọng cùng những tiện ích và công nghê hiện đại",Image="30.jpg"},
        new Product{Id=31,Name="Yamaha Mio M3 Xanh",Manufacturer="Honda",Price=31500000,Quantity=3,Desciption="Phong cách sang trọng cùng những tiện ích và công nghê hiện đại",Image="31.jpg"},
            };

                    return products;
       }
    }
}




