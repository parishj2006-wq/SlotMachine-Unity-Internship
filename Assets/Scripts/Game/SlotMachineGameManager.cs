
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotMachineGameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image[] reelImages;
    [SerializeField] private Sprite[] symbols;
    [SerializeField] private Button spinButton;
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text betText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject winEffectPanel;

    [Header("Game Settings")]
    [SerializeField] private int startingBalance = 1000;
    [SerializeField] private int betAmount = 50;
    [SerializeField] private float spinDuration = 1.5f;
    [SerializeField] private float spinUpdateInterval = 0.06f;
    [SerializeField] private float winEffectDuration = 1.5f;

    private int balance;
    private bool isSpinning;
    private Coroutine hideWinEffectCoroutine;

    private void Start()
    {
        balance = startingBalance;

        if (!ValidateReferences())
        {
            enabled = false;
            return;
        }

        spinButton.onClick.AddListener(Spin);

        winEffectPanel.SetActive(false);

        UpdateUI();
        resultText.text = "PRESS SPIN TO PLAY";
    }

    private bool ValidateReferences()
    {
        if (reelImages == null || reelImages.Length != 3)
        {
            Debug.LogError(
                "Assign exactly 3 reel Images in the Inspector.", this);
            return false;
        }

        if (symbols == null || symbols.Length < 2)
        {
            Debug.LogError(
                "Assign at least 2 symbol Sprites in the Inspector.", this);
            return false;
        }

        foreach (Image reel in reelImages)
        {
            if (reel == null)
            {
                Debug.LogError(
                    "A reel Image reference is missing.", this);
                return false;
            }
        }

        if (spinButton == null ||
            balanceText == null ||
            betText == null ||
            resultText == null ||
            winEffectPanel == null)
        {
            Debug.LogError(
                "One or more UI references are missing.", this);
            return false;
        }

        if (betAmount <= 0 || startingBalance < betAmount)
        {
            Debug.LogError(
                "Starting balance must cover a positive bet.", this);
            return false;
        }

        if (spinDuration <= 0f ||
            spinUpdateInterval <= 0f ||
            winEffectDuration <= 0f)
        {
            Debug.LogError(
                "All timing values must be greater than zero.", this);
            return false;
        }

        return true;
    }

    private void Spin()
    {
        if (isSpinning)
            return;

        if (balance < betAmount)
        {
            resultText.text = "NOT ENOUGH COINS!";
            return;
        }

        if (hideWinEffectCoroutine != null)
        {
            StopCoroutine(hideWinEffectCoroutine);
            hideWinEffectCoroutine = null;
        }

        winEffectPanel.SetActive(false);

        balance -= betAmount;
        UpdateUI();

        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;
        spinButton.interactable = false;
        resultText.text = "SPINNING...";

        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            for (int i = 0; i < reelImages.Length; i++)
            {
                reelImages[i].sprite =
                    symbols[Random.Range(0, symbols.Length)];
            }

            yield return new WaitForSeconds(spinUpdateInterval);
            elapsed += spinUpdateInterval;
        }

        // Select the final symbols for this spin.
        for (int i = 0; i < reelImages.Length; i++)
        {
            reelImages[i].sprite =
                symbols[Random.Range(0, symbols.Length)];
        }

        CheckWin();

        isSpinning = false;
        spinButton.interactable = true;

        UpdateUI();
    }

    private void CheckWin()
    {
        Sprite first = reelImages[0].sprite;

        bool won =
            first == reelImages[1].sprite &&
            first == reelImages[2].sprite;

        if (won)
        {
            int payout = betAmount * GetPayoutMultiplier(first);
            balance += payout;

            resultText.text =
                "YOU WIN! +" + payout + " COINS";

            winEffectPanel.SetActive(true);
            hideWinEffectCoroutine =
                StartCoroutine(HideWinEffect());
        }
        else
        {
            resultText.text = "TRY AGAIN!";
            winEffectPanel.SetActive(false);
        }
    }

    private int GetPayoutMultiplier(Sprite winningSymbol)
    {
        int symbolIndex =
            System.Array.IndexOf(symbols, winningSymbol);

        switch (symbolIndex)
        {
            case 0:
                return 2;

            case 1:
                return 3;

            case 2:
                return 5;

            default:
                return 10;
        }
    }

    private IEnumerator HideWinEffect()
    {
        yield return new WaitForSeconds(winEffectDuration);

        if (winEffectPanel != null)
        {
            winEffectPanel.SetActive(false);
        }

        hideWinEffectCoroutine = null;
    }

    private void UpdateUI()
    {
        balanceText.text =
            "BALANCE: " + balance + " COINS";

        betText.text =
            "BET: " + betAmount + " COINS";
    }

    private void OnDestroy()
    {
        if (spinButton != null)
        {
            spinButton.onClick.RemoveListener(Spin);
        }
    }
}
