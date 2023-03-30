using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythumButton : MonoBehaviour
{
    public GameObject rythumNodePrefab;
    public GameObject goldNodePrefab;
    public Transform nodeSpawnPosition;
    public float nodeSpeed = 1f;
    public ComboText comboText;

    [Space()]
    public Image buttonImage;
    public Sprite baseImage;
    public Sprite clickImage;

    Vector3 magnitude;
    GameObject node;
    nodeType type;

    enum nodeType
    {
        rhythm,
        gold
    }

    private void Start()
    {
        magnitude = (this.transform.position - nodeSpawnPosition.position).normalized;
    }

    public void Update()
    {
        if (node != null)
        {
            node.transform.position += magnitude * Time.deltaTime * nodeSpeed;
            if (node.transform.position.y < -80)
            {
                GameManager.Instance.PlusBadCombo();
                Destroy(node.gameObject);
            }
        }
    }

    public void CreateNode()
    {
        int percent = Random.Range(0, 101);

        if (percent >= 50)
        {
            node = Instantiate(goldNodePrefab, nodeSpawnPosition.position, Quaternion.identity, this.gameObject.transform);
            type = nodeType.gold;
        }
        else
        {
            node = Instantiate(rythumNodePrefab, nodeSpawnPosition.position, Quaternion.identity, this.gameObject.transform);
            type = nodeType.rhythm;
        }

    }

    public void PointerUP()
    {
        print("버튼 땜");
        buttonImage.sprite = baseImage;
    }

    public void PointerDown()
    {
        print("버튼 누름");
        buttonImage.sprite = clickImage;
    }

    public void Clicked()
    {
        if (node != null)
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, node.gameObject.transform.position);
            print(distance);
            if (distance <= 20f)
            {
                print("Cool");
                comboText.gameObject.SetActive(true);
                comboText.text.text = "Cool";
                GameManager.Instance.PlusCoolombo();
                if (type == nodeType.gold)
                {
                    GameManager.Instance.GoldPlus();
                }
                Destroy(node);
            }
            else if (distance > 20f && distance <= 60)
            {
                print("Good");
                comboText.gameObject.SetActive(true);
                comboText.text.text = "Good";
                GameManager.Instance.PlusGoodCombo();
                if (type == nodeType.gold)
                {
                    GameManager.Instance.GoldPlus();
                }
                Destroy(node);
            }
            else
            {
                print("Bad");
                comboText.gameObject.SetActive(true);
                comboText.text.text = "Bad";
                GameManager.Instance.PlusBadCombo();
                Destroy(node);
            }
            
        }
    }
}
