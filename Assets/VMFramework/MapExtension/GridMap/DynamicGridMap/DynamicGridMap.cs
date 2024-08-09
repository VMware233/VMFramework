using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Exceptions;
using VMFramework.Core;
using VMFramework.Core.Pools;

namespace VMFramework.Maps
{
    public abstract class DynamicGridMap : IGridMap
    {
        private readonly INormalPool<IGridChunk> chunkPool;
        
        public readonly DynamicGridMapConfig config;

        private readonly Dictionary<Vector3Int, IGridChunk> chunks = new();

        public Vector3Int ChunkSize => config.chunkSize;

        public event Action<IGridChunk> OnChunkCreated;

        public event Action<IGridChunk> OnChunkDestroyed;

        #region Constructors

        protected DynamicGridMap(DynamicGridMapConfig config, DynamicGridMapInitializationInfo info)
        {
            config.chunkSize.AssertIsAllNumberAbove(0, nameof(config.chunkSize));
            
            this.config = config;

            if (info != null)
            {
                chunkPool = info.ChunkPool;
            }

            chunkPool ??= new CreatablePoolItemsPool<IGridChunk, IGridMap>(this, gridMap => new GridChunk(), 70);
        }

        #endregion

        #region Chunk

        #region Creations and Destructions

        public IGridChunk CreateChunk(Vector3Int position)
        {
            if (chunks.ContainsKey(position))
            {
                throw new OperationException($"Chunk at position {position} already exists. Cannot create new chunk.");
            }

            var chunk = chunkPool.Get();
            chunk.Place(new(position));
            chunks[position] = chunk;
            
            OnChunkCreated?.Invoke(chunk);
            
            return chunk;
        }

        public void DestroyChunk(Vector3Int position)
        {
            if (chunks.TryGetValue(position, out var chunk) == false)
            {
                throw new OperationException($"Chunk at position {position} does not exist. Cannot destroy chunk.");
            }
            
            OnChunkDestroyed?.Invoke(chunk);
            
            chunkPool.Return(chunk);
        }

        #endregion
        
        public bool HasValidChunk(Vector3Int position)
        {
            return chunks.ContainsKey(position);
        }

        public bool TryGetChunk(Vector3Int chunkPosition, out IGridChunk chunk)
        {
            return chunks.TryGetValue(chunkPosition, out chunk);
        }

        public IGridChunk GetChunk(Vector3Int chunkPosition)
        {
            return chunks.GetValueOrDefault(chunkPosition);
        }

        public IEnumerable<IGridChunk> GetAllChunks() => chunks.Values;

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
            foreach (var chunk in chunks.Values)
            {
                foreach (var tile in chunk.GetAllTiles())
                {
                    yield return tile;
                }
            }
        }

        #endregion

        public virtual void ClearMap()
        {
            foreach (var chunk in chunks.Values)
            {
                chunk.ClearMap();
            }
        }
    }
}