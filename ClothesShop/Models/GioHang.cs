using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothesShop.Models
{
    public class GioHang
    {
        private ClothesShopEntities db = new ClothesShopEntities();
        public int productId;
        public string productName;
        public string productImg;
        public double productPrice;
        public int productQuantity;
        public int ShippingFee = 30000;
        public double totalPrice
        {
            get { return productPrice * productQuantity; }
        }

        public GioHang(int id)
        {
            productId = id;
            Product s = db.Products.Single(n => n.ProductId == productId);
            productName = s.ProductName;
            productImg = s.Image;
            productPrice = double.Parse(s.Price.ToString());
            productQuantity = 1;
        }
    }
}