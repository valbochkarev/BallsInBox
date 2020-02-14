using System;
using System.Windows.Forms;
// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;


namespace bib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        #region ///Объявление полей
        // объявление экземпляров шаров
        Ball a1 = new Ball(-1, 0.3, 0.3, 0.004, 1, 0, 2, 1, 255, 100, 0);
        Ball a2 = new Ball(1, 0.3, 0.3, 0.004, 3, 2, 1, 1, 0, 255, 100);
        // переменные отвечающие за скорость поворота сцены
        float xrotate = 0, yrotate = 0, zrotate = 0;
        // переменные отвечающие за поворот сцены
        float xrotation = 0f, yrotation = 0f, zrotation = 0f;
        // переменная отвечающая за пуск/паузу движения
        bool flag = true;
        #endregion

        

        #region ///отвечает за взаимодействие пользователя с программой
        // инициализация окна
        private void AnT_Load(object sender, EventArgs e)
        {
            // инициализация glut
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // отчитка окна 
            Gl.glClearColor(0, 0, 0, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);


            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(15, (float)AnT.Width / (float)AnT.Height, 0.1, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            

            // настройка параметров OpenGL для визуализации 
            Gl.glEnable(Gl.GL_DEPTH_TEST);
        }
        // функция перерисовки окна
        private void AnT_Paint(object sender, PaintEventArgs e)
        {
            //вызов того что мы хотим рисовать
            if (flag)
                fun();
        }
        // основная картина того что постоянно перерисовываем
        private void fun()
        {
            // настройки окна
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            // переход в точку просмотра из 0 0 -20
            Gl.glTranslated(0, 0, -20);
            // прибавление дельта углов поворота к общему повороту
            xrotation += xrotate;
            yrotation += yrotate;
            zrotation += zrotate;
            // отрисовка сцены с поворотом
            Gl.glRotated(yrotation, 0.0f, 1.0f, 0.0f);
            Gl.glRotated(xrotation, 1.0f, 0.0f, 0.0f);
            Gl.glRotated(zrotation, 0.0f, 0.0f, 1.0f);
            #region/// рисование 12 линий (количество ребер параллелепипеда)
            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, -1, -1);
            Gl.glVertex3d(2, -1, -1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, -1, -1);
            Gl.glVertex3d(-2, 1, -1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, -1, -1);
            Gl.glVertex3d(-2, -1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(2, -1, -1);
            Gl.glVertex3d(2, 1, -1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(2, -1, -1);
            Gl.glVertex3d(2, -1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, 1, 1);
            Gl.glVertex3d(2, 1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, -1, 1);
            Gl.glVertex3d(2, -1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, 1, -1);
            Gl.glVertex3d(2, 1, -1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, 1, -1);
            Gl.glVertex3d(-2, 1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(-2, -1, 1);
            Gl.glVertex3d(-2, 1, 1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(2, 1, 1);
            Gl.glVertex3d(2, 1, -1);

            Gl.glColor3f(0, 255f, 0);
            Gl.glVertex3d(2, 1, 1);
            Gl.glVertex3d(2, -1, 1);
            Gl.glEnd();
            #endregion
            #region///отрисовка и движение шариков
            /// достаточные условия
            // проверка направлений при соприкосновении шаров
            if (Dist(a1, a2))  
                Moveon(ref a1, ref a2);
            // движение и отрисовка первого экземпляра
            a1.down();
            a1.skok();
            DrawBall(a1);
            // движение и отрисовка второго экземпляра
            a2.down();
            a2.skok();
            DrawBall(a2);
            // конец рисования, вызов Paint
            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
            #endregion
        }
        public void DrawBall(Ball ball) // рисование шара
        {
            // сохранение текущих координат
            Gl.glPushMatrix();
            // переход в координаты центра шара
            Gl.glTranslated(ball.X, ball.Y, ball.Z);
            // задавание цвета GL
            Gl.glColor3d(ball.Red, ball.Green, ball.Blue);
            // шаблон сферы
            Glu.gluSphere(Glu.gluNewQuadric(), ball.R, 50, 50);
            // возвращение к "старым" координатам
            Gl.glPopMatrix();
        }
        // немного физики столкновения шаров
        public void Moveon(ref Ball a, ref Ball b)
        {
            double x = (b.X - a.X);
            double y = (b.Y - a.Y);
            double z = (b.Z - a.Z);
            double q = x * x + y * y + z * z;
            double Vx = (a.Vx - b.Vx);
            double Vy = (a.Vy - b.Vy);
            double Vz = (a.Vz - b.Vz);
            double k = (x * Vx + y * Vy + z * Vz) / q;
            a.Vx = ((Vx - x * k) + b.Vx);
            a.Vy = ((Vy - y * k) + b.Vy);
            a.Vz = ((Vz - z * k) + b.Vz);
            b.Vx = (x * k + b.Vx);
            b.Vy = (y * k + b.Vy);
            b.Vz = (z * k + b.Vz);
        }
        // проверка на столкновение шаров
        public bool Dist(Ball a, Ball b)
        {
            double x = (b.X - a.X);
            double y = (b.Y - a.Y);
            double z = (b.Z - a.Z);
            return Math.Sqrt(x * x + y * y + z * z) <= a.R + b.R;
        }
        #endregion

        #region ///немного кнопок
        // поворот сцены по оси Х
        private void Button1_Click_1(object sender, EventArgs e) 
            => xrotate = (xrotate == 0) ? 0.04f : 0;
        // поворот сцены по оси Y
        private void Button2_Click_1(object sender, EventArgs e) 
            => yrotate = (yrotate == 0) ? 0.04f : 0;
        // поворот сцены по оси Z
        private void Button3_Click_1(object sender, EventArgs e) 
            => zrotate = (zrotate == 0) ? 0.04f : 0;
        // пуск/пауза алгоритма программы
        private void Button4_Click_1(object sender, EventArgs e)
        {
            flag ^= true;
            button4.Text = flag ? "Pause" : "Play";
            AnT_Paint(sender, e as PaintEventArgs);
        }
        // возвращение в к "нулевому" углу поворота и нулевым скоростям поворота
        private void Button5_Click_1(object sender, EventArgs e)
        {
            xrotate = 0;
            yrotate = 0;
            zrotate = 0;
            xrotation = 0;
            yrotation = 0;
            zrotation = 0;
        }
        // включение/отключение гравитации
        private void Button6_Click_1(object sender, EventArgs e) 
            => Ball.G = (Ball.G == 0) ? 0.00001 : 0;
        // выход из программы
        private void Button7_Click_1(object sender, EventArgs e) 
            => Application.Exit();
        #endregion
    }
}
