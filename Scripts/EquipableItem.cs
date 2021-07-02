using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.CharacterStats;

public enum EquipmentType
{
    Helmet,
    Chest,
    Pants,
    Boots,
    Gloves,
    Ring,
    Amulet,
    Bracelet,
    Weapon,
    Weapon2
}

[CreateAssetMenu]
public class EquipableItem : Item
{
    public int strengthBonus;
    public int intelligenceBonus;
    public int vitalityBonus;
    public int armorBonus;
    [Space]
    public float strengthPercentBonus;
    public float intelligencePercentBonus;
    public float vitalityPercentBonus;
    public float armorPercentBonus;
    [Space]
    public EquipmentType equipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        //Destroy(this);
    }

    public void Equip(Character c)
    {
        if(strengthBonus != 0)
        {
            c.Strength.AddModifer(new StatModifier(strengthBonus, StatModType.Flat, this));
        }
        if (intelligenceBonus != 0)
        {
            c.Intelligence.AddModifer(new StatModifier(intelligenceBonus, StatModType.Flat, this));
        }
        if (vitalityBonus != 0)
        {
            c.Vitality.AddModifer(new StatModifier(vitalityBonus, StatModType.Flat, this));
        }
        if(armorBonus != 0)
        {
            c.Armor.AddModifer(new StatModifier(armorBonus, StatModType.Flat, this));
        }

        if (strengthPercentBonus != 0)
        {
            c.Strength.AddModifer(new StatModifier(strengthPercentBonus, StatModType.PercentMult, this));
        }
        if (intelligencePercentBonus != 0)
        {
            c.Intelligence.AddModifer(new StatModifier(intelligencePercentBonus, StatModType.PercentMult, this));
        }
        if (vitalityPercentBonus != 0)
        {
            c.Vitality.AddModifer(new StatModifier(vitalityPercentBonus, StatModType.PercentMult, this));
        }
        if(armorPercentBonus != 0)
        {
            c.Armor.AddModifer(new StatModifier(armorPercentBonus, StatModType.PercentMult, this));
        }
    }

    public void UnEquip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Vitality.RemoveAllModifiersFromSource(this);
        c.Armor.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return equipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(strengthBonus, "Strength");
        AddStat(intelligenceBonus, "Intelligence");
        AddStat(vitalityBonus, "Vitality");
        AddStat(armorBonus, "Armor");

        AddStat(strengthPercentBonus, "Strength", isPercent: true);
        AddStat(intelligencePercentBonus, "Intelligence", isPercent: true);
        AddStat(vitalityPercentBonus, "Vitality", isPercent: true);
        AddStat(armorPercentBonus, "Armor", isPercent: true);

        //AddStat(value, "Value");

        return sb.ToString();
    }

    public override string GetItemValue()
    {
        return "Value: $" + value.ToString();
    }

    public void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if(value > 0)
            {
                sb.Append("+");
            }
            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
