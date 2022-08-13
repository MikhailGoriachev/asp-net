using System.IO;

namespace Homework.Models.Products
{
/*
 * данные о бытовой технике на оптовом складе (наименование, артикул, цена, количество, изображение)
 */
	public class Product
	{
		private string? _name;
		private string? _vendorCode;
		private string? _type;
		private int _price;
		private int _amount;

		public string? Name
		{
			get => _name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new InvalidDataException("Недопустимое значение в поле наименования");
				_name = value;
			}
		}

		public string? VendorCode
		{
			get => _vendorCode;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new InvalidDataException("Недопустимое значение в поле артикула");
				_vendorCode = value;
			}
		}

		public string? Type
		{
			get => _type;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new InvalidDataException("Недопустимое значение в поле типа");
				_type = value;
			}
		}

		public int Price
		{
			get => _price;
			set
			{
				if (value <= 0)
					throw new InvalidDataException("Недопустимое значение цены техники");
				_price = value;
			}
		}

		public int Amount
		{
			get => _amount;
			set
			{
				if (value < 0)
					throw new InvalidDataException("Недопустимое значение количества");
				_amount = value;
			}
		}

		public string? Image { get; set; }

		public Product() { }

		public Product(string? name, string? vendorCode, string? type, int price, int amount, string? image)
		{
			Name = name;
			VendorCode = vendorCode;
			Type = type;
			Price = price;
			Amount = amount;
			Image = image;
		}
		public Product(string? name, string? vendorCode, string? type, int price, int amount)
		{
			Name = name;
			VendorCode = vendorCode;
			Type = type;
			Price = price;
			Amount = amount;
		}

		public Product(Product product)
		{
			Name = product.Name;
			VendorCode = product.VendorCode;
			Type = product.Type;
			Price = product.Price;
			Amount = product.Amount;
			Image = product.Image;
		}
		
	}
}