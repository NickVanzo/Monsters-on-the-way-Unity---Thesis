using System.Collections;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.Net.Http;
using System.Threading.Tasks;


public class Card
{
    private readonly HttpClient _httpClient = new HttpClient();
    private string id;
    private string uri;
    private int duration;
    private bool isActive;

    public Card(string id, string uri)
    {
        this.id = id;
        this.uri = uri;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public string GetUri()
    {
        return this.uri;
    }

    public string GetID()
    {
        return this.id;
    }

    public int GetDuration()
    {
        return this.duration;
    }

    public async Task<int> FetchDurationOfCard()
    {
        string uri = "https://us-central1-dangermonsters.cloudfunctions.net/api/cardInformations?id=" + this.id;
        var stringData = await _httpClient.GetStringAsync(uri);
       
        this.duration = Int32.Parse(JObject.Parse(stringData)["duration"].ToString());
        this.isActive = Convert.ToBoolean(JObject.Parse(stringData)["isActive"].ToString());
        return Int32.Parse(JObject.Parse(stringData)["duration"].ToString());
    }

    public IEnumerator RemoveUsageFromCard()
    {
        string uri = "https://us-central1-dangermonsters.cloudfunctions.net/api/removeUsageFromCard?id=" + this.id;
        UnityWebRequest uwr = UnityWebRequest.Post(uri, "");
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending" + uwr.error);
        }        
    }
}
