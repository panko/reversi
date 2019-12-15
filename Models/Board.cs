using System;
using System.Collections.Generic;
using System.Text;

namespace Reversi.Models
{
    public class Board
    {
        public enum Disk
        {
            Empty,
            Black,
            White
        }

        public List<List<Disk>> Disks { get; set; } = new List<List<Disk>>();

        public Board()
        {
            for (int i = 0; i < 8; ++i)
            {
                Disks.Add(new List<Disk>());
                for (int j = 0; j < 8; ++j)
                {
                    Disks[i].Add(Disk.Empty);
                }
            }
            Disks[3][3] = Disk.White;
            Disks[4][4] = Disk.White;
            Disks[4][3] = Disk.Black;
            Disks[3][4] = Disk.Black;
        }
    }
}
