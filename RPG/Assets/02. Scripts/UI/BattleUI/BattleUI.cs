using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;
using RPG.Battle.Ability;
using RPG.Character.Equipment;
using RPG.Character.Status;

namespace RPG.Battle.UI
{
    public class BattleUI : MonoBehaviour
    {
        public Canvas battleCanvas;

        [Header("ReadyUI")]
        [SerializeField] TextMeshProUGUI floorCountText;
        [SerializeField] TextMeshProUGUI readyText;
        [SerializeField] float removeReadyUITime = 1f;

        [Header("looting")]
        public Image backpack;

        [Header("PlayerUI")]
        // PlayerUI
        public PlayerHPBar playerHPBar;

        [Header("BattleResultUI")]
        public BattleResultWindow resultUI;

        [Header("AbilityButton")]
        public AbilityButton helmetAbility;
        public AbilityButton PantsAbility;

        public void InitAbility(Helmet helmet, Pants pants, BattleStatus status)
        {
            if (helmet.suffix != null && helmet.suffix.isIncantAbility)
            {
                HelmetIncant incant = helmet.suffix as HelmetIncant;

                helmetAbility.gameObject.SetActive(true);
                helmetAbility.Init(helmet.suffix.abilityIcon, incant.skillCoolTime);
                helmetAbility.AbilityBtn.onClick.AddListener(() => { incant.ActiveSkill(status); });
            }
            else
            {
                helmetAbility.gameObject.SetActive(false);
            }

            if (pants.suffix != null && pants.suffix.isIncantAbility)
            {
                PantsIncant incant = helmet.suffix as PantsIncant;

                PantsAbility.gameObject.SetActive(true);
                PantsAbility.Init(helmet.suffix.abilityIcon, incant.skillCoolTime);
                PantsAbility.AbilityBtn.onClick.AddListener(() => { incant.ActiveSkill(status); });
            }
            else
            {
                PantsAbility.gameObject.SetActive(false);
            }
        }

        public void ShowReady()
        {
            readyText.text = "준비";
            readyText.gameObject.SetActive(true);
        }

        public void ShowStart()
        {
            readyText.text = "시작";
            StartCoroutine(RemoveReadyUI(removeReadyUITime));
        }

        public void ShowWin()
        {
            readyText.text = "승리!";
            readyText.gameObject.SetActive(true);

        }

        public void ShowDefeat()
        {
            readyText.text = "패배~";
            readyText.gameObject.SetActive(true);

        }

        public void ShowResultUI()
        {
            resultUI.gameObject.SetActive(true);
        }

        private IEnumerator RemoveReadyUI(float duration)
        {
            yield return new WaitForSeconds(duration);
            readyText.gameObject.SetActive(false);
        }
    }
}
