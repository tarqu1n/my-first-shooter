using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public float spawnTime;
    private List<QueueItem> queue = new List<QueueItem>();

    private float spawnTimer;
    void Update()
    {
        if (spawnTimer <= 0)
        {
            Spawn();
            spawnTimer = spawnTime;
        }
        spawnTimer -= Time.deltaTime;
    }

    public void Spawn()
    {
        if (queue.Count > 0)
        {
            GameObject instance = Instantiate(queue[0].monsters[0].monster, transform.position, Quaternion.identity);

            // attack chance
            float rand = Random.Range(0f, 1f);
            if (rand < queue[0].monsters[0].attackChance)
            {
                instance.GetComponent<MonsterController>().startBehaviour = MonsterController.Behaviour.Attack;
            } else
            {
                instance.GetComponent<MonsterController>().startBehaviour = MonsterController.Behaviour.Rush;
            }
            queue[0].monsters[0].quantity--;

            if (queue[0].monsters[0].quantity <= 0)
            {
                queue[0].monsters.RemoveAt(0);
            }
            if (queue[0].monsters.Count == 0)
            {
                queue.RemoveAt(0);
            }
        }
    }

    public void Assign(GameObject monster, int stage, int quantity, float attackChance)
    {
        foreach (QueueItem item in queue)
        {
            if (item.stage == stage)
            {
                foreach (MonsterQueueItem monsterItem in item.monsters)
                {
                    if (monsterItem.monster.name == monster.name)
                    {
                        monsterItem.quantity += quantity;
                        return;
                    }
                }

                // if the queue item for the stage exists but the monster does not create it
                item.monsters.Add(
                    new MonsterQueueItem()
                    {
                        monster = monster,
                        quantity = quantity
                    }
                );
                return;
            }
        }


        // add the stage with the monster if neither exists
        queue.Add(new QueueItem()
        {
            stage = stage,
            monsters = new List<MonsterQueueItem>()
            {
                new MonsterQueueItem()
                {
                    monster = monster,
                    quantity = quantity,
                    attackChance = attackChance,
                }
            }
        });
        queue.Sort((x, y) => x.stage.CompareTo(y.stage)); // sort the queue into the right order
    }

    private class MonsterQueueItem
    {
        public GameObject monster;
        public int quantity;
        public float attackChance;

    }
    private class QueueItem
    {
        public int stage;
        public List<MonsterQueueItem> monsters = new List<MonsterQueueItem>();
        
    }
}
