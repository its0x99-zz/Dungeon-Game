using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    // Text fields in UI
    public Text levelText, hitPointText, coinText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    // Update the characters sprite
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Update Information in the Character Menu
    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }


        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitPoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();

        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "total experience points"; // Display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);
            int difference = currentLevelXp - prevLevelXp;
            int currentXpIntoLevel = GameManager.instance.experience - prevLevelXp;
            float levelCompletion = (float)currentXpIntoLevel / (float)difference;
            xpText.text = currentXpIntoLevel.ToString() + " / " + difference;
            xpBar.localScale = new Vector3(levelCompletion, 1, 1);
        }

        
    }
}
