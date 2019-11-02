using UnityEngine;
using System.Collections;

public class boss : Enemy {

    public Transform[] m_enemy;
    public int ColorNumber;
    protected override void Update()
    {
        m_GameManager.m_bosslife = (int)m_life;
        UpdateMove();
    }
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                if (m_color.CompareTo(rocket.m_color) == 0)
                {
                    m_life -= rocket.m_power;
                    m_transform.Translate(new Vector3(0, 20 * Time.deltaTime, 0));

                    if (m_life <= 0)
                    {
                        m_GameManager.m_bosslife = (int)m_life;
                        m_GameManager.AddScore(m_point);
                        change.NewScore = m_GameManager.m_score;
                        change.Life = m_gun.m_life;
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Enemy enemy = m_enemy[ColorNumber].gameObject.GetComponent<Enemy>();
                    enemy.m_speed = 15;
                    enemy.bossMake = true;
                    Instantiate(m_enemy[ColorNumber], m_transform.position, Quaternion.identity);
                    enemy.m_speed = 2;
                    enemy.bossMake = false;
                }
            }
        }
        else if (other.tag.CompareTo("Player") == 0)
        {
            m_life = 0;
            m_gun.m_life = 0;
            Destroy(this.gameObject);
        }

        if (other.tag.CompareTo("bound") == 0)
        {
            m_life = 0;
            m_gun.m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
