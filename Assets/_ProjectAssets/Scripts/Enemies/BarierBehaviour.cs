using UnityEngine;

public class BarierBehaviour : MonoBehaviour
{
    
    public float speed;
    public Constants.BarrierType barrierType;

    private bool canMove;
    
    public void Update()
    {
        if(canMove)
            transform.position += new Vector3(0,-speed *Time.deltaTime,0);
    }

    
    public void Appear(Constants.BarrierPosition barrierPosition)
    {
        switch (barrierType)
        {
            case Constants.BarrierType.LittleBarrier:
            {
                Move(DefinePosition(barrierPosition,Constants.littleBarrierAppearPosition));
                break;
            }
            case Constants.BarrierType.MediumBarrier:
            {
                Move(DefinePosition(barrierPosition,Constants.mediumBarrierAppearPosition));
                break;
            }
            case Constants.BarrierType.BigBarrier:
            {
                Move(DefinePosition(barrierPosition,Constants.bigBarrierAppearPosition));
                break;
            }
        }
    }

    private Vector3 DefinePosition(Constants.BarrierPosition barrierPosition,Vector3 direction)
    {
        if (barrierPosition == Constants.BarrierPosition.Left)
        {
            return direction;
        }
        
        direction.x *= -1;
        return direction;
    }
    
    private void Move(Vector3 destination)
    {
        LeanTween.move(gameObject, destination, 1f)
            .setEaseInQuad().setOnComplete(() =>
            {
                Debug.Log(destination);
                canMove = true;
            });
    }
}
