﻿using System;
using System.Collections.Generic;
using UniRx;

public class Unit
{
    public string Name { get; set; }
    public string Id { get; set; }
    public int MaxHp { get; set; }
    public int CurrentHp
    {
        get
        {
            return currentHp.Value;
        }
        set
        {
            if (value > MaxHp) currentHp.Value = MaxHp;
            else if (value < 1) currentHp.Value = 0;
            else currentHp.Value = value;
        }
    }
    private ReactiveProperty<int> currentHp = new ReactiveProperty<int>(0);

    public int MaxMp { get; set; }
    public int CurrentMp
    {
        get
        {
            return currentMp;
        }
        set
        {
            if (value > MaxMp) currentMp = MaxMp;
            else if (value < 1) currentMp = 0;
            else currentMp = value;
        }
    }
    private int currentMp;
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Agi { get; set; }
    public string ImageFilePath { get; set; }
    public bool IsAlive { get { return CurrentHp > 0; } }

    public bool IsCommandSelectable => !IsAlive;

    public Command CurrentCommand { get; set; }

    public List<Skill> Skills = new List<Skill>();
    
    public Unit(string name, string id, int hp, int mp, int atk, int def, int agi, string imageFilePath = null)
    {
        Name = name;
        Id = id;
        CurrentHp = MaxHp = hp;
        CurrentMp = MaxMp = mp;
        Atk = atk;
        Def = def;
        Agi = agi;
        ImageFilePath = imageFilePath;
    }

    /// <summary>
    /// HPが減少したときのみ呼ばれるメソッドを設定する
    /// </summary>
    /// <param name="action"></param>
    public void AddOnHpDecreased(Action<int> action)
    {
        currentHp.Zip(currentHp.Skip(1), (x, y) => new Tuple<int, int>(x, y)).
            Subscribe(t =>
            {
                if (t.Item1 > t.Item2)
                {
                    action.Invoke(t.Item1);
                }
            });
    }

    public override string ToString()
    {
        return Name;
    }

}
