namespace Swadify_API.Helpers
{
    public class OrderNumberHelper
    {
        private static int _counter = 0;
        private static readonly object _lock = new();

        public static string Generate()
        {
            lock (_lock)
            {
                //_counter++;
                var orderNumber = $"FD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
                //return $"FD-{DateTime.UtcNow:yyyyMMdd}-{_counter:D4}";
                return orderNumber;
            }
        }

        public static string GenerateDeliveryCode()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
