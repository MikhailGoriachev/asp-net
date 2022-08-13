namespace Homework.Models.Applicances;

/*
 * данные о бытовой технике на оптовом складе (наименование, артикул, цена, количество, изображение)
 */
public class Appliance
{
	private string? _name;
	private string? _article;
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

	public string? Article
	{
		get => _article;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new InvalidDataException("Недопустимое значение в поле артикула");
			_article = value;
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
			if (value < 0)
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

	public Appliance()
	{
	}

	public Appliance(string? name, string? article, string? type, int price, int amount, string? image)
	{
		Name = name;
		Article = article;
		Type = type;
		Price = price;
		Amount = amount;
		Image = image;
	}
}