using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BasicMovement : MonoBehaviour
{
    public float speed = 10;
    public float range = 10.0f;
    public float projectileVelocity = 2.0f;

    public Vector2 charPos;
    public Vector2 mousePos;
    public float ang;
    public int health;

    public Animator animator;
    public GameObject projectilePrefab;
    public GameObject crosshair;
    public GameObject healthBar;

    public C_Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        SaveData data = SaveSystem.Load();
        health = 100;
        if(data != null)
        {
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
        timer = new C_Timer(Test, 2.0f, 3, 2.0f);
        timer.setPrecision(2);
    }

    void Test()
    {
        //Debug.Log(timer.GetCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
        CharacterInput();
    }


    void SetCrosshair(Vector3 position)
    {
        position.z = 0.0f;
        crosshair.transform.position = position;
    }

    void CharacterInput()
    {

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        CharacterMovement(movement);

        SetCrosshair(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetButtonDown("Fire1"))
            CharacterFire();

        if (Input.GetButtonDown("Test"))
            transform.position = new Vector3(0, 0, 0);

    }

    void CharacterMovement(Vector2 movement)
    {

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void CharacterFire()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 charPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = (mousePosition - charPosition);
        direction.Normalize();

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation); 
        C_DestroyAfterDistance scriptDestroy = (C_DestroyAfterDistance)projectile.GetComponent(typeof(C_DestroyAfterDistance));
        scriptDestroy.Instantiate(transform.position, range);


        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileVelocity;
        projectile.GetComponent<Animation>().Play();
    }

    void TakeDmg(int amount)
    {
        ChangeSlider slider = (ChangeSlider)healthBar.GetComponent(typeof(ChangeSlider));
        health -= amount;

        if(slider != null)
        {
            slider.ChangeFill(health);
        }
    }
        
    private void OnApplicationQuit()
    {
        SaveSystem.Save(this);
    }

}
