using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PersonalPackage.ObjectPooling
{
    public class FlyweightFactory : MonoBehaviour // Todo: Make the spawned objects be under a parent game object
    {
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 100;
        private static FlyweightFactory s_instance;
        private readonly Dictionary<FlyweightType, IObjectPool<Flyweight>> pools = new();

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        public static Flyweight Spawn(FlyweightSettings settings)
        {
            return s_instance.GetPoolFor(settings)?.Get();
        }

        public static void ReturnToPool(Flyweight flyweight)
        {
            s_instance.GetPoolFor(flyweight.settings)?.Release(flyweight);
        }

        private IObjectPool<Flyweight> GetPoolFor(FlyweightSettings settings)
        {
            if (pools.TryGetValue(settings.flyweightType, out IObjectPool<Flyweight> pool)) return pool;
            pool = new ObjectPool<Flyweight>(
                settings.Create,
                settings.OnGet,
                settings.OnRelease,
                settings.OnDestroyPoolObject,
                collectionCheck,
                defaultCapacity,
                maxPoolSize
            );
            pools.Add(settings.flyweightType, pool);
            return pool;
        }
    }
}
