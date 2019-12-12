
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reversi
{

    class Ai
    {
        public Ai()
        {
            String text = "";
            String[] token;
            char[][] tábla = new char[10][];
            Stopwatch sw = new Stopwatch();



            for (int i = 0; i < 10; i++)
            {


                tábla[i] = new char[10];
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || i == 9 || j == 0 || j == 9) tábla[i][j] = '-';
                    else tábla[i][j] = '0';

                }
            }

            tábla[4][4] = 'W';
            tábla[4][5] = 'B';
            tábla[5][4] = 'B';
            tábla[5][5] = 'W';

            Node jatek1 = new Node(tábla);
            while (!check_for_finish(jatek1))
            {
                sw.Start();
                makemove_AI(Best_position(jatek1).x, Best_position(jatek1).y, jatek1);
                Console.WriteLine("A Player köre jön");
                Console.WriteLine("A fekete babuk szama: " + calculate_scores(jatek1)[0] + "   A feér bábuk száma: " + calculate_scores(jatek1)[1] + "Elapsed time:" + sw.Elapsed);

                text = Console.ReadLine();
                if (text.Equals("exit")) break;
                else
                {
                    token = text.Split(' ');
                    while (!Check_move(Int32.Parse(token[0]), Int32.Parse(token[1]), jatek1))
                    {
                        Console.WriteLine("A megadott lépés helytelen! Adjon meg egy új lépést");
                        text = Console.ReadLine();
                        token = text.Split(' ');
                    }
                    player_move(jatek1, token);
                }


            }
            sw.Stop();
        }

        public static void player_move(Node jatek, String[] token)
        {
            makemove_Player(Int32.Parse(token[0]), Int32.Parse(token[1]), jatek);
            Console.WriteLine("Az AI köre jön");
            Console.WriteLine("A fekete babuk szama: " + calculate_scores(jatek)[0] + "   A feér bábuk száma: " + calculate_scores(jatek)[1]);
        }

        public static Boolean check_for_finish(Node jatek)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (jatek.tabla[i][j] == '0') return false;
                }
            }
            return true;
        }

        public static int Calculate_aktual_gamestate(Node jatek)     //Mivel a fehérrrel van a gép ezért visszatéríti a W-Bt
        {
            int B_points = 0;
            int W_points = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (jatek.tabla[i][j] == 'B') B_points++;
                    if (jatek.tabla[i][j] == 'W') W_points++;
                }

            }
            return W_points - B_points;
        }

        public static int[] calculate_scores(Node jatek)
        {
            int[] tomb = new int[2];
            tomb[0] = 0;
            tomb[1] = 0;

            for(int i = 1; i < 9; i++)
            {
                for(int j = 1; j < 9; j++)
                {
                    if (jatek.tabla[i][j] == 'B') tomb[0]++;
                    if (jatek.tabla[i][j] == 'W') tomb[1]++;
                }
            }
            return tomb;
        }


        public static Boolean Check_move(int koord_1, int koord_2, Node jatek)
        {
            foreach (Koordinate n in GenerateValidKoordinates_for_Player(jatek))
            {
                if (n.x == koord_1 && n.y == koord_2)
                {
                    return true;
                }

            }
            return false;
        }

        public static void makemove_AI(int x, int y, Node jatek)
        {
            List<Koordinate> flipback_list_final = new List<Koordinate>();
            flipback_list_final = jatek.Flip_pieces_B(x, y);
            foreach (Koordinate n in flipback_list_final)
            {
                jatek.set_piece_W(n.x, n.y);
            }
            jatek.set_piece_W(x, y);
            jatek.kiir();
        }


        public static void makemove_Player(int x, int y,Node jatek)
        {
            List<Koordinate> flipback_list_final = new List<Koordinate>();
            flipback_list_final = jatek.Flip_pieces_W(x, y);
            foreach (Koordinate n in flipback_list_final)
            {
                jatek.set_piece_B(n.x, n.y);
            }
            jatek.set_piece_B(x, y);
            jatek.kiir();
        }

        public static Koordinate Best_position(Node jatek)
        {
            List<Koordinate> Valid_moves = new List<Koordinate>();
            List<Koordinate> flipback_list = new List<Koordinate>();
            List<Koordinate> bestmove = new List<Koordinate>();
            int bestscore = 0;
            Valid_moves = GenerateValidKoordinates_for_AI(jatek);

            //foreach (Koordinate n in Valid_moves)
            //{
            //    Console.WriteLine(n.x + " " + n.y);
            //}

            foreach (Koordinate act in Valid_moves)
            {

                flipback_list = jatek.Flip_pieces_B(act.x, act.y);
                if (Calculate_aktual_gamestate(jatek) + flipback_list.Count > bestscore)
                {
                    bestscore = Calculate_aktual_gamestate(jatek) + flipback_list.Count;
                    bestmove.Clear();
                    bestmove.Add(act);
                }
                if (Calculate_aktual_gamestate(jatek) == bestscore)
                {
                    bestmove.Add(act);
                }
                foreach (Koordinate n in flipback_list)
                {
                    jatek.set_piece_B(n.x, n.y);
                }
            }


            if (bestmove.Count > 1)
            {
                Random rnd = new Random();
                return bestmove[rnd.Next(1, bestmove.Count)];
            }
            else
                return bestmove[0];
        }

        public static List<Koordinate> GenerateValidKoordinates_for_AI(Node jatek) //fehér lépéseinek a generálása
        {

            List<Koordinate> Valid_Moves_W = new List<Koordinate>();
            for (int i = 1; i < 8; i++)
            {     //mátrix bejárása
                for (int j = 1; j < 8; j++)
                {
                    if (jatek.tabla[i][j] == '0')
                    {
                        if (jatek.tabla[i + 1][j] == 'B' || jatek.tabla[i - 1][j] == 'B' || jatek.tabla[i][j + 1] == 'B' || jatek.tabla[i][j - 1] == 'B' || jatek.tabla[i + 1][j + 1] == 'B' || jatek.tabla[i + 1][j - 1] == 'B' || jatek.tabla[i - 1][j + 1] == 'B' || jatek.tabla[i - 1][j - 1] == 'B')
                        {
                            Valid_Moves_W.Add(new Koordinate(i, j));
                        }

                    }
                }
            }

            return Valid_Moves_W;
        }

        public static List<Koordinate> GenerateValidKoordinates_for_Player(Node jatek) //fehér lépéseinek a generálása
        {

            List<Koordinate> Valid_Moves_player = new List<Koordinate>();
            for (int i = 1; i < 8; i++)
            {     //mátrix bejárása
                for (int j = 1; j < 8; j++)
                {
                    if (jatek.tabla[i][j] == '0')
                    {
                        if (jatek.tabla[i + 1][j] == 'W' || jatek.tabla[i - 1][j] == 'W' || jatek.tabla[i][j + 1] == 'W' || jatek.tabla[i][j - 1] == 'W' || jatek.tabla[i + 1][j + 1] == 'W' || jatek.tabla[i + 1][j - 1] == 'W' || jatek.tabla[i - 1][j + 1] == 'W' || jatek.tabla[i - 1][j - 1] == 'W')
                        {
                            Valid_Moves_player.Add(new Koordinate(i, j));
                        }

                    }
                }
            }

            return Valid_Moves_player;
        }

    }

    public class Koordinate
    {
        public int x;
        public int y;

        public Koordinate(int x, int y)
        {
            this.x = x;
            this.y = y;

        }
        public override string ToString()
        {
            return x + "    " + y;
        }

    }

    class Node
    {
        public char[][] tabla;      //játéktábla

        public Node(char[][] tabla)
        {
            this.tabla = tabla;
        }

        public void kiir()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tabla[i][j] + " ");

                }
                Console.WriteLine();
            }
        }


        public void set_piece_W(int set_x, int set_y)
        {
            this.tabla[set_x][set_y] = 'W';

        }
        public void set_piece_B(int set_x, int set_y)
        {
            this.tabla[set_x][set_y] = 'B';

        }
        public void set_piece_clear(int set_x, int set_y)
        {
            this.tabla[set_x][set_y] = '0';

        }

    
      public List<Koordinate> Flip_pieces_B(int x, int y)
        {
            List<Koordinate> bejart_ut_lista = new List<Koordinate>();
            List<Koordinate> atvaltott_lista = new List<Koordinate>();


            //jobbra check
            for (int k = 1; k < 9; k++)
            {
                if (y + k == 9) break;
                if (tabla[x][y + k] == 'B') bejart_ut_lista.Add(new Koordinate(x, y + k));
                if (tabla[x][y + k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //balra check
            for (int k = 1; k < 9; k++)
            {
                if (y - k == 0) break;
                if (tabla[x][y - k] == 'B') bejart_ut_lista.Add(new Koordinate(x, y - k));
                if (tabla[x][y - k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //fel check

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0) break;
                if (tabla[x - k][y] == 'B') bejart_ut_lista.Add(new Koordinate(x - k, y));
                if (tabla[x - k][y] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //le check

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 0) break;
                if (tabla[x + k][y] == 'B') bejart_ut_lista.Add(new Koordinate(x + k, y));
                if (tabla[x + k][y] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check fel jobbra

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0 || y + k == 9) break;
                if (tabla[x - k][y + k] == 'B') bejart_ut_lista.Add(new Koordinate(x - k, y + k));
                if (tabla[x - k][y + k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();


            //átló check le jobbra

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 9 || y + k == 9) break;
                if (tabla[x + k][y + k] == 'B') bejart_ut_lista.Add(new Koordinate(x + k, y + k));
                if (tabla[x + k][y + k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check le balra

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 9 || y - k == 0) break;
                if (tabla[x + k][y - k] == 'B') bejart_ut_lista.Add(new Koordinate(x + k, y - k));
                if (tabla[x + k][y - k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check fel balra

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0 || y - k == 0) break;
                if (tabla[x - k][y - k] == 'B') bejart_ut_lista.Add(new Koordinate(x - k, y - k));
                if (tabla[x - k][y - k] == 'W')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();


            return atvaltott_lista;
        }


        public List<Koordinate> Flip_pieces_W(int x, int y)
        {
            List<Koordinate> bejart_ut_lista = new List<Koordinate>();
            List<Koordinate> atvaltott_lista = new List<Koordinate>();


            //jobbra check
            for (int k = 1; k < 9; k++)
            {
                if (y + k == 9) break;
                if (tabla[x][y + k] == 'W') bejart_ut_lista.Add(new Koordinate(x, y + k));
                if (tabla[x][y + k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //balra check
            for (int k = 1; k < 9; k++)
            {
                if (y - k == 0) break;
                if (tabla[x][y - k] == 'W') bejart_ut_lista.Add(new Koordinate(x, y - k));
                if (tabla[x][y - k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //fel check

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0) break;
                if (tabla[x - k][y] == 'W') bejart_ut_lista.Add(new Koordinate(x - k, y));
                if (tabla[x - k][y] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //le check

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 0) break;
                if (tabla[x + k][y] == 'W') bejart_ut_lista.Add(new Koordinate(x + k, y));
                if (tabla[x + k][y] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check fel jobbra

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0 || y + k == 9) break;
                if (tabla[x - k][y + k] == 'W') bejart_ut_lista.Add(new Koordinate(x - k, y + k));
                if (tabla[x - k][y + k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();


            //átló check le jobbra

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 9 || y + k == 9) break;
                if (tabla[x + k][y + k] == 'W') bejart_ut_lista.Add(new Koordinate(x + k, y + k));
                if (tabla[x + k][y + k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y + k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check le balra

            for (int k = 1; k < 9; k++)
            {
                if (x + k == 9 || y - k == 0) break;
                if (tabla[x + k][y - k] == 'W') bejart_ut_lista.Add(new Koordinate(x + k, y - k));
                if (tabla[x + k][y - k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x + k][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();

            //átló check fel balra

            for (int k = 1; k < 9; k++)
            {
                if (x - k == 0 || y - k == 0) break;
                if (tabla[x - k][y - k] == 'W') bejart_ut_lista.Add(new Koordinate(x - k, y - k));
                if (tabla[x - k][y - k] == 'B')
                {
                    if (bejart_ut_lista.Count > 0)
                    {
                        foreach (Koordinate act in bejart_ut_lista)
                        {
                            set_piece_W(act.x, act.y);
                            atvaltott_lista.Add(new Koordinate(act.x, act.y));
                        }

                    }
                }
                if (tabla[x - k][y - k] == '0')
                {
                    bejart_ut_lista.Clear();
                    break;
                }

            }

            bejart_ut_lista.Clear();


            return atvaltott_lista;
        }

       

    }
}
