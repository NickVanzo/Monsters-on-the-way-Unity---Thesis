using UnityEngine;
using TMPro;

public class PowerUPMessageHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageToShowOnHover;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void ShowMessage()
    {
        switch(name)
        {
            case "immersion":
                if (playerStats.IsImmersionActive())
                {
                    messageToShowOnHover.text = "Immersion: active";
                }
                else
                {
                    messageToShowOnHover.text = "Immersion: not active";
                }
                break;
            case "path of gold":
                if(playerStats.IsGoldPathActive())
                {
                    messageToShowOnHover.text = "Path of gold: active";
                } else
                {
                    messageToShowOnHover.text = "Path of gold: not active";
                }
                break;
            case "sharpening":
                if(playerStats.IsSharpeningActive())
                {
                    messageToShowOnHover.text = "Sharpening: active";
                } else
                {
                    messageToShowOnHover.text = "Sharpening: not active";
                }
                break;
            case "wrath":
                if(playerStats.IsWrathActive())
                {
                    messageToShowOnHover.text = "Wrath: active";
                } else
                {
                    messageToShowOnHover.text = "Wrath: not active";
                }
                break;
            case "midnight":
                if(playerStats.IsMidnightHuntActive())
                {
                    messageToShowOnHover.text = "Midnight hunt: active";
                } else
                {
                    messageToShowOnHover.text = "Midnight hunt: not active";
                }
                break;
        }
        
    }

    public void CancelMessage()
    {
        messageToShowOnHover.text = "";
    }
}
