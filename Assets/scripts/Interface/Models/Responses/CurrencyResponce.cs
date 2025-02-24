using System.Collections.Generic;

public class CurrencyResponse
{
    public int Status { get; set; }
    public string Message { get; set; }
    public List<CurrencyData> Data { get; set; }
}