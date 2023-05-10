namespace Tienda.Domain.Core
{
    public static class NumberHelpers
    {
        public static int SafeDivision(double numerator, double denominator)
        {
            _ = int.TryParse(Math.Ceiling(numerator / denominator).ToString(), out int result);
            return result;
        }
    }
}
