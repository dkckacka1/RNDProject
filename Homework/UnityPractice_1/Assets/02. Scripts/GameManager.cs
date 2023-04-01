using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text GoldText;
    public Text ComboText;
    public ResultUI resultUI;

    public Button failButton;
    public RawImage[] heartImages;
    public RythumButton[] buttons;
    public Animator gameCharacterAnimation;


    public int gold;
    public int CoolCombo;
    public int badCombo;
    public int GoodCombo;
    public int heart = 5;
    public int comboCount;

    public bool isPlaying = true;
    public bool isTee = false;
    public bool isDoorOpening = false;
    public float createNodeTime = 3f;

    [Header("Audio")]
    public AudioSource audio;
    public AudioClip cash;
    public AudioClip coin;
    public AudioClip coo;
    public AudioClip prec;
    public AudioClip pop;
    public AudioClip tap;
    public AudioClip universal02;

    private float nodeTimer;

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

    private void Start()
    {
        audio.clip = universal02;
        audio.Play();
    }

    private void Update()
    {
        if (isPlaying)
        {
            nodeTimer += Time.deltaTime;

            if (nodeTimer > createNodeTime)
            {
                CreateNode();
                nodeTimer = 0;
            }
        }
    }

    public void ShowResultUI()
    {
        this.resultUI.gameObject.SetActive(true);
        this.resultUI.Init(gold,GoodCombo,badCombo,CoolCombo);
    }

    public void GameFail()
    {
        // 패배 유아이 보여주기
        isPlaying = false;
        failButton.gameObject.SetActive(true);
        gameCharacterAnimation.SetTrigger("Dead");
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
        gameCharacterAnimation.SetInteger("comboCount", comboCount);
        ComboText.text = comboCount.ToString();
    }

    public void PlusBadCombo()
    {
        comboCount = 0;
        gameCharacterAnimation.SetInteger("comboCount", comboCount);
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
        if (isPlaying)
        {
            heartImages[heart - 1].gameObject.SetActive(false);
            heart--;
        }
        if (heart <= 0)
        {
            print("실패");
            GameFail();
        }
    }

    public void TakeTee()
    {
        isTee = true;
        gameCharacterAnimation.SetBool("isTake", isTee);
    }

    public void DanceStart()
    {
        isTee = false;
        gameCharacterAnimation.SetBool("isTake", isTee);
    }
}
