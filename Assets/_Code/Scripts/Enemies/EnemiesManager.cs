using System.Collections.Generic;
using Hearings.Core.Singleton;
using UnityEngine;

namespace _Code.Scripts.Enemies
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        public List<GameObject> enemies;

        public GameObject GetRandomEnemy()
        {
            return enemies[Random.Range(0, enemies.Count)];
        }
    }
}