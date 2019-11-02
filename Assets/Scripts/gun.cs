using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour 
{
    public float m_speed = 20;
    public float m_life = 5;
    protected Transform m_transform;
    public Transform[] m_rocket;
    float m_rocketRate = 0;
    public delegate void rocketchoosing(out int i);
    public event rocketchoosing ChooseRocket;
    protected int m_rocketchoose = -1;
	// Use this for initialization
	void Start () 
    {
        m_transform = this.transform;
        ChooseRocket(out m_rocketchoose);
        m_life = change.Life;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float moveh = 0;
        m_rocketRate -= Time.deltaTime;

        // 按左鍵
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveh -= m_speed * Time.deltaTime;
        }

        // 按右鍵
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveh += m_speed * Time.deltaTime;
        }

        if (m_rocketRate <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_rocketRate = 0.3f;
                Instantiate(m_rocket[m_rocketchoose], m_transform.position, m_transform.rotation);
                ChooseRocket(out m_rocketchoose);
            }
        }
        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }

        // 移動
        this.m_transform.Translate(new Vector3(moveh, 0, 0));
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("bound1") == 0)
        {
            this.m_transform.position = new Vector3(-7.5f, -4.5f, 0);
        }
        if (other.tag.CompareTo("bound2") == 0)
        {
            this.m_transform.position = new Vector3(7.5f, -4.5f, 0);
        }
        if (other.tag.CompareTo("Ememy") == 0)
        {
            m_life--;

            if (m_life <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Fire()
    {
        if (m_rocketRate <= 0)
        {
            m_rocketRate = 0.3f;
            Instantiate(m_rocket[m_rocketchoose], m_transform.position, m_transform.rotation);
            ChooseRocket(out m_rocketchoose);
        }
    }
}
