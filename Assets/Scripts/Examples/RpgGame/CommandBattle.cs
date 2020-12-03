using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.EGUI;
using EGUI.Base;
using EGUI.Examples;
using EGUI.GameObjects;
using Examples.RpgGame.Views;
using UnityEngine;

namespace Examples.RpgGame
{
    public class CommandBattle
    {
        public int AllyIndex = 0;

        public List<Command> Commands = new List<Command>();

        private string message = "";

        public List<Item> TempRemovedItem = new List<Item>();

        private List<Ally> allies = GameInfos.Allies;
        private List<Enemy> enemies = GameInfos.Enemies;

        public RpgCanvas Canvas;
        public CommandsWindow CommandsWindow;
        public EnemiesWindow EnemiesWindow;
        public AlliesWindow AlliesWindow;

        public List<Ally> LivingAllies
        {
            get { return allies.Where(a => a.IsAlive).ToList(); }
        }

        public List<Enemy> LivingEnemies
        {
            get { return enemies.Where(e => e.IsAlive).ToList(); }
        }

        public Ally CommandSelectingAlly
        {
            get
            {
                if (AllyIndex < allies.Count) return allies[AllyIndex];
                return null;
            }
        }

        public CommandBattle()
        {
            Canvas = new RpgCanvas(); 
            new EGGameObject(Canvas).SetImageColor(Color.black).SetRelativeSize(1, 1);

            var enemies = GameInfos.Enemies;
            EnemiesWindow = new EnemiesWindow(Canvas, this, enemies);
            var allies = GameInfos.Allies;
            AlliesWindow = new AlliesWindow(Canvas, this, allies);

            StartNewTurn();
        }
        
        /// <summary>
        /// コマンド選択の初期化処理
        /// </summary>
        public void StartNewTurn()
        {
            AllyIndex = 0;
            Commands = new List<Command>();
            message = "";
            TempRemovedItem = new List<Item>();
            SelectNextCommand();
        }

        public void SelectNextCommand()
        {
            CommandsWindow?.DestroySelf();
            
            // コマンドの実行を開始する
            if (AllyIndex >= allies.Count)
            {
                StartExecutingCommands();
                return;
            }
            if (CommandSelectingAlly.IsAlive)
            {
                AlliesWindow.SetCommand(CommandSelectingAlly, "どうする？");
                CommandsWindow = new CommandsWindow(Canvas, this);
            }
            else
            {
                AllyIndex++;
                SelectNextCommand();
            }
        }

        /// <summary>
        /// 決定されたコマンドをコマンドsリストに追加する
        /// </summary>
        /// <param name="command"></param>
        public void SetCommand(Command command)
        {
            AlliesWindow.SetCommand(CommandSelectingAlly, command.Skill.Name.ToString());
            Commands.Add(command);
            command.User.CurrentCommand = command;
            if (command.Skill.Name == SkillName.アイテム使用)
            {
                var item = GameInfos.Items.Find(i => i.Id == command.itemId);
                TempRemovedItem.Add(item);
            }
            AllyIndex++;
            SelectNextCommand();
        }
        
        /// <summary>
        /// 仲間のコマンドを選択後、コマンドの実行を開始する
        /// </summary>
        public void StartExecutingCommands()
        {
            AlliesWindow.ClearCommandViews();
            AllyIndex = 9999;
            CommandsWindow?.DestroySelf();
            var enemyCommands = new List<Command>();
            LivingEnemies.ForEach(e =>
            {
                var command = e.ChooseCommand();
                enemyCommands.Add(command);
            });
            Commands.AddRange(enemyCommands);
            var ordered = Commands.OrderBy(c => c.User.Agi);
            Commands = new List<Command>(ordered);
            ExecuteNextCommand();
        }

        /// <summary>
        /// コマンドリストから、次のコマンドを実行
        /// </summary>
        public void ExecuteNextCommand()
        {
            // 全部実行したら次のターン
            if (Commands.Count <= 0)
            {
                StartNewTurn();
                return;
            }
            
            var command = Commands.GetAndRemove(0);
            // 死んでいたらスキップ
            if (!command.User.IsAlive)
            {
                ExecuteNextCommand();
                return;
            }
            
            // スキルの対象が死んでいる場合、対象を選択しなおす
            if (command.Skill.Scope == TargetScope.Single && command.Target != null && !command.Target.IsAlive)
            {
                if (command.Target is Ally)
                {
                    var target = allies.Where(a => a.IsAlive).ToList().GetAtRandom();
                    command.Target = target;
                }
                else
                {
                    var target = enemies.Where(e => e.IsAlive).ToList().GetAtRandom();
                    command.Target = target;
                }
            }

            var effectText = "";
            if (command.Skill.Scope == TargetScope.Single)
            {
                 effectText = SkillEffecter.Invoke(command.Skill, command.User, command.Target) + "\n";
            }
            else
            {
                if (command.User is Ally)
                {
                    effectText = SkillEffecter.Invoke(command.Skill, command.User,  enemies) + "\n";
                }
                else
                {
                    effectText = SkillEffecter.Invoke(command.Skill, command.User,  allies) + "\n";
                }
            }

            if (StringUtils.GetLineCount(message + effectText) > 4) message = "";
            message += effectText;
            var queue = new Queue<(string, Action)>();
            queue.Enqueue((message, null));
            // メッセージウィンドウをクリック後の処理
            var onSentEveryMessage = new Action(() =>
            {
                if (enemies.Count(e => e.CurrentHp <= 0) == enemies.Count) ExitBattle(true);
                else if (allies.Count(a => a.CurrentHp <= 0) == allies.Count) ExitBattle(false);
                else ExecuteNextCommand();
            });
            new RpgMessageWindow(Canvas, queue, onSentEveryMessage);
        }
        
        /// <summary>
        /// 戦闘を終了
        /// </summary>
        /// <param name="message"></param>
        public void ExitBattle(bool hasWon)
        {
            var queue = new Queue<(string, Action)>();
            if (hasWon)
            {
                queue.Enqueue(("勝利した", null));
            }
            else
            {
                queue.Enqueue(("全滅した…", null));
            }
            new RpgMessageWindow(Canvas, queue, Canvas.DestroySelf);
        }

        /// <summary>
        /// 味方を選択する画面を表示
        /// </summary>
        /// <param name="command"></param>
        public void ShowAllySelectView(Command command, Action callbackSelected, Action callbackCanceled)
        {
            var label = new AutoResizedTextLabel(Canvas, "誰に？")
                .SetAnchorType(AnchorType.TopLeft)
                .SetPosition(60, -180);
            AlliesWindow.SetSelectMode(id =>
                {
                    var ally = GameInfos.Allies.Find(a => a.Id == id);
                    command.Target = ally; 
                    label.DestroySelf();
                    callbackSelected?.Invoke();
                    SetCommand(command);
                },
                () =>
                {
                    label.DestroySelf();
                    callbackCanceled?.Invoke();
                });
        }


        /// <summary>
        /// 敵を選択する画面を表示
        /// </summary>
        /// <param name="command"></param>
        public void ShowEnemySelectView(Command command, Action callbackSelected, Action callbackCanceled)
        {
            var label = new AutoResizedTextLabel(Canvas, "誰に？")
                .SetAnchorType(AnchorType.BottomLeft)
                .SetPosition(60, 148);
            EnemiesWindow.SetSelectMode(id =>
                {
                    var enemy = GameInfos.Enemies.Find(e => e.Id == id);
                    command.Target = enemy;
                    label.DestroySelf();
                    callbackSelected?.Invoke();
                    SetCommand(command);
                },
                () =>
                {
                    label.DestroySelf();
                    callbackCanceled?.Invoke();
                });
        }
        
    }
}