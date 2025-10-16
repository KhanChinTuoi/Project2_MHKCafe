using System;
using System.Collections.Generic;
using System.Linq;

namespace MHKCafe.Models.ViewModels
{
    public class ShoppingCart
    {
        /// <summary>
        /// Danh sách sản phẩm trong giỏ hàng.
        /// </summary>
        public List<CartItem> Items { get; set; } = new();

        /// <summary>
        /// Tổng giá trị của giỏ hàng.
        /// </summary>
        public decimal GrandTotal => Items.Sum(x => x.Total);

        /// <summary>
        /// Tổng số lượng sản phẩm trong giỏ hàng.
        /// </summary>
        public int TotalItems => Items.Sum(x => x.Quantity);

        /// <summary>
        /// Kiểm tra giỏ hàng có trống không.
        /// </summary>
        public bool IsEmpty => !Items.Any();

        /// <summary>
        /// Làm trống giỏ hàng.
        /// </summary>
        public void Clear() => Items.Clear();
    }

    public class CartItem
    {
        /// <summary>
        /// Mã sản phẩm (ProductId trong database).
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Tên món / sản phẩm.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Giá sản phẩm.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Số lượng sản phẩm.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Đường dẫn hình ảnh.
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Tổng giá của sản phẩm này (Price * Quantity).
        /// </summary>
        public decimal Total => Price * Quantity;
    }
}
