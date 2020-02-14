using System;
namespace bib
{
    #region/// класс шар
    public class Ball
    {
        #region/// поля шара
        // координаты шара
        private double x, y, z;

        // скорости шара по осям
        private double vx, vy, vz;
        // гравитация для всех шаров
        static double g = 0;

        // радиус шара
        private double r = 0.4;

        // цвет шара
        private double red;
        private double green;
        private double blue;


        #endregion
        #region/// конструктор шара

        public Ball(double X, double Y, double Z,              //координаты
                    double V, double x1, double y1, double z1, // скорость и коллинеарный вектор
                    double M,                                  //масса
                    double R, double G, double B)              //цвет
        {
            // присваивание шару начальных координат
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            // задавание шару начальных скоростей по осям через общую скорость и коллинеарный вектор
            double l = Math.Sqrt(x1 * x1 + y1 * y1 + z1 * z1);
            Vx = V * x1 / l;
            Vy = V * y1 / l;
            Vz = V * z1 / l;

            // задавание цвета шара
            Red = R;
            Green = G;
            Blue = B;
        }

        #endregion
        #region/// набор свойств
        public double Vx { get => vx; set => vx = value; }
        public double Vy { get => vy; set => vy = value; }
        public double Vz { get => vz; set => vz = value; }
        public double R { get => r; set => r = value; }
        public double Red { get => red; set => red = value; }
        public double Blue { get => blue; set => blue = value; }
        public double Green { get => green; set => green = value; }
        public double X { get => X1; set => X1 = value; }
        public double Y { get => Y1; set => Y1 = value; }
        public double Z { get => Z1; set => Z1 = value; }
        public static double G { get => g; set => g = value; }
        public double X1 { get => x; set => x = value; }
        public double Y1 { get => y; set => y = value; }
        public double Z1 { get => z; set => z = value; }
        #endregion
        #region/// набор функций

        public bool skok() // обеспечивает отскок от "стенки"
        {                  // а так же возвращающая "наличие" отскока
                           // пока отскок не произошел - false
            bool o = false;
            // проверка отскока по оси Х
            if ((X + R > 2) | (X - R < -2))
            {
                // смена скорости по оси на противоположную
                Vx = -Vx;
                // отскок "наверняка", чтобы выйти за пределы проверки
                X = X + 1.1 * Vx;
                // отскок - true
                o = true;
            }
            // проверка отскока по оси Y
            if ((Y + R > 1) | (Y - R < -1))
            {
                // смена скорости по оси на противоположную
                Vy = -Vy;
                // отскок "наверняка", чтобы выйти за пределы проверки
                Y = Y + 1.1 * Vy;
                // доп условие для замедления прыгающего шарика при вкл декорации
                if ((G != 0) & (Y < 0))
                    Vy *= 0.85;
                // отскок - true
                o = true;
            }
            // проверка отскока по оси Z
            if ((Z + R > 1) | (Z - R < -1))
            {
                // смена скорости по оси на противоположную
                Vz = -Vz;
                // отскок "наверняка", чтобы выйти за пределы проверки
                Z = Z + 1.1 * Vz;
                // отскок - true
                o = true;
            }
            // возвращение булевого значения события "отскок"
            return o;
        }
        public void down() // обеспечивает движение по осям
        {
            // ускорение от тяжести
            Vy -= G;
            // изменение координат шаров
            X += Vx;
            Y += Vy;
            Z += Vz;
            // доп. условия остановки при включенной гравитации
            if (G != 0)
            {
                // когда Y очень мал и скорость по оси Y тоже, 
                // скорости по осям X и Z начинают уменьшаться (трение?)
                if ((Y < Math.Pow(Math.E, -5) - 1 + R) & (Math.Abs(Vy) < 0.000001))
                {
                    Vx *= 0.9;
                    Vz *= 0.9;
                }
                // при очень малой скорости по оси X, скорость по Х зануляется
                if (Math.Abs(Vx) < 0.0001)
                    Vx = 0;
                // при очень малой скорости по оси Z, скорость по Z зануляется
                if (Math.Abs(Vz) < 0.0001)
                    Vz = 0;
            }
        }
        #endregion
    }
    #endregion
}
