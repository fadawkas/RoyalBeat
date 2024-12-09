using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;

    private int gems;
    [SerializeField] private TMP_Text gemsDisplay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        gemsDisplay.text = gems.ToString();
    }

    public void ChangeGems(int amount)
    {
        gems += amount;
    }
}
