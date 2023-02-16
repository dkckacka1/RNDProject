using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UI.ButtonSystem
{
    public class SkillButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public GameObject skillRange;
        GameObject clickSkill;
        bool isDrag = false;

        public void OnDrag(PointerEventData eventData)
        {
            isDrag = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 10f);
            foreach (var hit in hits)
            {
                if (hit.transform.tag == "Plane")
                {
                    this.clickSkill.transform.position = hit.point;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 10f);
            foreach (var hit in hits)
            {
                if (hit.transform.tag == "Plane")
                {
                    clickSkill = Instantiate(skillRange, hit.point, Quaternion.identity);
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isDrag && clickSkill)
            {
                clickSkill.GetComponent<SkillRange>().skillAction.Invoke();
                print("스킬 사용!");
                isDrag = false;
                Destroy(clickSkill);
            }
        }
    }
}
