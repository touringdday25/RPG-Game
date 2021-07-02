using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using My.CharacterStats;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStats _stat;
    public CharacterStats Stat
    {
        get { return _stat; }
        set {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            nameText.text = _name.ToLower();
        }
    }

    public Text nameText;
    public Text valueText;
    [SerializeField] StatTooltip toolTip;

    private bool showingToolTip;


    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if(toolTip == null)
        {
            toolTip = FindObjectOfType<StatTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //toolTip.ShowTooltip(Stat, Name);
        showingToolTip = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //toolTip.HideToolTip();
        showingToolTip = false;
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
        if (showingToolTip)
        {
            //toolTip.ShowTooltip(Stat, Name);
        }
    }
}
