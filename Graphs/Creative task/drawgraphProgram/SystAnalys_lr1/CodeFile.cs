using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    class Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Edge
    {
        public int v1, v2;
        public string name = null;

        public Edge(int v1, int v2, int count)
        {
            this.v1 = v1;
            this.v2 = v2;
            char n= (char)('a' + count);
            name += n;
        }
        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
        public Edge(int v1, int v2, string name)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.name = name;
        }
    }

    class DrawGraph
    {
        Bitmap bitmap; //объект, используемый для работы с изображениями, определяемых данными о пикселях
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr; //Инкапсулирует поверхность рисования GDI+
        Font fo; //шрифт
        Brush br; //заливка
        PointF point; //точка
        public int R = 20; //радиус окружности вершины

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap); //Создает новый объект Graphics из указанного объекта Image
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }
        public int GetBitmapWidth()
        {
            return bitmap.Width;
        }
        
        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R); //заливка
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - R/2, y - R/2); //определяет точку
            gr.DrawString(number, fo, br, point);
        }

        public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString(E.name, fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                //if (Math.Abs(V1.x - V2.x) < 500 && Math.Abs(V1.y - V2.y) < 500)
                //{
                //    fo = new Font("Arial", 8);
                //}
                gr.DrawString(E.name, fo, br, point);
                drawVertex(V1.x, V1.y, E.v1 .ToString());
                drawVertex(V2.x, V2.y, E.v2.ToString());
            }
        }

        public void drawALLGraph(SortedList<int, Vertex> V, List<Edge> E)
        {
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(E[i].name, fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);                    
                    gr.DrawString(E[i].name, fo, br, point);
                }
            }
            //рисуем вершины
            foreach (int node in V.Keys)
            {
                drawVertex(V[node].x, V[node].y, node.ToString());
            }
        }
        public void drawALLGraphCombine(SortedList<int, Vertex> V, List<Edge> E, double width, double heigth)
        {
            double alpha = alpha = 360 / (V.Count * 2);
            if (V.Count > 9)
            {
                this.R = 13;
                fo = new Font("Arail", 10);
                alpha = 360 / (V.Count * 2 - 2);
            }
            else
            {
                this.R = 20;
                fo = new Font("Arail", 15);
                if (V.Count == 8)
                {
                    alpha = 360 / (V.Count * 2 + 2);
                }
            }                       
            double RC = 80;
            double[] center = { width / 2, heigth / 2 };
            int k = 0;
            foreach (int node in V.Keys)
            {
                if (node == 1)
                {
                    V[node].x = (int)(center[0] + RC);
                    V[node].y = (int)(center[1] + RC);
                }
                else
                {
                    //определяем координаты по часовой стрелке   
                    V[node].x = (int)(center[0] + (V[k].x - center[0]) * Math.Cos(alpha) - (V[k].y - center[1]) * Math.Sin(alpha));
                    V[node].y = (int)(center[1] + (V[k].x - center[0]) * Math.Sin(alpha) + (V[k].y - center[1]) * Math.Cos(alpha));
                }
                k = node;
            }
            //рисуем ребра
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(E[i].name, fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    gr.DrawString(E[i].name, fo, br, point);
                }
            }
            //рисуем вершины
            foreach (int node in V.Keys)
            {
               drawVertex(V[node].x, V[node].y, node.ToString());
            }
        }
    }
}