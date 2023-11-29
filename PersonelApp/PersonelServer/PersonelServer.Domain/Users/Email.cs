namespace PersonelServer.Domain.Users;

public sealed record Email
{
    public Email(string value)
    {
        //ArgumentNullException.ThrowIfNullOrEmpty(value);

        //if (!value.Contains("@"))
        //{
        //    throw new ArgumentException("Geçerli bir mail adresi girin");
        //}

        Value = value;
    }
    public string Value { get; init; }
}
