using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSystem : MonoBehaviour
{
    const float moveOffset = 1;

    [SerializeField] private ParticleSystem hitParticle;
    private bool MovingUp =false;
    private bool canClick;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Vector3 defoultPosition;

    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        defoultPosition = transform.position;
    }
    private void Update()
    {
        if (MovingUp)
        {
            if(transform.position.y < defoultPosition.y + moveOffset ) 
            {
                transform.position += new Vector3(0, GameManager.Instance.moleSpeedLevel, 0); 
            }
            else
            {
                MovingUp = false;
            }
        }
        else
        {
            if (transform.position.y > defoultPosition.y ) 
            {
                transform.position += new Vector3(0, -GameManager.Instance.moleSpeedLevel, 0); 
            }
            else
            {
                canClick = false;
            }
           
        }
    }

    private void OnMouseDown()
    {
        if (!canClick) { return; }
        OnHit();

    }
    public void PopUp()
    {
        if (canClick) { return; }

        canClick = true;
        MovingUp = true;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 0);
    }

    private void OnHit()
    {
        canClick = false;
        MovingUp = false;
        hitParticle.Play();
        GameManager.Instance.AddScore(1);
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100);
    }
}
