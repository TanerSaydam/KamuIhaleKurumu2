using Microsoft.Win32;

public string ReadRegistryValue(string keyPath, string keyName)
{
	// Registry'den deðer okuma
	try
	{
		using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
		{
			if (key != null)
			{
				Object o = key.GetValue(keyName);
				if (o != null)
				{
					// Þifreli veriyi çözmek için kendi þifre çözme metodunuzu burada kullanýn
					string decryptedValue = Decrypt(o.ToString());
					return decryptedValue;
				}
			}
		}
	}
	catch (Exception ex)
	{
		// Hata yönetimi
		Console.WriteLine($"An error occurred: {ex.Message}");
	}
	return null;
}

private string Decrypt(string encryptedValue)
{
	// Þifre çözme iþlemlerinizi burada yapýn
	// Bu çok basit bir örnektir, güvenli þifreleme yöntemleri kullanmalýsýnýz
	return Encoding.UTF8.GetString(Convert.FromBase64String(encryptedValue));
}