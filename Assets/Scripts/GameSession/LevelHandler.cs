using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip audioClipForQuitting;

    [SerializeField] GameObject endScreenUI;
    [SerializeField] GameObject quitScreenUI;
    [SerializeField] GameObject collectiblesUI;
    [SerializeField] GameObject statsUI;
    [SerializeField] TextMeshProUGUI goldLootText;
    [SerializeField] TextMeshProUGUI tokensLootText;
    [SerializeField] TextMeshProUGUI commonTicketsText;
    [SerializeField] TextMeshProUGUI rareTicketsText;
    [SerializeField] TextMeshProUGUI veryRareTickets;

    bool isPlayerAlive = true;
    PlayerStats playerStats;
    DAO dao;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dao = GameObject.Find("DAO").GetComponent<DAO>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckIfPlayerIsAlive();
        RestartSessionIfPlayerIsDead();
    }

    void CheckIfPlayerIsAlive()
    {
        if(!playerStats.PlayerIsAlive())
        {
            isPlayerAlive = false;
        }
    }

    void RestartSessionIfPlayerIsDead()
    {
        if(!isPlayerAlive)
        {
            Time.timeScale = 0;
            endScreenUI.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadFirstLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);        
    }

    public void QuitUsingDoor()
    {
        audioSource.PlayOneShot(audioClipForQuitting);
        Time.timeScale = 0;
        quitScreenUI.SetActive(true);
        collectiblesUI.SetActive(false);
        statsUI.SetActive(false);
        goldLootText.text = "Gold: " + playerStats.TotalGold().ToString();
        tokensLootText.text = "Tokens: " + playerStats.TotalTokens().ToString();
        commonTicketsText.text = "Common tickets: " + playerStats.GetCommonTickets().ToString();
        rareTicketsText.text = "Rare tickets: " + playerStats.GetRareTickets().ToString();
        veryRareTickets.text = "Very rare tickets: " + playerStats.GetVeryRareTickets().ToString();
        StartCoroutine(dao.AddGold());
    }

    public void OpenMarkeplace()
    {
        Application.OpenURL("https://dangermonsters.web.app/marketplaceNFT");
    }
}
