namespace MyFirstWebApi.Exceptions;

public class NotFoundIndexException : Exception
{
    public NotFoundIndexException() : base("Bu indexe ait kayıt bulunamadı!")
    {
        
    }
}
