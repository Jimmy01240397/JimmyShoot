using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    // 速度
    public float m_speed = 1;

    // 生命
    public float m_life = 1;

    protected Transform m_transform;

    public int m_point = 1;

    public string m_color;
    public bool bossMake = false;
    protected Vector3 getPlayerXY;

    protected GameManager m_GameManager;
    public GameManager GameManager
    {
        get { return m_GameManager; }
    }
    protected gun m_gun;

	// Use this for initialization
	void Start () {

        m_transform = this.transform;
        if (bossMake)
        {
            m_point = 0;
        }
        GameObject obj = GameObject.FindGameObjectWithTag("GameManager");
        if (obj != null)
        {
            m_GameManager = obj.GetComponent<GameManager>();
        }
        GameObject obj2 = GameObject.FindGameObjectWithTag("Player");
        if (obj2 != null)
        {
            m_gun = obj2.GetComponent<gun>();
        }

        if(m_GameManager.Stop)
        {
            getPlayerXY = new Vector3(m_gun.transform.position.x, m_gun.transform.position.y, 0);
        }
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {

        if (!m_GameManager.Stop || bossMake)
        {
            UpdateMove();
        }
	}

    protected virtual void UpdateMove()
    {
        if (!bossMake)
        {
            // 前進
            m_transform.Translate(new Vector3(0, -m_speed * Time.deltaTime, 0));
        }
        else
        {
            Move();
            if(m_transform.position == getPlayerXY)
            {
                getPlayerXY.y = -7;
            }
        }
    }

    protected void Move()
    {
        float directX = getPlayerXY.x - m_transform.position.x;
        float directY = getPlayerXY.y - m_transform.position.y;
        float NextX = m_transform.position.x;
        float NextY = m_transform.position.y;
        float SpeedX;
        float SpeedY;
        float _directX = directX;
        float _directY = directY;
        if (_directX < 0)
        {
            _directX *= -1;
        }
        if (_directY < 0)
        {
            _directY *= -1;
        }
        if (_directX == _directY)
        {
            SpeedX = m_speed;
            SpeedY = m_speed;
            if (directX > 0)
            {
                NextX += SpeedX * Time.deltaTime;
                if (NextX > getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            else
            {
                NextX -= SpeedX * Time.deltaTime;
                if (NextX < getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            if (directY > 0)
            {
                NextY += SpeedY * Time.deltaTime;
                if (NextY > getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
            else
            {
                NextY -= SpeedY * Time.deltaTime;
                if (NextY < getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
        }
        else if (_directX > _directY)
        {
            float x = _directX / m_speed;
            SpeedY = _directY / x;
            SpeedX = m_speed;
            if (directX > 0)
            {
                NextX += SpeedX * Time.deltaTime;
                if (NextX > getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            else
            {
                NextX -= SpeedX * Time.deltaTime;
                if (NextX < getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            if (directY > 0)
            {
                NextY += SpeedY * Time.deltaTime;
                if (NextY > getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
            else
            {
                NextY -= SpeedY * Time.deltaTime;
                if (NextY < getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
        }
        else if (_directX < _directY)
        {
            float x = _directY / m_speed;
            SpeedX = _directX / x;
            SpeedY = m_speed;
            if (directX > 0)
            {
                NextX += SpeedX * Time.deltaTime;
                if (NextX > getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            else
            {
                NextX -= SpeedX * Time.deltaTime;
                if (NextX < getPlayerXY.x)
                {
                    NextX = getPlayerXY.x;
                }
            }
            if (directY > 0)
            {
                NextY += SpeedY * Time.deltaTime;
                if (NextY > getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
            else
            {
                NextY -= SpeedY * Time.deltaTime;
                if (NextY < getPlayerXY.y)
                {
                    NextY = getPlayerXY.y;
                }
            }
        }
        m_transform.position = new Vector3(NextX, NextY, 0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                if (m_color.CompareTo(rocket.m_color) == 0)
                {
                    m_life -= rocket.m_power;

                    if (m_life <= 0)
                    {
                        m_GameManager.AddScore(m_point);
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    m_speed = 15;
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
        if (other.tag.CompareTo("boss") == 0)
        {
            if (!bossMake)
            {
                boss Boss = other.GetComponent<boss>();
                Boss.m_life++;
                Boss.m_point++;
                Destroy(this.gameObject);
            }
        }
    }
}
