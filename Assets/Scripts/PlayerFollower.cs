using System.Collections.Generic;
using UnityEngine;

public class DelayedPlayerFollower : MonoBehaviour
{
    public Transform player;

    public float distanceTrigger = 3f;
    public float delay = 2f;
    public GameObject _talkingNpc;
    public Animator _anim;
    private struct PlayerState
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;

        public PlayerState(Vector3 position, Quaternion rotation, float time)
        {
            this.position = position;
            this.rotation = rotation;
            this.time = time;
        }
    }

    private List<PlayerState> history = new List<PlayerState>();

    private bool wasFollowing = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        this.transform.position = _talkingNpc.transform.position;
        Destroy(_talkingNpc);
    }
    void Update()
    {
        // Enregistre la position du joueur
        history.Add(new PlayerState(
            player.position,
            player.rotation,
            Time.time
        ));

        // Nettoyage
        while (history.Count > 0 && Time.time - history[0].time > delay + 1f)
        {
            history.RemoveAt(0);
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > distanceTrigger)
        {
            if (!wasFollowing)
            {
                // On commence à suivre sans téléportation
                wasFollowing = true;
            }

            FollowDelayed();
            _anim.SetBool("IsWalking", true);
        }
        else
        {
            wasFollowing = false;
            _anim.SetBool("IsWalking", false);
        }
    }

    void FollowDelayed()
    {
        float targetTime = Time.time - delay;
     
        if (history.Count < 2)
            return;

        for (int i = 0; i < history.Count - 1; i++)
        {
            if (history[i].time <= targetTime &&
                history[i + 1].time >= targetTime)
            {
                float t = Mathf.InverseLerp(
                    history[i].time,
                    history[i + 1].time,
                    targetTime
                );

                Vector3 targetPosition = Vector3.Lerp(
                    history[i].position,
                    history[i + 1].position,
                    t
                );

                Quaternion targetRotation = Quaternion.Slerp(
                    history[i].rotation,
                    history[i + 1].rotation,
                    t
                );

                // Déplacement progressif vers la position retardée
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    5f * Time.deltaTime
                );

                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    360f * Time.deltaTime
                );

                break;
            }
        }
    }
}