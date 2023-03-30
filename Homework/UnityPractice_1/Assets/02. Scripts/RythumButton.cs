using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythumButton : MonoBehaviour
{
    public GameObject rythumNodePrefab;
    public GameObject node;
    public Transform nodeSpawnPosition;
    public float nodeSpeed = 1f;
    public Text comboText;
    public Vector3 magnitude;
    private void Start()
    {
        node = Instantiate(rythumNodePrefab, nodeSpawnPosition.position, Quaternion.identity, this.gameObject.transform);
        magnitude = (this.transform.position - nodeSpawnPosition.position).normalized;
    }

    public void Update()
    {
        if (node != null)
        {
            node.transform.position += magnitude * Time.deltaTime * nodeSpeed;
            if (node.transform.position.y < -80)
            {
                Destroy(node.gameObject);
            }
        }
    }

    public void ButtonDown()
    {
        if (node != null)
        {

            
        }
    }
}
