using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI.EnemyHealthBar
{
    class Enemy : MonoBehaviour
    {
        public Slider HPbar;
        public float hpHeight = 1f;

        private void Update()
        {
            Vector3 hpBarWorldPosition =
                Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hpHeight, transform.position.z));
            HPbar.GetComponent<RectTransform>().position = hpBarWorldPosition;
        }
    }
}
