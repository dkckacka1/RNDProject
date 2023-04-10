using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Core;

namespace RPG.Main.UI
{
    public class StageChoiceWindowUI : MonoBehaviour
    {
        List<StageWindowUI> stageWindows = new List<StageWindowUI>();

        [SerializeField] StageWindowUI StageWindowUIPrefab;
        [SerializeField] Transform ScrollViewContextLayout;

        public void Init()
        {
            CreateStageWindow();
        }

        public void CreateStageWindow()
        {
            var stageList = GameManager.Instance.stageDataDic
                .Select(item => item.Value)
                .ToList();

            foreach (var stage in stageList)
            {
                StageWindowUI ui = Instantiate<StageWindowUI>(this.StageWindowUIPrefab, ScrollViewContextLayout);
                bool isClear = (stage.ID < GameManager.Instance.UserInfo.risingTopCount);
                bool isLast = (stage.ID == GameManager.Instance.UserInfo.risingTopCount);
                ui.Init(stage, isClear, isLast);

                stageWindows.Add(ui);
            }
        }
    }
}