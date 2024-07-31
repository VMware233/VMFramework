using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.Exceptions;
using VMFramework.Core;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public abstract class FixedGridMap : IFixedGridMap
    {
        private readonly IPool<IGridChunk> chunkPool;
        
        public readonly FixedGridMapConfig config;

        private readonly IGridChunk[,,] chunks;

        public readonly CubeInteger chunkPositions;

        public Vector3Int ChunkSize => config.chunkSize;
        
        public Vector3Int FixedSize => config.fixedSize;

        public event Action<IGridChunk> OnChunkCreated;

        public event Action<IGridChunk> OnChunkDestroyed; 

        #region Constructors

        public FixedGridMap(FixedGridMapConfig config, FixedGridMapInitializationInfo info)
        {
            config.fixedSize.AssertIsAllNumberAbove(0, nameof(config.fixedSize));
            config.chunkSize.AssertIsAllNumberAbove(0, nameof(config.chunkSize));
            
            this.config = config;
            chunkPositions = new(config.fixedSize);
            
            chunkPool = info.ChunkPool;

            config.fixedSize.CreateArray(ref chunks);
        }

        #endregion

        #region Creations and Destructions

        public void CreateChunk(Vector3Int position)
        {
            position.AssertIsAllNumberAboveOrEqual(0, nameof(position));

            if (chunks.Get(position) != null)
            {
                throw new OperationException($"Chunk at position {position} already exists. Cannot create new chunk.");
            }

            var chunk = chunkPool.Get();
            chunk.Init(new(this, position));
            
            OnChunkCreated?.Invoke(chunk);
        }

        public void DestroyChunk(Vector3Int position)
        {
            position.AssertIsAllNumberAboveOrEqual(0, nameof(position));
            
            var chunk = chunks.Get(position);
            
            if (chunk == null)
            {
                throw new OperationException($"Chunk at position {position} does not exist. Cannot destroy chunk.");
            }
            
            OnChunkDestroyed?.Invoke(chunk);
            
            chunk.Destroy();
            chunkPool.Return(chunk);
        }

        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasValidChunk(Vector3Int position)
        {
            return chunks.Get(position) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetChunk(Vector3Int chunkPosition, out IGridChunk chunk)
        {
            chunkPosition.AssertContainsBy(chunkPositions, nameof(chunkPosition), nameof(chunkPositions));

            chunk = chunks.Get(chunkPosition);
            return chunk != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGridChunk GetChunk(Vector3Int chunkPosition)
        {
            chunkPosition.AssertContainsBy(chunkPositions, nameof(chunkPosition), nameof(chunkPositions));
            
            return chunks.Get(chunkPosition);
        }
    }
}