namespace HVACSimulator
{
  using System;
  using System.Text;
  using System.Threading.Tasks;
  using Microsoft.ServiceBus.Messaging;

  class Program
  {
    static string ConnectionString = "<輸入您的連接字串>";

    static void Main(string[] args)
    {
      Console.WriteLine("按下 Ctrl-C 來停止傳送訊息");
      Console.WriteLine("按下 Enter 鍵後開始傳送訊息");
      Console.ReadLine();
      SendingSensorDataAsync().Wait();
    }

    static async Task SendingSensorDataAsync()
    {
      // 根據連接字串建立 event hub client
      var eventHubClient = EventHubClient.CreateFromConnectionString(ConnectionString);

      // 產生隨機種子
      var random = new Random();

      // 開始不斷送訊息
      while (true)
      {
        // 用來代表訊息的 GUID
        var guid = Guid.NewGuid().ToString();
        // 現在時間
        var time = DateTime.UtcNow.ToString();
        // 溫度，模擬從攝氏 18 至 28 度, 精確到小數點下一位
        var temperature = random.Next(180, 280) / 10.0;
        // 溼度
        var humidity = random.Next(40, 90);

        // 送出訊息的 JSON 字串
        var message = $"{{\"id\":\"{guid}\",\"temperature\":{temperature},\"humidity\":{humidity},\"time\":\"{time}\"}}";

        try
        {
          Console.WriteLine($"傳送訊息:{message}");
          // 送出訊息到 event hub
          await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }
        catch (Exception exception)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"錯誤: {exception.Message}");
          Console.ResetColor();
        }

        // 間隔 200 ms 再傳下一筆
        await Task.Delay(200);
      }
    }
  }
}
