using System;
using System.Threading;
using System.IO;
namespace Project3._2
{
    class Program
    {
        static char[,] color = new char[8, 8];
        static char[,] elements = new char[8, 8];
        static int cursorx = 7, cursory = 6;               // position of cursor
        static int paranthesisx = 6, paranthesisy = 7;
        static ConsoleKeyInfo cki;
        static int Player = 0;
        static char bilmiyom;
        static int location_x = 0, location_y = 0;
        static int location_x2 = 0, location_y2 = 0;
        static char[] Piece = new char[1];
        static char[] Piece2 = new char[1];
        static int[] PerviousIndex = new int[2];
        static char[] PerviousColor = new char[1];
        static string Notation = " ";
        static bool pfm;
        static bool pfm1;
        static int counter_pawn;
        static int counter_pawn2;
        static char[] Letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        static int[] Numbers = { 8, 7, 6, 5, 4, 3, 2, 1 };
        static int position_x = 85;
        static int position_y = 4;
        static char symbol_check = ' ';
        static bool rook = false;
        static bool Kingside = false;
        static bool Queenside = false;
        static int king1 = 0, king2 = 0, kale11 = 0, kale12 = 0, kale21 = 0, kale22 = 0;
        static bool promoted = false;
        static char demo_promotion = ' ';
        static char[,] hinttasları = new char[16, 3];
        static int[] demotasi = new int[2];
        static int perx = 0, pery = 0;
        static int ykismi = 0;
        static int xkismi = 0;
        static bool passantdemo = false;
        static bool aynıanda = false;
        static int aynıandakale = 0;
        static int aynıandavezir = 0;
        static int aynıandafil = 0;
        static int aynıandaat = 0;
        static bool aynıkale = false;
        static bool aynıvezir = false;
        static bool aynıfil = false;
        static bool aynıat = false;
        static int[,] kalekonumları = new int[2, 2];
        static int[,] atkonumları = new int[2, 2];
        static int[,] filkonumları = new int[2, 2];
        static int[,] vezirkonumları = new int[2, 2];
        static char[] demoaynıtas = new char[1];
        static bool aynıandagidebiliyor = false;
        static bool capturing = false;
        static string filename;
        static int kingcounter = 0;
        static bool gameover = false;
        static bool saved = false;
        static bool checking_placement = false;
        static int aynitasinsayisi = 0;
        static char aynitasinharfi = ' ';
        static bool sonrook = false;
        static StreamWriter filename18;
        static bool Rooks(int x, int y)
        {//R
            bool result = false;
            if (elements[x, y] == 'R' && ((PerviousIndex[0] == x && PerviousIndex[1] != y) || (PerviousIndex[0] != x && PerviousIndex[1] == y)))
                result = true;
            if (result == true && PerviousIndex[0] == x)
            {
                if (y > PerviousIndex[1])        //to the right
                {
                    for (int i = PerviousIndex[1] + 1; i < y; i++)
                    {
                        if (elements[x, i] != '.')
                        { result = false; break; }
                    }
                }
                else
                {
                    for (int i = y + 1; i < PerviousIndex[1]; i++) //to the left
                    {
                        if (elements[x, i] != '.')
                        { result = false; break; }
                    }
                }
            }
            else if (result == true && PerviousIndex[1] == y)
            {
                if (x > PerviousIndex[0])
                {
                    for (int i = PerviousIndex[0] + 1; i < x; i++) //to the down
                    {
                        if (elements[i, y] != '.')
                        { result = false; break; }
                    }
                }
                else
                {
                    for (int i = x + 1; i < PerviousIndex[0]; i++) //to the up
                    {
                        if (elements[i, y] != '.')
                        { result = false; break; }
                    }
                }
            }
            if (result == true)
            {
                if (PerviousIndex[0] == 0 && PerviousIndex[1] == 0)
                {
                    kale22++;     //sol üst
                }
                else if (PerviousIndex[0] == 0 && PerviousIndex[1] == 7)
                {
                    kale21++;     //sağ üst
                }
                else if (PerviousIndex[0] == 7 && PerviousIndex[1] == 0)
                {
                    kale12++;     //sol alt
                }
                else if (PerviousIndex[0] == 7 && PerviousIndex[1] == 7)
                {
                    kale11++;     //sağ alt
                }
            }
            return result;
        }
        static bool Knights(int x, int y)
        {//N
            bool result = false;
            if (((PerviousIndex[0] == x - 1 && PerviousIndex[1] == y - 2) || (PerviousIndex[0] == x - 2 && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x + 1 && PerviousIndex[1] == y - 2) || (PerviousIndex[0] == x + 2 && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x + 2 && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x - 2 && PerviousIndex[1] == y + 1) || (PerviousIndex[0] == x - 1 && PerviousIndex[1] == y + 2) || (PerviousIndex[0] == x + 2 && PerviousIndex[1] == y + 1) || (PerviousIndex[0] == x + 1 && PerviousIndex[1] == y + 2)))
                result = true; return result;
        }
        static bool Bishops(int x, int y)
        {//B
            bool result = false;
            for (int i = 0; i < 8; i++)
            {
                if (((PerviousIndex[0] == x - i && PerviousIndex[1] == y - i) || (PerviousIndex[0] == x + i && PerviousIndex[1] == y + i) || (PerviousIndex[0] == x + i && PerviousIndex[1] == y - i) || (PerviousIndex[0] == x - i && PerviousIndex[1] == y + i)))
                { result = true; break; }
            }
            if (result == true && x > PerviousIndex[0] && y > PerviousIndex[1]) //to the right down
            {
                for (int i = PerviousIndex[1] + 1; i < y; i++)
                {
                    if (elements[PerviousIndex[0] + (i - PerviousIndex[1]), i] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x > PerviousIndex[0] && y < PerviousIndex[1]) //to the left down
            {
                for (int i = PerviousIndex[0] + 1; i < x; i++)
                {
                    if (elements[i, PerviousIndex[1] - (i - PerviousIndex[0])] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x < PerviousIndex[0] && y > PerviousIndex[1]) //to the right up
            {
                for (int i = PerviousIndex[1] + 1; i < y; i++)
                {
                    if (elements[PerviousIndex[0] - (i - PerviousIndex[1]), i] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x < PerviousIndex[0] && y < PerviousIndex[1]) //to the left up
            {
                for (int i = x + 1; i < PerviousIndex[0]; i++)
                {
                    if (elements[i, PerviousIndex[1] - (PerviousIndex[0] - i)] != '.')
                    { result = false; break; }

                }

            }
            return result;
        }
        static bool Queen(int x, int y)
        {//Q
            bool result = false;
            for (int i = 0; i < 8; i++)
            {
                if (((PerviousIndex[0] == x - i && PerviousIndex[1] == y - i) || (PerviousIndex[0] == x + i && PerviousIndex[1] == y + i) || (PerviousIndex[0] == x + i && PerviousIndex[1] == y - i) || (PerviousIndex[0] == x - i && PerviousIndex[1] == y + i)) || ((PerviousIndex[0] == x && PerviousIndex[1] != y) || (PerviousIndex[0] != x && PerviousIndex[1] == y)))
                { result = true; break; }
            }
            if (result == true && PerviousIndex[0] == x)
            {
                if (y > PerviousIndex[1])        //to the right
                {
                    for (int i = PerviousIndex[1] + 1; i < y; i++)
                    {
                        if (elements[x, i] != '.')
                        { result = false; break; }
                    }
                }
                else
                {
                    for (int i = y + 1; i < PerviousIndex[1]; i++) // to the left
                    {
                        if (elements[x, i] != '.')
                        { result = false; break; }
                    }
                }
            }
            else if (result == true && PerviousIndex[1] == y)
            {
                if (x > PerviousIndex[0])
                {
                    for (int i = PerviousIndex[0] + 1; i < x; i++) //to the down
                    {
                        if (elements[i, y] != '.')
                        { result = false; break; }
                    }
                }
                else
                {
                    for (int i = x + 1; i < PerviousIndex[0]; i++) //to the up
                    {
                        if (elements[i, y] != '.')
                        { result = false; break; }
                    }
                }
            }
            else if (result == true && x > PerviousIndex[0] && y > PerviousIndex[1]) //to the right down
            {
                for (int i = PerviousIndex[1] + 1; i < y; i++)
                {
                    if (elements[PerviousIndex[0] + (i - PerviousIndex[1]), i] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x > PerviousIndex[0] && y < PerviousIndex[1]) // to the left down
            {
                for (int i = PerviousIndex[0] + 1; i < x; i++)
                {
                    if (elements[i, PerviousIndex[1] - (i - PerviousIndex[0])] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x < PerviousIndex[0] && y > PerviousIndex[1]) //to the right up
            {
                for (int i = PerviousIndex[1] + 1; i < y; i++)
                {
                    if (elements[PerviousIndex[0] - (i - PerviousIndex[1]), i] != '.')
                    { result = false; break; }

                }
            }
            else if (result == true && x < PerviousIndex[0] && y < PerviousIndex[1]) //to the left up
            {
                for (int i = x + 1; i < PerviousIndex[0]; i++)
                {
                    if (elements[i, PerviousIndex[1] - (PerviousIndex[0] - i)] != '.')
                    { result = false; break; }

                }

            }
            return result;
        }
        static bool Pawns(int x, int y)
        {//P
            bool result = false;
            int c1 = y - 1;
            int b1 = y + 1;
            int d1 = y - 1;
            int e1 = y + 1;
            if ((Player % 2 == 0 && elements[x, y] != '.' && PerviousIndex[0] == x + 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1)) || Player % 2 == 1 && elements[x, y] != '.' && PerviousIndex[0] == x - 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1))
            { //çaprazlık
                result = true;
            }
            else if (pfm == true && ((Player % 2 == 0 && elements[x, y] == '.' && PerviousIndex[0] == x + 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1)) || Player % 2 == 1 && elements[x, y] == '.' && PerviousIndex[0] == x - 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1)))
            {//passant
                counter_pawn++;
                result = true;
            }
            else if (pfm1 == true && ((Player % 2 == 0 && elements[x, y] == '.' && PerviousIndex[0] == x + 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1)) || Player % 2 == 1 && elements[x, y] == '.' && PerviousIndex[0] == x - 1 && (PerviousIndex[1] == y - 1 || PerviousIndex[1] == y + 1)))
            {//passant
                counter_pawn2++;
                result = true;
            }
            else if ((Player % 2 == 0 && elements[x, y] != '.') || (Player % 2 == 1 && elements[x, y] != '.'))
            {
                result = false;
            }
            else if (elements[x + 1, y] == '.' && Player % 2 == 0 && PerviousIndex[0] == 6 && PerviousIndex[0] == x + 2 && PerviousIndex[1] == y)
            {

                if (counter_pawn == 1)
                {
                    counter_pawn = 0;
                }
                if (y - 1 == -1) c1 = y;

                else if (y + 1 == 8) b1 = y;

                if (elements[x, c1] == 'P' || elements[x, b1] == 'P')
                {
                    pfm = true;
                    counter_pawn++;
                }
                result = true;

            }
            else if (Player % 2 == 0 && ((PerviousIndex[0] == 6 && ((PerviousIndex[0] == x + 2 && elements[x + 1, y] == '.') || PerviousIndex[0] == x + 1)) || (PerviousIndex[0] != 6 && PerviousIndex[0] == x + 1)) && PerviousIndex[1] == y)
            {
                pfm = false;
                result = true;
                for (int i = PerviousIndex[0] - 1; i > x; i--)
                {
                    if (elements[i, y] != '.')
                    { result = false; break; }
                }

            }
            else if (Player % 2 == 1 && PerviousIndex[0] == 1 && PerviousIndex[0] == x - 2 && elements[x - 1, y] == '.' && PerviousIndex[1] == y)
            {
                if (counter_pawn2 == 1)
                {
                    counter_pawn2 = 0;
                }
                if (y - 1 == -1) d1 = y;

                else if (y + 1 == 8) e1 = y;
                if (elements[x, d1] == 'P' || elements[x, e1] == 'P')
                {
                    pfm1 = true;
                    counter_pawn2++;
                }

                result = true;
            }
            else if (Player % 2 == 1 && ((PerviousIndex[0] == 1 && ((PerviousIndex[0] == x - 2 && elements[x - 1, y] == '.') || PerviousIndex[0] == x - 1)) || (PerviousIndex[0] != 1 && PerviousIndex[0] == x - 1)) && PerviousIndex[1] == y)
            {
                result = true;
                pfm1 = false;
                for (int i = PerviousIndex[0] + 1; i < x; i++)
                {
                    if (elements[i, y] != '.')
                    { result = false; break; }
                }
            }
            return result;
        }
        static bool King(int x, int y)
        {//K
            bool result = false;
            if (Rook(x, y))
                result = true;
            else if ((PerviousIndex[0] == x - 1 && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x + 1 && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x && PerviousIndex[1] == y - 1) || (PerviousIndex[0] == x - 1 && PerviousIndex[1] == y) || (PerviousIndex[0] == x + 1 && PerviousIndex[1] == y) || (PerviousIndex[0] == x - 1 && PerviousIndex[1] == y + 1) || (PerviousIndex[0] == x && PerviousIndex[1] == y + 1) || (PerviousIndex[0] == x + 1 && PerviousIndex[1] == y + 1))
            {
                if (Player % 2 == 0)
                    king1++;
                else
                    king2++;
                result = true;
            }
            return result;
        }
        static bool Rook(int x, int y)
        {
            Kingside = false;
            Queenside = false;
            rook = false;
            if (Player % 2 == 0 && Piece[0] == 'K' && elements[x, y] == 'R' && color[x, y] == 'B')
            {
                if (x == 7 && y == 7 && elements[x, y - 1] == '.' && elements[x, y - 2] == '.')
                {
                    if (allahinbelasi(x, y - 3) == false && allahinbelasi(x, y - 1) == false && allahinbelasi(x, y - 2) == false)
                    {
                        Kingside = true;
                        rook = true;
                    }
                }
                else if (x == 7 && y == 0 && elements[x, y + 1] == '.' && elements[x, y + 2] == '.' && elements[x, y + 3] == '.')
                {
                    if (allahinbelasi(7, 4) == false && allahinbelasi(x, y + 1) == false && allahinbelasi(x, y + 2) == false && allahinbelasi(x, y + 3) == false)
                    {
                        Queenside = true;
                        rook = true;
                    }
                }
            }
            if (Player % 2 == 1 && Piece[0] == 'K' && elements[x, y] == 'R' && color[x, y] == 'R')
            {
                if (x == 0 && y == 7 && elements[x, y - 1] == '.' && elements[x, y - 2] == '.')
                {
                    if (allahinbelasi(0, 4) == false && allahinbelasi(x, y - 1) == false && allahinbelasi(x, y - 2) == false)
                    {
                        Kingside = true;
                        rook = true;
                    }
                }
                else if (x == 0 && y == 0 && elements[x, y + 1] == '.' && elements[x, y + 2] == '.' && elements[x, y + 3] == '.')
                {
                    if (allahinbelasi(0, 4) == false && allahinbelasi(x, y + 1) == false && allahinbelasi(x, y + 2) == false && allahinbelasi(x, y + 3) == false)
                    {
                        Queenside = true;
                        rook = true;
                    }
                }
            }
            return rook;
        }
        static bool checking(int x, int y)
        {
            bool checking = false;
            perx = PerviousIndex[0];
            pery = PerviousIndex[1];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Player % 2 == 0 && color[i, j] == 'B' && elements[i, j] != '.')
                    {
                        PerviousIndex[0] = i;
                        PerviousIndex[1] = j;
                        if (elements[i, j] == 'P')
                        {
                            if ((i - 1 >= 0) && (j + 1 < 8) && (j - 1 >= 0) && ((elements[i - 1, j + 1] == elements[x, y] && color[i - 1, j + 1] == 'R') || (elements[i - 1, j - 1] == elements[x, y]) && color[i - 1, j - 1] == 'R'))
                            {
                                checking = true;
                                demotasi[0] = i;
                                demotasi[1] = j;

                            }
                        }
                        else if (elements[i, j] == 'B')
                            checking = Bishops(x, y);
                        else if (elements[i, j] == 'Q')
                            checking = Queen(x, y);
                        else if (elements[i, j] == 'R')
                            checking = Rooks(x, y);
                        else if (elements[i, j] == 'K')
                            checking = King(x, y);
                        else if (elements[i, j] == 'N')
                            checking = Knights(x, y);
                    }
                    if (Player % 2 == 1 && color[i, j] == 'R' && elements[i, j] != '.')
                    {
                        PerviousIndex[0] = i;
                        PerviousIndex[1] = j;
                        if (elements[i, j] == 'P')
                        {
                            if ((i + 1 < 8) && (j + 1 < 8) && (j - 1 >= 0) && ((elements[i + 1, j + 1] == elements[x, y] && color[i + 1, j + 1] == 'B') || (elements[i + 1, j - 1] == elements[x, y]) && color[i + 1, j - 1] == 'B'))
                            {
                                checking = true;
                                demotasi[0] = i;
                                demotasi[1] = j;
                            }
                        }
                        else if (elements[i, j] == 'B')
                            checking = Bishops(x, y);
                        else if (elements[i, j] == 'Q')
                            checking = Queen(x, y);
                        else if (elements[i, j] == 'R')
                            checking = Rooks(x, y);
                        else if (elements[i, j] == 'K')
                            checking = King(x, y);
                        else if (elements[i, j] == 'N')
                            checking = Knights(x, y);
                    }
                    if (checking == true)
                        break;
                }
                if (checking == true)
                    break;
            }
            if (checking == true && rook == false)
            {
                Console.SetCursorPosition(10, 26);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!! CHECK !!!", (Player % 2));
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.SetCursorPosition(10, 26);
                Console.WriteLine("                 ");
                symbol_check = '+';
            }
            else
                symbol_check = ' ';
            PerviousIndex[0] = perx;
            PerviousIndex[1] = pery;
            return checking;
        }
        static bool allahinbelasi(int x, int y)
        {
            bool checking = false;
            perx = PerviousIndex[0];
            pery = PerviousIndex[1];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Player % 2 == 0 && color[i, j] == 'R' && elements[i, j] != '.')
                    {
                        PerviousIndex[0] = i;
                        PerviousIndex[1] = j;
                        if (elements[i, j] == 'P')
                        {
                            if ((i - 1 >= 0) && (j + 1 < 8) && (j - 1 >= 0) && ((elements[i - 1, j + 1] == elements[x, y] && color[i - 1, j + 1] == 'B') || (elements[i - 1, j - 1] == elements[x, y]) && color[i - 1, j - 1] == 'B'))
                            {
                                checking = true;
                                demotasi[0] = i;
                                demotasi[1] = j;

                            }
                        }
                        else if (elements[i, j] == 'B')
                            checking = Bishops(x, y);
                        else if (elements[i, j] == 'Q')
                            checking = Queen(x, y);
                        else if (elements[i, j] == 'R')
                            checking = Rooks(x, y);
                        else if (elements[i, j] == 'K')
                            checking = King(x, y);
                        else if (elements[i, j] == 'N')
                            checking = Knights(x, y);
                    }
                    if (Player % 2 == 1 && color[i, j] == 'B' && elements[i, j] != '.')
                    {
                        PerviousIndex[0] = i;
                        PerviousIndex[1] = j;
                        if (elements[i, j] == 'P')
                        {
                            if ((i + 1 < 8) && (j + 1 < 8) && (j - 1 >= 0) && ((elements[i + 1, j + 1] == elements[x, y] && color[i + 1, j + 1] == 'R') || (elements[i + 1, j - 1] == elements[x, y]) && color[i + 1, j - 1] == 'R'))
                            {
                                checking = true;
                                demotasi[0] = i;
                                demotasi[1] = j;
                            }
                        }
                        else if (elements[i, j] == 'B')
                            checking = Bishops(x, y);
                        else if (elements[i, j] == 'Q')
                            checking = Queen(x, y);
                        else if (elements[i, j] == 'R')
                            checking = Rooks(x, y);
                        else if (elements[i, j] == 'K')
                            checking = King(x, y);
                        else if (elements[i, j] == 'N')
                            checking = Knights(x, y);
                    }
                    if (checking == true)
                        break;
                }
                if (checking == true)
                    break;
            }
            if (checking == true && rook == false)
            {
                Console.SetCursorPosition(10, 26);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("!!! CHECK !!!", (Player % 2));
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.SetCursorPosition(10, 26);
                Console.WriteLine("                 ");
                symbol_check = '+';
            }
            else
                symbol_check = ' ';
            PerviousIndex[0] = perx;
            PerviousIndex[1] = pery;
            return checking;
        }
        static void Check_kontrol()
        {
            int şahx = 0, şahy = 0;
            bool kontrol = false;
            bool kontrol2 = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Player % 2 == 0 && color[i, j] == 'R' && elements[i, j] == 'K')
                    {
                        şahx = i;
                        şahy = j;
                        kontrol2 = true;
                        break;
                    }
                    else if (Player % 2 == 1 && color[i, j] == 'B' && elements[i, j] == 'K')
                    {
                        şahx = i;
                        şahy = j;
                        kontrol2 = true;
                        break;
                    }
                }
                if (kontrol2 == true)
                    break;
            }
            kontrol = checking(şahx, şahy);
        }
        static bool aynıandabulunma(int x, int y)
        {
            aynıanda = false;
            aynıkale = false;
            aynıat = false;
            aynıfil = false;
            aynıvezir = false;
            aynıandafil = 0;
            aynıandavezir = 0;
            aynıandakale = 0;
            aynıandaat = 0;
            perx = PerviousIndex[0];
            pery = PerviousIndex[1];
            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, z = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PerviousIndex[0] = i;
                    PerviousIndex[1] = j;
                    if ((Player % 2 == 0 && color[i, j] == 'B') || (Player % 2 == 1 && color[i, j] == 'R'))
                    {
                        if (elements[i, j] == 'R')
                        {
                            if (Rooks(x, y) == true)
                            {
                                kalekonumları[b, c] = i;
                                c++;
                                kalekonumları[b, c] = j;
                                c = 0;
                                b++;
                                aynıandakale++;
                            }
                            if (aynıandakale > 1)
                            {
                                aynıanda = true;
                                aynıkale = true;
                            }
                        }
                        if (elements[i, j] == 'B')
                        {
                            if (Bishops(x, y) == true)
                            {
                                filkonumları[f, g] = i;
                                g++;
                                filkonumları[f, g] = j;
                                g = 0;
                                f++;
                                aynıandafil++;
                            }
                            if (aynıandafil > 1)
                            {
                                aynıanda = true;
                                aynıfil = true;
                            }
                        }
                        if (elements[i, j] == 'N')
                        {
                            if (Knights(x, y) == true)
                            {
                                atkonumları[d, e] = i;
                                e++;
                                atkonumları[d, e] = j;
                                e = 0;
                                d++;
                                aynıandaat++;
                            }
                            if (aynıandaat > 1)
                            {
                                aynıanda = true;
                                aynıat = true;
                            }
                        }
                        if (elements[i, j] == 'Q')
                        {
                            if (Queen(x, y) == true)
                            {
                                vezirkonumları[a, z] = i;
                                z++;
                                vezirkonumları[a, z] = j;
                                z = 0;
                                a++;
                                aynıandavezir++;
                            }
                            if (aynıandavezir > 1)
                            {
                                aynıanda = true;
                                aynıvezir = true;
                            }
                        }
                    }
                }
            }
            PerviousIndex[0] = perx;
            PerviousIndex[1] = pery;
            return aynıanda;
        }
        static bool Hint()
        {
            perx = PerviousIndex[0];
            pery = PerviousIndex[1];
            for (int i = 0; i < hinttasları.GetLength(0); i++)
            {
                for (int j = 0; j < hinttasları.GetLength(1); j++)
                {
                    hinttasları[i, j] = ' ';
                }
            }
            int a = 0, b = 0;
            bool hint = false;
            bool fil = false;
            bool at = false;
            bool kale = false;
            bool vezir = false;
            bool sah = false;
            for (int i = 0; i < 8; i++)    ///kendi takımının taslarında dolasacak
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Player % 2 == 0 && color[i, j] == 'B')
                    {
                        if (elements[i, j] == 'P' && ((j + 1) < 8 && color[i - 1, j + 1] == 'R'))
                        {
                            hint = true;
                            hinttasları[a, b] = Letters[j];
                            hinttasları[a, b + 1] = Letters[j + 1];
                            hinttasları[a, b + 2] = (char)((8 - (i - 1)) + '0');
                            b = 0;
                            a++;
                        }
                        if (elements[i, j] == 'P' && ((j - 1) >= 0 && color[i - 1, j - 1] == 'R'))
                        {
                            hint = true;
                            hinttasları[a, b] = Letters[j];
                            hinttasları[a, b + 1] = Letters[j - 1];
                            hinttasları[a, b + 2] = (char)((8 - (i - 1)) + '0');
                            b = 0;
                            a++;
                        }
                        if (elements[i, j] == 'B')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'R' && elements[k, l] != '.')
                                    {
                                        fil = Bishops(k, l);
                                        if (fil == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'B';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        fil = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'N')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'R' && elements[k, l] != '.')
                                    {
                                        at = Knights(k, l);
                                        if (at == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'N';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        at = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'R')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'R' && elements[k, l] != '.')
                                    {
                                        kale = Rooks(k, l);
                                        if (kale == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'R';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        kale = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'Q')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'R' && elements[k, l] != '.')
                                    {
                                        vezir = Queen(k, l);
                                        if (vezir == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'Q';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        vezir = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'K')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'R' && elements[k, l] != '.')
                                    {
                                        sah = King(k, l);
                                        if (sah == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'K';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        sah = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (Player % 2 == 1 && color[i, j] == 'R')
                    {
                        if (elements[i, j] == 'P' && ((j + 1) < 8 && color[i + 1, j + 1] == 'B'))
                        {
                            hint = true;
                            hinttasları[a, b] = Letters[j];
                            hinttasları[a, b + 1] = Letters[j + 1];
                            hinttasları[a, b + 2] = (char)((8 - (i + 1)) + '0');
                            b = 0;
                            a++;
                        }
                        if (elements[i, j] == 'P' && ((j - 1) >= 0 && color[i + 1, j - 1] == 'B'))
                        {
                            hint = true;
                            hinttasları[a, b] = Letters[j];
                            hinttasları[a, b + 1] = Letters[j - 1];
                            hinttasları[a, b + 2] = (char)((8 - (i + 1)) + '0');
                            b = 0;
                            a++;
                        }
                        if (elements[i, j] == 'B')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'B' && elements[k, l] != '.')
                                    {
                                        fil = Bishops(k, l);
                                        if (fil == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'B';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        fil = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'N')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'B' && elements[k, l] != '.')
                                    {
                                        at = Knights(k, l);
                                        if (at == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'N';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        at = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'R')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'B' && elements[k, l] != '.')
                                    {
                                        kale = Rooks(k, l);
                                        if (kale == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'R';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        kale = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'Q')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'B' && elements[k, l] != '.')
                                    {
                                        vezir = Queen(k, l);
                                        if (vezir == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'Q';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        vezir = false;
                                    }
                                }
                            }
                        }
                        if (elements[i, j] == 'K')
                        {
                            PerviousIndex[0] = i;
                            PerviousIndex[1] = j;
                            for (int k = 0; k < 8; k++)
                            {
                                for (int l = 0; l < 8; l++)
                                {
                                    if (color[k, l] == 'B' && elements[k, l] != '.')
                                    {
                                        sah = King(k, l);
                                        if (sah == true)
                                        {
                                            hint = true;
                                            hinttasları[a, b] = 'K';
                                            hinttasları[a, b + 1] = Letters[l];
                                            hinttasları[a, b + 2] = (char)(8 - k + '0');
                                            b = 0;
                                            a++;
                                        }
                                        sah = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            PerviousIndex[0] = perx;
            PerviousIndex[1] = pery;
            return hint;
        }
        static void Demo(int x, int y)
        {

            bool demoyerleştirme = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PerviousIndex[0] = i;
                    PerviousIndex[1] = j;

                    if (Player % 2 == 0 && color[i, j] == 'B')
                    {
                        if (elements[i, j] == 'P' && bilmiyom == 'P')
                        {
                            demoyerleştirme = Pawns(x, y);
                        }
                        else if (elements[i, j] == 'R' && bilmiyom == 'R')
                        {
                            demoyerleştirme = Rooks(x, y);
                        }
                        else if (elements[i, j] == 'N' && bilmiyom == 'N')
                        {
                            demoyerleştirme = Knights(x, y);
                        }
                        else if (elements[i, j] == 'Q' && bilmiyom == 'Q')
                        {
                            demoyerleştirme = Queen(x, y);
                        }
                        else if (elements[i, j] == 'B' && bilmiyom == 'B')
                        {
                            demoyerleştirme = Bishops(x, y);
                        }
                        else if (elements[i, j] == 'K' && bilmiyom == 'K')
                        {
                            demoyerleştirme = King(x, y);
                        }
                    }
                    else if (Player % 2 == 1 && color[i, j] == 'R')
                    {
                        if (elements[i, j] == 'P' && bilmiyom == 'P')
                        {
                            demoyerleştirme = Pawns(x, y);
                        }
                        else if (elements[i, j] == 'R' && bilmiyom == 'R')
                        {
                            demoyerleştirme = Rooks(x, y);
                        }
                        else if (elements[i, j] == 'N' && bilmiyom == 'N')
                        {
                            demoyerleştirme = Knights(x, y);
                        }
                        else if (elements[i, j] == 'Q' && bilmiyom == 'Q')
                        {
                            demoyerleştirme = Queen(x, y);
                        }
                        else if (elements[i, j] == 'B' && bilmiyom == 'B')
                        {
                            demoyerleştirme = Bishops(x, y);
                        }
                        else if (elements[i, j] == 'K' && bilmiyom == 'K')
                        {
                            demoyerleştirme = King(x, y);
                        }
                    }
                    if (demoyerleştirme == true)
                    {
                        Piece[0] = elements[i, j];
                        PerviousColor[0] = color[i, j];
                        perx = i;
                        pery = j;
                        Place_Piece(x, y);
                        break;
                    }
                }
                if (demoyerleştirme == true)
                {
                    break;
                }
            }
        }
        static void Cursor_Movement()
        {
            Console.SetCursorPosition(cursorx - 1, paranthesisy);
            Console.Write(" ");
            Console.SetCursorPosition(cursorx + 1, paranthesisy);
            Console.Write(" ");
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.RightArrow && cursorx < 34)
                cursorx = cursorx + 4;
            paranthesisx = cursorx;
            if (cki.Key == ConsoleKey.LeftArrow && cursorx > 7)
                cursorx = cursorx - 4;
            paranthesisx = cursorx;
            if (cki.Key == ConsoleKey.UpArrow && cursory > 6)
                cursory = cursory - 2;
            paranthesisy = cursory;
            if (cki.Key == ConsoleKey.DownArrow && cursory < 20)
                cursory = cursory + 2;
            if (cki.Key == ConsoleKey.Escape)
                saved = true;
            paranthesisy = cursory;
            Console.SetCursorPosition(cursorx - 1, paranthesisy);
            Console.Write("(");
            Console.SetCursorPosition(cursorx + 1, paranthesisy);
            Console.Write(")");
        }
        static void FirstValues()
        {
            for (int row = 0; row < 8; row++)///////////// assigning the first array elements
            {
                for (int col = 0; col < 8; col++)
                {
                    if (row == 1 || row == 6)
                    {
                        elements[row, col] = 'P';////array's elements
                        if (row % 2 == 1)/////////////////////////////////color assignments ('R' red 'B' Blue 'W' white)
                            color[row, col] = 'R';
                        else
                            color[row, col] = 'B';
                    }
                    else if (row == 0 || row == 7)
                    {
                        if (col == 0 || col == 7)
                            elements[row, col] = 'R';
                        else if (col == 1 || col == 6)
                            elements[row, col] = 'N';
                        else if (col == 2 || col == 5)
                            elements[row, col] = 'B';
                        else if (col == 3)
                            elements[row, col] = 'Q';
                        else if (col == 4)
                            elements[row, col] = 'K';
                        if (row % 2 == 0)
                            color[row, col] = 'R';
                        else
                            color[row, col] = 'B';
                    }
                    else
                    {
                        elements[row, col] = '.';
                        color[row, col] = 'W';
                    }
                }
            }
        }
        static int Cursor_Locations_X(int a)
        {
            int location = (a - 6) - ((a - 6) / 2); //a=cursory
            return location;
        }
        static int Cursor_Locations_Y(int a)
        {
            int location = (a - 7) - ((a - 7) / 4 * 3); //a=cursorx
            return location;
        }
        static void Menu()
        {
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------MENU------------------------------------");
            Console.SetCursorPosition(10, 11);
            Console.WriteLine("|  1) To Play With Cursor Movements              -->PLEASE PRESS SPACEBAR  |");
            Console.SetCursorPosition(10, 12);
            Console.WriteLine("|  2) To Play With Entering the Move As a String -->PLEASE PRESS N         |");
            Console.SetCursorPosition(10, 13);
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.ResetColor();
        }
        static void Play_or_Demo_Menu()
        {
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------MENU-----------------------");
            Console.SetCursorPosition(10, 11);
            Console.WriteLine("|  1) Play Mode        -->PLEASE PRESS SPACEBAR  |");
            Console.SetCursorPosition(10, 12);
            Console.WriteLine("|  2) Demo Mode        -->PLEASE PRESS ENTER     |");
            Console.SetCursorPosition(10, 13);
            Console.WriteLine("--------------------------------------------------");
            Console.ResetColor();
        }
        static void Table()
        {
            Console.SetCursorPosition(16, 2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("~ CHESS ~");
            Console.ResetColor();
            Console.SetCursorPosition(50, 4);
            Console.WriteLine("The Piece");
            Console.SetCursorPosition(0, 4);/////////////printing to screen
            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("    +----------------------------------+");
                    Console.WriteLine("    |                                  |");
                }
                Console.Write("  {0} |  ", (8 - i));///////////to write the numbers at the beginning of the lines
                for (int j = 0; j < 8; j++)////////////to print colors
                {
                    if (color[i, j] == 'R')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (color[i, j] == 'B')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(elements[i, j] + "   ");/////////////////////////printing the array to the screen
                    Console.ResetColor();
                    if (j == 7)
                        Console.Write("|");
                }
                Console.WriteLine();
                if (i != 7)
                    Console.WriteLine("    |                                  |");
                if (i == 7)
                {
                    Console.WriteLine("    +----------------------------------+");
                    Console.WriteLine("       a   b   c   d   e   f   g   h  ");
                }
            }////end of screen printing
        }
        static void Take_Piece(int x, int y)
        {
            Console.SetCursorPosition(50, 5);
            Console.WriteLine(elements[x, y]);

            Piece[0] = elements[x, y];
            PerviousIndex[0] = x;
            PerviousIndex[1] = y;
            PerviousColor[0] = color[x, y];
        }
        static void Print_Moves(int x, int y, StreamWriter f)
        {
            Console.SetCursorPosition(position_x, position_y);
            char symbol = ' ';
            capturing = false;
            char capturing_x = ' ';
            perx = PerviousIndex[0];
            pery = PerviousIndex[1];
            if (sonrook == true)
            {
                if (Player % 2 == 0)
                {
                    if (Kingside == true)
                    {
                        Console.WriteLine("{0}. O-O", (position_y - 3));
                        f.Write("{0}. O-O", (position_y - 3));
                    }
                    else
                    {
                        Console.WriteLine("{0}. O-O-O", (position_y - 3));
                        f.Write("{0}. O-O-O", (position_y - 3));
                    }

                    position_x = position_x + 12;
                }
                else if (Player % 2 == 1)
                {
                    if (Kingside == true)
                    {
                        Console.WriteLine("O-O");
                        f.WriteLine("O-O");
                    }

                    else
                    {
                        Console.WriteLine("O-O-O");
                        f.WriteLine("O-O-O");
                    }

                    position_x = position_x - 12;
                    position_y++;
                }
            }
            else
            {
                char tasletter = ' ';
                int tasnumber = 0;
                string passant = " ";
                if (Piece2[0] != '.' || passantdemo == true)            //capturing x
                {
                    capturing = true;
                    capturing_x = 'x';
                }
                if (Piece[0] != 'P')
                {
                    symbol = Piece[0];
                }
                else if (Piece[0] == 'P' && capturing == true)
                {
                    capturing_x = 'x';
                    symbol = Letters[pery];
                }
                if ((pfm == true && counter_pawn == 2) || (pfm1 == true && counter_pawn2 == 2))
                {
                    passant = "e.p.";
                    symbol = Letters[pery];
                    capturing_x = 'x';
                    if (Player % 2 == 0)
                    {
                        Console.WriteLine("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], passant);
                        f.Write("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], passant);
                        position_x = position_x + 12;
                    }
                    else if (Player % 2 == 1)
                    {
                        Console.WriteLine("{0}{1}{2}{3}{4}", symbol, capturing_x, Letters[y], Numbers[x], passant);
                        f.WriteLine("{0}{1}{2}{3}{4}", symbol, capturing_x, Letters[y], Numbers[x], passant);
                        position_x = position_x - 12;
                        position_y++;
                    }
                    capturing = false;
                }
                else if (promoted == true)
                {
                    symbol = Letters[pery];
                    capturing_x = 'x';
                    if (Player % 2 == 0)
                    {
                        Console.WriteLine("{0}. {1}{2}{3}{4}{5}{6}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], Piece[0], symbol_check);
                        f.Write("{0}. {1}{2}{3}{4}{5}{6}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], Piece[0], symbol_check);
                        position_x = position_x + 12;
                    }
                    else if (Player % 2 == 1)
                    {
                        Console.WriteLine("{0}{1}{2}{3}{4}{5}", symbol, capturing_x, Letters[y], Numbers[x], Piece[0], symbol_check);
                        f.WriteLine("{0}{1}{2}{3}{4}{5}", symbol, capturing_x, Letters[y], Numbers[x], Piece[0], symbol_check);
                        position_x = position_x - 12;
                        position_y++;
                    }
                    capturing = false;
                }
                else if (aynıandagidebiliyor == true)
                {
                    if (Piece[0] == 'R' && aynıkale == true)
                    {
                        if (kalekonumları[0, 0] == kalekonumları[1, 0])
                            tasletter = Letters[pery];
                        else if (kalekonumları[0, 1] == kalekonumları[1, 1])
                            tasletter = (char)(8 - Numbers[pery] + '0');
                        else
                            tasletter = Letters[pery];
                    }
                    else if (Piece[0] == 'N' && aynıat == true)
                    {
                        if (atkonumları[0, 0] == atkonumları[1, 0])
                            tasletter = Letters[pery];
                        else if (atkonumları[0, 1] == atkonumları[1, 1])
                            tasletter = (char)(8 - Numbers[pery] + '0');
                        else
                            tasletter = Letters[pery];
                    }
                    else if (Piece[0] == 'B' && aynıfil == true)
                    {
                        if (filkonumları[0, 0] == filkonumları[1, 0])
                            tasletter = Letters[pery];
                        else if (filkonumları[0, 1] == filkonumları[1, 1])
                            tasletter = (char)(8 - Numbers[pery] + '0');
                        else
                            tasletter = Letters[pery];
                    }
                    else if (Piece[0] == 'Q' && aynıvezir == true)
                    {
                        if (vezirkonumları[0, 0] == vezirkonumları[1, 0])
                            tasletter = Letters[pery];
                        else if (vezirkonumları[0, 1] == vezirkonumları[1, 1])
                            tasletter = (char)(8 - Numbers[pery] + '0');
                        else
                            tasletter = Letters[pery];
                    }
                    if (capturing == true)
                    {
                        if (Player % 2 == 0)
                        {
                            Console.WriteLine("{0}. {1}{2}{3}{4}{5}{6}", (position_y - 3), symbol, tasletter, capturing_x, Letters[y], Numbers[x], symbol_check);
                            f.Write("{0}. {1}{2}{3}{4}{5}{6}", (position_y - 3), symbol, tasletter, capturing_x, Letters[y], Numbers[x], symbol_check);
                            position_x = position_x + 12;
                        }
                        else if (Player % 2 == 1)
                        {
                            Console.WriteLine("{0}{1}{2}{3}{4}{5}", symbol, tasletter, capturing_x, Letters[y], Numbers[x], symbol_check);
                            f.WriteLine("{0}{1}{2}{3}{4}{5}", symbol, tasletter, capturing_x, Letters[y], Numbers[x], symbol_check);
                            position_x = position_x - 12;
                            position_y++;
                        }
                    }
                    else
                    {
                        if (Player % 2 == 0)
                        {
                            Console.WriteLine("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, tasletter, Letters[y], Numbers[x], symbol_check);
                            f.Write("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, tasletter, Letters[y], Numbers[x], symbol_check);
                            position_x = position_x + 12;
                        }
                        else if (Player % 2 == 1)
                        {
                            Console.WriteLine("{0}{1}{2}{3}{4}", symbol, tasletter, Letters[y], Numbers[x], symbol_check);
                            f.WriteLine("{0}{1}{2}{3}{4}", symbol, tasletter, Letters[y], Numbers[x], symbol_check);
                            position_x = position_x - 12;
                            position_y++;
                        }
                    }
                }
                else if (capturing == true && promoted == false)
                {
                    if (Player % 2 == 0)
                    {
                        Console.WriteLine("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], symbol_check);
                        f.Write("{0}. {1}{2}{3}{4}{5}", (position_y - 3), symbol, capturing_x, Letters[y], Numbers[x], symbol_check);
                        position_x = position_x + 12;
                    }
                    else if (Player % 2 == 1)
                    {
                        Console.WriteLine("{0}{1}{2}{3}{4}", symbol, capturing_x, Letters[y], Numbers[x], symbol_check);
                        f.WriteLine("{0}{1}{2}{3}{4}", symbol, capturing_x, Letters[y], Numbers[x], symbol_check);
                        position_x = position_x - 12;
                        position_y++;
                    }
                }
                else if (capturing == false && promoted == false)
                {
                    if (Player % 2 == 0)
                    {
                        Console.WriteLine("{0}. {1}{2}{3}{4}", (position_y - 3), symbol, Letters[y], Numbers[x], symbol_check);
                        f.Write("{0}. {1}{2}{3}{4}", (position_y - 3), symbol, Letters[y], Numbers[x], symbol_check);
                        position_x = position_x + 12;
                    }
                    else if (Player % 2 == 1)
                    {
                        Console.WriteLine("{0}{1}{2}{3}", symbol, Letters[y], Numbers[x], symbol_check);
                        f.WriteLine("{0}{1}{2}{3}", symbol, Letters[y], Numbers[x], symbol_check);
                        position_x = position_x - 12;
                        position_y++;
                    }
                }
                if (pfm == true)                     ///passant sıfırlama
                {
                    if (counter_pawn == 2)
                    {
                        elements[x - 1, y] = '.';
                        color[x - 1, y] = 'W';
                        counter_pawn = 0;
                        pfm = false;
                    }
                }
                else if (pfm1 == true)              /////passant sıfırlama
                {
                    if (counter_pawn2 == 2)
                    {
                        elements[x + 1, y] = '.';
                        color[x + 1, y] = 'W';
                        counter_pawn2 = 0;
                        pfm1 = false;
                    }
                }
            }
        }
        static void Place_Piece(int x, int y)
        {
            aynıandagidebiliyor = false;
            Piece2[0] = elements[x, y];
            if (Piece[0] == 'K')      ///rook kontrolü seçilen taş K ise
            {
                if (Rook(x, y))
                {
                    if (Kingside == true)
                    {
                        elements[x, y - 1] = 'K';
                        elements[x, y - 2] = 'R';

                        if (Player % 2 == 0)
                        {
                            color[x, y - 1] = 'B';      //yeni konumların rengini belirleme
                            color[x, y - 2] = 'B';
                        }
                        else
                        {
                            color[x, y - 1] = 'R';    //yeni konumların rengini belirleme
                            color[x, y - 2] = 'R';
                        }

                        elements[x, y] = '.';   //kalenin eski konumlarını silme
                        color[x, y] = 'W';
                    }
                    else if (Queenside == true)
                    {
                        elements[x, y + 2] = 'K';
                        elements[x, y + 3] = 'R';
                        if (Player % 2 == 0)
                        {
                            color[x, y + 2] = 'B';      //yeni konumların rengini belirleme
                            color[x, y + 3] = 'B';
                        }
                        else
                        {
                            color[x, y + 2] = 'R';    //yeni konumların rengini belirleme
                            color[x, y + 3] = 'R';
                        }

                        elements[x, y] = '.';
                        color[x, y] = 'W';
                    }
                    elements[PerviousIndex[0], PerviousIndex[1]] = '.';    //deleting old ones
                    color[PerviousIndex[0], PerviousIndex[1]] = 'W';
                }

            }
            else
            {
                promoted = false;
                if ((Piece[0] == 'P' && (x == 7)) || (Piece[0] == 'P' && (x == 0))) //PROMOTION
                {
                    promoted = true;
                    Console.SetCursorPosition(10, 26);
                    if (demo_promotion == ' ')
                    {
                        Console.Write("Choose one of them! R,N,B,Q :  ");
                        char promotion = Convert.ToChar(Console.ReadLine());
                        Piece[0] = promotion;
                        promotion = ' ';
                        Console.SetCursorPosition(10, 26);
                        Console.Write("                                     ");
                    }
                    else
                        Piece[0] = demo_promotion;
                    elements[PerviousIndex[0], PerviousIndex[1]] = '.';    //deleting old ones
                    color[PerviousIndex[0], PerviousIndex[1]] = 'W';
                }
                if (aynıandabulunma(xkismi, ykismi) == true)
                {
                    aynıandagidebiliyor = true;
                    if (bilmiyom == 'R' && aynıkale == true)
                    {
                        if (aynitasinsayisi == 0)
                        {
                            PerviousIndex[1] = harftensayıya(aynitasinharfi);
                            if (kalekonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = kalekonumları[0, 0];
                            }
                            else if (kalekonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = kalekonumları[1, 0];
                            }
                        }
                        else
                        {
                            PerviousIndex[1] = 8 - aynitasinsayisi;
                            if (kalekonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = kalekonumları[0, 0];
                            }
                            else if (kalekonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = kalekonumları[1, 0];
                            }
                        }
                    }
                    else if (bilmiyom == 'N' && aynıat == true)
                    {
                        if (aynitasinsayisi == 0)
                        {
                            PerviousIndex[1] = harftensayıya(aynitasinharfi);
                            if (atkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = atkonumları[0, 0];
                            }
                            else if (atkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = atkonumları[1, 0];
                            }
                        }
                        else
                        {
                            PerviousIndex[1] = 8 - aynitasinsayisi;
                            if (atkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = atkonumları[0, 0];
                            }
                            else if (atkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = atkonumları[1, 0];
                            }
                        }
                    }
                    else if (bilmiyom == 'B' && aynıfil == true)
                    {
                        if (aynitasinsayisi == 0)
                        {
                            PerviousIndex[1] = harftensayıya(aynitasinharfi);
                            if (filkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = filkonumları[0, 0];
                            }
                            else if (filkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = filkonumları[1, 0];
                            }
                        }
                        else
                        {
                            PerviousIndex[1] = 8 - aynitasinsayisi;
                            if (filkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = filkonumları[0, 0];
                            }
                            else if (filkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = filkonumları[1, 0];
                            }
                        }
                    }
                    else if (bilmiyom == 'Q' && aynıvezir == true)
                    {
                        if (aynitasinsayisi == 0)
                        {
                            PerviousIndex[1] = harftensayıya(aynitasinharfi);
                            if (vezirkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = vezirkonumları[0, 0];
                            }
                            else if (vezirkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[0] = vezirkonumları[1, 0];
                            }
                        }
                        else
                        {
                            PerviousIndex[1] = 8 - aynitasinsayisi;
                            if (vezirkonumları[0, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = vezirkonumları[0, 0];
                            }
                            else if (vezirkonumları[1, 1] == PerviousIndex[1])
                            {
                                PerviousIndex[1] = vezirkonumları[1, 0];
                            }
                        }
                    }
                }
                elements[x, y] = Piece[0];     //placing new ones
                color[x, y] = PerviousColor[0];
                elements[PerviousIndex[0], PerviousIndex[1]] = '.';    //deleting old ones
                color[PerviousIndex[0], PerviousIndex[1]] = 'W';
            }



        }
        static int harftensayıya(char a)
        {
            int sayı = 0;
            if (a == 'a')
                sayı = 0;
            else if (a == 'b')
                sayı = 1;
            else if (a == 'c')
                sayı = 2;
            else if (a == 'd')
                sayı = 3;
            else if (a == 'e')
                sayı = 4;
            else if (a == 'f')
                sayı = 5;
            else if (a == 'g')
                sayı = 6;
            else if (a == 'h')
                sayı = 7;
            return sayı;
        }
        static int indextensayıya(int a)
        {
            int sayı = 0;
            if (a == '1')
                sayı = 0;
            else if (a == '2')
                sayı = 1;
            else if (a == '3')
                sayı = 2;
            else if (a == '4')
                sayı = 3;
            else if (a == '5')
                sayı = 4;
            else if (a == '6')
                sayı = 5;
            else if (a == '7')
                sayı = 6;
            else if (a == '8')
                sayı = 0;
            return sayı;
        }
        static void Warning(int a)
        {
            string warning = "";
            if (a == 0)
                warning = "!!!Can Not Take This Piece !!!";
            else if (a == 1)
                warning = "!!! This Piece Can Not Place Here !!!";
            Console.SetCursorPosition(10, 25);
            Console.WriteLine(warning);
            Thread.Sleep(1000);
            Console.SetCursorPosition(10, 25);
            Console.WriteLine("                                     ");
        }
        static int Notation_y(string Notation)
        {
            int location_y = 0;
            if (Notation[0] == 'a')
                location_y = 0;
            else if (Notation[0] == 'b')
                location_y = 1;
            else if (Notation[0] == 'c')
                location_y = 2;
            else if (Notation[0] == 'd')
                location_y = 3;
            else if (Notation[0] == 'e')
                location_y = 4;
            else if (Notation[0] == 'f')
                location_y = 5;
            else if (Notation[0] == 'g')
                location_y = 6;
            else if (Notation[0] == 'h')
                location_y = 7;
            return location_y;
        }
        static int Notation_x(string Notation)
        {
            int location_x = 0;
            if (Notation[1] == '1')
                location_x = 7;
            else if (Notation[1] == '2')
                location_x = 6;
            else if (Notation[1] == '3')
                location_x = 5;
            else if (Notation[1] == '4')
                location_x = 4;
            else if (Notation[1] == '5')
                location_x = 3;
            else if (Notation[1] == '6')
                location_x = 2;
            else if (Notation[1] == '7')
                location_x = 1;
            else if (Notation[1] == '8')
                location_x = 0;
            return location_x;
        }
        static void hintsorgulama()
        {
            if (Hint() == true)
            {
                int konum = 25;
                Console.SetCursorPosition(10, konum);
                Console.WriteLine("HINT:");
                konum++;
                for (int i = 0; i < hinttasları.GetLength(0); i++)
                {
                    Console.SetCursorPosition(10, konum);

                    for (int j = 0; j < hinttasları.GetLength(1); j++)
                    {
                        if (hinttasları[i, j] != ' ')
                        {
                            Console.Write(hinttasları[i, j]);
                            if (j == 0)
                                Console.Write("x");
                            if (j == 2)
                                konum++;
                        }
                    }
                }
                Console.WriteLine();
                Thread.Sleep(5000);
                Console.SetCursorPosition(10, 29);
                Console.WriteLine("                                     ");
                Console.SetCursorPosition(10, 30);
                Console.WriteLine("                                     ");
                Console.SetCursorPosition(10, 31);
                Console.WriteLine("                                     ");
                Console.SetCursorPosition(10, 32);
                Console.WriteLine("                                     ");
                Console.SetCursorPosition(10, 33);
                Console.WriteLine("                                     ");
                Console.SetCursorPosition(10, 34);
                Console.WriteLine("                                     ");
            }
        }
        static void oyun(StreamWriter f)
        {
            while (true)
            {
                Console.SetCursorPosition(10, 24);
                if (Player % 2 == 0)
                    Console.WriteLine("1 . PLAYER (Blue)");
                else
                    Console.WriteLine("2 . PLAYER (Red) ");
                Console.SetCursorPosition(50, 5);
                Console.WriteLine("                          ");      //after the piece is placed, piece is deleted it so that it does not remain on the screen
                Console.SetCursorPosition(60, 5);
                if (saved == true)
                {
                    Console.Clear();
                    saved = true;
                    Console.SetCursorPosition(24, 12);
                    Console.WriteLine("-------------THE GAME HAS BEEN SAVED-------------");
                    f.Close();
                    break;
                }
                while (true)///////////////////////////////////////////Cursor Movement
                {
                    if (saved == true) break;
                    Console.SetCursorPosition(cursorx, cursory);
                    if (Console.KeyAvailable)
                    {
                        Cursor_Movement();
                        if (cki.Key == ConsoleKey.Enter)
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            location_x = Cursor_Locations_X(cursory);
                            location_y = Cursor_Locations_Y(cursorx);

                            if ((Player % 2 == 0 && color[location_x, location_y] == 'R') || (Player % 2 == 1 && color[location_x, location_y] == 'B'))
                                Warning(0);
                            if ((Player % 2 == 0 && color[location_x, location_y] == 'B' && elements[location_x, location_y] != '.') || (Player % 2 == 1 && color[location_x, location_y] == 'R' && elements[location_x, location_y] != '.'))
                            {
                                Take_Piece(location_x, location_y);
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(50, 5);
                                Console.WriteLine("                          ");
                            }
                        }
                        else if (cki.Key == ConsoleKey.H)
                        {
                            hintsorgulama();
                        }
                    }
                }
                while (true)///////////////////////////////////////////Cursor Movement
                {
                    sonrook = false;
                    if (saved == true) break;
                    Console.SetCursorPosition(cursorx, cursory);
                    if (Console.KeyAvailable)
                    {
                        Cursor_Movement();
                        if (cki.Key == ConsoleKey.Enter)
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            location_x2 = Cursor_Locations_X(cursory);
                            location_y2 = Cursor_Locations_Y(cursorx);

                            if (Piece[0] == 'R')
                                checking_placement = Rooks(location_x2, location_y2);
                            else if (Piece[0] == 'N')
                                checking_placement = Knights(location_x2, location_y2);
                            else if (Piece[0] == 'B')
                                checking_placement = Bishops(location_x2, location_y2);
                            else if (Piece[0] == 'Q')
                                checking_placement = Queen(location_x2, location_y2);
                            else if (Piece[0] == 'P')
                                checking_placement = Pawns(location_x2, location_y2);
                            //  else if (Piece[0] == 'K')
                            //   checking_placement = King(location_x2, location_y2);
                            sonrook = Rook(location_x2, location_y2);

                            if (checking_placement == true && (color[PerviousIndex[0], PerviousIndex[1]] != color[location_x2, location_y2]))
                            {
                                Place_Piece(location_x2, location_y2);
                                Check_kontrol();
                                Print_Moves(location_x2, location_y2, f);
                                Player++;
                            }
                            else if (sonrook == true && checking_placement == true && Piece[0] == 'K' && elements[location_x2, location_y2] == 'R' && (color[PerviousIndex[0], PerviousIndex[1]] == color[location_x2, location_y2]))
                            {
                                Place_Piece(location_x2, location_y2);
                                // Check_kontrol();
                                Print_Moves(location_x2, location_y2, f);
                                Player++;
                                Kingside = false;
                                Queenside = false;
                                rook = false;
                                sonrook = false;
                            }
                            else
                                Warning(1);
                            Table();
                            kingcounter = 0;
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (elements[i, j] == 'K')
                                    {
                                        kingcounter++;

                                    }
                                }
                            }
                            if (kingcounter == 1)
                            {
                                Console.Clear();
                                gameover = true;
                                Console.SetCursorPosition(24, 12);
                                Console.WriteLine("-------------GAME OVER-------------");
                                f.Close();
                            }

                            break;

                        }

                    }

                }
                if (saved == true)
                {
                    Console.Clear();
                    saved = true;
                    Console.SetCursorPosition(24, 12);
                    Console.WriteLine("-------------THE GAME HAS BEEN SAVED-------------");
                    f.Close();
                    break;
                }
                if (gameover == true || saved == true)
                    break;

            }
        }
        static void demonotation(string nottt)
        {
            aynitasinsayisi = 0;
            aynitasinharfi = ' ';
            passantdemo = false;
            string tas = " ";
            tas = nottt;
            if (nottt.Length == 2)
            {
                bilmiyom = 'P';
            }
            else if (nottt.Length == 3)
            {
                bilmiyom = tas[0];
                tas = tas.Substring(1);
            }
            else if (nottt.Contains("ep") == true || nottt.Contains("e.p.") == true)
            {
                passantdemo = true;
                bilmiyom = 'P';
                tas = tas.Substring(2, 2);
            }
            else if (nottt.Contains("+") == true)
            {
                if (nottt.Length == 4)
                {
                    bilmiyom = tas[0];
                    tas = tas.Substring(1, 2);
                }
                else if (nottt.Length == 3)
                {
                    bilmiyom = 'P';
                    tas = tas.Substring(0, 2);
                }
            }
            else if (nottt.Length == 4 && nottt.Contains('x') == false)  //aynı anda gidilebilirlik 
            {
                bilmiyom = tas[0];
                if (Convert.ToInt16(nottt[1]) < 97)
                {
                    aynitasinsayisi = nottt[1];
                }
                aynitasinharfi = nottt[1];
                tas = tas.Substring(2, 2);
            }
            else if (nottt.Length == 5 && nottt.Contains('x') == true)
            {
                bilmiyom = tas[0];
                if (Convert.ToInt16(nottt[1]) < 97)
                {
                    aynitasinsayisi = nottt[1];
                }
                aynitasinharfi = nottt[1];
                tas = tas.Substring(3, 2);
            }
            else if (tas[tas.Length - 1] == 'Q' || tas[tas.Length - 1] == 'R' || tas[tas.Length - 1] == 'N' || tas[tas.Length - 1] == 'B')
            {//promotion
                demo_promotion = tas[tas.Length - 1];
                bilmiyom = 'R';
                if (tas.Length == 3)
                {
                    tas = tas.Substring(0, 2);
                }
                else if (tas.Contains('x') == true)
                {
                    tas = tas.Substring(2, 2);
                }
            }
            else if (nottt.Length == 4 && nottt.Contains('x') == true)
            {
                if (tas[0] != 'a' && tas[0] != 'b' && tas[0] != 'c' && tas[0] != 'd' && tas[0] != 'e' && tas[0] != 'f' && tas[0] != 'g' && tas[0] != 'h') //yani piyon değilse
                {
                    bilmiyom = tas[0];
                }
                else
                {
                    bilmiyom = 'P';
                }
                tas = tas.Substring(2, 2);
            }
            else if (nottt.Length == 4 && nottt.Contains('x') == false)
            {
                bilmiyom = tas[0];
                demoaynıtas[0] = (char)(tas[1] + '0');
                tas = tas.Substring(2, 2);
            }
            ykismi = Notation_y(tas);
            xkismi = Notation_x(tas);
        }
        static void Main(string[] args)
        {
            bool p_stat = false;
            Play_or_Demo_Menu();
            cki = Console.ReadKey(true);
            if ((cki.Key == ConsoleKey.Enter))
            {
                int a = 0;
                int b = 0;
                string kaceleman;
                int hamlesayi = 0;
                string tas = " ";
                Console.Clear();
                Console.SetCursorPosition(35, 12);
                Console.Write("Please enter your file name: ");
                filename = Console.ReadLine();
                Console.SetCursorPosition(35, 15);
                Console.Write("Please enter your name: ");
                string filename2 = Console.ReadLine();
                filename18 = File.CreateText(filename2 + ".txt");

                Console.Clear();
                StreamReader demo2 = File.OpenText(filename + ".txt");
                do
                {
                    kaceleman = demo2.ReadLine();
                    hamlesayi++;

                } while (!demo2.EndOfStream);
                string[] boss = new string[hamlesayi * 2];
                demo2.Close();
                StreamReader demo = File.OpenText(filename + ".txt");
                do
                {
                    string str = demo.ReadLine();
                    string[] str_2 = new string[3];
                    str = str.Replace(".", "");
                    str = str.Replace("  ", " ");
                    str_2 = str.Split(" ");
                    boss[a] = str_2[1];
                    a++;
                    boss[a] = str_2[2];
                    a++;
                } while (!demo.EndOfStream);
                demo.Close();
                FirstValues();
                Table();
                ykismi = 0;
                xkismi = 0;
                do
                {
                    demo_promotion = ' ';
                    if (b > (boss.Length - 1))
                        break;
                    Console.SetCursorPosition(10, 24);
                    if (Player % 2 == 0)
                        Console.WriteLine("1 . PLAYER (Blue)");
                    else
                        Console.WriteLine("2 . PLAYER (Red) ");
                    Console.SetCursorPosition(50, 5);
                    Console.WriteLine("                          ");      //after the piece is placed, piece is deleted it so that it does not remain on the screen
                    Console.SetCursorPosition(60, 5);
                    tas = boss[b];
                    passantdemo = false;
                    if (boss[b] == "") break;

                    demonotation(boss[b]);
                    PerviousIndex[0] = xkismi;
                    PerviousIndex[1] = ykismi;
                    Demo(xkismi, ykismi);
                    Check_kontrol();
                    Print_Moves(xkismi, ykismi, filename18);
                    Table();

                    cki = Console.ReadKey(true);
                    b++;
                    Player++;

                    if (cki.Key == ConsoleKey.P)
                    {
                        p_stat = true;
                        break;
                    }
                } while (cki.Key == ConsoleKey.Spacebar);
                filename18.Close();
            }
            if (cki.Key == ConsoleKey.Spacebar || cki.Key == ConsoleKey.P)
            {
                int a = 0;
                int b = 0;
                string kaceleman;
                int hamlesayi = 0;
                string tas = " ";
                Console.Clear();
                Menu();         /////////printing menu
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();        //deleting menu
                    Console.SetCursorPosition(35, 9);
                    Console.Write("Do You Want To Continue Saved Game (Y OR N)");
                    string choice = Console.ReadLine();
                    if (choice == "Y")
                        p_stat = true;
                    Console.SetCursorPosition(35, 12);
                    Console.Write("Please enter your name:");
                    filename = Console.ReadLine();
                    if (p_stat == true)
                    {
                        Console.Clear();
                        StreamReader f1 = File.OpenText(filename + ".txt");
                        do
                        {
                            kaceleman = f1.ReadLine();
                            hamlesayi++;

                        } while (!f1.EndOfStream);
                        string[] boss1 = new string[hamlesayi * 2];
                        f1.Close();
                        StreamReader demo22 = File.OpenText(filename + ".txt");
                        do
                        {
                            string str1 = demo22.ReadLine();
                            string[] str_22 = new string[3];
                            str1 = str1.Replace(".", "");
                            str1 = str1.Replace("  ", " ");
                            str_22 = str1.Split(" ");
                            boss1[a] = str_22[1];
                            a++;
                            boss1[a] = str_22[2];
                            a++;
                        } while (!demo22.EndOfStream);
                        demo22.Close();
                        StreamWriter f2_2_2 = File.AppendText(filename + ".txt");
                        FirstValues();
                        Table();
                        ykismi = 0;
                        xkismi = 0;
                        position_y = 4;
                        do
                        {
                            demo_promotion = ' ';
                            if (b > (boss1.Length - 1))
                                break; Console.SetCursorPosition(10, 24);
                            if (Player % 2 == 0)
                                Console.WriteLine("1 . PLAYER (Blue)");
                            else
                                Console.WriteLine("2 . PLAYER (Red) ");
                            Console.SetCursorPosition(50, 5);
                            Console.WriteLine("                          ");      //after the piece is placed, piece is deleted it so that it does not remain on the screen
                            Console.SetCursorPosition(60, 5);
                            tas = boss1[b];
                            passantdemo = false;
                            if (boss1[b] == "") break;

                            demonotation(boss1[b]);
                            PerviousIndex[0] = xkismi;
                            PerviousIndex[1] = ykismi;
                            Demo(xkismi, ykismi);
                            Check_kontrol();
                            Print_Moves(xkismi, ykismi, f2_2_2);
                            Table();
                            //cki = Console.ReadKey(true);
                            b++;
                            Player++;
                        } while (true);

                        oyun(f2_2_2);
                    }
                    else
                    {
                        StreamWriter f = File.CreateText(filename + ".txt");
                        Console.Clear();        //deleting menu

                        FirstValues();
                        Table();
                        oyun(f);
                    }
                }
            }
            if (cki.Key == ConsoleKey.N)     ///////////////////////////NOTATION
            {
                Console.Clear();        //deleting menu
                Console.SetCursorPosition(35, 12);
                Console.Write("Please enter your names:");
                filename = Console.ReadLine();
                StreamWriter f = File.CreateText(filename + ".txt");
                Console.Clear();        //deleting menu
                FirstValues();
                Table();
                ykismi = 0;
                xkismi = 0;
                Console.SetCursorPosition(10, 24);
                if (Player % 2 == 0)
                    Console.WriteLine("1 . PLAYER (Blue)");
                else
                    Console.WriteLine("2 . PLAYER (Red) ");
                Console.SetCursorPosition(50, 5);
                Console.WriteLine("                          ");
                while (true)
                {
                    Console.SetCursorPosition(50, 7);//print under the part
                    Console.WriteLine("Please enter the notation : ");
                    Console.SetCursorPosition(50, 8);
                    Notation = Console.ReadLine();
                    Console.SetCursorPosition(50, 8);
                    Console.WriteLine("                             ");
                    if (Notation == "H")
                        hintsorgulama();
                    else if (Notation == "S")
                    {
                        Console.Clear();
                        saved = true;
                        Console.SetCursorPosition(24, 12);
                        Console.WriteLine("-------------THE GAME HAS BEEN SAVED-------------");
                        f.Close();
                        break;
                    }
                    else
                    {
                        demonotation(Notation);
                        PerviousIndex[0] = xkismi;
                        PerviousIndex[1] = ykismi;
                        Demo(xkismi, ykismi);
                        Check_kontrol();
                        Print_Moves(xkismi, ykismi, f);
                        Table();
                        Player++;
                        kingcounter = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (elements[i, j] == 'K')
                                {
                                    kingcounter++;

                                }
                            }
                        }
                        if (kingcounter == 1)
                        {
                            Console.Clear();
                            gameover = true;
                            Console.SetCursorPosition(24, 12);
                            Console.WriteLine("-------------GAME OVER-------------");
                            f.Close();
                            break;
                        }
                    }
                }
            }

        }//main
    }
}