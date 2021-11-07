using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AttackBehavior
{
    public bool Debug = false;
    public KeyCode Keybind;

    LineRenderer lineRenderer;

    SpriteRenderer MageStaff;

    static float nextFire;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        MageStaff = GetMageStaffRenderer();

        if (MageStaff == null)
        {
            MageStaff = GetComponent<SpriteRenderer>();
        }

        if (Debug) lineRenderer = DebugTools.AddLineRenderer(gameObject);
    }

    public override void FixedUpdate()
    {
        if (Time.time >= nextFire)
            base.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Keybind))
        {
            Position = MageStaff.transform.position; //MageStaff.bounds.center;
            AttackDirection = MouseController.GetDirectionToMouse(Position);
            TargetPoint = MouseController.GetTargetpoint();

            IsAttacking = true;

            if (Debug) DebugTools.DrawLine(lineRenderer, Position, TargetPoint);
        }
        else
        {
            IsAttacking = false;
        }

    }

    public override float CalcNextFire()
    {
        nextFire = base.CalcNextFire();
        return base.CalcNextFire();
    }

    private SpriteRenderer GetMageStaffRenderer()
    {
        Transform mageStaffTransform = gameObject.transform.Find("Staff");

        if (mageStaffTransform != null)
        {
            SpriteRenderer renderer = mageStaffTransform.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                return renderer;
            }
        }
        return null;
    }

}
