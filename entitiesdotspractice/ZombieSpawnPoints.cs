using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace TMG.Zombies
{
    public struct ZombieSpawnPoints : IComponentData
    {
        public NativeArray<float3> Value;
    }
}