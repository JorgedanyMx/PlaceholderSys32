using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    [SerializeField] PlayerData playerData;
    public void UpdateWallet()
    {
        tmpText.text = playerData.GetCoins().ToString();
    }
    private void Start()
    {
        tmpText.text = playerData.GetCoins().ToString();
    }
}
