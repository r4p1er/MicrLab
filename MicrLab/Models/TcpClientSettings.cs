namespace MicrLab.Models;

public class TcpClientSettings
{
    public string TargetHost { get; set; } = null!;
    
    public int TargetPort { get; set; }
}