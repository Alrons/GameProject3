using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class WavesServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "https://localhost:7090/api/";

    // Use a readonly field for the HTTP client
    public HttpClient HttpClient { get; set; }

    public string Waves {  get; set; }

    public WavesServiceProperties() 
    {
        // Initialize the HTTP client
        HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

        Waves = "Waves/";
    }
}
