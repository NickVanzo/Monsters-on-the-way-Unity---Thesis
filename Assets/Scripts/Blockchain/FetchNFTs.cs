using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class FetchNFTs : MonoBehaviour
{
    Dictionary<string, string> deckOfCardsByURIOfNFT = new Dictionary<string, string>();
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        
    }
}
