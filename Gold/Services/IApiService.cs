using Refit;

namespace Gold.Services;

public partial interface IApiService
{
    [Headers("Authorization: bearer")]
    [Get("/gold")]
    Task<Rootobject> GetGolds();
}



public interface IPlatformHttpMessageHandler
{
    HttpMessageHandler GetHttpMessageHandler();

}

public class TokenService
{
    public string Token { get; private set; } = HashHelper.ComputeHash512(ServiceHelper.GetService<GetDeviceInfo>().GetDeviceID());

    public void SetToken(string token) => Token = token;
}




public class Rootobject
{
    public string today { get; set; }
    public Golds golds { get; set; }
    public Owner owner { get; set; }
}

public class Golds
{
    public Gold24 gold24 { get; set; }
    public Gold18 gold18 { get; set; }
    public Gold18_Dast2 gold18_dast2 { get; set; }
    public decimal balance { get; set; }
}

public class Gold24
{
    public decimal grams { get; set; }
    public decimal rials { get; set; }
}

public class Gold18
{
    public decimal grams { get; set; }
    public decimal rials { get; set; }
}

public class Gold18_Dast2
{
    public decimal grams { get; set; }
    public decimal rials { get; set; }
}

public class Owner
{
    public string fullname { get; set; }
    public decimal balance { get; set; }
    public string shetab { get; set; }
    public string sheba { get; set; }
    public Tran[] trans { get; set; }
}

public class Tran
{
    public string at { get; set; }
    public decimal amount { get; set; }
    public decimal gold18 { get; set; }
    public decimal gold24 { get; set; }
}
