namespace WebApiTemplate.Core
{
    public class Test
    {
        public async Task Run()
        {
            await Task.Yield();

            Console.WriteLine("Ok");
        }
    }
}