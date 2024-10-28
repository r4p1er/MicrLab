using MicrLab.Abstractions;
using MicrLab.Exceptions;
using MicrLab.Models;

namespace MicrLab.Services;

public class TcpClient(TcpClientSettings settings) : ITcpClient
{
    public void WriteAsync(byte[] data)
    {
        try
        {
            using (var client = new System.Net.Sockets.TcpClient(settings.TargetHost, settings.TargetPort))
            {
                var stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Thread.Sleep(1000);
                stream.Close();
            }
        }
        catch (Exception ex)
        {
            throw new ConnectionFailedException("Не удалось подключиться!");
        }
    }
}