/*──────────────────────────────────────────────────────────────
 * FileName     : Map2DFile.cs
 * Created      : 2021-05-13 17:57:59
 * Author       : Xu Zhe
 * Description  : 
 * ──────────────────────────────────────────────────────────────*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LF.Mathematics;

namespace LF.UnmannedAerialVehicle
{
    public class Map2DFile
    {
        #region Fields
        private string _path;
        private string _name;
        public readonly string file = ".map2d";

        private StreamWriter writer;
        private StreamReader reader;

        public readonly string header = "Map";
        public readonly string obSeparator = "### Obstacle2d ###";

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public Map2DFile(string path)
        {
            _path = path;
            _name = Default.Name;
        }

        public Map2DFile()
        {
            _path = Default.Path;
            _name = Default.Name;
        }
        #endregion

        #region Methods
        public void WriteFile(LFMap2D map)
        {
            try
            {
                string tmp = _path + _name + file;
                writer = new StreamWriter(tmp);
            }
            catch { Console.WriteLine("1"); }

            try
            {
                // 写入头文件
                writer.WriteLine(header);
                writer.WriteLine("### Range ###");
                writer.WriteLine("X" + " " + map.Xmin.ToString() + " " + map.Xmax.ToString());
                writer.WriteLine("Y" + " " + map.Ymin.ToString() + " " + map.Ymax.ToString());
                writer.WriteLine("Start And End");
                writer.WriteLine(obSeparator);
                foreach (LFObstacle2D ob in map.Obstacle2DList)
                {
                    writer.WriteLine("OB2D");
                    foreach (LFPoint2D p in ob.Points)
                    {
                        writer.WriteLine("P" + " " + p.X.ToString() + " " + p.Y.ToString());
                    }
                }
                writer.Close();
            }
            catch { Console.WriteLine("2"); }
        }

        LFObstacle2D ob;

        public LFMap2D ReadFile()
        {
            reader = new StreamReader(_path + @"\" + _name + file);
            LFMap2D map = new LFMap2D();

            int flag = 0;

            while (reader.Peek() != -1)
            {

                string str = reader.ReadLine();

                if (str == "Range")
                {
                    flag = 4;
                }
                if (str == "Start")
                {
                    flag = 1;
                }
                if (str == "End")
                {
                    flag = 2;
                }

                if (str == "OB2D")
                {
                    ob = new LFObstacle2D();
                    ob.Index = map.Obstacle2DList.Count+1;
                    map.Obstacle2DList.Add(ob);
                    flag = 0;
                }

                if (str == "Waypoints")
                {
                    map.Waypoints = new List<LFPoint2D>();
                    flag = 3;
                }
                if (str.First<char>() == 'P')
                {
                    try
                    {
                        string[] arr = str.Split(' ');

                        LFPoint2D p = new LFPoint2D(Convert.ToDouble(arr[1]), Convert.ToDouble(arr[2]));
                        if (flag == 1)
                        {
                            map.Start = p;
                            flag = 0;
                        }
                        else if (flag == 2)
                        {
                            map.End = p;
                            flag = 0;
                        }
                        else if (flag == 3)
                        {
                            map.Waypoints.Add(p);
                        }
                        else
                        {
                            ob.Points.Add(p);
                        }
                    }
                    catch { }
                }
                if (str.First<char>() == 'R')
                {
                    try
                    {
                        string[] arr = str.Split(' ');

                        map.Xmin = Convert.ToDouble(arr[1]);
                        map.Xmax = Convert.ToDouble(arr[2]);
                        map.Ymin = Convert.ToDouble(arr[3]);
                        map.Ymax = Convert.ToDouble(arr[4]);

                    }
                    catch { }
                }

            }

            reader.Close();

            return map;
        }


        #endregion

        #region Serializations
        #endregion

        #region Defaults
        public struct Default
        {
            public static string Path = @"F:\Documents\tmp\";
            public static string Name = "mapFile";
        }
        #endregion
    }
}