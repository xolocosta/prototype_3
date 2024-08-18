using UnityEngine;
using System.Collections;
public class WallCollideFreeze : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
            StartCoroutine(SuspendMovement());
    }
    private IEnumerator SuspendMovement()
    {
        Rigidbody2D playerRB = this.gameObject.GetComponent<Rigidbody2D>();
        Freeze(playerRB);

        yield return new WaitForSeconds(1);
        UnFreeze(playerRB);
    }
    private void Freeze(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        this.gameObject.GetComponent<Core>().Freeze();
        _animator.SetBool("freeze", true);
    }
    private void UnFreeze(Rigidbody2D rb)
    {
        rb.velocity = (Vector2.zero - this.gameObject.GetComponent<Rigidbody2D>().position).normalized * 4;
        this.gameObject.GetComponent<Core>().UnFreeze();
        _animator.SetBool("freeze", false);
        rb.gravityScale = 1;
    }
}
