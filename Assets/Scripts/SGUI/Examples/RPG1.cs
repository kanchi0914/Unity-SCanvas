using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class CommandBattleRpg
{

    public List<Command> Commands = new List<Command>();

    private List<string> messageList = new List<string>();
    private string message = "";

    private List<Item> tempRemovedItem = new List<Item>();

    private EGCanvas mainCanvas;
    private EGCanvas enemiesCanvas;

    private EgSubCanvas commandsView;
    private EgSubCanvas subCommandsView;

    private List<AllyView> allyViews = new List<AllyView>();
    private List<EnemyView> enemyViews = new List<EnemyView>();

    private List<Ally> allies = new List<Ally>();
    private List<Enemy> enemies = new List<Enemy>();

    public List<Ally> LivingAllies
    {
        get
        {
            return allies.Where(a => a.IsAlive).ToList();
        }
    }

    public List<Enemy> LivingEnemies
    {
        get
        {
            return enemies.Where(e => e.IsAlive).ToList();
        }
    }

    public Ally CommandSelectingAlly
    {
        get
        {
            if (allyIndex < allies.Count) return allies[allyIndex];
            else return null;
        }
    }

    private EgHorizontalLayoutView alliesView;

    private int allyIndex;
    
    public CommandBattleRpg()
    {
        mainCanvas = new EGCanvas("Canvas");

        allies = GameInfos.Allies;
        enemies = GameInfos.Enemies;

        var alliesView = new EgHorizontalLayoutView(mainCanvas, isAutoSizingWidth:false)
            .SetPresetRect(RectInfos.alliesView);

        allies.ForEach(ally =>
        {
            var view = new AllyView(this, ally, alliesView).SetPresetRect(RectInfos.allyView) as AllyView;
            allyViews.Add(view);
        });

        var enemiesView = new EgHorizontalLayoutView(mainCanvas)
            .SetPresetRect(RectInfos.enemiesView) as EgHorizontalLayoutView;
        enemies.ForEach(e =>
        {
            enemyViews.Add(new EnemyView(this, e, enemiesView));
        });

        ShowCommandsView();

    }

    /// <summary>
    /// 決定されたコマンドをコマンドsリストに追加する
    /// </summary>
    /// <param name="command"></param>
    public void SetCommand(Command command)
    {
        Commands.Add(command);
        command.User.CurrentCommand = command;
        if (command.Skill.Name == Skill.SkillName.アイテム使用)
        {
            var item = GameInfos.Items.Find(i => i.Id == command.itemId);
            tempRemovedItem.Add(item);
        }
        // 味方全員のコマンドを選び終わったら実行
        if (Commands.Count == LivingAllies.Count())
        {
            StartExecutingCommands();
        }
        else
        {
            allyIndex++;
            ShowCommandsView();
        }
    }

    /// <summary>
    /// コマンド選択画面を表示
    /// </summary>
    public void ShowCommandsView()
    {
        // 味方が死亡していた場合
        if (!CommandSelectingAlly.IsAlive)
        {
            if (allyIndex >= allies.Count)
            {
                StartExecutingCommands();
            }
            allyIndex++;
            ShowCommandsView();
        }
        commandsView?.Dispose();
        commandsView = new EgSubCanvas(mainCanvas).SetPresetRect(RectInfos.commandsView) as EgSubCanvas;

        var commandButtonsView = new EGGridLayoutView(commandsView, 3, 2).SetPresetRect(RectInfos.commandButtons);
        var attackButton = new EGButton(commandButtonsView, "攻撃").SetOnOnClick(() => OnClickedAttack());
        var guardButton = new EGButton(commandButtonsView, "防御").SetOnOnClick(() => OnClickedGuard());
        var skillButton = new EGButton(commandButtonsView, "スキル").SetOnOnClick(() => OnClickedSkill());
        var itemButton = new EGButton(commandButtonsView, "アイテム").SetOnOnClick(() => OnClickedItem());
        var excapeButton = new EGButton(commandButtonsView, "逃げる");
    }

    public void OnClickedAttack()
    {
        var command = new Command()
        {
            Skill = new Skill(Skill.SkillName.攻撃),
            User = allies[allyIndex]
        };
        ShowEnemySelectView(command);
    }

    public void OnClickedGuard()
    {
        commandsView.Dispose();
        var command = new Command()
        {
            Skill = new Skill(Skill.SkillName.防御),
            User = allies[allyIndex]
        };
        SetCommand(command);
    }

    public void OnClickedItem()
    {
        subCommandsView?.Dispose();
        subCommandsView = new ItemSubCommandsView(this, commandsView);
        subCommandsView.SetBackGroundColor(ColorType.White, 0.2f);
    }

    public void OnClickedSkill()
    {
        subCommandsView?.Dispose();
        subCommandsView = new SkillCommandView(this, allies[allyIndex], commandsView);
        subCommandsView.SetBackGroundColor(ColorType.White, 0.2f);
    }

    /// <summary>
    /// コマンド選択の初期化処理
    /// </summary>
    public void StartNewTurn()
    {
        allyIndex = 0;
        Commands = new List<Command>();
        message = "";
        tempRemovedItem = new List<Item>();
        ShowCommandsView();
    }

    /// <summary>
    /// 仲間のコマンドを選択後、コマンドの実行を開始する
    /// </summary>
    public void StartExecutingCommands()
    {
        allyIndex = 9999;
        commandsView?.Dispose();
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
        if (command.Target != null && !command.Target.IsAlive)
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
        var effectText = SkillEffecter.Effect(command.Skill, command.User, command.Target) + "\n";
        if (GetLineCount(message + effectText) > 4) message = "";
        message += effectText;
        var queue = new Queue<(string, Action)>();
        queue.Enqueue((message, null));
        // メッセージウィンドウをクリック後の処理
        var onSentEveryMessage = new Action(() =>
        {
            if (enemies.Count(e => e.CurrentHp <= 0) == enemies.Count) ExitBattle("戦闘に勝利した");
            else if (allies.Count(a => a.CurrentHp <= 0) == allies.Count) ExitBattle("全滅した....");
            else ExecuteNextCommand();
        });
        var mw = new EgMessageWindow(mainCanvas, queue, onSentEveryMessage)
            .SetPresetRect(RectInfos.messageWindow) as EgMessageWindow;
        mw.MessageText.SetFontSize(28);
    }

    /// <summary>
    /// 戦闘を終了
    /// </summary>
    /// <param name="message"></param>
    public void ExitBattle(string message)
    {
        var postProcess = new Action(() =>
        {
            mainCanvas.Dispose();
        });
        var queue = new Queue<(string, Action)>();
        queue.Enqueue((message, null));
        var mw = new EgMessageWindow(mainCanvas, queue, postProcess).SetPresetRect(RectInfos.messageWindow);
    }

    int GetLineCount(string str)
    {
        string before = str;
        string after = str.Replace("\n", "");
        return before.Length - after.Length;
    }

    /// <summary>
    /// 味方を選択する画面を表示
    /// </summary>
    /// <param name="command"></param>
    public void ShowAllySelectView(Command command)
    {
        var sc = new EGCanvas();
        var panel = new EgSelectableImage(sc).SetBackGroundColor(ColorType.Black, 0.2f)
            .SetOnClick(() =>
            {
                sc.Dispose();
            });
        var alliesView = new EgSubCanvas(sc).SetPresetRect(RectInfos.alliesView);
        var whoPanel = new EgImage(sc).SetPresetRect(RectInfos.whomAllyPanel);
        var whoText = new EGText(whoPanel, "誰に？").SetRectSizeByRatio(1,1);
        var index = 0;
        allyViews
            .Where(av => av.Ally.IsAlive)
            .ToList()
            .ForEach(allyView =>
        {
            var rectSize = allyView.ApparentRectSize;
            var pos = allyView.GameObject.transform.position;
            var select = new EgSelectableImage(alliesView);
            select.SetBackGroundColor(ColorType.Yellow, 0.2f);
            select.SetRectSize(rectSize.x, rectSize.y);
            select.SetGlobalPos(pos);
            select.SetOnClick(() =>
            {
                sc.Dispose();
                command.Target = allyView.Ally;
                SetCommand(command);
            });
            index++;
        });
    }

    /// <summary>
    /// 敵を選択する画面を表示
    /// </summary>
    /// <param name="command"></param>
    public void ShowEnemySelectView(Command command)
    {
        var sc = new EGCanvas();
        var panel = new EgSelectableImage(sc).SetBackGroundColor(ColorType.Black, 0.0f)
            .SetOnClick(() =>
            {
                sc.Dispose();
            });
        var whoPanel = new EgImage(sc).SetPresetRect(RectInfos.whomEnemyPanel);
        var whoText = new EGText(whoPanel, "誰に？", true);
        enemyViews
            .Where(e => e.Enemy.CurrentHp > 0)
            .ToList()
            .ForEach(enemyView =>
        {
            var rectSize = enemyView.ApparentRectSize;
            var pos = enemyView.RectTransform.position;
            var select = new EgSelectableImage(sc);
            select.SetBackGroundColor(ColorType.Yellow, 0.2f);
            select.SetRectSize(rectSize.x, rectSize.y);
            select.SetGlobalPos(pos);
            select.SetOnClick(() =>
            {
                sc.Dispose();
                command.Target = enemyView.Enemy;
                SetCommand(command);
            });
        });
    }

    /// <summary>
    /// 敵画像を表示する画面
    /// </summary>
    public class EnemyView : EgSubCanvas
    {
        CommandBattleRpg rpg;
        public Enemy Enemy;
        public EgSelectableImage Image;
        public EnemyView(CommandBattleRpg rpg, Enemy enemy, EGGameObject parent) : base(parent)
        {
            Image = new EgSelectableImage(this, imageFilePath:enemy.ImageFilePath, keepsAspectRatio:true);
            Image.SetFullStretchAnchor().SetLocalPos(0, 0);
            Enemy = enemy;
            enemy.AddOnHpDecreased(_ => 
            {
                if (!enemy.IsAlive) Image.SetBackGroundColor(ColorType.White, 0f);
                AnimateBlink();
            });
        }
    }

    /// <summary>
    /// 味方の情報を表示する画面
    /// </summary>
    public class AllyView : EgSubCanvas
    {
        CommandBattleRpg rpg;
        public Ally Ally;
        EGText nameText;
        EGText hpText;
        EGText mpText;
        public EgSelectableImage Image;
        public AllyView(CommandBattleRpg rpg, Ally ally, EGGameObject parent) : base(parent)
        {
            this.rpg = rpg;
            this.Ally = ally;
            SetPresetRect(RectInfos.allyView);
            Image = new EgSelectableImage(this, keepsAspectRatio: false)
                .SetBackGroundColor(ColorType.White, 0.2f)
                .SetFullStretchAnchor() as EgSelectableImage;
            nameText = new EGText(this, ally.Name, true)
                .SetLocalPosByRatio(0, 0).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as EGText;
            hpText = new EGText(this, "", true)
                .SetLocalPosByRatio(0, 0.33f).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as EGText;
            mpText = new EGText(this, "", true)
                .SetLocalPosByRatio(0, 0.66f).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as EGText;
            OnHpChanged();
            OnMpChanged();
            gameObject.ObserveEveryValueChanged(_ => rpg.allyIndex).Subscribe(_ => OnAllyIndexChanged());
            gameObject.ObserveEveryValueChanged(_ => ally.CurrentHp).Subscribe(_ => OnHpChanged());
            gameObject.ObserveEveryValueChanged(_ => ally.CurrentMp).Subscribe(_ => OnMpChanged());
            ally.AddOnHpDecreased(_ => this.AnimateShake());
        }

        private void OnAllyIndexChanged()
        {
            if (rpg.CommandSelectingAlly == Ally) Image.SetBackGroundColor(ColorType.Green, 0.2f);
            else if (Ally.IsAlive) Image.SetBackGroundColor(ColorType.White, 0.2f);
        }

        private void OnHpChanged()
        {
            hpText.SetText($"HP: {Ally.CurrentHp}/{Ally.MaxHp}");
            if (!Ally.IsAlive) Image.SetBackGroundColor(ColorType.Black, 0.2f);
        }

        private void OnMpChanged()
        {
            mpText.SetText($"HP: {Ally.CurrentMp}/{Ally.MaxMp}");
        }
    }

    public class ItemSubCommandsView : EgSubCanvas
    {
        CommandBattleRpg rpg;
        public ItemSubCommandsView(CommandBattleRpg rpg, EGGameObject parent) : base(parent)
        {
            this.rpg = rpg;
            SetPresetRect(RectInfos.subCommandsView);
            var itemsView = new EgVerticalGridScrollView(this, 100, 2)
                .SetPresetRect(RectInfos.subCommands);
            GameInfos.Items
                .Where(i => !(rpg.tempRemovedItem.Contains(i)))
                .ToList().ForEach(item =>
            {
                var itemButton = new EGButton(itemsView, item.Name.ToString());
                var command = new Command()
                {
                    itemId = item.Id,
                    User = GameInfos.Allies[rpg.allyIndex],
                    Skill = new Skill(Skill.SkillName.アイテム使用) 
                };
                itemButton.SetOnOnClick(() =>
                {
                    if (item.Scope == TargetScope.Single)
                    {
                        rpg.ShowAllySelectView(command);
                    }
                    else
                    {
                        rpg.SetCommand(command);
                    }
                });
            });
        }
    }

    public class SkillCommandView : EgSubCanvas
    {
        //CommandBattleRpg rpg;
        //Ally ally;
        public SkillCommandView(CommandBattleRpg rpg, Ally ally, EGGameObject parent) : base(parent)
        {
            //this.rpg = rpg;
            SetPresetRect(RectInfos.subCommandsView);
            var skillsView = new EgVerticalGridScrollView(this, 100, 2)
                .SetPresetRect(RectInfos.subCommands);
            ally.Skills
                .Where(s => s.ConsumptionMp < ally.CurrentMp)
                .ToList().ForEach(skill =>
                {
                    var skillButton = new EGButton(skillsView, skill.Name.ToString());
                    var command = new Command()
                    {
                        User = GameInfos.Allies[rpg.allyIndex],
                        Skill = skill
                    };
                    skillButton.SetOnOnClick(() =>
                    {
                        if (skill.Scope == TargetScope.Single)
                        {
                            if (skill.Target == TargetType.Ally) rpg.ShowAllySelectView(command);
                            else if (skill.Target == TargetType.Enemy) rpg.ShowEnemySelectView(command);
                            else rpg.SetCommand(command);
                        }
                        else
                        {
                            rpg.SetCommand(command);
                        }
                    });
                });
        }
    }

}