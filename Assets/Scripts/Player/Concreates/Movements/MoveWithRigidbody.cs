using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Player.Abstracts.Movements;
using UnityEngine;

namespace Game.Player.Concreates.Movement
{
    public class MoveWithRigidbody : IPlayerMovement
    {
        private Rigidbody2D rb;
        private BoxCollider2D collider;
        private ContactFilter2D contactFilter;
        private List<RaycastHit2D> hit;

        public float speed { get; set; }
        private float jumpForce;
        private float collisionOffset;
        private float currentScale;
        private float gravity;

        private Vector2 halfSize;
        private Vector2 halfOffset;

        private bool isGrounded;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        public MoveWithRigidbody(Rigidbody2D rb, ContactFilter2D contactFilter, float speed, float jumpForce, float collisionOffset)
        {
            this.rb = rb;
            this.contactFilter = contactFilter;
            this.speed = speed;
            this.collisionOffset = collisionOffset;
            this.jumpForce = jumpForce;

            gravity = -9.81f;

            collider = rb.GetComponent<BoxCollider2D>();
            hit = new List<RaycastHit2D>();

            currentScale = rb.transform.localScale.x;

            halfSize = new Vector2(0, collider.size.y * 0.25f);
            halfOffset = new Vector2(0, collider.offset.y * -4f);
        }

        public void Move(float horizontal)
        {
            Vector2 direction = new Vector2(horizontal, 0);
            if(direction != Vector2.zero)
            {
                bool isMoving = TryMove(direction);
                if(!isMoving)
                {
                    direction = new Vector2(-horizontal, 0);
                    TryMove(direction);
                }
            }
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            isGrounded = !TryApplyGravity();
        }

        private bool TryMove(Vector2 direction)
        {
            int collisionCount = rb.Cast(direction, contactFilter, hit, collisionOffset + speed * Time.fixedDeltaTime);

            if (collisionCount == 0)
            {
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
                return true;
            }
            return false;
        }

        private bool TryApplyGravity()
        {
            int collisionCount = rb.Cast(Vector2.down, contactFilter, hit, collisionOffset + speed * Time.fixedDeltaTime);

            float gravityScaler = 1f;
            if (collisionCount == 0)
            {
                rb.MovePosition(rb.position + Vector2.up * gravity * gravityScaler * Time.fixedDeltaTime);
                gravityScaler *= 1.5f;
                return true;
            }
            return false;
        }

        public void Rotate(float horizontal)
        {
            if (horizontal == 0) return;
            float sign = horizontal > 0 ? currentScale : -1 * currentScale;
            rb.transform.localScale = new Vector3(sign, currentScale, currentScale);
        }

        public void Crouch(bool isCrouch)
        {
            if (isCrouch)
            {
                collider.size -= halfSize;
                collider.offset -= halfOffset;
            }
            else if (!isCrouch)
            {
                collider.size += halfSize;
                collider.offset += halfOffset;
            }
        }

        public void Jump()
        {
            if (isGrounded)
            {
               JumpAsync();
            }
        }
        //TODO: Jump 
        private async void JumpAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                rb.MovePosition((rb.transform.position + rb.transform.up) / 10);
                await Task.Delay(10);
            }
        }
    }
}
