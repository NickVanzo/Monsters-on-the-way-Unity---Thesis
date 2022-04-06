using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using TMPro;
using System.Numerics;
using System.Threading.Tasks;

public class ConnectMetamask : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI promethiumText;
    [SerializeField] TextMeshProUGUI nftsText;
    GameObject gameSession;
    DAO dao;

    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private string account;

    private void Start()
    {
        dao = GameObject.Find("DAO").GetComponent<DAO>();
        gameSession = GameObject.Find("Game session");
        coinsText.text = "0";
        promethiumText.text = "0";
        nftsText.text = "0";
        if(!PlayerPrefs.GetString("Account").Equals(""))
        {
            Web3Connect();
            StartCoroutine(SetTextOfStats());
        }
    }

    async public void OnLogin()
    {
        //If the playerPref is not set then the user is not logged with Metamask
        if(PlayerPrefs.GetString("Account").Equals(""))
        {
            Web3Connect();
            OnConnected();
        } 
        StartCoroutine(SetTextOfStats());
        await SetPromethiumText();
        await FetchNumberOfNFTsOwned();
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        PlayerPrefs.SetString("Account", account);

        // reset login message
        gameSession.GetComponent<GameSession>().SetPlayerAddress(account);
        SetConnectAccount("");
    }

    private IEnumerator SetTextOfStats()
    {
        string uri = "https://us-central1-dangermonsters.cloudfunctions.net/api/playerStats?address=" + PlayerPrefs.GetString("Account");
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending" + uwr.error);
        }
        else
        {
            JObject obj = JObject.Parse(uwr.downloadHandler.text);
            coinsText.text = obj["gold"].ToString();
        }
    }

    async Task<bool> SetPromethiumText()
    {
        BigInteger balance = await dao.FetchPromethiumFromBlockchain();
        promethiumText.text = balance.ToString().Substring(0, balance.ToString().Length - 6);
        return true;
    }

    

    async Task<bool> FetchNumberOfNFTsOwned()
    {
        BigInteger balanceOf = await dao.FetchNumberOfNFTsOwned();
        nftsText.text = balanceOf.ToString();
        return true;
    }
}
