using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace SystAnalys_lr1
{
    public partial class Form1 : Form
    {
        DrawGraph G;
        DrawGraph G2;
        DrawGraph G3;
        SortedList<int, Vertex> V1;
        int CountV1 = 1;
        List<Edge> E1;
        SortedList<int, Vertex> V2;
        int CountV2 = 1;
        List<Edge> E2;
        SortedList<int, Vertex> V3;
        List<Edge> E3;

        int selected_A1; //выбранные вершины, для соединения линиями
        int selected_A2;
        int selected_B1; //выбранные вершины, для соединения линиями
        int selected_B2;
        int selected_C1;
        string info;

        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;
            V1 = new SortedList<int, Vertex>();
            V2 = new SortedList<int, Vertex>();
            V3 = new SortedList<int, Vertex>();
            G = new DrawGraph(sheet1.Width, sheet1.Height);
            G2 = new DrawGraph(sheet2.Width, sheet2.Height);
            G3 = new DrawGraph(sheet3.Width, sheet3.Height);
            E1 = new List<Edge>();
            E2 = new List<Edge>();
            E3 = new List<Edge>();
            sheet1.Image = G.GetBitmap();
            sheet2.Image = G2.GetBitmap();
            sheet3.Image = G3.GetBitmap();
        }

        //кнопка - выбрать вершину
        private void selectButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = false;
            button1.Visible = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            selected_A1 = -1;
            selected_B1 = -1;
            selected_C1 = -1;
        }

        //кнопка - рисовать вершину графа
        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawVertexButton.Enabled = false;
            button1.Visible = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V1, E1);
            sheet1.Image = G.GetBitmap();
            G2.clearSheet();
            G2.drawALLGraph(V2, E2);
            sheet2.Image = G2.GetBitmap();
        }
        //кнопка - рисовать ребро
        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            button1.Visible = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V1, E1);
            sheet1.Image = G.GetBitmap();
            selected_A1 = -1;
            selected_A2 = -1;
            G2.clearSheet();
            G2.drawALLGraph(V2, E2);
            sheet2.Image = G2.GetBitmap();
            selected_B1 = -1;
            selected_B2 = -1;
        }

        //кнопка - удалить элемент
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;
            button1.Visible = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V1, E1);
            sheet1.Image = G.GetBitmap();
            G2.clearSheet();
            G2.drawALLGraph(V2, E2);
            sheet2.Image = G2.GetBitmap();
        }

        //кнопка - удалить граф
        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            drawVertexButton.Enabled = true;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить графы?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {               
                if (V1 != null)
                {
                    V1.Clear();
                    E1.Clear();
                    G.clearSheet();
                    sheet1.Image = G.GetBitmap();
                    CountV1 = 1;
                }
                if (V2 != null)
                {
                    V2.Clear();
                    E2.Clear();
                    G2.clearSheet();
                    sheet2.Image = G2.GetBitmap();
                    CountV2 = 1;
                }
                if (V3 != null)
                {
                    V3.Clear();
                    E3.Clear();
                    G3.clearSheet();
                    sheet3.Image = G3.GetBitmap();
                    listBox.Items.Clear();
                }
                sheet1.Enabled = true;
                sheet2.Enabled = true;
            }
        }       
       

        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            //нажата кнопка "выбрать вершину", ищем степень вершины
            if (selectButton.Enabled == false)
            {
                foreach (int i in V1.Keys)
                {
                    if (Math.Pow((V1[i].x - e.X), 2) + Math.Pow((V1[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        if (selected_A1 != -1)
                        {
                            selected_A1 = -1;
                            G.clearSheet();
                            G.drawALLGraph(V1, E1);
                            sheet1.Image = G.GetBitmap();
                        }
                        if (selected_A1 == -1)
                        {
                            G.drawSelectedVertex(V1[i].x, V1[i].y);
                            selected_A1 = i;
                            sheet1.Image = G.GetBitmap();
                            listBox.Items.Clear();
                            int degree = 0;
                            foreach (var para in E1)
                            {
                                if ((para.v1 == selected_A1) || (para.v2 == selected_A1))
                                    degree += 1;
                            }
                            listBox.Items.Add("Степень вершины №" + (selected_A1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {                
                V1.Add(CountV1, new Vertex(e.X, e.Y));
                G.drawVertex(e.X, e.Y, CountV1.ToString());
                sheet1.Image = G.GetBitmap();
                CountV1++;
            }
          
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (int i in V1.Keys)
                    {
                        if (Math.Pow((V1[i].x - e.X), 2) + Math.Pow((V1[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            if (selected_A1 == -1)
                            {
                                G.drawSelectedVertex(V1[i].x, V1[i].y);
                                selected_A1 = i;
                                sheet1.Image = G.GetBitmap();
                                break;
                            }
                            if (selected_A2 == -1)
                            {
                                G.drawSelectedVertex(V1[i].x, V1[i].y);
                                selected_A2 = i;
                                E1.Add(new Edge(selected_A1, selected_A2, E1.Count));                               
                                G.drawEdge(V1[selected_A1], V1[selected_A2], E1[E1.Count - 1]);                                
                                selected_A1 = -1;
                                selected_A2 = -1;
                                sheet1.Image = G.GetBitmap();
                                break;
                            }
                        }                   
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected_A1 != -1) &&
                    (Math.Pow((V1[selected_A1].x - e.X), 2) + Math.Pow((V1[selected_A1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVertex(V1[selected_A1].x, V1[selected_A1].y, (selected_A1 + 1).ToString());
                        selected_A1 = -1;
                        sheet1.Image = G.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                foreach (int i in V1.Keys)
                {
                    if (Math.Pow((V1[i].x - e.X), 2) + Math.Pow((V1[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        for (int j = 0; j < E1.Count; j++)
                        {
                            if ((E1[j].v1 == i) || (E1[j].v2 == i))
                            {
                                E1.RemoveAt(j);
                                j--;
                            }
                        }
                        V1.Remove(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    for (int i = 0; i < E1.Count; i++)
                    {
                        if (E1[i].v1 == E1[i].v2) //если это петля
                        {
                            if ((Math.Pow((V1[E1[i].v1].x - G.R - e.X), 2) + Math.Pow((V1[E1[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                (Math.Pow((V1[E1[i].v1].x - G.R - e.X), 2) + Math.Pow((V1[E1[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                            {
                                E1.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V1[E1[i].v1].x) * (V1[E1[i].v2].y - V1[E1[i].v1].y) / (V1[E1[i].v2].x - V1[E1[i].v1].x) + V1[E1[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V1[E1[i].v1].x) * (V1[E1[i].v2].y - V1[E1[i].v1].y) / (V1[E1[i].v2].x - V1[E1[i].v1].x) + V1[E1[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V1[E1[i].v1].x <= V1[E1[i].v2].x && V1[E1[i].v1].x <= e.X && e.X <= V1[E1[i].v2].x) ||
                                    (V1[E1[i].v1].x >= V1[E1[i].v2].x && V1[E1[i].v1].x >= e.X && e.X >= V1[E1[i].v2].x))
                                {
                                    E1.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (flag)
                {
                    G.clearSheet();
                    G.drawALLGraph(V1, E1);
                    sheet1.Image = G.GetBitmap();
                }
            }
        }
        private void sheet2_MouseClick(object sender, MouseEventArgs e)
        {
            //нажата кнопка "выбрать вершину", ищем степень вершины
            if (selectButton.Enabled == false)
            {
                foreach (int i in V2.Keys)
                {
                    if (Math.Pow((V2[i].x - e.X), 2) + Math.Pow((V2[i].y - e.Y), 2) <= G2.R * G2.R)
                    {
                        if (selected_B1 != -1)
                        {
                            selected_B1 = -1;
                            G2.clearSheet();
                            G2.drawALLGraph(V2, E2);
                            sheet2.Image = G2.GetBitmap();
                        }
                        if (selected_B1 == -1)
                        {
                            G2.drawSelectedVertex(V2[i].x, V2[i].y);
                            selected_B1 = i;
                            sheet2.Image = G2.GetBitmap();
                            listBox.Items.Clear();
                            int degree = 0;
                            foreach (var para in E2)
                            {
                                if ((para.v1 == selected_B1) || (para.v2 == selected_B1))
                                    degree += 1;
                            }
                            listBox.Items.Add("Степень вершины №" + (selected_B1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            //нажата кнопка "рисовать вершину"
            if (drawVertexButton.Enabled == false)
            {                
                V2.Add(CountV2, new Vertex(e.X, e.Y));
                G2.drawVertex(e.X, e.Y, CountV2.ToString());
                sheet2.Image = G2.GetBitmap();
                CountV2++;
            }
            //нажата кнопка "рисовать ребро"
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (int i in V2.Keys)
                    {
                        if (Math.Pow((V2[i].x - e.X), 2) + Math.Pow((V2[i].y - e.Y), 2) <= G2.R * G2.R)
                        {
                            if (selected_B1 == -1)
                            {
                                G2.drawSelectedVertex(V2[i].x, V2[i].y);
                                selected_B1 = i;
                                sheet2.Image = G2.GetBitmap();
                                break;
                            }
                            if (selected_B2 == -1)
                            {
                                G2.drawSelectedVertex(V2[i].x, V2[i].y);
                                selected_B2 = i;
                                E2.Add(new Edge(selected_B1, selected_B2, E2.Count));                                
                                G2.drawEdge(V2[selected_B1], V2[selected_B2], E2[E2.Count - 1]);

                                selected_B1 = -1;
                                selected_B2 = -1;
                                sheet2.Image = G2.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected_B1 != -1) &&
                    (Math.Pow((V2[selected_B1].x - e.X), 2) + Math.Pow((V2[selected_B1].y - e.Y), 2) <= G2.R * G2.R))
                    {
                        G2.drawVertex(V2[selected_B1].x, V2[selected_B1].y, (selected_B1 + 1).ToString());
                        selected_B1 = -1;
                        sheet2.Image = G2.GetBitmap();
                    }
                }
            }
            //нажата кнопка "удалить элемент"
            if (deleteButton.Enabled == false)
            {
                bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                //ищем, возможно была нажата вершина
                foreach (int i in V2.Keys)
                {
                    if (Math.Pow((V2[i].x - e.X), 2) + Math.Pow((V2[i].y - e.Y), 2) <= G2.R * G2.R)
                    {
                        for (int j = 0; j < E2.Count; j++)
                        {
                            if ((E2[j].v1 == i) || (E2[j].v2 == i))
                            {
                                E2.RemoveAt(j);
                                j--;
                            }
                        }
                        V2.Remove(i);
                        flag = true;
                        break;
                    }
                }
                //ищем, возможно было нажато ребро
                if (!flag)
                {
                    for (int i = 0; i < E2.Count; i++)
                    {
                        if (E2[i].v1 == E2[i].v2) //если это петля
                        {
                            if ((Math.Pow((V2[E2[i].v1].x - G2.R - e.X), 2) + Math.Pow((V2[E2[i].v1].y - G2.R - e.Y), 2) <= ((G2.R + 2) * (G2.R + 2))) &&
                                (Math.Pow((V2[E2[i].v1].x - G2.R - e.X), 2) + Math.Pow((V2[E2[i].v1].y - G2.R - e.Y), 2) >= ((G2.R - 2) * (G2.R - 2))))
                            {
                                E2.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else //не петля
                        {
                            if (((e.X - V2[E2[i].v1].x) * (V2[E2[i].v2].y - V2[E2[i].v1].y) / (V2[E2[i].v2].x - V2[E2[i].v1].x) + V2[E2[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V2[E2[i].v1].x) * (V2[E2[i].v2].y - V2[E2[i].v1].y) / (V2[E2[i].v2].x - V2[E2[i].v1].x) + V2[E2[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V2[E2[i].v1].x <= V2[E2[i].v2].x && V2[E2[i].v1].x <= e.X && e.X <= V2[E2[i].v2].x) ||
                                    (V2[E2[i].v1].x >= V2[E2[i].v2].x && V2[E2[i].v1].x >= e.X && e.X >= V2[E2[i].v2].x))
                                {
                                    E2.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                //если что-то было удалено, то обновляем граф на экране
                if (flag)
                {
                    G2.clearSheet();
                    G2.drawALLGraph(V2, E2);
                    sheet2.Image = G2.GetBitmap();
                }
            }
        }

        private void sheet3_MouseClick(object sender, MouseEventArgs e)
        {
            //нажата кнопка "выбрать вершину", ищем степень вершины
            if (selectButton.Enabled == false)
            {
                foreach (int i in V3.Keys)
                {
                    if (Math.Pow((V3[i].x - e.X), 2) + Math.Pow((V3[i].y - e.Y), 2) <= G3.R * G3.R)
                    {
                        if (selected_C1 != -1)
                        {
                            selected_C1 = -1;
                            G3.clearSheet();
                            G3.drawALLGraph(V3, E3);
                            sheet3.Image = G3.GetBitmap();
                        }
                        if (selected_C1 == -1)
                        {
                            G3.drawSelectedVertex(V3[i].x, V3[i].y);
                            selected_C1 = i;
                            sheet3.Image = G3.GetBitmap();
                            listBox.Items.Clear();
                            int degree = 0;
                            foreach (var para in E3)
                            {
                                if ((para.v1 == selected_C1) || (para.v2 == selected_C1))
                                    degree += 1;
                            }
                            listBox.Items.Add("Степень вершины №" + (selected_C1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
        }

        private void buttonCombine_Click(object sender, EventArgs e)
        {
            if (V1.Count != 0 || V2.Count != 0)
            {
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = false;
                deleteButton.Enabled = false;
                deleteALLButton.Enabled = true;
                button1.Enabled = false;
                selectButton.Enabled = true;
                V3 = new SortedList<int, Vertex>(V1);
                foreach (var i in V2)
                {
                    if (!V3.ContainsKey(i.Key))
                    {
                        V3.Add(i.Key, i.Value);
                    }
                }
                E3 = new List<Edge>(E1);
                foreach (var i in E2)
                {
                    Edge j = new Edge(i.v2, i.v1);
                    if (E3.Contains(i))
                    {
                        E3[i.v1].name += ", ";
                        E3[i.v1].name += i.name;
                    }
                    else if (E3.Contains(j))
                    {
                        E3[j.v1].name += ", ";
                        E3[j.v1].name += i.name;
                    }
                    else
                    {
                        E3.Add(i);
                    }
                }
                listBox.Items.Add("Ребра \n");
                foreach (var i in E3)
                {                    
                    listBox.Items.Add((i.v1) + " " + (i.v2) + " " + " = " + i.name);
                }
                G3.drawALLGraph(V3, E3);
                sheet3.Image = G3.GetBitmap();
            }
            else
            {
                MessageBox.Show("Создай граф");
            }
        }
        private bool NotContainsEdge(List<Edge> listE,Edge i)
        {
            foreach (var edge in listE)
            {
                if ((edge.v1 == i.v1 && edge.v2 == i.v2) || (edge.v1 == i.v2 && edge.v2 == i.v1))
                {
                    edge.name += ", ";
                    edge.name += i.name;
                    return false;
                }
            }
            return true;
        }
        private void buttonCycleCombine_Click(object sender, EventArgs e)
        {
            if (V1.Count !=0 || V2.Count != 0)
            {
                drawVertexButton.Enabled = false;
                drawEdgeButton.Enabled = false;
                deleteButton.Enabled = false;
                deleteALLButton.Enabled = true;
                button1.Enabled = false;
                selectButton.Enabled = true;
                info += "Вершины графа 1 \r\n";
                foreach (var v in V1.Keys)
                {
                    info +=v + " ";
                }
                info += "\r\nРебра графа 1 \r\n";
                foreach (var i in E1)
                {
                    info += i.v1 + " " + i.v2 + " " + i.name + "\r\n";
                }
                V3 = new SortedList<int, Vertex>(V1);
                info += "Вершины графа 2 \r\n";
                foreach (var i in V2)
                {
                    info += i.Key + " ";
                    if (!V3.ContainsKey(i.Key))
                    {
                        V3.Add(i.Key, i.Value);
                    }
                }
                E3 = new List<Edge>(E1);
                info += "\r\nРебра графа 2 \r\n";
                foreach (var i in E2)
                {
                    info += i.v1 + " " + i.v2 + " " + i.name + "\r\n";
                    if (NotContainsEdge(E3,i))
                    {
                        E3.Add(i);
                    }
                }
                G3.clearSheet();
                if (V3.Count > 14)
                {
                    MessageBox.Show("Количество вершин должно быть не более 14");
                }
                else
                {
                    G3.drawALLGraphCombine(V3, E3, sheet3.Width, sheet3.Height);
                    listBox.Items.Clear();
                    info += "Вершины графа 3 \r\n";
                    foreach (var v in V3.Keys)
                    {
                        info += v + " ";
                    }
                    listBox.Items.Add("Ребра \n");
                    info += "\r\nРебра графа 3 \r\n";
                    foreach (var i in E3)
                    {
                        info += i.v1 + " " + i.v2 + " " + i.name + "\r\n";
                        listBox.Items.Add((i.v1) + " " + (i.v2) + " " + " = " + i.name);
                    }
                }
                sheet3.Image = G3.GetBitmap();
            }
            else
            {
                MessageBox.Show("Создай граф");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (sheet3.Image != null)
            {
                SaveFileDialog savedialog = new SaveFileDialog(); //спрашивает у пользователя местоположение для сохранения файла
                //String savedialog1 = savedialog.FileName + "1";
                //String savedialog2 = savedialog.FileName + "2";

                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true; //Возвращает или задает значение, указывающее, отображает ли диалоговое окно Save As предупреждение, если пользователь указывает имя файла, который уже существует.
                savedialog.CheckPathExists = true; //Возвращает или задает значение, указывающее, отображает ли диалоговое окно предупреждение, если пользователь указывает путь, который не существует.
                savedialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        sheet3.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);                       
                        using (StreamWriter fs = new StreamWriter(savedialog.FileName + ".txt"))
                        {
                            fs.Write(info);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            drawVertexButton.Enabled = false;
            drawEdgeButton.Enabled = false;
            deleteButton.Enabled = false;
            deleteALLButton.Enabled = false;
            sheet1.Enabled = false;
            saveButton.Enabled = false;
            sheet2.Enabled = false;
            buttonCombine.Enabled = false;
            sheet3.Enabled = false;
            selectButton.Enabled = false;
            listBox.Enabled = false;
            buttonCycleCombine.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            deleteALLButton.Enabled = true;
            sheet1.Enabled = true;
            saveButton.Enabled = true;
            sheet2.Enabled = true;
            buttonCombine.Enabled = true;
            sheet3.Enabled = true;
            selectButton.Enabled = true;
            listBox.Enabled = true;
            buttonCycleCombine.Enabled = true;
        }
    }
}
