using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBehavior
{

    public GameObject DashSpell;

    private DashSpell _dashSpell;
    private float _dashCooldown;
    private float _nextDash = 0;

    private bool _isDashing;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        _dashSpell = DashSpell.GetComponent<DashSpell>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.y = Input.GetAxisRaw("Vertical");
        direction.x = Input.GetAxisRaw("Horizontal");
        _isDashing = Input.GetKey(KeyCode.Space);

        //FindObjectOfType<AudioManager>().Play("Steps"); // Step-Audio
    }

    public override void FixedUpdate()
    {
        if (CanMove)
        {
            base.FixedUpdate();

            if (_dashSpell != null && _isDashing && Time.time >= _nextDash)
            {
                Dash();
                _nextDash = Time.time + _dashSpell.Cooldown/1000;
            }
        }
    }

    void Dash()
    {
        GameObject dashObject = Instantiate(DashSpell, gameObject.transform.position, Quaternion.identity);

        DashSpell dashSpell = dashObject.GetComponent<DashSpell>();
        dashSpell.Initialize(gameObject, direction, MouseController.GetTargetpoint(), 0);
    }


}
