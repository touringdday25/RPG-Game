using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace My.CharacterStats
{

    [Serializable]
    public class CharacterStats
    {
        public float BaseValue;

        protected bool updateRequired = true;
        protected float lastBaseValue = float.MinValue;


        protected float _value;
        public virtual float Value
        {
            get
            {
                if (updateRequired || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    updateRequired = false;
                }
                return _value;
            }
        }


        protected readonly List<StatModifier> statMods;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStats()
        {
            statMods = new List<StatModifier>();
            StatModifiers = statMods.AsReadOnly();
        }

        public CharacterStats(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifer(StatModifier mod)
        {
            updateRequired = true;
            statMods.Add(mod);
            statMods.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifer(StatModifier mod)
        {
            if (statMods.Remove(mod))
            {
                updateRequired = true;
                return true;

            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statMods.Count - 1; i >= 0; i--)
            {
                if(statMods[i].Source == source)
                {
                    updateRequired = true;
                    didRemove = true;
                    statMods.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if(a.Order < b.Order)
            {
                return -1;
            }
            else if(a.Order > b.Order)
            {
                return 1;
            }
            return 0;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statMods.Count; i++)
            {
                StatModifier mod = statMods[i];

                if(mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if(mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if(i + 1 >= statMods.Count || statMods[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}
