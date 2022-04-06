using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI promethiumText;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI playerMeleeDamageText;
    [SerializeField] TextMeshProUGUI playerAttackDelayText;
    [SerializeField] TextMeshProUGUI playerSpeedText;

    DAO dao;

    PlayerStats playerStats;
    string playerAddress;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        HideUIForStatsIfMainMenu();
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            dao = GameObject.Find("DAO").GetComponent<DAO>();
            dao.FetchNFTsOwnerByAddress(PlayerPrefs.GetString("Account"));
        }
    }

    void HideUIForStatsIfMainMenu()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameObject.Find("Stats").SetActive(false);
        }
    }    

    public void StartLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {

        } else
        {
            coinsText.text = playerStats.GetGoldOfPlayer().ToString();
            promethiumText.text = playerStats.GetPromethiumOfPlayer().ToString();
            playerHealthText.text = playerStats.GetHealth().ToString();
            playerMeleeDamageText.text = playerStats.GetMeleeDamage().ToString();
            playerAttackDelayText.text = playerStats.GetAttackDelay().ToString();
            playerSpeedText.text = playerStats.GetPlayerSpeed().ToString();
        }            
    }

    public void SetPlayerAddress(string address)
    {
        this.playerAddress = address;
    }

    public string GetAddress()
    {
        return playerAddress;
    }
}
