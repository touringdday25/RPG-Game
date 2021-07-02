using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillList
{
    Agility,
    Strength,
    Smithing
}

public class Skills : MonoBehaviour
{

    //private SkillList myS
    private float baseXP = 60;
    private float xpCurve = 1.8f;
    #region Agility
    [SerializeField]private int agilityLev = 1;
    [SerializeField]private float agilityEXP = 0;
    private float agilityMaxXP = 250;
    #endregion
    #region Strength
    [SerializeField]private int strengthLev = 1;
    [SerializeField]private float strengthEXP = 0;
    private float strengthMaxXP = 250;
    #endregion
    #region Smithing
    [SerializeField] private int smithingLev = 1;
    [SerializeField] private float smithingEXP = 0;
    private float smithingMaxXP = 250;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            agilityEXP += 1000000;
        }
        CalculateNextLevel(SkillList.Agility ,agilityLev, agilityEXP, agilityMaxXP);
        CalculateNextLevel(SkillList.Strength, strengthLev, strengthEXP, strengthMaxXP);
    }

    public void CalculateNextLevel(SkillList skill, float xpToAdd)
    {
        
        switch (skill)
        {
            case SkillList.Agility:
                agilityEXP += xpToAdd;
                while(agilityEXP > agilityMaxXP)
                {
                    agilityEXP -= agilityMaxXP;
                    agilityLev++;
                    Debug.Log("Level Up! " + skill.ToString() + " " + agilityLev);
                    agilityMaxXP += Mathf.Floor(Mathf.Floor(agilityLev + 300 * Mathf.Pow(2, agilityLev / 7)) / 4);
                }
                break;
            case SkillList.Strength:
                strengthEXP += xpToAdd;
                while (strengthEXP > strengthMaxXP)
                {
                    strengthEXP -= strengthMaxXP;
                    strengthLev++;
                    Debug.Log("Level Up! " + skill.ToString() + " " + strengthLev);
                    strengthMaxXP += Mathf.Floor(Mathf.Floor(strengthLev + 300 * Mathf.Pow(2, strengthLev / 7)) / 4);
                }
                break;
            case SkillList.Smithing:
                smithingEXP += xpToAdd;
                while (smithingEXP > smithingMaxXP)
                {
                    smithingEXP -= smithingMaxXP;
                    smithingLev++;
                    Debug.Log("Level Up! " + skill.ToString() + " " + smithingLev);
                    smithingMaxXP += Mathf.Floor(Mathf.Floor(smithingLev + 300 * Mathf.Pow(2, smithingLev / 7)) / 4);
                }
                break;
            default:
                break;
        }
    }

    private void CalculateNextLevel(SkillList skill ,int SkillLevel, float SkillXP, float MaxXP)
    {
        if(SkillXP >= MaxXP)
        {
            while (SkillXP > MaxXP)
            {
                SkillXP -= MaxXP;
                SkillLevel++;
                MaxXP += Mathf.Floor(Mathf.Floor(SkillLevel + 300 * Mathf.Pow(2, SkillLevel / 7)) / 4);
                //MaxXP = Mathf.Pow(((SkillLevel * xpCurve) + baseXP) / 2, xpCurve);

                Debug.Log("Max Xp " + MaxXP.ToString() + " Skill Level " + SkillLevel.ToString());
            }
            if(skill == SkillList.Agility)
            {
                agilityLev = SkillLevel;
                agilityEXP = SkillXP;
                agilityMaxXP = MaxXP;
                this.gameObject.GetComponent<Movement>().UpdateMoveSpeed();
            }
            else if(skill == SkillList.Strength)
            {
                strengthLev = SkillLevel;
                strengthEXP = SkillXP;
                strengthMaxXP = MaxXP;
                this.gameObject.GetComponent<Combat>().UpdateDamage();
            }
        }
    }

    public bool HasLevel(SkillList skill, int requiredLevel)
    {
        switch (skill)
        {
            case SkillList.Agility:
                if (agilityLev >= requiredLevel)
                {
                    return true;
                }
                break;
            case SkillList.Strength:
                if (strengthLev >= requiredLevel)
                {
                    return true;
                }
                break;
            case SkillList.Smithing:
                if(smithingLev >= requiredLevel)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public float AgilityXP
    {
        get { return agilityEXP; }
        set { agilityEXP += value; }
    }

    public float StrengthXP
    {
        get { return strengthEXP; }
        set { strengthEXP += value; }
    }

    public int AgilityLevel
    {
        get { return agilityLev; }
    }

    public int StrengthLevel
    {
        get { return strengthLev; }
    }

}
