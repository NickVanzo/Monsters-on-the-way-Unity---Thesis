using System.Collections;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

public class DAO : MonoBehaviour
{
    PlayerStats playerStats;
    Dictionary<string, string> deckOfCardsByURIOfNFT = new Dictionary<string, string>();

    readonly string contractAddressERC20 = "0x09849B1cA45D647800f2C8F95e6ADef132983000";
    readonly string contractAddressERC721 = "0xa60c65d55bb67174cd84a5d3fdf69cb6501309e1";
    readonly string abi = "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Paused\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"Unpaused\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"burn\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"burnFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"subtractedValue\",\"type\":\"uint256\"}],\"name\":\"decreaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"str\",\"type\":\"string\"}],\"name\":\"extractNumberOfTokensFromHash\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"addedValue\",\"type\":\"uint256\"}],\"name\":\"increaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"initialize\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"_hash\",\"type\":\"bytes32\"}],\"name\":\"isCodeValid\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"_hash\",\"type\":\"bytes32\"},{\"internalType\":\"bytes\",\"name\":\"_signature\",\"type\":\"bytes\"}],\"name\":\"mint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"pause\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"paused\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"origin\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"receiveTokensFromNFTMint\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"addr\",\"type\":\"address\"}],\"name\":\"setAddressOfNFTContract\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"numString\",\"type\":\"string\"}],\"name\":\"st2num\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"data\",\"type\":\"bytes32\"}],\"name\":\"toHex\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"unpause\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    readonly string method = "mint";
    readonly string network = "rinkeby";
    readonly string chain = "ethereum";


    private void Awake()
    {
        PopulateNFTDeck();
    }

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public IEnumerator GenerateMintMessageSigned()
    {
        WWWForm form = new WWWForm();
        string tokensToMint = playerStats.GetPromethiumOfPlayer() + "000000";
        form.AddField("tokens", tokensToMint);

        UnityWebRequest uwr = UnityWebRequest.Post("https://us-central1-dangermonsters.cloudfunctions.net/api/signMintOfTokens", form);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending" + uwr.error);
        }
        else
        {
            JObject obj = JObject.Parse(uwr.downloadHandler.text);
            string messageSigned = obj["signedMessage"]["message"].ToString();
            string signatureOfSigner = obj["signedMessage"]["signature"].ToString();
            RedeemTokens(messageSigned, signatureOfSigner);
            StartCoroutine(AddGold());
        }
    }

    private async void RedeemTokens(string messageSigned, string signature)
    {
        string value = "0";
        string args = "[\"" + messageSigned + "\", \"" + signature + "\"]";
        try
        {
            string response = await Web3GL.SendContract(method, abi, contractAddressERC20, args, value);
            playerStats.ResetPromethiumOfPlayer();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    public IEnumerator AddGold()
    {
        WWWForm form = new WWWForm();

        form.AddField("gold", playerStats.GetGoldOfPlayer());
        form.AddField("address", PlayerPrefs.GetString("Account"));

        UnityWebRequest uwr = UnityWebRequest.Post("https://us-central1-dangermonsters.cloudfunctions.net/api/rewardGold", form);

        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending" + uwr.error);
        } else
        {
            playerStats.ResetGold();
        }
    }

    void PopulateNFTDeck()
    {
        deckOfCardsByURIOfNFT.Add("ipfs://QmcASoo86H9dboZoL6CQ18MBPbaVkF82ydr7mBUwzXMmYh", "Wrath");
        deckOfCardsByURIOfNFT.Add("ipfs://QmWcqbvK5qkJRkrfokhnQHk6tc6hzjQjM3ZuRiQLa7KQBv", "Blade of Rashes");
        deckOfCardsByURIOfNFT.Add("ipfs://QmUVccUPYCnaJqT6NrDjfiScQdjTiBaBrFRDuFXV6snL3t", "Midnight Hunt");
        deckOfCardsByURIOfNFT.Add("ipfs://QmQLVp88bgYvpgYh9aPowHKSQ3RCaT9s472yoirgwXyDaW", "Vampire");
        deckOfCardsByURIOfNFT.Add("ipfs://QmWCvjL4JKWuwmbK6rCjGAWnEztnW8DMHNZDAucq6thXmx", "Sharpening");
        deckOfCardsByURIOfNFT.Add("ipfs://QmP4dt9ByjwDqwCvx2goTUCcFuU9u7DhhyfhnPb6mtGWa2", "Path of gold");
    }

    async public void FetchNFTsOwnerByAddress(string addressOwner)
    {
        string response = await EVM.AllErc721(chain, network, addressOwner, contractAddressERC721);
        JArray responseArray = JArray.Parse(response);

        foreach (JObject obj in responseArray)
        {
            string uri = (string)obj["uri"];
            string id = (string)obj["tokenId"];
            Card card = new Card(id, uri);
            ReadCardNameAndGiveEffectToPlayer(card);
        }
    }

    async void ReadCardNameAndGiveEffectToPlayer(Card card)
    {
        await card.FetchDurationOfCard();
        if (card.GetDuration() > 0 && card.GetIsActive())
        {
            Debug.Log("Duration of card: " + card.GetDuration());
            Debug.Log("Is this card active? " + card.GetIsActive());
            StartCoroutine(card.RemoveUsageFromCard());
            string uriOfNFT = card.GetUri();
            string nameOfCard = deckOfCardsByURIOfNFT.GetValueOrDefault(uriOfNFT);
            switch (nameOfCard)
            {
                case "Wrath":
                    playerStats.ActivateWrath();
                    playerStats.AddDamageStat(4);
                    break;
                case "Sharpening":
                    playerStats.SetSharpening();
                    playerStats.AddDamageStat(1);
                    break;
                case "Midnight Hunt":
                    if (IsMidnight())
                    {
                        playerStats.ActivateMidnightHunt();
                        playerStats.AddDamageStat(4);
                    }
                    break;
                case "Path of gold":
                    playerStats.ActivatePathOfGold();
                    break;
                case "Immersion":
                    playerStats.ActivateImmersion();
                    break;
            }
        }
    }

    bool IsMidnight()
    {
        string hours = DateTime.Now.ToString("t");
        return hours.Substring(0, 2).Equals("00") && hours.Substring(hours.Length - 2, 2).Equals("AM");
    }

    async public Task<BigInteger> FetchPromethiumFromBlockchain()
    {
        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contractAddressERC20, PlayerPrefs.GetString("Account"));
        return balanceOf;
    }

    async public Task<BigInteger> FetchNumberOfNFTsOwned()
    {
        BigInteger balanceOf = await ERC721.BalanceOf(chain, network, contractAddressERC721, PlayerPrefs.GetString("Account"));
        return balanceOf;
    }
}
