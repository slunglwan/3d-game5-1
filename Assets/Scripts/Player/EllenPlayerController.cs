using System;
using UnityEngine;

public class EllenPlayerController : PlayerController, IWeaponObserver<GameObject>
{
    [SerializeField] private Transform weaponAttachTransform;
    
    private MeleeController _meleeController;                   
    
    private void Start()
    {
        // 무기 할당
        var staffObject = Resources.Load<GameObject>("Weapon/Staff");
        _meleeController = Instantiate(staffObject, weaponAttachTransform).GetComponent<MeleeController>();
        _meleeController.Subscribe(this);
    }
    
    public void MeleeAttackStart()
    {
        _meleeController.StartTrigger();
    }

    public void MeleeAttackEnd()
    {
        _meleeController.EndTrigger();
    }

    public void OnNext(GameObject value)
    {
        var enemyController = value.GetComponent<EnemyController>();
        if (enemyController)
        {
            enemyController.SetHit(30, transform.forward);
        }
    }

    public void OnCompleted()
    {
        _meleeController.Unsubscribe(this);
    }

    public void OnError(Exception error)
    {
    }
}
