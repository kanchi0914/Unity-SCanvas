using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Assets.Scripts.Examples.AdvGame.Objects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    /// <summary>
    /// シナリオのモデルクラス
    /// </summary>
    public class Scenario
    {
        
        protected CanvasRenderer Renderer = CanvasRenderer.Instance;
            
        public Dictionary<string, List<Section>> Scripts =
            new Dictionary<string, List<Section>>();
        
        // 現在再生中のスクリプト
        public List<Section> CurrentScript = new List<Section>();
        // 現在再生中のスクリプトID(セーブデータに必要)
        public string CurrentScriptId;
        // 現在のスクリプト中、何番目のセクションを表示しているか
        public int SectionNumber = -1;
        // ログ表示に必要な部分
        public List<Section> SentSections = new List<Section>();

        public string ScenarioName => GetType().Name;

        public Scenario ()
        {
            Renderer.SetNewScenario(this);
            InitScripts();
        }

        protected virtual void InitScripts()
        {
        }

        // メッセージ送り
        public void SendSection()
        {
            SectionNumber++;
            // スクリプト中の全セクションを表示し終わった
            if (SectionNumber >= CurrentScript.Count)
            {
                Renderer.CloseMessageWindow();
                return;
            }
            var section = CurrentScript[SectionNumber];
            SentSections.Add(section);
            Renderer.SetTalkerText(section.Talker);
            Renderer.SetMessageText(section.Text);
            section.Action?.Invoke();
        }

        public void LoadScript(string scriptId = null, int sectionNumber = -1)
        {
            SectionNumber = -1;
            if (scriptId == null) scriptId = Scripts.First().Key;
            CurrentScriptId = scriptId;
            CurrentScript = Scripts[scriptId];
            while (sectionNumber >= 0)
            {
                SendSection();
                sectionNumber--;
            }
            SendSection();
        }

    }
}