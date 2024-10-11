using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class ItemServiceProperties
{
    // Use a constant for the base URL
    public readonly string baseUrl = "https://localhost:7075/api/";

    // Use a readonly field for the HTTP client
    public HttpClient HttpClient { get; set; }

    // Use a readonly field for the URL
    public string AddedItems { get; set; }

    public string Items { get; set; }

    public string Tables { get; set; }

    public int UserId { get; set; }

    public ItemServiceProperties()
    {
        // Initialize the HTTP client
        HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

        // Initialize the URL
        AddedItems = "AddedItems/";
        Items = "Items/";
        Tables = "Tables/";
        UserId = 1;
    }
}
