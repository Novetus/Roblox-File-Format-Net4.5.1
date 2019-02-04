﻿using System;
using System.IO;
using System.Text;
using LZ4;

namespace RobloxFiles.BinaryFormat
{
    /// <summary>
    /// BinaryRobloxChunk represents a generic LZ4-compressed chunk
    /// of data in Roblox's Binary File Format.
    /// </summary>
    public class BinaryRobloxChunk
    {
        public readonly string ChunkType;

        public readonly int CompressedSize;
        public readonly int Size;

        public readonly byte[] Reserved;

        public readonly byte[] CompressedData;
        public readonly byte[] Data;

        public bool HasCompressedData => (CompressedSize > 0);

        public override string ToString()
        {
            return ChunkType + " Chunk [" + Size + " bytes]";
        }

        public BinaryRobloxReader GetReader(string chunkType)
        {
            if (ChunkType == chunkType)
            {
                MemoryStream buffer = new MemoryStream(Data);
                return new BinaryRobloxReader(buffer);
            }

            throw new Exception("Expected " + chunkType + " ChunkType from the input RobloxBinaryChunk");
        }

        public BinaryRobloxChunk(BinaryRobloxReader reader)
        {
            byte[] bChunkType = reader.ReadBytes(4);
            ChunkType = Encoding.ASCII.GetString(bChunkType);

            CompressedSize = reader.ReadInt32();
            Size = reader.ReadInt32();
            Reserved = reader.ReadBytes(4);

            if (HasCompressedData)
            {
                CompressedData = reader.ReadBytes(CompressedSize);
                Data = LZ4Codec.Decode(CompressedData, 0, CompressedSize, Size);
            }
            else
            {
                Data = reader.ReadBytes(Size);
            }
        }
    }
}
