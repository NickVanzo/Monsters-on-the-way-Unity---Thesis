using System.Collections;
using UnityEngine;
using TMPro;


public class RewardHandler : MonoBehaviour
{
    [SerializeField] GameObject[] rewards;
    [SerializeField] float secondsToWaitToDestroyPopupText = 5f;
    [SerializeField] TextMeshProUGUI popup;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int indexOfReward = Random.Range(0, rewards.Length);
            Instantiate(rewards[indexOfReward]);
            SpawnPopupText(rewards[indexOfReward].GetComponent<ItemDescriptor>().GetDescription());
            GetComponent<Collider2D>().enabled = false;
            if (tag == "Chest")
            {
                animator.SetBool("openChest", true);
            }
        }        
    }

    void SpawnPopupText(string itemDescription)
    {
        popup.text = itemDescription;
        StartCoroutine(DestroyTextPopUp(popup));
    }

    IEnumerator DestroyTextPopUp(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(secondsToWaitToDestroyPopupText);
        text.text = "";
    }
}
