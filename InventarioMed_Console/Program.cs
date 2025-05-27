using InventarioMed_Console;
using InventarioMed.Shared.Data.BD;
using InventarioMed.Shared.Entities;
using System.Xml;

internal class Program
{
    public static Dictionary<string, Order> OrderList = new();

    private static void Main(string[] args)
    {
        var orderDAL = new DAL<Order>();
        var itemDAL = new DAL<Item>();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Você chegou na InventarioMed - Gestão de Encomendas!\n");
            Console.WriteLine("Digite 1 para registrar uma encomenda");
            Console.WriteLine("Digite 2 para adicionar um item a uma encomenda");
            Console.WriteLine("Digite 3 para mostrar todas as encomendas");
            Console.WriteLine("Digite 4 para mostrar os itens de uma encomenda");
            Console.WriteLine("Digite -1 para sair\n");

            Console.Write("Escolha sua opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    OrderRegistration();
                    break;
                case 2:
                    ItemRegistration();
                    break;
                case 3:
                    OrderListAll();
                    break;
                case 4:
                    ItemListByOrder();
                    break;
                case -1:
                    Console.WriteLine("Até mais!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        void OrderRegistration()
        {
            Console.Clear();
            Console.WriteLine("Registro de encomenda");
            Console.Write("Digite o nome do cliente: ");
            string clientName = Console.ReadLine();

            var order = new Order { ClientName = clientName };
            orderDAL.Create(order);

            Console.WriteLine($"Encomenda para {clientName} registrada com sucesso!");
            Console.ReadKey();
        }

        void ItemRegistration()
        {
            Console.Clear();
            Console.WriteLine("Adicionar item à encomenda");
            Console.Write("Digite o nome do cliente (encomenda): ");
            string clientName = Console.ReadLine();

            var order = orderDAL.ReadBy(o => o.ClientName.Equals(clientName));
            if (order != null)
            {
                Console.Write("Digite o nome do produto: ");
                string productName = Console.ReadLine();

                var item = new Item { ProductName = productName, OrderId = order.Id };
                itemDAL.Create(item);

                Console.WriteLine($"Item {productName} adicionado à encomenda de {clientName}.");
            }
            else
            {
                Console.WriteLine($"Encomenda de {clientName} não encontrada.");
            }
            Console.ReadKey();
        }

        void OrderListAll()
        {
            Console.Clear();
            Console.WriteLine("Lista de todas as encomendas:");
            foreach (var order in orderDAL.Read())
            {
                Console.WriteLine($"ID: {order.Id} - Cliente: {order.ClientName}");
            }
            Console.ReadKey();
        }

        void ItemListByOrder()
        {
            Console.Clear();
            Console.WriteLine("Ver itens de uma encomenda");
            Console.Write("Digite o nome do cliente: ");
            string clientName = Console.ReadLine();

            var order = orderDAL.ReadBy(o => o.ClientName.Equals(clientName));
            if (order != null)
            {
                Console.WriteLine($"Itens da encomenda de {clientName}:");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"- {item.ProductName}");
                }
            }
            else
            {
                Console.WriteLine($"Encomenda de {clientName} não encontrada.");
            }
            Console.ReadKey();
        }
    }
}
