using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;


public class EnemyLogic : MonoBehaviour 
{
  public enum EnemyState
  {
    Disabled,
    Still,
    Patrolling,
    Suspicious,
    Alerted,
    Eliminate
  };
  // modifiers
  public float walkSpeed;
  public float runSpeed;
  public List<Transform> patrolPath;
  public Tilemap map;


  public GameObject player;

  // runtime variables
  int currentPatrolPoint = 0;
  int patrolDirection = 1;
  bool needsPath = true;
  bool pathing = false;
  EnemyState state = EnemyState.Patrolling;
  Vector3 pathTarget;
  Vector3 pathPoint;
  List<Vector3> path;
  float attentionTimer;
  float soundTimer;

  Vector3 lastMove;


  bool canSeePlayer = false;


	// Use this for initialization
	void Start () 
  {
    path = new List<Vector3>();
    pathPoint = gameObject.transform.position;
    foreach(var pt in patrolPath)
    {
      pt.position.Set(pt.position.x, pt.position.y, gameObject.transform.position.z);
    }
	}

  public void OnFoundPath(Path p)
  {
    if(p.error)
    {
      // death
      // change state to still probably
      Debug.Log("path failed.");
    }
    else
    {
      //Debug.Log("ON NEW PATH");
      needsPath = false;
      path.Clear();
      path = p.vectorPath;
      path.Reverse();
    }
    pathing = false;
  }

  void PathTo(Vector3 target)
  {
    //Debug.Log("target is" + target);
    if(!pathing)
    {
      target.z = gameObject.transform.position.z;
      pathTarget = target;
      Seeker seeker = gameObject.GetComponent<Seeker>();
      seeker.StartPath(gameObject.transform.position, target, OnFoundPath);
      pathing = true;
    }
  }

	// Update is called once per frame
	void FixedUpdate () 
  {
    soundTimer -= Time.fixedDeltaTime;
    attentionTimer -= Time.fixedDeltaTime;
    //Debug.Log(attentionTimer);
    float moveSpeed = walkSpeed;
    switch(state) 
    {
      case EnemyState.Disabled:
        // do nothing herp derp
        moveSpeed = 0.0f;
        break;
      case EnemyState.Still:
        moveSpeed = 0.0f;
        break;
      case EnemyState.Patrolling:
        if(needsPath)
        {
          if(!pathing)
          {
            PathTo(patrolPath[currentPatrolPoint].position);
          }
        }
        else if((gameObject.transform.position - pathTarget).sqrMagnitude < 0.01f)
        {
          // find next patrol point

          //Debug.Log("NEXT NODE");
          if(currentPatrolPoint == patrolPath.Count - 1)
          {
            patrolDirection = -1;
          }
          else if(currentPatrolPoint == 0)
          {
            patrolDirection = 1;
          }
          currentPatrolPoint += patrolDirection;
          if (!pathing)
          {
            PathTo(patrolPath[currentPatrolPoint].position);
          }
        }
        break;
      case EnemyState.Suspicious:
        moveSpeed = walkSpeed * 0.5f;        
        if(needsPath)
        {
          if(!pathing)
          {
            PathTo(player.transform.position);
          }
        }
        else
        {
          if(attentionTimer < 1.0f)
          {
            moveSpeed = 0.0f;
          }
          else if(attentionTimer < 0.0f)
          {
            state = EnemyState.Patrolling;
            //Debug.Log("back 2 calm");
            Debug.Log("suspect");
            needsPath = true;
          }
        }
        break;
      case EnemyState.Alerted:
        moveSpeed = walkSpeed;        
        if(needsPath)
        {
          if(!pathing)
          {
            PathTo(player.transform.position);
          }
        }
        else
        {
          if(attentionTimer < 0.0f)
          {
            attentionTimer = 4.0f;
            //Debug.Log("back 2 suspect");
            state = EnemyState.Suspicious;
            Debug.Log("alerted");
            needsPath = true;
          }
        }
        break;
      case EnemyState.Eliminate:
        moveSpeed = runSpeed;
        if(needsPath)
        {
          if(!pathing)
          {
            PathTo(player.transform.position);
            Debug.Log(path.Count);
            Debug.Log("tried path");
          }
        }
        else
        {
          if(attentionTimer < 0.0f)
          {
            attentionTimer = 8.0f;
            //Debug.Log("back 2 alert");
            state = EnemyState.Alerted;
            Debug.Log("refind state");
            needsPath = true;
          }
        }
        break;
    }
    Vector3 playerDir = (player.transform.position - gameObject.transform.position).normalized;
    var cast = Physics2D.Raycast(gameObject.transform.position + (playerDir * 0.5f), playerDir);
    //Debug.Log(cast.distance);
    if(cast.collider.gameObject == player && cast.distance < 3.0f)
    {
      attentionTimer = 8.0f;
      //Debug.Log("I SEE YOU");
      canSeePlayer = true;
    }
    else
    {
      if(canSeePlayer)
      {
        // repath
        Debug.Log("can see player?");
        needsPath = false;
        path.Clear();
        state = EnemyState.Eliminate;
        Debug.Log("cant see anymore");
        Debug.Log(pathing);
      }
      canSeePlayer = false;
    }

    if(canSeePlayer)
    {
      Vector3 movement = playerDir;
      movement.Normalize();
      gameObject.transform.Translate(movement.x * moveSpeed * Time.fixedDeltaTime, movement.y * moveSpeed * Time.fixedDeltaTime, 0.0f);
      lastMove = movement;
    }
    else if(!needsPath)
    {
      if(path.Count > 0 && (pathPoint - gameObject.transform.position).sqrMagnitude < 0.01f)
      {
        pathPoint = path[path.Count - 1];
        pathPoint.z = gameObject.transform.position.z;
        path.RemoveAt(path.Count - 1);
      }
      else
      {

        Vector3 movement = pathPoint - gameObject.transform.position;
        movement.Normalize();
        gameObject.transform.Translate(movement.x * moveSpeed * Time.fixedDeltaTime, movement.y * moveSpeed * Time.fixedDeltaTime, 0.0f);
        Debug.Log(movement);
        Debug.Log(moveSpeed);
        lastMove = movement;
      }
    }
  }

  void OnParticleCollision(GameObject other)
  {
    if(canSeePlayer || pathing)
    {
      return;
    }
    if(soundTimer < 0.0f)
    {
      soundTimer = 2.0f;
      if(other.transform.parent.gameObject == player)
      {
        switch(state)
        {
          case EnemyState.Suspicious:
            state = EnemyState.Alerted;
            attentionTimer = 8.0f;
            needsPath = true;
            Debug.Log("collision");
            //Debug.Log("alert");
            break;
          case EnemyState.Alerted:
            state = EnemyState.Eliminate;
            attentionTimer = 10.0f;
            Debug.Log("collision");
            needsPath = true;
            //Debug.Log("elim");
            break;
          case EnemyState.Eliminate:
            attentionTimer = 10.0f;
            Debug.Log("collision");
            needsPath = true;
            //Debug.Log("elim 2");
            break;
          default:
            //Debug.Log("suspect");
            state = EnemyState.Suspicious;
            attentionTimer = 4.0f;
            Debug.Log("collision");
            needsPath = true;
            break;
        }
      }
      else
      {
        //Debug.Log("TIME 2 ASSIMILATE");
        // emit message to other dudes
      }
    }
  }
}
