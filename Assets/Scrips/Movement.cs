﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.25f;
    [SerializeField]
    float rayLength = 1.4f;
    [SerializeField]
    float rayOffsetX = 0.5f;
    [SerializeField]
    float rayOffsetY = 0.5f;
    [SerializeField]
    float rayOffsetZ = 0.5f;

    Vector3 targetPosition;
    Vector3 startPosition;
    bool moving;

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            if (Vector3.Distance(startPosition, transform.position) > 1f) {
                transform.position = targetPosition;
                moving = false;
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (CanMove(Vector3.forward)) {
                targetPosition = transform.position + (Vector3.forward * 2);
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (CanMove(Vector3.back)) {
                targetPosition = transform.position + (Vector3.back * 2);
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (CanMove(Vector3.left)) {
                targetPosition = transform.position + (Vector3.left * 2);
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (CanMove(Vector3.right)) {
                targetPosition = transform.position + (Vector3.right * 2);
                startPosition = transform.position;
                moving = true;
            }
        } 
    }

    bool CanMove(Vector3 direction) {
        if (Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction)) {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, rayLength)) return false;
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction)) {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
        }
        return true;
    }
}

