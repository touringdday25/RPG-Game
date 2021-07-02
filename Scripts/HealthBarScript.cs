using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Health player;
    [SerializeField][Tooltip("Update Speed In Seconds.")] private float updateSpeed;

    private void Awake()
    {
        if(player == null)
        {
            GetComponentInParent<EnemyHealth>().OnHealthPctChanged += HandleHealthChanged;
        }
        else
        {
            player.OnHealthPctChanged += HandleHealthChanged;
            Debug.Log("Set Player Health Info.");
        }
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangedPct = healthBar.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeed);
            yield return null;
        }
    }

    private void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform);
    }
}
