namespace ShopBlazor.Pages.Dtos;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IList<Contract>? Contracts { get; set; }
}

public class Contract
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }

    public EContractStatus Status { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}

public enum EContractStatus
{
    Created,
    Active,
    Paid
}