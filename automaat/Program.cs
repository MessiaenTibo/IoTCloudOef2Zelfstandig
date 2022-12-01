// See https://aka.ms/new-console-template for more information
using System.Text;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using automaat.Models;

AutomaatConfig config = new AutomaatConfig();
string connectionString = "HostName=TiboIot.azure-devices.net;DeviceId=naam;SharedAccessKey=ks7MGNBuIKPmNh8V8awbqOfX3x2eEa90bD7uMw4fWSA=";

var deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
//await deviceClient.SetMethodHandlerAsync(OnDesiredPropertiesChanged(), null);



#region "Boot"
async Task OnDesiredPropertiesChanged(TwinCollection dp, object userContext)
{
    Console.WriteLine("Desired properties changed");
    Console.WriteLine(JsonConvert.SerializeObject(dp));
    config = JsonConvert.DeserializeObject<AutomaatConfig>(dp.ToJson());
}

async Task GetDesiredProperties(){
    var twin = await deviceClient.GetTwinAsync();
    var desiredProperties = twin.Properties.Desired;
    var desiredPropertiesJson = desiredProperties.ToJson();
    config = JsonConvert.DeserializeObject<AutomaatConfig>(desiredPropertiesJson);
    Console.WriteLine(desiredPropertiesJson);
}

#endregion


#region Menu

await GetDesiredProperties();
await Loop();
async Task Loop()
{
    while (true)
    {
        Console.WriteLine("Maak uw keuze: ");
        Console.WriteLine("1. Water");
        Console.WriteLine("2. Cola");
        Console.WriteLine("3. Fruitsap");
        Console.WriteLine("4. Afsluiten");
        string keuze = Console.ReadLine();
        await ProcessChoice(keuze);

    }
}

async Task ProcessChoice(string choise)
{
    switch (choise)
    {
        case "1":
            await SendWater();
            break;
        case "2":
            await SendCola();
            break;
        case "3":
            await SendFruitsap();
            break;
        case "4":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Ongeldige keuze");
            break;
    }

}


async Task SendWater()
{
    OrderMessage orderMessage = new OrderMessage
    {
        Product = "Water",
        Amount = 1,
        UnitPrice = config.PriceWater,
        TotalPrice = 1.5M,
        Location = "A1"
    };
    var json = JsonConvert.SerializeObject(orderMessage);
    var bytes = Encoding.UTF8.GetBytes(json);
    var message = new Message(bytes);
    await deviceClient.SendEventAsync(message);
}

async Task SendCola()
{
    OrderMessage orderMessage = new OrderMessage
    {
        Product = "Cola",
        Amount = 1,
        UnitPrice = config.PriceCola,
        TotalPrice = 2.5M,
        Location = "A1"
    };

    var json = JsonConvert.SerializeObject(orderMessage);
    var bytes = Encoding.UTF8.GetBytes(json);
    var message = new Message(bytes);
    await deviceClient.SendEventAsync(message);
}

async Task SendFruitsap()
{
    OrderMessage orderMessage = new OrderMessage
    {
        Product = "Fruitsap",
        Amount = 1,
        UnitPrice = config.PriceFruitsap,
        TotalPrice = 2.5M,
        Location = "A1"
    };

    var json = JsonConvert.SerializeObject(orderMessage);
    var bytes = Encoding.UTF8.GetBytes(json);
    var message = new Message(bytes);
    await deviceClient.SendEventAsync(message);
}



#endregion

