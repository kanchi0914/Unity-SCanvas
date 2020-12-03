using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Examples.RpgGame.Views
{
    public class CommandsWindow : EGGameObject
    {
        private CommandsTypeWindow commandTypeWindow;
        public CommandBattle CommandBattle;

        public CommandsWindow(RpgCanvas canvas, CommandBattle commandBattle) : base(canvas)
        {
            CommandBattle = commandBattle;
            SetAnchorType(AnchorType.BottomCenter)
                .SetPosition(0, 10)
                .SetSize(480, 128);
            Reset();
            var canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            Observable.NextFrame().Subscribe(_ => { canvasGroup.alpha = 1; });
        }

        public void Reset()
        {
            commandTypeWindow?.DestroySelf();
            commandTypeWindow = new CommandsTypeWindow(this, CommandBattle);
        }

        private class CommandsTypeWindow : EGGameObject
        {
            private CommandsWindow commandsWindow;
            private EGGameObject disabledPanel;
            private List<CommandTextLabel> commandTextLabels = new List<CommandTextLabel>();
            private EGGameObject image;
            private CommandBattle commandBattle;

            public CommandsTypeWindow(CommandsWindow commandsWindow, CommandBattle commandBattle) : base(commandsWindow)
            {
                this.commandsWindow = commandsWindow;
                this.commandBattle = commandBattle;

                SetAnchorType(AnchorType.MiddleLeft)
                    .SetSize(170, commandsWindow.RectSize.y);
                image = new EGGameObject(this)
                    .SetRelativeSize(1, 1)
                    .SetImage("Images/clear_box");
                var commands = new EGGameObject(this).SetRelativeSize(1, 1);

                var layoutView = new EGVerticalLayoutView(commands, isAutoAlignment: true)
                    .SetRelativeSize(1, 1);
                var attackComamndLabel = new CommandTextLabel(layoutView, "攻撃", OnAttackSelected)
                    .SetRelativeSize(1, .25f)
                    .As<CommandTextLabel>();
                var skillComamndLabel = new CommandTextLabel(layoutView, "特技", OnSkillSelected).SetRelativeSize(1, .25f)
                    .As<CommandTextLabel>();
                var itemComamndLabel = new CommandTextLabel(layoutView, "アイテム", OnItemSelected)
                    .SetRelativeSize(1, .25f).As<CommandTextLabel>();
                var guardComamndLabel = new CommandTextLabel(layoutView, "防御", OnGuardSelected).SetRelativeSize(1, .25f)
                    .As<CommandTextLabel>();
                commandTextLabels.AddItems(attackComamndLabel, skillComamndLabel, itemComamndLabel, guardComamndLabel);
            }

            void OnAttackSelected()
            {
                Disable();
                var command = new Command()
                {
                    Skill = new Skill(SkillName.攻撃),
                    User = commandsWindow.CommandBattle.CommandSelectingAlly
                };
                commandsWindow.CommandBattle.ShowEnemySelectView(command, null, () => commandsWindow.Reset());
            }

            void OnGuardSelected()
            {
                var command = new Command()
                {
                    Skill = new Skill(SkillName.防御),
                    User = commandsWindow.CommandBattle.CommandSelectingAlly
                };
                commandsWindow.CommandBattle.SetCommand(command);
            }

            void OnSkillSelected()
            {
                Disable();
                new SkillCommandsCanvas(commandsWindow, commandBattle, commandBattle.CommandSelectingAlly);
            }

            void OnItemSelected()
            {
                Disable();
                new ItemCommandsCanvas(commandsWindow, commandBattle, commandBattle.CommandSelectingAlly);
            }

            void Disable()
            {
                image.SetImageColor(Color.gray);
                commandTextLabels.ForEach(label => { label.Disable(); });
            }
        }

        private class SkillCommandsCanvas : SubCommandsCanvas
        {
            public SkillCommandsCanvas(CommandsWindow commandsWindow, CommandBattle commandBattle, Ally ally) : base(
                commandsWindow)
            {
                if (!ally.Skills.Any())
                {
                    new RpgText(subCommandsWindow).SetText("特技なし");
                    return;
                }

                ally.Skills.ForEach(skill =>
                {
                    var command = new Command()
                    {
                        User = ally,
                        Skill = skill
                    };
                    // スキル名クリック時に呼ばれるメソッド
                    var onSelected = new Action(() =>
                    {
                        if (skill.Target == TargetType.Ally)
                        {
                            if (skill.Scope == TargetScope.Single)
                            {
                                Disable();
                                commandsWindow.CommandBattle.ShowAllySelectView(command, DestroySelf, Enable);
                            }
                            else
                            {
                                DestroySelf();
                                commandsWindow.CommandBattle.SetCommand(command);
                            }
                        }
                        else
                        {
                            if (skill.Scope == TargetScope.Single)
                            {
                                if (commandBattle.LivingEnemies.Count == 1)
                                {
                                    DestroySelf();
                                    commandsWindow.CommandBattle.SetCommand(command);
                                }
                                else
                                {
                                    Disable();
                                    commandsWindow.CommandBattle.ShowEnemySelectView(command, DestroySelf, Enable);
                                }
                            }
                            else
                            {
                                DestroySelf();
                                commandsWindow.CommandBattle.SetCommand(command);
                            }
                        }
                    });

                    var commandTextLabel = new CommandTextLabel(layoutGrid, skill.Name.ToString(),
                        () => { onSelected(); },
                        () => { descriptionTextLabel.SetText($"{skill.Description} MP{skill.ConsumptionMp,2}"); },
                        () => { descriptionTextLabel.SetText(""); });

                    if (skill.ConsumptionMp > ally.CurrentHp) commandTextLabel.Disable();
                });
            }
        }

        private class ItemCommandsCanvas : SubCommandsCanvas
        {
            public ItemCommandsCanvas(CommandsWindow commandsWindow, CommandBattle commandBattle, Ally ally) : base(
                commandsWindow)
            {
                GameInfos.Items
                    .Where(i => !(GameInfos.TempRemovedItems.Contains(i)))
                    .ToList().ForEach(item =>
                    {
                        var command = new Command()
                        {
                            itemId = item.Id,
                            User = GameInfos.Allies[commandsWindow.CommandBattle.AllyIndex],
                            Skill = new Skill(SkillName.アイテム使用)
                        };
                        var onSelected = new Action(() =>
                        {
                            if (item.Target == TargetType.Ally)
                            {
                                if (item.Scope == TargetScope.Single)
                                {
                                    Disable();
                                    commandsWindow.CommandBattle.ShowAllySelectView(command, () =>
                                    {
                                        DestroySelf();
                                        GameInfos.TempRemovedItems.Add(item);
                                    }, Enable);
                                }
                                else
                                {
                                    DestroySelf();
                                    GameInfos.TempRemovedItems.Add(item);
                                    commandsWindow.CommandBattle.SetCommand(command);
                                }
                            }
                            else
                            {
                                if (item.Scope == TargetScope.Single)
                                {
                                    if (commandBattle.LivingEnemies.Count == 1)
                                    {
                                        DestroySelf();
                                        GameInfos.TempRemovedItems.Add(item);
                                        commandsWindow.CommandBattle.SetCommand(command);
                                    }
                                    else
                                    {
                                        Disable();
                                        commandsWindow.CommandBattle.ShowEnemySelectView(command, () =>
                                        {
                                            DestroySelf();
                                            GameInfos.TempRemovedItems.Add(item);
                                        }, Enable);
                                    }
                                }
                                else
                                {
                                    DestroySelf();
                                    GameInfos.TempRemovedItems.Add(item);
                                    commandsWindow.CommandBattle.SetCommand(command);
                                }
                            }
                        });

                        var commandTextLabel = new CommandTextLabel(layoutGrid, item.Name.ToString(),
                            () => { onSelected(); },
                            () => { descriptionTextLabel.SetText($"{item.Description}"); },
                            () => { descriptionTextLabel.SetText(""); });
                    });
            }
        }
    }
}