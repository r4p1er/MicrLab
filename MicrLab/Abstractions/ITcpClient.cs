namespace MicrLab.Abstractions;

public interface ITcpClient
{
    void WriteAsync(byte[] data);
}