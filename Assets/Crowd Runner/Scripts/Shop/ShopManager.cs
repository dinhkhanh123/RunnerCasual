using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinButtons;


    [Header(" Skins ")]
    [SerializeField] private Sprite[] skins;

    [Header(" Pricing ")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header(" Events ")]
    public static Action<int> onSkinSelected;


    private void Awake()
    {
        UnlockSkin(0);

        priceText.text = skinPrice.ToString();
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        ConfigureButtons();
        UpdatePurchaseButton();

        yield return null;

        SelectSkin(GetLastSelectedSkin());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UnlockSkin(Random.Range(0, skinButtons.Length));
        if (Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll();

    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;


            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        //  Debug.Log("Skin "+skinIndex+" has been selected");

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();
            else
                skinButtons[i].Deselect();
        }

        onSkinSelected?.Invoke(skinIndex);

        SaveLastSelectesSkin(skinIndex);
    }



    public void PurchaseSkin()
    {

        List<SkinButton> skinButtonList = new List<SkinButton>();

        for (int i = 0; i < skinButtons.Length; i++)
            if (!skinButtons[i].IsUnlocked())
                skinButtonList.Add(skinButtons[i]);

        if (skinButtonList.Count <= 0)
            return;



        SkinButton ramdomSkinButton = skinButtonList[Random.Range(0, skinButtonList.Count)];

        UnlockSkin(ramdomSkinButton);
        SelectSkin(ramdomSkinButton.transform.GetSiblingIndex());

        DataManager.instance.UseCoins(skinPrice);

        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        if (DataManager.instance.GetCoin() < skinPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }


    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectesSkin(int skinIndex)
    {
         PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
    }
        
}
