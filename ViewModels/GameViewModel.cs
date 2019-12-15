using System;
using System.Collections.Generic;
using System.Text;
using Reversi.Models;

namespace Reversi.ViewModels
{
    public class GameViewModel
    {
        public Board Board { get; set; } = new Board();
        public GameViewModel()
        {

        }
    }
}
