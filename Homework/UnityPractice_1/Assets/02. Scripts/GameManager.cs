using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text GoldText;
    public Text ComboText;

    public RawImage[] heartImages;
    public RythumButton[] buttons;
    public Animator animator;

    public int gold;
    public int CoolCombo;
    public int badCombo;
    public int GoodCombo;
    public int heart = 5;
    public int comboCount;

    public bool isPlaying = true;
    public float createNodeTime = 3f;

    private float nodeTimer;
    private float knockTimer;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            nodeTimer += Time.deltaTime;
            knockTimer += Time.deltaTime;

            if (nodeTimer > createNodeTime)
            {
                CreateNode();
                nodeTimer = 0;
            }
        }
    }

    private void CreateNode()
    {
        int randomButtonIndex = UnityEngine.Random.Range(0, this.buttons.Length);
        buttons[randomButtonIndex].CreateNode();
    }

    public void PlusGoodCombo()
    {
        CoolCombo++;
        NiceTiming();
    }

    public void PlusCoolombo()
    {
        GoodCombo++;
        NiceTiming();
    }

    public void NiceTiming()
    {
        comboCount++;
        animator.SetInteger("comboCount", comboCount);
        ComboText.text = comboCount.ToString();
    }

    public void PlusBadCombo()
    {
        comboCount = 0;
        animator.SetInteger("comboCount", comboCount);
        ComboText.text = comboCount.ToString();
        BadTiming();
    }

    public void GoldPlus()
    {
        gold += 10;
        GoldText.text = gold.ToString();
    }

    public void BadTiming()
    {
        heartImages[heart - 1].gameObject.SetActive(false);
        heart--;
        if (heart == 0)
        {
            print("실패");
            isPlaying = false;
        }
    }

    public void TakeTee()
    {
        animator.SetBool("isTake", true);
    }

    public void DanceStart()
    {
        animator.SetBool("isTake", false);
    }
    #region ButtonPlugin


    #endregion
}
