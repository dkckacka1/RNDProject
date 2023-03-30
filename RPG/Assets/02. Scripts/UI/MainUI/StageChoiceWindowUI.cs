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
        UserInfo userInfo;
        List<StageWindowUI> stageWindows = new List<StageWindowUI>();

        [SerializeField] StageWindowUI StageWindowUIPrefab;
        [SerializeField] Transform ScrollViewContextLayout;

        public void Init(UserInfo userInfo)
        {
            this.userInfo = userInfo;
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
                bool isClear = (stage.ID < userInfo.risingTopCount);
                bool isLast = (stage.ID == userInfo.risingTopCount);
                ui.Init(stage, isClear, isLast);

                stageWindows.Add(ui);
            }
        }
    }
}