using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTriggersHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popup;
    [SerializeField] float secondsToWaitToDestroyPopupText = 0.5f;
    [SerializeField] Vector3 dimensionsOfPopup = new Vector3(0.3f, 0.3f, 0.3f);

    PlayerStats playerStats;
    GameObject canvas;

    Color colorOfTextPopup = Color.yellow;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Coin":
                if(playerStats.PlayerIsAlive())
                {
                    HandleCoinCollected(collision);
                }
                break;
        }
    }


    private void HandleCoinCollected(Collider2D collision)
    {
        SpawnPopupText();
        playerStats.AddGold(1);
        Destroy(collision.gameObject);
    }

    void SpawnPopupText()
    {
        TextMeshProUGUI popupInstantiated = Instantiate(popup, transform.position, transform.rotation);
        popupInstantiated.transform.SetParent(canvas.transform, true);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        popupInstantiated.text = "+1";
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
