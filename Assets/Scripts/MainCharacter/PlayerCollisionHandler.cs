using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popup;
    [SerializeField] Vector3 dimensionsOfPopup = new Vector3(0.3f,0.3f,0.3f);
    [SerializeField] float secondsToWaitToDestroyPopupText = 0.5f;

    PlayerStats playerStats;
    GameObject canvas;
    Color colorOfTextPopup = new Color(148, 0, 211, 255);

    readonly int valueOfPromethiumSmall = 1;
    readonly int valueOfPromethiumMedium = 2;
    readonly int valueOfPromethiumGem = 3;
    readonly int valueOfPromethiumDiamond = 4;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                HandleCollisionWithEnemy(collision.gameObject.GetComponent<EnemyStats>().GetDamageDealtByEnemy());
                break;
            case "PromethiumSmall":
                HandlePromethiumCollected(collision, valueOfPromethiumSmall);
                break;
            case "PromethiumMedium":
                HandlePromethiumCollected(collision, valueOfPromethiumMedium);
                break;
            case "PromethiumGem":
                HandlePromethiumCollected(collision, valueOfPromethiumGem);
                break;
            case "PromethiumDiamond":
                HandlePromethiumCollected(collision, valueOfPromethiumDiamond);
                break;
            default:
                break;
        }        
    }

    private void HandleCollisionWithEnemy(int damage)
    {
        if (playerStats.PlayerIsAlive())
        {
            playerStats.RemoveHealth(damage);
        }
    }


    private void HandlePromethiumCollected(Collision2D collision, int promethiumToAdd)
    {
        SpawnPopupText(promethiumToAdd);
        Destroy(collision.gameObject);
        playerStats.AddPromethium(promethiumToAdd);        
    }

    void SpawnPopupText(int promethiumToAdd)
    {
        TextMeshProUGUI popupInstantiated = Instantiate(popup, transform.position, transform.rotation);
        popupInstantiated.transform.SetParent(canvas.transform, true);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        popupInstantiated.text = "+" + promethiumToAdd.ToString();
        popupInstantiated.color = colorOfTextPopup;
        popupInstantiated.transform.localScale = dimensionsOfPopup;
        popupInstantiated.transform.position = screenPosition;

        StartCoroutine(DestroyTextPopUp(popupInstantiated));
    }

    IEnumerator DestroyTextPopUp(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(secondsToWaitToDestroyPopupText);
        Destroy(text);
    }
}
