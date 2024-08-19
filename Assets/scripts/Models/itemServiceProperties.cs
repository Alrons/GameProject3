using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class ItemServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "https://localhost:7090/api/";

    // Use a readonly field for the HTTP client
    public HttpClient _httpClient { get; set; }

    // Use a readonly field for the URL
    public string _AddedItemsUrl { get; set; }

    public string _Items { get; set; }

    public string _SizeTables { get; set; }

    public string _OurTables { get; set; }

    public ItemServiceProperties()
    {
        // Initialize the HTTP client
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

        // Initialize the URL
        _AddedItemsUrl = "AddedItems/";
        _Items = "Items/";
        _SizeTables = "SizeTables/";
        _OurTables = "Tables/";
    }
}
