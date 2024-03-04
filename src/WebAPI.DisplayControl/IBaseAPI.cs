namespace CLI.Client;

public interface IBaseAPI
{
    IResult Restart();

    IResult Shutdown();

    Task Screenshot(HttpContext context);
}

