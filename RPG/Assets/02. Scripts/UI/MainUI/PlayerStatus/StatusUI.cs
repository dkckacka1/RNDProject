using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Main.UI.StatusUI
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] UserinfoDescUI userinfoUI;
        [SerializeField] PlayerStatusDescUI statusUI;

        private void OnEnable()
        {
            //transform.DOScale(Vector3.one, scaleAnimDuration).SetEase(Ease.InOutBounce);
        }

        public void Exit(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}