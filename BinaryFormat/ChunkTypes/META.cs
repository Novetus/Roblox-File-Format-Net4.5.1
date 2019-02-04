﻿using System.Collections.Generic;

namespace RobloxFiles.BinaryFormat.Chunks
{
    public class META
    {
        public int NumEntries;
        public Dictionary<string, string> Entries;

        public META(BinaryRobloxChunk chunk)
        {
            using (BinaryRobloxReader reader = chunk.GetReader("META"))
            {
                NumEntries = reader.ReadInt32();
                Entries = new Dictionary<string, string>(NumEntries);

                for (int i = 0; i < NumEntries; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    Entries.Add(key, value);
                }
            }
        }
    }
}
