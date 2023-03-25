using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    //������ �������� �� ������������ ��������
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void SetMovingAnimation(bool _isMoving)
    {
        _playerAnimator.SetBool("IsMoving", _isMoving);
    }

    public void SetDamageAnimation()
    {
        _playerAnimator.SetTrigger("GetDamage");
    }
}
