using System;
using System.Collections.Generic;
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

            if (info != null)
            {
                chunkPool = info.ChunkPool;
            }
            chunkPool ??= new GridChunkPool(this, () => new GridChunk(), 200);

            config.fixedSize.CreateArray(ref chunks);
        }

        #endregion

        #region Chunk

        #region Creations and Destructions

        public IGridChunk CreateChunk(Vector3Int position)
        {
            position.AssertIsAllNumberAboveOrEqual(0, nameof(position));

            if (chunks.Get(position) != null)
            {
                throw new OperationException($"Chunk at position {position} already exists. Cannot create new chunk.");
            }

            var chunk = chunkPool.Get();
            chunk.Place(new(position));
            
            OnChunkCreated?.Invoke(chunk);
            
            return chunk;
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
            
            chunkPool.Return(chunk);
        }

        #endregion
        
        public bool HasValidChunk(Vector3Int position)
        {
            return chunks.Get(position) != null;
        }

        public bool TryGetChunk(Vector3Int chunkPosition, out IGridChunk chunk)
        {
            chunkPosition.AssertContainsBy(chunkPositions, nameof(chunkPosition), nameof(chunkPositions));

            chunk = chunks.Get(chunkPosition);
            return chunk != null;
        }

        public IGridChunk GetChunk(Vector3Int chunkPosition)
        {
            chunkPosition.AssertContainsBy(chunkPositions, nameof(chunkPosition), nameof(chunkPositions));
            
            return chunks.Get(chunkPosition);
        }

        public IEnumerable<IGridChunk> GetAllChunks()
        {
            foreach (var chunk in chunks)
            {
                if (chunk != null)
                {
                    yield return chunk;
                }
            }
        }

        #endregion

        #region Tile

        public IEnumerable<Vector3Int> GetAllPoints()
        {
            foreach (var tile in GetAllTiles())
            {
                yield return tile.Position;
            }
        }

        public IEnumerable<IGridTile> GetAllTiles()
        {
            foreach (var chunk in chunks)
            {
                if (chunk == null)
                {
                    continue;
                }
                
                foreach (var tile in chunk.GetAllTiles())
                {
                    if (tile == null)
                    {
                        continue;
                    }
                    
                    yield return tile;
                }
            }
        }

        #endregion
    }
}