using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarheadsButtonMaker
{
    internal class Program
    {
        private const int SPRITE_SIZE = 79;

        private static void Main(string[] args)
        {
            Dictionary<string, bool[,]> letters = new Dictionary<string, bool[,]>();

            Console.WriteLine("Creating Sprite");
            VoxelSprite sprite = new VoxelSprite();
            sprite.Init(SPRITE_SIZE);

            // Read in letters dictionary
            Console.WriteLine("Reading Letters");
            using (TextReader tr = File.OpenText("letters.txt"))
            {
                while (true)
                {
                    string l = tr.ReadLine();
                    if (string.IsNullOrEmpty(l)) break;
                    bool[,] dots = new bool[5, 5];
                    for (int i = 0; i < 5; i++)
                    {
                        string line = tr.ReadLine();
                        for (int x = 0; x < line.Length; x++) if (line[x] == '*') dots[i, x] = true;
                    }
                    letters.Add(l, dots);
                    Console.Write(l);
                }
                Console.WriteLine();
            }

            VoxelSpriteChunk cb = new VoxelSpriteChunk();
            cb.Init(SPRITE_SIZE, SPRITE_SIZE, SPRITE_SIZE);
            sprite.Chunks.Add(cb);
            CreateBase(cb, 11);
            CreateScrews(cb, 11);

            cb = new VoxelSpriteChunk();
            cb.Init(SPRITE_SIZE, SPRITE_SIZE, SPRITE_SIZE);
            sprite.Chunks.Add(cb);
            CreateBase(cb, 46);
            CreateScrews(cb, 46);

            Console.WriteLine("Reading Buttons");
            using (TextReader tr = File.OpenText("buttons.txt"))
            {
                while (true)
                {
                    string text = tr.ReadLine();
                    if (string.IsNullOrEmpty(text)) break;
                    Console.Write(text);

                    VoxelSpriteChunk c = new VoxelSpriteChunk();
                    c.Init(SPRITE_SIZE, SPRITE_SIZE, SPRITE_SIZE);
                    sprite.Chunks.Add(c);

                    CreateBase(c, 11);
                    Console.Write(".");

                    CreateScrews(c, 11);
                    Console.Write(".");

                    CreateText(c, 11, text, letters);
                    Console.Write(".");

                    Console.WriteLine();
                }
            }

            

            Console.WriteLine("Saving sprite");
            sprite.Save("mainmenu.vxs");

            Console.Write("Done, press enter");
            Console.ReadLine();
        }

        private static void CreateBase(VoxelSpriteChunk c, int height)
        {
            int center = (SPRITE_SIZE/2);
            int heightcenter = (height/2)+1;

            for (int z = center - 3; z <= center + 2; z++)
                for (int y = center - (heightcenter-2); y <= center + heightcenter; y++)
                    for (int x = 0; x < c.X_SIZE; x++)
                    {
                        if (z == center - 3 &&
                            (x == 0 || x == c.X_SIZE - 1 || y == center - (heightcenter-2) || y == center + heightcenter))
                            continue;
                        c.SetVoxel(x, y, z, true, new Color(0.5f, 0.5f, 0.5f));
                    }

        }

        private static void CreateScrews(VoxelSpriteChunk c, int height)
        {
            int center = (SPRITE_SIZE/2);
            int heightcenter = (height/2)+1;

            // TL
            c.SetVoxel(2, (center + heightcenter) - 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(3, (center + heightcenter) - 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(2, (center + heightcenter) - 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(3, (center + heightcenter) - 3, center - 4, true, new Color(0.4f, 0.4f, 0.4f));

            // TR
            c.SetVoxel(c.X_SIZE - 4, (center + heightcenter) - 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 3, (center + heightcenter) - 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 4, (center + heightcenter) - 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 3, (center + heightcenter) - 3, center - 4, true, new Color(0.4f, 0.4f, 0.4f));

            // BL
            c.SetVoxel(2, (center - (heightcenter - 2)) + 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(3, (center - (heightcenter - 2)) + 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(2, (center - (heightcenter - 2)) + 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(3, (center - (heightcenter - 2)) + 2, center - 4, true, new Color(0.4f, 0.4f, 0.4f));

            // BR
            c.SetVoxel(c.X_SIZE - 4, (center - (heightcenter - 2)) + 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 3, (center - (heightcenter - 2)) + 3, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 4, (center - (heightcenter - 2)) + 2, center - 4, true, new Color(0.8f, 0.8f, 0.8f));
            c.SetVoxel(c.X_SIZE - 3, (center - (heightcenter - 2)) + 2, center - 4, true, new Color(0.4f, 0.4f, 0.4f));
        }

        private static void CreateText(VoxelSpriteChunk c, int height, string text, Dictionary<string,bool[,]> letters)
        {
            int center = (SPRITE_SIZE/2);
            int heightcenter = (height/2);
            int textstart = center - (((text.Length*6) - 1)/2);

            int vx = textstart;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    vx += 6;
                    continue;
                }

                for(int x=0;x<5;x++)
                    for(int y=0;y<5;y++)
                        c.SetVoxel(vx+x,(center+3) - y, center - 4, letters[text[i].ToString()][y,x], new Color(1f,1f,1f));

                vx += 6;
            }
        }
    }
}
