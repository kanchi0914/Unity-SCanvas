using SGUI.Base;
using SGUI.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using static Enums;

public class RPG1
{

    public List<Command> Commands = new List<Command>();

    public List<Enemy> Enemies = new List<Enemy>();

    private List<string> messageList = new List<string>();
    private string message = "";

    private List<Item> tempRemovedItem = new List<Item>();

    private SCanvas canvas;
    //private SCanvas alliesCanvas;
    private SCanvas enemiesCanvas;

    private SSubCanvas commandCanvas;
    private SSubCanvas subCommandView;

    private List<AllyView> allyViews = new List<AllyView>();
    private List<EnemyView> enemyViews = new List<EnemyView>();

    private List<Ally> allies = new List<Ally>();

    private List<Enemy> LivingEnemies
    {
        get
        {
            return Enemies.Where(e => e.IsAlive).ToList();
        }
    }

    private SHorizontalLayoutView allyPanels;

    private int allyIndex;

    private Ally CommandSelectingAlly
    {
        get
        {
            if (allyIndex < allies.Count) return allies[allyIndex];
            else return null;
        }
    }

    public RPG1()
    {
        allies = GameManager.Allies;
        CreateEnemies();

        canvas = new SCanvas("Canvas");
        allyPanels = new SHorizontalLayoutView(canvas)
            .SetLocalPosByRatio(0.1f, 0).SetRectSizeByRatio(0.8f, 0.3f);
        allies.ForEach(a =>
        {
            allyViews.Add(new AllyView(this, a, allyPanels));
        });

        var enemyPanels = new SHorizontalLayoutView(canvas)
            .SetLocalPosByRatio(0.1f, 0.35f).SetRectSizeByRatio(0.8f, 0.3f);
        Enemies.ForEach(e =>
        {
            var view = new EnemyView(this, e, enemyPanels);
            enemyViews.Add(view);
        });

        ShowCommandView();

    }

    public void CreateEnemies()
    {
        var enemy1 = new Enemy("ゴリラ", 20, 0, 10, 10, 5, "Images/animal_gorilla")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        var enemy2 = new Enemy("アリクイ", 15, 0, 10, 12, 7, "Images/animal_ooarikui_arikui")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        var enemy3 = new Enemy("ウシ", 25, 0, 8, 10, 8, "Images/animal_ushi_aurochs")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        Enemies.Add(enemy1);
        Enemies.Add(enemy2);
        Enemies.Add(enemy3);
    }


    public void SetCommand(Command command)
    {
        Commands.Add(command);
        command.User.CurrentCommand = command;
        if (command.Skill.Name == Skill.SkillName.アイテム使用)
        {
            var item = GameManager.Items.Find(i => i.Id == command.itemId);
            tempRemovedItem.Add(item);
        }
        if (Commands.Count == allies.Where(a => a.IsAlive).Count())
        {
            StartExecutingCommands();
        }
        else
        {
            allyIndex++;
            ShowCommandView();
        }
    }

    public void ShowCommandView()
    {
        if (!allies[allyIndex].IsAlive)
        {
            if (allyIndex >= allies.Count)
            {
                StartExecutingCommands();
            }
            allyIndex++;
            ShowCommandView();
        }
        commandCanvas?.Dispose();
        commandCanvas = new SSubCanvas(canvas).SetLocalPosByRatio(0, 0.7f).SetRectSizeByRatio(1f, 0.3f);
        var commandButtonsView = new SGridLayoutView(commandCanvas, 3, 2).SetRectSizeByRatio(0.4f, 1f);

        var attackButton = new SButton(commandButtonsView, "攻撃").SetOnOnClick(() => OnClickedAttack());
        var guardButton = new SButton(commandButtonsView, "防御").SetOnOnClick(() => OnClickedGuard());
        var skillButton = new SButton(commandButtonsView, "スキル").SetOnOnClick(() => OnClickedSkill());
        var itemButton = new SButton(commandButtonsView, "アイテム").SetOnOnClick(() => OnClickedItem());
        var excapeButton = new SButton(commandButtonsView, "逃げる");
    }

    public void OnClickedAttack()
    {
        commandCanvas.Dispose();
        var command = new Command()
        {
            Skill = new Skill(Skill.SkillName.攻撃),
            User = allies[allyIndex]
        };
        ShowEnemySelectView(command);
    }

    public void OnClickedGuard()
    {
        commandCanvas.Dispose();
        var command = new Command()
        {
            Skill = new Skill(Skill.SkillName.防御),
            User = allies[allyIndex]
        };
        SetCommand(command);
    }

    public void OnClickedItem()
    {
        subCommandView?.Dispose();
        subCommandView = new ItemDetailView(this, commandCanvas);
        subCommandView.SetRectSizeByRatio(0.6f, 1f).SetLocalPosByRatio(0.4f, 0);
        subCommandView.SetBackGroundColor(ColorType.White, 0.2f);
    }

    public void OnClickedSkill()
    {
        subCommandView?.Dispose();
        subCommandView = new SkillCommandView(this, allies[allyIndex], commandCanvas);
        subCommandView.SetRectSizeByRatio(0.6f, 1f).SetLocalPosByRatio(0.4f, 0);
        subCommandView.SetBackGroundColor(ColorType.White, 0.2f);
    }

    public void StartNextTurn()
    {
        allyIndex = 0;
        Commands = new List<Command>();
        message = "";
        tempRemovedItem = new List<Item>();
        ShowCommandView();
    }

    public void StartExecutingCommands()
    {
        allyIndex = 9999;
        commandCanvas?.Dispose();
        var enemyCommands = new List<Command>();
        LivingEnemies.ForEach(e =>
        {
            var command = e.ChooseCommand();
            enemyCommands.Add(command);
        });
        Commands.AddRange(enemyCommands);
        Commands.OrderBy(c => c.User.Agi);
        ExecuteNextCommand();
    }

    public void ExecuteNextCommand()
    {
        if (Commands.Count <= 0)
        {
            StartNextTurn();
            return;
        }
        var command = Commands.GetAndRemove(0);
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
                var target = Enemies.Where(e => e.IsAlive).ToList().GetAtRandom();
                command.Target = target;
            }
        }
        var effectText = SkillEffecter.Effect(command.Skill, command.User, command.Target) + "\n";
        if (GetLineCount(message + effectText) > 4) message = "";
        message += effectText;
        var queue = new Queue<(string, Action)>();
        var postProcess = new Action(() =>
        {
            if (Enemies.Count(e => e.CurrentHp <= 0) == Enemies.Count) ExitBattle("戦闘に勝利した");
            else if (allies.Count(a => a.CurrentHp <= 0) == allies.Count) ExitBattle("全滅した....");
            else ExecuteNextCommand();
        });
        queue.Enqueue((message, null));
        var mw = new SMessageWindow(canvas, "message", queue, postProcess)
            .SetLocalPosByRatio(0.15f, 0.65f).SetRectSizeByRatio(0.7f, 0.2f);
        mw.MessageText.SetFontSize(28);
    }

    public void ExitBattle(string message)
    {
        var postProcess = new Action(() =>
        {
            canvas.Dispose();
        });
        var queue = new Queue<(string, Action)>();
        queue.Enqueue((message, null));
        var mw = new SMessageWindow(canvas, "message", queue, postProcess)
            .SetLocalPosByRatio(0.15f, 0.65f).SetRectSizeByRatio(0.7f, 0.2f);
    }

    int GetLineCount(string str)
    {
        string before = str;
        string after = str.Replace("\n", "");
        return before.Length - after.Length;
    }


    public void ShowAllySelectView(Command command)
    {
        var sc = new SCanvas();
        var panel = new SSelectableImage(sc).SetBackGroundColor(ColorType.Black, 0.2f)
            .SetOnClick(() =>
            {
                sc.Dispose();
            });
        var whoPanel = new SImage(sc).SetLocalPosByRatio(0.1f, 0.3f).SetRectSize(100, 50);
        var whoText = new SText(whoPanel, "誰に？").SetRectSizeByRatio(1, 1);
        allyViews
            .Where(av => av.Ally.IsAlive)
            .ToList()
            .ForEach(allyView =>
        {
            var rectSize = allyView.ApparentRectSize;
            var pos = allyView.RectTransform.position;
            var select = new SSelectableImage(sc, keepsAspectRatio: false);
            select.SetBackGroundColor(ColorType.Yellow, 0.2f);
            select.SetRectSize(rectSize.x, rectSize.y);
            select.RectTransform.position = pos;
            select.SetOnClick(() =>
            {
                sc.Dispose();
                command.Target = allyView.Ally;
                SetCommand(command);
            });
        });
    }

    public void ShowEnemySelectView(Command command)
    {
        var sc = new SCanvas();
        var panel = new SSelectableImage(sc).SetBackGroundColor(ColorType.Black, 0.0f)
            .SetOnClick(() =>
            {
                sc.Dispose();
            });
        var whoPanel = new SImage(sc).SetLocalPosByRatio(0.1f, 0.7f).SetRectSize(100, 50);
        var whoText = new SText(whoPanel, "誰に？", true).SetRectSizeByRatio(1, 1);
        enemyViews
            .Where(e => e.Enemy.CurrentHp > 0)
            .ToList()
            .ForEach(enemyView =>
        {
            var rectSize = enemyView.ApparentRectSize;
            var pos = enemyView.RectTransform.position;
            var select = new SSelectableImage(sc);
            select.SetBackGroundColor(ColorType.Yellow, 0.2f);
            select.SetRectSize(rectSize.x, rectSize.y);
            select.RectTransform.position = pos;
            select.SetOnClick(() =>
            {
                sc.Dispose();
                command.Target = enemyView.Enemy;
                SetCommand(command);
            });
        });
    }


    public class EnemyView : SSubCanvas
    {
        RPG1 rpg;
        public Enemy Enemy;
        public SSelectableImage Image;
        public EnemyView(RPG1 rpg, Enemy enemy, SGameObject parent) : base(parent)
        {
            Image = new SSelectableImage(this, imageFilePath:enemy.ImageFilePath, keepsAspectRatio:true);
            Image.SetFullStretchAnchor().SetLocalPos(0, 0);
            Enemy = enemy;
            gameObject.ObserveEveryValueChanged(_ => enemy.CurrentHp)
                .Subscribe(_ => { if (!enemy.IsAlive) Image.SetBackGroundColor(ColorType.White, 0f); });
        }
    }

    public class AllyView : SSubCanvas
    {
        RPG1 rpg;
        public Ally Ally;
        SText nameText;
        SText hpText;
        SText mpText;
        public SSelectableImage Image;
        public AllyView(RPG1 rpg, Ally ally, SGameObject parent) : base(parent)
        {
            this.rpg = rpg;
            this.Ally = ally;
            Image = new SSelectableImage(this, keepsAspectRatio: false)
                .SetBackGroundColor(ColorType.White, 0.2f)
                .SetLocalPosByRatio(0, 0)
                .SetFullStretchAnchor() as SSelectableImage;
            nameText = new SText(this, ally.Name, true)
                .SetLocalPosByRatio(0, 0).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as SText;
            hpText = new SText(this, "", true)
                .SetLocalPosByRatio(0, 0.33f).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as SText;
            mpText = new SText(this, "", true)
                .SetLocalPosByRatio(0, 0.66f).SetRectSizeByRatio(1, 0.33f)
                .SetFullStretchAnchor() as SText;
            OnHpChanged();
            OnMpChanged();
            gameObject.ObserveEveryValueChanged(_ => rpg.allyIndex).Subscribe(_ => OnAllyIndexChanged());
            gameObject.ObserveEveryValueChanged(_ => ally.CurrentHp).Subscribe(_ => OnHpChanged());
            gameObject.ObserveEveryValueChanged(_ => ally.CurrentMp).Subscribe(_ => OnMpChanged());
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

    public class ItemDetailView : SSubCanvas
    {
        RPG1 rpg;
        public ItemDetailView(RPG1 rpg, SGameObject parent) : base(parent)
        {
            this.rpg = rpg;
            var itemsView = new SVerticalGridScrollView(this, 100, 2)
                .SetMiddleCenterAnchor().SetLocalPos(0, 0).SetRectSizeByRatio(0.9f, 0.9f)
                .SetFullStretchAnchor();
            GameManager.Items
                .Where(i => !(rpg.tempRemovedItem.Contains(i)))
                .ToList().ForEach(item =>
            {
                if (rpg.tempRemovedItem.Contains(item))
                {

                }
                var itemButton = new SButton(itemsView, item.Name.ToString());
                var command = new Command()
                {
                    itemId = item.Id,
                    User = GameManager.Allies[rpg.allyIndex],
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

    public class SkillCommandView : SSubCanvas
    {
        RPG1 rpg;
        Ally ally;
        public SkillCommandView(RPG1 rpg, Ally ally, SGameObject parent) : base(parent)
        {
            this.rpg = rpg;
            var skillsView = new SVerticalGridScrollView(this, 100, 2)
                .SetMiddleCenterAnchor().SetLocalPos(0, 0).SetRectSizeByRatio(0.9f, 0.9f)
                .SetFullStretchAnchor();
            ally.Skills
                .Where(s => s.ConsumptionMp < ally.CurrentMp)
                .ToList().ForEach(skill =>
                {
                    var skillButton = new SButton(skillsView, skill.Name.ToString());
                    var command = new Command()
                    {
                        User = GameManager.Allies[rpg.allyIndex],
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