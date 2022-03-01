using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningApplication2
{
    class PatternMatching
    {
        public static void LearnPattern()
        {
            Circle circle = new(5);
            Rectangle rectangle = new(420, 1337);
            Rectangle square = new(69, 69);

            List<Shape> shapes = new() { circle, rectangle, square };

            Random random = new();
            Shape randomShape = shapes[random.Next(shapes.Count)];

            //from C# 8 we can do like this:
            if (randomShape is Circle { Radius: 10, Area: 50 })
            {

            }

            var shapeDetails = randomShape switch
            {
                Circle cir => $"This is a circle with area {cir.Area}",
                Rectangle rec when rec.Height == rec.Width => "This is a square",
                { Area: 100 } => "This area was 100",
                _ => "This is a default. It didn't match anything"
            };

            //from C# 9 we can do like this:
            if (randomShape is Circle { Radius: > 100 and < 200, Area: >= 1000 })
            {
            }

            var shapeDetails2 = randomShape switch
            {
                Circle { Area: > 100 and < 200 } => "My magic circle",
                Circle { Diameter: 100 } => "something",
                _ => "This is a default"
            };

            var areaDetails = randomShape.Area switch
            {
                >= 100 and <= 100 => "something",
                _ => "default"
            };

        }
    }

    //C# 9
    public static class Extensions
    {
        //extension method
        public static bool IsLetter(this char c) =>
            c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }

    #region Helpers
    public abstract class Shape
    {
        public abstract double Area { get; }
    }

    public class Rectangle : Shape, ISquare
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public override double Area => Height * Width;

        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }
    }

    public class Circle : Shape
    {
        private const double PI = Math.PI;
        public double Diameter { get; set; }
        public double Radius => Diameter / 2.0;
        public override double Area => PI * Radius * Radius;

        public Circle(double diameter)
        {
            Diameter = diameter;
        }
    }

    public interface ISquare
    {
        double Height { get; set; }
        double Width { get; set; }
    }
    #endregion Helpers
}
