﻿using Homework.Infrastructure;
using Newtonsoft.Json;

namespace Homework.Models.Figures;

public sealed class Square : IFigure
{
	private double _a;

	public string Name => "Квадрат";
	public string Image => "square.png";

	public double SideA
	{
		get => _a;
		set => _a = value > 0
			? value
			: throw new InvalidDataException($"Недопустимое значение: {value}");
	}

	public Square(double sideA) => SideA = sideA;

	[JsonIgnore]
	public double Area => SideA * SideA;
	[JsonIgnore]
	public double Perimeter => 4 * SideA;
	[JsonIgnore]
	public Dictionary<string, double> Parameters => new()
	{
		["a"] = _a
	};
}