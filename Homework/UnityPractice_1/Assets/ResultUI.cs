using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    public Text gold;
    public Text good;
    public Text bad;
    public Text cool;

    public void Init(int gold, int good, int bad, int cool)
    {
        this.gold.text = gold.ToString();
        this.good.text = good.ToString();
        this.bad.text = bad.ToString();
        this.cool.text = cool.ToString();

    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
