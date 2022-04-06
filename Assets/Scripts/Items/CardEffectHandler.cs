using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CardEffectHandler : MonoBehaviour
{
    [SerializeField] string rarity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AddCard());
        }
    }

    private IEnumerator AddCard()
    {
        WWWForm form = new WWWForm();
        form.AddField("rarity", this.rarity);
        form.AddField("address", PlayerPrefs.GetString("Account"));

        UnityWebRequest uwr = UnityWebRequest.Post("https://us-central1-dangermonsters.cloudfunctions.net/api/addTicket", form);
        this.gameObject.SetActive(false);
        yield return uwr.SendWebRequest();
        

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error while sending" + uwr.error);
        } else
        {
            Destroy(this.gameObject);
        }
    }

}
