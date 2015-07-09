using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;

namespace JarheadsButtonMaker
{
    public enum SpriteRenderMode
    {
        Sprite,
        Map
    }

    public class HistorySpriteVoxel
    {
        public int X, Y, Z;
        public VoxelSpriteVoxel Voxel;

        public HistorySpriteVoxel(int x, int y, int z, VoxelSpriteVoxel v)
        {
            X = x;
            Y = y;
            Z = z;
            Voxel = v;
        }
    }

    public class VoxelSprite
    {
        public int X_SIZE;
        public int Y_SIZE;
        public int Z_SIZE;

        public string Filename;

        public bool Ready = false;
        public List<VoxelSpriteChunk> Chunks = new List<VoxelSpriteChunk>();

        public int CurrentFrame = 0;

        public Color[] Palette = new Color[10];


        public void Init(int size)
        {
            X_SIZE = size;
            Y_SIZE = size;
            Z_SIZE = size;

            Chunks.Clear();
           
            Palette[0] = new Color(1f,1f,1f);
            Palette[2] = new Color(0.75f, 0.75f, 0.75f);
            Palette[4] = new Color(0.5f, 0.5f, 0.5f);
            Palette[6] = new Color(0.25f, 0.25f, 0.25f);
            Palette[8] = new Color(0f, 0f, 0f);
            Palette[1] = new Color(1f,0f,0f);
            Palette[3] = new Color(0f, 1f, 0f);
            Palette[5] = new Color(0f, 0f, 1f);
            Palette[7] = new Color(1f, 1f, 0f);
            Palette[9] = new Color(Helper.ByteToFloat(100),Helper.ByteToFloat(60),0);

        }

        public void SetFrame(int frame)
        {
            if (frame >= 0 && frame < Chunks.Count)
            {
                CurrentFrame = frame;
            }
        }


        public void Save(string fn)
        {

            using (FileStream str = new FileStream(fn, FileMode.Create))
            {
                //str.Write(gameWorld.X_CHUNKS + "," + gameWorld.Y_CHUNKS + "," + gameWorld.Z_CHUNKS + "\n");
                using (GZipStream gzstr = new GZipStream(str, CompressionMode.Compress))
                {

                    gzstr.WriteByte(Convert.ToByte(X_SIZE));
                    gzstr.WriteByte(Convert.ToByte(Y_SIZE));
                    gzstr.WriteByte(Convert.ToByte(Z_SIZE));
                    gzstr.WriteByte(Convert.ToByte(Chunks.Count));

                    for (int i = 0; i < 10; i++)
                    {
                        gzstr.WriteByte(Helper.FloatToByte(Palette[i].r));
                        gzstr.WriteByte(Helper.FloatToByte(Palette[i].g));
                        gzstr.WriteByte(Helper.FloatToByte(Palette[i].b));
                    }

                    foreach (VoxelSpriteChunk c in Chunks)
                    {
                        //str.Write("C\n");

                        //Chunk c = gameWorld.Chunks[x, y, z];
                        for (int vx = 0; vx < X_SIZE; vx++)
                            for (int vy = 0; vy <Y_SIZE; vy++)
                                for (int vz = 0; vz < Z_SIZE; vz++)
                                {
                                    if (!c.Voxels[vx, vy, vz].Active) continue;

                                    //string vox = vx + "," + vy + "," + vz + ",";
                                    //vox += ((int)c.Voxels[vx, vy, vz].Type);
                                    //str.Write(vox + "\n");

                                    gzstr.WriteByte(Convert.ToByte(vx));
                                    gzstr.WriteByte(Convert.ToByte(Y_SIZE - 1 - vy));
                                    gzstr.WriteByte(Convert.ToByte(vz));
                                    gzstr.WriteByte(Helper.FloatToByte(c.Voxels[vx, vy, vz].Color.r));
                                    gzstr.WriteByte(Helper.FloatToByte(c.Voxels[vx, vy, vz].Color.g));
                                    gzstr.WriteByte(Helper.FloatToByte(c.Voxels[vx, vy, vz].Color.b));
                                }
                        gzstr.WriteByte(Convert.ToByte('c'));


                    }
                    //str.Flush();

                }
            }
            //}

            //using (StreamWriter fs = new StreamWriter(fn))
            //{
            //    fs.Write(Compress(sb.ToString()));
            //    fs.Flush();
            //}

            //sb.Clear();
            //sb = null;

            GC.Collect();
        }

    }
}