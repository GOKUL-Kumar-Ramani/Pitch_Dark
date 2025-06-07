using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door Instance;

    public int totalCoins = 2;
    private int coinsCollected = 0;

    public GameObject doorObject;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log("Coins Collected: " + coinsCollected);

        if (coinsCollected >= totalCoins)
        {
            ActivateDoor();
        }
    }

    void ActivateDoor()
    {
        if (doorObject != null)
        {
            doorObject.SetActive(true);
        }
    }
}
