using Leitura_Arquivo.Entities;
using System.Globalization;

Console.Write("Entre com o caminho do arquivo!");
string caminho = Console.ReadLine();

List<Products> products = new List<Products>();

try
{
    using (StreamReader sr = File.OpenText(caminho))
    {
        while (!sr.EndOfStream)
        {
            string[] campos = sr.ReadLine().Split(',');
            string name = campos[0];
            double price = double.Parse(campos[1], CultureInfo.InvariantCulture);

            products.Add(new Products(name, price));

        }
    }

    var avg = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();

    Console.WriteLine($"A média dos preços é: {avg.ToString("f2", CultureInfo.InvariantCulture)}");

    var names = products.Where(p => p.Price < avg).OrderByDescending(p => p.Name).Select(p => p.Name);

    foreach (string name in names)
    {
        Console.WriteLine(name);
    }
}
catch (Exception ex)
{
    Console.WriteLine("Houve um erro!!!");
    Console.WriteLine(ex.Message);
    
}