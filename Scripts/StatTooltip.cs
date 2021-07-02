using System.Text;
using UnityEngine;
using UnityEngine.UI;
using My.CharacterStats;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] Text statNameText;
    [SerializeField] Text statModifiersLabelText;
    [SerializeField] Text statModifiersText;

    private readonly StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    public void ShowTooltip(CharacterStats stat, string statName)// Disabled until Tooltip is created for stat display
    {
        //statNameText.text = GetStatTopText(stat, statName);
        //statModifiersText.text = GetStatModifiersText(stat);
        //gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        //gameObject.SetActive(false);
    }

    private string GetStatTopText(CharacterStats stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        //sb.Append(stat.Value);

        if(stat.Value != stat.BaseValue)
        {
            sb.Append(" (");
            sb.Append(stat.BaseValue);
            if(stat.Value > stat.BaseValue)
            {
                sb.Append("+");
            }

            sb.Append(System.Math.Round(stat.Value - stat.BaseValue, 4));
            sb.Append(")");
        }
        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStats stat)
    {
        sb.Length = 0;

        foreach (StatModifier mod in stat.StatModifiers)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if(mod.Value > 0)
            {
                sb.Append("+");
            }
            if(mod.Type == StatModType.Flat)
            {
                sb.Append(mod.Value);
            }
            else
            {
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }

            Item item = mod.Source as Item;

            if(item != null)
            {
                sb.Append(" ");
                sb.Append(item.itemName);
            }
            else
            {
                Debug.LogError("Modifier is not an Item!");
            }
        }

        return sb.ToString();
    }
}
