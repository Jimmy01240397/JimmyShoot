using UnityEngine;
using System.Collections;

public class EnemyHPorReturn : Enemy {

    public int m_HPorReturn;
	// Update is called once per frame
	protected override void Update () {

        UpdateMove();
	}
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;
                if (m_life <= 0)
                {
                    m_GameManager.AddScore(m_point);
                    if (m_HPorReturn == 1)
                    {
                        m_gun.m_life++;
                    }
                    else
                    {
                        m_GameManager.Retran++;
                    }
                    Destroy(this.gameObject);
                }
            }
        }
        else if (other.tag.CompareTo("Player") == 0)
        {
            m_life = 0;
            Destroy(this.gameObject);
        }

        if (other.tag.CompareTo("bound") == 0)
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
