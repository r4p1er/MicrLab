using System.Text;
using MicrLab.Abstractions;
using MicrLab.Enums;
using MicrLab.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace MicrLab.Pages;

[IgnoreAntiforgeryToken]
public class IndexModel(ITcpClient client) : PageModel
{
    private static readonly Regex PhoneRegex = new(@"^\+7\d{10}$");

    public PageStatus Status { get; set; } = PageStatus.Pending;
    public string FailureMessage { get; set; } = "";

    public void OnGet()
    {
    }

    public void OnPost(string phone, string message)
    {
        if (!PhoneRegex.IsMatch(phone))
        {
            Status = PageStatus.Failure;
            FailureMessage = "Неправильный формат телефона. Он должен быть в виде '+78005553535'";
            return;
        }

        var phoneBytes = Encoding.ASCII.GetBytes(phone);
        var messageBytes = Encoding.ASCII.GetBytes(message);
        var resultBytes = phoneBytes.Concat(messageBytes).ToArray();

        try
        {
            client.WriteAsync(resultBytes);
            Status = PageStatus.Success;
        }
        catch (ConnectionFailedException ex)
        {
            Console.WriteLine(ex.Message);
            Status = PageStatus.Failure;
            FailureMessage = ex.Message;
        }
    }
}