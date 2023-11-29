namespace PersonelServer.Domain.Users;

public sealed record PhoneNumber
{
    public PhoneNumber(string value)
    {
        //ArgumentNullException.ThrowIfNullOrEmpty(value);

        //if(value.Length != 10)
        //{
        //    throw new ArgumentException("Telefon numarası 10 karakter olmalıdır");
        //}

        Value = value;
    }
    public string Value { get; init; }
}
