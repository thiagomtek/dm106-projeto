using EncomendasConsole;

internal class Program
{
    public static Dictionary<string, Order> OrderList = new();
    public static List<DeliveryLocation> LocationList = new();

    private static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nBem-vindo ao Sistema de Encomendas!\n");
            Console.WriteLine("Digite 1 para registrar uma encomenda");
            Console.WriteLine("Digite 2 para adicionar um item à encomenda");
            Console.WriteLine("Digite 3 para adicionar um local de entrega à encomenda");
            Console.WriteLine("Digite 4 para mostrar todas as encomendas");
            Console.WriteLine("Digite 5 para mostrar os itens de uma encomenda");
            Console.WriteLine("Digite 6 para mostrar os locais de entrega de uma encomenda");
            Console.WriteLine("Digite 7 para mostrar encomendas por local de entrega");
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
                    LocationRegistration();
                    break;
                case 4:
                    ShowAllOrders();
                    break;
                case 5:
                    ShowOrderItems();
                    break;
                case 6:
                    ShowOrderLocations();
                    break;
                case 7:
                    ShowOrdersByLocation();
                    break;
                case -1:
                    Console.WriteLine("Encerrando o sistema. Até logo!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }

    private static void OrderRegistration()
    {
        Console.Clear();
        Console.WriteLine("Registro de encomenda");
        Console.Write("Digite o código da encomenda: ");
        string code = Console.ReadLine();
        Console.Write("Digite o nome do cliente: ");
        string customer = Console.ReadLine();

        Order order = new(code, customer);
        OrderList.Add(code, order);
        Console.WriteLine($"Encomenda {code} registrada com sucesso!");
    }

    private static void ItemRegistration()
    {
        Console.Clear();
        Console.WriteLine("Adicionar item à encomenda");
        Console.Write("Digite o código da encomenda: ");
        string code = Console.ReadLine();

        if (OrderList.ContainsKey(code))
        {
            Console.Write("Descrição do item: ");
            string desc = Console.ReadLine();
            Console.Write("Quantidade: ");
            int qty = int.Parse(Console.ReadLine());

            OrderItem item = new(desc, qty);
            OrderList[code].AddItem(item);
            Console.WriteLine("Item adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Encomenda não encontrada.");
        }
    }

    private static void LocationRegistration()
    {
        Console.Clear();
        Console.WriteLine("Adicionar local de entrega à encomenda");
        Console.Write("Digite o código da encomenda: ");
        string code = Console.ReadLine();

        if (OrderList.ContainsKey(code))
        {
            Console.Write("Endereço de entrega: ");
            string address = Console.ReadLine();

            var existingLocation = LocationList.FirstOrDefault(loc => loc.Address == address);
            if (existingLocation == null)
            {
                existingLocation = new DeliveryLocation(address);
                LocationList.Add(existingLocation);
            }

            OrderList[code].AddLocation(existingLocation);
            Console.WriteLine("Local de entrega associado com sucesso!");
        }
        else
        {
            Console.WriteLine("Encomenda não encontrada.");
        }
    }

    private static void ShowAllOrders()
    {
        Console.Clear();
        Console.WriteLine("Todas as encomendas cadastradas:");
        foreach (var order in OrderList.Values)
        {
            Console.WriteLine(order);
        }
    }

    private static void ShowOrderItems()
    {
        Console.Clear();
        Console.WriteLine("Itens de uma encomenda");
        Console.Write("Digite o código da encomenda: ");
        string code = Console.ReadLine();

        if (OrderList.ContainsKey(code))
        {
            OrderList[code].ShowItems();
        }
        else
        {
            Console.WriteLine("Encomenda não encontrada.");
        }
    }

    private static void ShowOrderLocations()
    {
        Console.Clear();
        Console.WriteLine("Locais de entrega de uma encomenda");
        Console.Write("Digite o código da encomenda: ");
        string code = Console.ReadLine();

        if (OrderList.ContainsKey(code))
        {
            OrderList[code].ShowLocations();
        }
        else
        {
            Console.WriteLine("Encomenda não encontrada.");
        }
    }

    private static void ShowOrdersByLocation()
    {
        Console.Clear();
        Console.WriteLine("Encomendas por local de entrega");
        Console.Write("Digite o endereço de entrega: ");
        string address = Console.ReadLine();

        var location = LocationList.FirstOrDefault(loc => loc.Address == address);
        if (location != null)
        {
            location.ShowOrders();
        }
        else
        {
            Console.WriteLine("Endereço não encontrado.");
        }
    }
}
