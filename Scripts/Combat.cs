using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private float reach;
    private Camera myCamera;
    private Skills mySkills;
    [SerializeField]private Transform startPos;
    [SerializeField]private Transform endPos;
    [SerializeField]private GameObject myLArm;
    private bool isPunching;
    private float punchSpeed = 1.6f;
    private float startTime;
    private float punchDist;
    private float damage;
    private float strMod;
    private bool invOpen;

    private void Awake()
    {
        GameObject.FindObjectOfType<InventoryInput>().InvOpen += Combat_InvOpen;
        myCamera = Camera.main;
        mySkills = this.gameObject.GetComponent<Skills>();
        //myLArm = GetComponentInChildren<Transform>().gameObject;
    }

    private void Combat_InvOpen(bool obj)
    {
        invOpen = obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        reach = 3;
        damage = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myLArm.transform.position == endPos.position)
        {
            isPunching = false;
            myLArm.transform.position = startPos.position;
        }
        if (isPunching)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * punchSpeed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / punchDist;

            myLArm.transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);

        }
        if (Input.GetMouseButtonDown(0) && !isPunching && !invOpen)
        {
            Punch();

        }
    }

    public void UpdateDamage()
    {
        damage = (GetComponent<Character>().Strength.Value + (mySkills.StrengthLevel / 3)) + 3;
        Debug.Log("Damage :" + damage);
    }

    private void Punch()
    {
        startTime = Time.time;
        punchDist = Vector3.Distance(startPos.position, endPos.position);
        isPunching = true;
        RaycastHit hit;
        if(Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, reach))
        {
            if (hit.transform.gameObject.name.Contains("Enemy"))
            {
                hit.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                mySkills.StrengthXP = 1000f;
                Debug.Log("Damage: " + damage);
            }
        }
    }
}
