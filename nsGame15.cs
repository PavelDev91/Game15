/*
 * Game15
 * Author: Pavel Dev
 * GitHub: PavelDev91
 * E-mail: PavelDev1991@gmail.com
 */

using System;
using System.IO;
using System.Threading;

//-----------------------------
using nsMyFunc_2;
using nsMyConsole;
//-----------------------------

namespace nsGame15
{
    //---------------------------------------------------------
    class Game15
    {
        //---------------------
        private struct sGameLevel
        {
            public int left;
            public int top;

            public int stepsCount;
            public int selectedIndex;

            public int[] gameLevel_Array;
        }
        //---------------------
        private struct sMyTimer
        {
            public Thread timerThread;

            public int timerValue;

            public string hour;
            public string minute;
            public string second;

            public int left;
            public int top;
        }
        //---------------------
        private struct sGameStatistics
        {
            public string hour;
            public string minute;
            public string second;

            public string dateTime;

            public int timeValue;

            public int stepsCount;
        }
        //---------------------
        private MyConsole workConsole;

        private ConsoleKey pressKey;

        private sGameLevel gameLevel;

        private sMyTimer myTimer;

        private sGameStatistics[] gameStatistics;
        //-----------------------------------------------------
        public Game15()
        {
            Console.SetWindowSize(128, 40);
            Console.SetBufferSize(128, 40);

            Console.SetWindowPosition(0, 0);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.CursorVisible = false;

            Load_Settings();

            Console.Clear();

            workConsole = new MyConsole(0, 0, Console.WindowWidth, Console.WindowHeight);

            Draw_GameMenu();
        }
        //-----------------------------------------------------
        private void GameTimer()
        {
            while (true)
            {
                myTimer.timerValue++;

                Thread.Sleep(1000);
            }
        }
        //-----------------------------------------------------
        private void MyTimer_Draw()
        {
            int hour, min, sec;

            string value;

            while(true)
            {
                hour = myTimer.timerValue / 3600;
                min = (myTimer.timerValue % 3600) / 60;
                sec = myTimer.timerValue % 60;

                if (hour < 10)
                {
                    value = "0" + hour + ":";

                    myTimer.hour = "0" + hour.ToString();
                }
                else
                {
                    value = hour + ":";

                    myTimer.hour = hour.ToString();
                }

                if (min < 10)
                {
                    value += "0" + min + ":";

                    myTimer.minute = "0" + min.ToString();
                }
                else
                {
                    value += min + ":";

                    myTimer.minute = min.ToString();
                }

                if (sec < 10)
                {
                    value += "0" + sec;

                    myTimer.second = "0" + sec.ToString();
                }
                else
                {
                    value += sec;

                    myTimer.second = sec.ToString();
                }

                Draw_GameField();
                Draw_GameLevel();

                workConsole.Write(myTimer.left, myTimer.top, "Game Timer: " + value);

                workConsole.Write(71, 4, "Steps Count: " + gameLevel.stepsCount.ToString());

                workConsole.Draw();
            }
        }
        //-----------------------------------------------------
        private void Draw_CopyRight()
        {
            workConsole.Clear();
            workConsole.Draw();

            Console.CursorVisible = false;

            Console.Title = "| Game15 | CopyRight |";

            //-------------------------------------------------
            workConsole.Write(2, 0, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 1, "| Program Name | Game15");
            workConsole.Write(2, 2, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 3, "| Version      | 1.0");
            workConsole.Write(2, 4, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 5, "| Author       | Pavel Dev");
            workConsole.Write(2, 6, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 7, "| CopyRight    | SB - SOFTWARE");
            workConsole.Write(2, 8, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 9, "| GitHub       | PavelDev91");
            workConsole.Write(2, 10, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 11, "| E-mail       | PavelDev1991@gmail.com");
            workConsole.Write(2, 12, new string('-', workConsole.GetWidth() - 4));

            workConsole.Write(2, 13, "");
            workConsole.Write(2, 14, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, 15, "| <=== Back to Menu | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
            workConsole.Write(2, 16, new string('-', workConsole.GetWidth() - 4));

            workConsole.Draw();
            //-------------------------------------------------

            while(true)
            {
                pressKey = Console.ReadKey(true).Key;

                if (pressKey == ConsoleKey.Escape)
                {
                    Draw_GameMenu();

                    return;
                }
            }
        }
        //-----------------------------------------------------
        private void Draw_GameMenu()
        {
            workConsole.Clear();
            workConsole.Draw();

            Console.CursorVisible = false;

            Console.Title = "Game15";
            //-------------------------------------------------
            workConsole.Write(2, 0, new string('-', workConsole.GetWidth() - 4));
            workConsole.Write((workConsole.GetWidth() - 7) / 2, 1, "Game15");
            workConsole.Write(2, 2, new string('-', workConsole.GetWidth() - 4));

            workConsole.Write(2, 3, "| New Game   | Press Key: '" + ConsoleKey.N.ToString() + "'");
            workConsole.Write(2, 4, "| CopyRight  | Press Key: '" + ConsoleKey.A.ToString() + "'");
            workConsole.Write(2, 5, "| Statistics | Press Key: '" + ConsoleKey.I.ToString() + "'");
            workConsole.Write(2, 6, "| Settings   | Press Key: '" + ConsoleKey.S.ToString() + "'");
            workConsole.Write(2, 7, new string('-', 40));
            workConsole.Write(2, 8, "| Exit       | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
            workConsole.Write(2, 9, new string('-', 40));

            workConsole.Draw();
            //-------------------------------------------------
            while (true)
            {
                pressKey = Console.ReadKey(true).Key;
                //---------------------------------------------
                if (pressKey == ConsoleKey.N)
                {
                    NewGame();

                    return;
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.A)
                {
                    Draw_CopyRight();

                    return;
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.I)
                {
                    Draw_Statistics();

                    return;
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.S)
                {
                    Draw_Settings();

                    return;
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.Escape)
                {
                    return;
                }
                //---------------------------------------------
            }
            //-------------------------------------------------
        }
        //-----------------------------------------------------
        private void Draw_GameResult()
        {
            workConsole.Clear();
            workConsole.Draw();

            Console.Title = "| Game15 | Game Result |";

            Console.CursorVisible = false;

            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            workConsole.Write((workConsole.GetWidth() - 11) / 2, workConsole.GetLineCount(), "Game Result");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));

            workConsole.Write(2, workConsole.GetLineCount(), "- Date/Time   | " + DateTime.Now.ToString("dd/MM/yy | HH:mm"));
            workConsole.Write(2, workConsole.GetLineCount(), "- Game Time   | " + myTimer.hour + ":" + myTimer.minute + ":" + myTimer.second);
            workConsole.Write(2, workConsole.GetLineCount(), "- Steps Count | " + gameLevel.stepsCount);

            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));

            workConsole.Draw();
            //-------------------------------------------------
            Load_GameStatistics();

            if (gameStatistics == null)
            {
                gameStatistics = new sGameStatistics[1];

                gameStatistics[0].hour = myTimer.hour;
                gameStatistics[0].minute = myTimer.minute;
                gameStatistics[0].second = myTimer.second;

                gameStatistics[0].dateTime = DateTime.Now.ToString("dd/MM/yy | HH:mm");
                gameStatistics[0].timeValue = myTimer.timerValue;
                gameStatistics[0].stepsCount = gameLevel.stepsCount;

                Save_GameStatistics();

                gameStatistics = null;
            }
            //-------------------------------------------------
            if (gameStatistics != null && gameStatistics.Length < 3)
            {
                sGameStatistics[] buf = gameStatistics;

                gameStatistics = new sGameStatistics[buf.Length + 1];

                for (int i = 0; i < buf.Length; i++)
                {
                    gameStatistics[i] = buf[i];
                }

                gameStatistics[gameStatistics.Length - 1].hour = myTimer.hour;
                gameStatistics[gameStatistics.Length - 1].minute = myTimer.minute;
                gameStatistics[gameStatistics.Length - 1].second = myTimer.second;

                gameStatistics[gameStatistics.Length - 1].dateTime = DateTime.Now.ToString("dd/MM/yy | HH:mm");
                gameStatistics[gameStatistics.Length - 1].timeValue = myTimer.timerValue;
                gameStatistics[gameStatistics.Length - 1].stepsCount = gameLevel.stepsCount;

                Save_GameStatistics();

                gameStatistics = null;
            }
            //-------------------------------------------------
            if (gameStatistics != null && gameStatistics.Length == 3)
            {
                bool wStatus = false;

                for (int i = 0; i < gameStatistics.Length; i++)
                {
                    if (gameStatistics[i].timeValue > myTimer.timerValue)
                    {
                        wStatus = true;

                        break;
                    }
                }

                if (wStatus == true)
                {
                    gameStatistics[gameStatistics.Length - 1].hour = myTimer.hour;
                    gameStatistics[gameStatistics.Length - 1].minute = myTimer.minute;
                    gameStatistics[gameStatistics.Length - 1].second = myTimer.second;

                    gameStatistics[gameStatistics.Length - 1].dateTime = DateTime.Now.ToString("dd/MM/yy | HH:mm");
                    gameStatistics[gameStatistics.Length - 1].timeValue = myTimer.timerValue;
                    gameStatistics[gameStatistics.Length - 1].stepsCount = gameLevel.stepsCount;
                }

                Save_GameStatistics();

                gameStatistics = null;
            }
            //-------------------------------------------------
            Load_GameStatistics();

            if (gameStatistics != null && gameStatistics.Length > 1)
            {
                sGameStatistics buf;

                int x = 0;

                bool wStatus;

                while (true)
                {
                    if (gameStatistics[x].timeValue > gameStatistics[x + 1].timeValue)
                    {
                        buf = gameStatistics[x];

                        gameStatistics[x] = gameStatistics[x + 1];
                        gameStatistics[x + 1] = buf;
                    }

                    x++;

                    if (x + 1 >= gameStatistics.Length)
                    {
                        x = 0;
                    }

                    wStatus = true;

                    for (int i = 0; i < gameStatistics.Length; i++)
                    {
                        if (i == gameStatistics.Length - 1)
                        {
                            break;
                        }

                        if (gameStatistics[i].timeValue > gameStatistics[i + 1].timeValue)
                        {
                            wStatus = false;

                            break;
                        }
                    }

                    if (wStatus == true)
                    {
                        break;
                    }
                }
            }

            Save_GameStatistics();
            //-------------------------------------------------
            while (true)
            {
                pressKey = Console.ReadKey(true).Key;
                //---------------------------------------------
                if (pressKey == ConsoleKey.Escape)
                {
                    Draw_GameMenu();

                    return;
                }
                //---------------------------------------------
            }
            //-------------------------------------------------
        }
        //-----------------------------------------------------
        private void Draw_Settings()
        {
            workConsole.Clear();
            workConsole.Draw();

            Console.CursorVisible = false;

            Console.Title = "| Game15 | Settings |";
            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            workConsole.Write((workConsole.GetWidth() - 8) / 2, workConsole.GetLineCount(), "Settings");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            //-------------------------------------------------

            string[] myList = new string[16];

            for (int i = 0; i < myList.Length; i++)
            {
                myList[i] = ((ConsoleColor)i).ToString();
            }

            int backColor = (int)Console.BackgroundColor;
            int foreColor = (int)Console.ForegroundColor;

            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), "| Background Color:");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 32));
            workConsole.DrawList(2, 5, workConsole.GetWidth() - 4, 4, myList, backColor, "* ", "- ");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 32));
            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), "| Foreground Color:");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 32));
            workConsole.DrawList(2, 12, workConsole.GetWidth() - 4, 4, myList, foreColor, "* ", "- ");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 32));
            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, workConsole.GetLineCount(), "* Set Background Color | Press Key: '" + ConsoleKey.B.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), "* Set Foreground Color | Press Key: '" + ConsoleKey.F.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            workConsole.Write(2, workConsole.GetLineCount(), "<=== Back to Menu      | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            //-------------------------------------------------
            workConsole.Draw();

            while (true)
            {
                pressKey = Console.ReadKey(true).Key;
                //---------------------------------------------
                if (pressKey == ConsoleKey.B)
                {
                    while (true)
                    {
                        pressKey = Console.ReadKey(true).Key;
                        //-------------------------------------
                        if (pressKey == ConsoleKey.UpArrow)
                        {
                            backColor--;

                            if (Console.ForegroundColor == (ConsoleColor)backColor)
                            {
                                backColor--;
                            }

                            if (backColor < 0)
                            {
                                if (Console.ForegroundColor == (ConsoleColor)(myList.Length - 1))
                                {
                                    backColor = myList.Length - 2;
                                }
                                else
                                {
                                    backColor = myList.Length - 1;
                                }
                            }

                            Console.BackgroundColor = (ConsoleColor)backColor;

                            workConsole.ReDraw();
                        }
                        //-------------------------------------
                        if (pressKey == ConsoleKey.DownArrow)
                        {
                            backColor++;

                            if (Console.ForegroundColor == (ConsoleColor)backColor)
                            {
                                backColor++;
                            }

                            if (backColor >= myList.Length)
                            {
                                if (Console.ForegroundColor == 0)
                                {
                                    backColor = 1;
                                }
                                else
                                {
                                    backColor = 0;
                                }
                            }

                            Console.BackgroundColor = (ConsoleColor)backColor;

                            workConsole.ReDraw();
                        }
                        //-------------------------------------
                        if (pressKey == ConsoleKey.Enter)
                        {
                            Console.BackgroundColor = (ConsoleColor)backColor;

                            workConsole.ReDraw();

                            Save_Settings();

                            break;
                        }
                        //-------------------------------------
                        workConsole.DrawList(2, 5, workConsole.GetWidth() - 4, 4, myList, backColor, "* ", "- ");

                        workConsole.Draw();
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.F)
                {
                    while (true)
                    {
                        pressKey = Console.ReadKey(true).Key;
                        //-------------------------------------
                        if (pressKey == ConsoleKey.UpArrow)
                        {
                            foreColor--;

                            if (Console.BackgroundColor == (ConsoleColor)foreColor)
                            {
                                foreColor--;
                            }

                            if (foreColor < 0)
                            {
                                if (Console.BackgroundColor == (ConsoleColor)(myList.Length - 1))
                                {
                                    foreColor = myList.Length - 2;
                                }
                                else
                                {
                                    foreColor = myList.Length - 1;
                                }
                            }

                            Console.ForegroundColor = (ConsoleColor)foreColor;

                            workConsole.ReDraw();
                        }
                        //-------------------------------------
                        if (pressKey == ConsoleKey.DownArrow)
                        {
                            foreColor++;

                            if (Console.BackgroundColor == (ConsoleColor)foreColor)
                            {
                                foreColor++;
                            }

                            if (foreColor >= myList.Length)
                            {
                                if (Console.BackgroundColor == 0)
                                {
                                    foreColor = 1;
                                }
                                else
                                {
                                    foreColor = 0;
                                }
                            }

                            Console.ForegroundColor = (ConsoleColor)foreColor;

                            workConsole.ReDraw();
                        }
                        //-------------------------------------
                        if (pressKey == ConsoleKey.Enter)
                        {
                            Console.ForegroundColor = (ConsoleColor)foreColor;

                            workConsole.ReDraw();

                            Save_Settings();

                            break;
                        }
                        //-------------------------------------
                        workConsole.DrawList(2, 12, workConsole.GetWidth() - 4, 4, myList, foreColor, "* ", "- ");

                        workConsole.Draw();
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.Escape)
                {
                    Draw_GameMenu();

                    return;
                }
                //---------------------------------------------
            }
        }
        //-----------------------------------------------------
        private void Draw_Statistics()
        {
            workConsole.Clear();
            workConsole.Draw();

            Console.CursorVisible = false;

            Console.Title = "| Game15 | Statistics |";
            //-------------------------------------------------
            if (Load_GameStatistics() == false)
            {
                string[] messageText = new string[1];

                messageText[0] = "Файл не найден, либо поврежден!";

                workConsole.ShowMessage(messageText, MyConsole.eMessageType.info, MyConsole.eMessageBtn.btnOk, out pressKey);

                Draw_GameMenu();

                return;
            }
            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            workConsole.Write((workConsole.GetWidth() - 9) / 2, workConsole.GetLineCount(), "Statistics");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));

            string buf;

            for (int i = 0; i < gameStatistics.Length; i++)
            {
                buf = "- Date/Time:   | " + gameStatistics[i].dateTime;

                workConsole.Write(2, workConsole.GetLineCount(), buf);

                buf = gameStatistics[i].hour + ":";
                buf += gameStatistics[i].minute + ":";
                buf += gameStatistics[i].second;

                buf = "- Game Time:   | " + buf;

                workConsole.Write(2, workConsole.GetLineCount(), buf);

                buf = gameStatistics[i].stepsCount.ToString();
                buf = "- Steps Count: | " + buf;

                workConsole.Write(2, workConsole.GetLineCount(), buf);

                workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));
            }

            workConsole.Write(2, workConsole.GetLineCount(), "| <=== Back to Menu | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', workConsole.GetWidth() - 4));

            workConsole.Draw();
            //-------------------------------------------------
            while (true)
            {
                pressKey = Console.ReadKey(true).Key;
                //---------------------------------------------
                if (pressKey == ConsoleKey.Escape)
                {
                    Draw_GameMenu();

                    return;
                }
                //---------------------------------------------
            }
            //-------------------------------------------------
        }
        //-----------------------------------------------------
        private void NewGame()
        {
            Console.CursorVisible = false;
            //-------------------------------------------------
            workConsole.Write(2, workConsole.GetLineCount(), "* Game Navigation:");
            workConsole.Write(2, workConsole.GetLineCount(), "- " + ConsoleKey.UpArrow.ToString());
            workConsole.Write(2, workConsole.GetLineCount(), "- " + ConsoleKey.DownArrow.ToString());
            workConsole.Write(2, workConsole.GetLineCount(), "- " + ConsoleKey.LeftArrow.ToString());
            workConsole.Write(2, workConsole.GetLineCount(), "- " + ConsoleKey.RightArrow.ToString());
            workConsole.Write(2, workConsole.GetLineCount(), "- Move | Press Key: '" + ConsoleKey.Enter.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 40));
            workConsole.Write(2, workConsole.GetLineCount(), "<=== Back to Menu | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
            workConsole.Write(2, workConsole.GetLineCount(), new string('-', 40));
            //-------------------------------------------------
            Create_GameLevel();
            //-------------------------------------------------
            Thread myThread = new Thread(GameTimer);
            myThread.IsBackground = true;
            //-------------------------------------------------
            myTimer = new sMyTimer();
            myTimer.left = 71;
            myTimer.top = 3;

            myTimer.timerValue = 0;

            myTimer.timerThread = new Thread(MyTimer_Draw);
            myTimer.timerThread.IsBackground = true;

            myThread.Start();
            myTimer.timerThread.Start();
            //-------------------------------------------------
            while (true)
            {
                bool win = false;

                for (int i = 0; i < gameLevel.gameLevel_Array.Length; i++)
                {
                    win = true;

                    if (gameLevel.gameLevel_Array[i] != i + 1)
                    {
                        win = false;

                        break;
                    }
                }

                if (win == true)
                {
                    myThread.Abort();
                    myTimer.timerThread.Abort();

                    Draw_GameResult();

                    return;
                }
                //---------------------------------------------
                pressKey = Console.ReadKey(true).Key;
                //---------------------------------------------
                if (pressKey == ConsoleKey.UpArrow)
                {
                    if (gameLevel.selectedIndex >= 0 && gameLevel.selectedIndex <= 3)
                    {
                        gameLevel.selectedIndex += 12;
                    }
                    else
                    {
                        gameLevel.selectedIndex -= 4;
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.DownArrow)
                {
                    if (gameLevel.selectedIndex >= 12 && gameLevel.selectedIndex <= 15)
                    {
                        gameLevel.selectedIndex -= 12;
                    }
                    else
                    {
                        gameLevel.selectedIndex += 4;
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.RightArrow)
                {
                    if (gameLevel.selectedIndex == 15)
                    {
                        gameLevel.selectedIndex = 0;
                    }
                    else
                    {
                        gameLevel.selectedIndex++;
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.LeftArrow)
                {
                    if (gameLevel.selectedIndex == 0)
                    {
                        gameLevel.selectedIndex = 15;
                    }
                    else
                    {
                        gameLevel.selectedIndex--;
                    }
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.Enter)
                {
                    if (gameLevel.gameLevel_Array[gameLevel.selectedIndex] == 16)
                    {
                        continue;
                    }
                    //-----------------------------------------
                    if (gameLevel.selectedIndex - 1 >= 0)
                    {
                        if (gameLevel.gameLevel_Array[gameLevel.selectedIndex - 1] == 16)
                        {
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex - 1] = gameLevel.gameLevel_Array[gameLevel.selectedIndex];
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex] = 16;

                            gameLevel.stepsCount++;
                            gameLevel.selectedIndex--;
                            continue;
                        }
                    }
                    //-----------------------------------------
                    if (gameLevel.selectedIndex + 1 <= 15)
                    {
                        if (gameLevel.gameLevel_Array[gameLevel.selectedIndex + 1] == 16)
                        {
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex + 1] = gameLevel.gameLevel_Array[gameLevel.selectedIndex];
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex] = 16;

                            gameLevel.stepsCount++;
                            gameLevel.selectedIndex++;
                            continue;
                        }
                    }
                    //-----------------------------------------
                    if (gameLevel.selectedIndex - 4 >= 0)
                    {
                        if (gameLevel.gameLevel_Array[gameLevel.selectedIndex - 4] == 16)
                        {
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex - 4] = gameLevel.gameLevel_Array[gameLevel.selectedIndex];
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex] = 16;

                            gameLevel.stepsCount++;
                            gameLevel.selectedIndex -= 4;
                            continue;
                        }
                    }
                    //-----------------------------------------
                    if (gameLevel.selectedIndex + 4 <= 15)
                    {
                        if (gameLevel.gameLevel_Array[gameLevel.selectedIndex + 4] == 16)
                        {
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex + 4] = gameLevel.gameLevel_Array[gameLevel.selectedIndex];
                            gameLevel.gameLevel_Array[gameLevel.selectedIndex] = 16;

                            gameLevel.stepsCount++;
                            gameLevel.selectedIndex += 4;
                            continue;
                        }
                    }
                    //-----------------------------------------
                }
                //---------------------------------------------
                if (pressKey == ConsoleKey.Escape)
                {
                    myThread.Abort();
                    myTimer.timerThread.Abort();

                    Draw_GameMenu();

                    return;
                }
                //---------------------------------------------
                #region Secret Key
                if (pressKey == ConsoleKey.Delete)
                {
                    for (int i = 0; i < gameLevel.gameLevel_Array.Length; i++)
                    {
                        if (gameLevel.gameLevel_Array[i] != i + 1)
                        {
                            for (int x = 0; x < gameLevel.gameLevel_Array.Length; x++)
                            {
                                if (gameLevel.gameLevel_Array[x] == i + 1)
                                {
                                    gameLevel.gameLevel_Array[x] = gameLevel.gameLevel_Array[i];

                                    break;
                                }
                            }

                            gameLevel.gameLevel_Array[i] = i + 1;

                            break;
                        }
                    }
                }
                #endregion
                //---------------------------------------------
            }
        }
        //-----------------------------------------------------
        private void Draw_GameField()
        {
            int left = gameLevel.left;
            int top = gameLevel.top;

            workConsole.Write(left, top,     " ------   ------   ------   ------");
            workConsole.Write(left, top + 1, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 2, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 3, " ------   ------   ------   ------");

            workConsole.Write(left, top + 4, " ------   ------   ------   ------");
            workConsole.Write(left, top + 5, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 6, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 7, " ------   ------   ------   ------");

            workConsole.Write(left, top + 8,  " ------   ------   ------   ------");
            workConsole.Write(left, top + 9,  "|      | |      | |      | |      |");
            workConsole.Write(left, top + 10, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 11, " ------   ------   ------   ------");

            workConsole.Write(left, top + 12, " ------   ------   ------   ------");
            workConsole.Write(left, top + 13, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 14, "|      | |      | |      | |      |");
            workConsole.Write(left, top + 15, " ------   ------   ------   ------");
        }
        //-----------------------------------------------------
        private void Draw_GameLevel()
        {
            int x = gameLevel.left + 1;
            int y = gameLevel.top + 1;

            string value;

            for (int i = 0; i < gameLevel.gameLevel_Array.Length; i++)
            {
                value = gameLevel.gameLevel_Array[i].ToString();

                if (value.Length == 1)
                {
                    value = "0" + value;
                }

                if (gameLevel.selectedIndex == i)
                {
                    value = "<|" + value + "|>";
                }
                else
                {
                    value = "  " + value + "  ";
                }

                if (gameLevel.gameLevel_Array[i] == i + 1)
                {
                    workConsole.Write(x, y + 1, " |ok| ");
                }
                else
                {
                    workConsole.Write(x, y + 1, "      ");
                }

                if (gameLevel.gameLevel_Array[i] == 16)
                {
                    value = "      ";
                    workConsole.Write(x, y + 1, "      ");

                    if (gameLevel.selectedIndex == i)
                    {
                        value = "  <>  ";
                        workConsole.Write(x, y + 1, "      ");
                    }
                }

                workConsole.Write(x, y, value);

                x += 9;

                if (i == 3 || i == 7 || i == 11)
                {
                    x = gameLevel.left + 1;
                    y += 4;
                }
            }
        }
        //-----------------------------------------------------
        private void Create_GameLevel()
        {
            gameLevel = new sGameLevel();

            gameLevel.left = 64;
            gameLevel.top = 6;

            gameLevel.stepsCount = 0;
            gameLevel.selectedIndex = 0;

            gameLevel.gameLevel_Array = new int[16];

            Random rRandom = new Random();

            int rValue;

            int x = 0;

            while (true)
            {
                if (x > gameLevel.gameLevel_Array.Length - 1)
                {
                    break;
                }

                rValue = rRandom.Next(1, 17);

                for (int f = 0; f < gameLevel.gameLevel_Array.Length; f++)
                {
                    if (gameLevel.gameLevel_Array[f] == rValue)
                    {
                        rValue = -1;

                        break;
                    }
                }

                if (rValue != -1)
                {
                    gameLevel.gameLevel_Array[x] = rValue;

                    x++;
                }
            }
        }
        //-----------------------------------------------------
        private bool Load_GameStatistics()
        {
            gameStatistics = null;

            string myFile = Directory.GetCurrentDirectory() + "\\GameStatistics.db";

            if (File.Exists(myFile) == false)
            {
                return false;
            }

            FileStream fName = new FileStream(myFile, FileMode.Open);

            if (fName.Length == 0 || fName.Length % 62 != 0)
            {
                fName.Dispose();
                fName.Close();

                return false;
            }

            gameStatistics = new sGameStatistics[fName.Length / 62];

            byte[] workArray = new byte[2];

            string data = "";

            for (int i = 0; i < fName.Length; i += 2)
            {
                workArray[0] = (byte)fName.ReadByte();
                workArray[1] = (byte)fName.ReadByte();

                data += MyFunc_2.ByteToChar(workArray);
            }

            fName.Dispose();
            fName.Close();

            int x = 0;
            int z = 0;

            string buf;

            bool wStatus = true;

            while (true)
            {
                buf = data.Substring(x, 2);
                x += 2;

                gameStatistics[z].hour = buf;

                if (MyFunc_2.IntOrNotInt(buf) == false)
                {
                    wStatus = false;

                    break;
                }
                //-------------
                buf = data.Substring(x, 2);
                x += 2;

                gameStatistics[z].minute = buf;

                if (MyFunc_2.IntOrNotInt(buf) == false)
                {
                    wStatus = false;

                    break;
                }
                //-------------
                buf = data.Substring(x, 2);
                x += 2;

                gameStatistics[z].second = buf;

                if (MyFunc_2.IntOrNotInt(buf) == false)
                {
                    wStatus = false;

                    break;
                }
                //---------------------------------------------
                buf = data.Substring(x, 16);
                x += 16;

                gameStatistics[z].dateTime = buf;
                //---------------------------------------------
                buf = data.Substring(x, 5);
                x += 5;

                buf = MyFunc_2.String_DeleteXLength(buf, '*');

                if (MyFunc_2.IntOrNotInt(buf) == false)
                {
                    wStatus = false;

                    break;
                }

                gameStatistics[z].timeValue = (int)MyFunc_2.StringToInt(buf);
                //---------------------------------------------
                buf = data.Substring(x, 4);
                x += 4;

                buf = MyFunc_2.String_DeleteXLength(buf, '*');

                if (MyFunc_2.IntOrNotInt(buf) == false)
                {
                    wStatus = false;

                    break;
                }

                gameStatistics[z].stepsCount = (int)MyFunc_2.StringToInt(buf);
                //---------------------------------------------

                z++;

                if (x == data.Length)
                {
                    break;
                }
            }

            if (wStatus == false)
            {
                gameStatistics = null;

                return false;
            }

            return true;
        }
        //-----------------------------------------------------
        private void Save_GameStatistics()
        {
            if (gameStatistics == null || gameStatistics.Length == 0)
            {
                return;
            }

            string myFile = Directory.GetCurrentDirectory() + "\\GameStatistics.db";

            FileStream fName = new FileStream(myFile, FileMode.Create);

            string buf;
            string data = "";

            for (int i = 0; i < gameStatistics.Length; i++)
            {
                data += gameStatistics[i].hour;
                data += gameStatistics[i].minute;
                data += gameStatistics[i].second;

                data += gameStatistics[i].dateTime;

                buf = gameStatistics[i].timeValue.ToString();
                buf = MyFunc_2.String_AddXLength(buf, 5, '*');
                data += buf;

                buf = gameStatistics[i].stepsCount.ToString();
                buf = MyFunc_2.String_AddXLength(buf, 4, '*');
                data += buf;
            }

            byte[] byteValue = new byte[2];

            for (int i = 0; i < data.Length; i++)
            {
                byteValue = MyFunc_2.CharToByte(data[i]);

                fName.WriteByte(byteValue[0]);
                fName.WriteByte(byteValue[1]);
            }

            fName.Dispose();
            fName.Close();
        }
        //-----------------------------------------------------
        private bool Load_Settings()
        {
            string myFile = Directory.GetCurrentDirectory() + "\\Settings.db";

            if (File.Exists(myFile) == false)
            {
                return false;
            }

            FileStream fName = new FileStream(myFile, FileMode.Open);

            if (fName.Length == 2)
            {
                Console.BackgroundColor = (ConsoleColor)fName.ReadByte();
                Console.ForegroundColor = (ConsoleColor)fName.ReadByte();

                fName.Dispose();
                fName.Close();

                return true;
            }

            fName.Dispose();
            fName.Close();

            return false;
        }
        //-----------------------------------------------------
        private void Save_Settings()
        {
            string myFile = Directory.GetCurrentDirectory() + "\\Settings.db";

            FileStream fName = new FileStream(myFile, FileMode.Create);

            fName.WriteByte((byte)Console.BackgroundColor);
            fName.WriteByte((byte)Console.ForegroundColor);

            fName.Dispose();
            fName.Close();
        }
        //-----------------------------------------------------
    }
    //---------------------------------------------------------
}
