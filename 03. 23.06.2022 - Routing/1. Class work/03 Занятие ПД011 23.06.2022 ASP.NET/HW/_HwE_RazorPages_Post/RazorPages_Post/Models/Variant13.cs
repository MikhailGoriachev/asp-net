namespace RazorPages_Post.Models
{
    // Реализация вычислений по варианту 13 лаборатрной работы 1
    // из учебника Павловской Т.А. по программированию на C#
    public class Variant13
    {
        public double A { get; set; } = 0;
        public double B { get; set; } = 0;

        public (double Z1, double Z2) Calc() {
            double z2 = 2 * B - A;
            double z1 = (Math.Sin(A) + Math.Cos(z2))/ (Math.Cos(A) - Math.Sin(z2));
            z2 = (1 + Math.Sin(2*B)) / Math.Cos(2 * B);

            return (z1, z2);
        } // Calc

    } // class Variant13
}
