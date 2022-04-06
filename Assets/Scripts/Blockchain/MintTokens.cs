using System.Collections;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.Threading.Tasks;

public class MintTokens : MonoBehaviour
{
    
    DAO dao;

    private void Start()
    {
        dao = GameObject.Find("DAO").GetComponent<DAO>();
    }

    public void MintTokensOfPlayer()
    {
        StartCoroutine(dao.GenerateMintMessageSigned());        
    }

    
}
