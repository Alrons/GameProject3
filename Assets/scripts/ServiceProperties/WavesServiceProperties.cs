using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


public class WavesServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "https://localhost:7075/api/";

    public HttpClient HttpClient { get; set; }

    public string Wave {  get; set; }

    public WavesServiceProperties() 
    {
        // Initialize the HTTP client
        HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

        Wave = "Wave/";
    }
}
