namespace CsharpAdvanced.DesignPatterns.SOLID;

class OpenClosed
{
    // open for extensions, closed for modification
    // Open for extensions means when the new requirements occurs then the new functionality can be added. We can use inheritance to do this.
    // Closed for modification means that after the unit testing we should not change the code

    public static void LearnPrinciple()
    {
        Rectangle2 rectangle0 = new() { Height = 10, Width = 5 };
        Rectangle2 rectangle1 = new() { Height = 15, Width = 2 };
        Rectangle2 rectangle2 = new() { Height = 6, Width = 3 };
        Circle2 circle0 = new() { Radius = 3 };
        Circle2 circle1 = new() { Radius = 5 };
        Circle2 circle2 = new() { Radius = 7 };
        //List<IArea> areas = new();
        IArea[] areas1 = new IArea[6];
        areas1[0] = rectangle0;
        areas1[1] = rectangle1;
        areas1[2] = rectangle2;
        areas1[3] = circle0;
        areas1[4] = circle1;
        areas1[5] = circle2;
        var a = AreaCalculator2.TotalArea(areas1);
        Console.WriteLine(a);
    }
}

#region Bad Example

class Rectangle
{
    public double Height { get; set; }
    public double Wight { get; set; }
}

class Circle
{
    public double Radius { get; set; }
}

class AreaCalculator //for the Single Responsibility purpose
{
    public double TotalArea(Rectangle[] arrRectangles)
    {
        double area = 0;

        foreach (var rectangle in arrRectangles)
        {
            area += rectangle.Height * rectangle.Wight;
        }
        return area;
        //in order to include calculating the new geometry object area, we need to modify this
        //function. This is bad
        /*
         public double TotalArea(object[] arrObjects)  
         {  
          double area = 0;  
          Rectangle objRectangle;  
          Circle objCircle;  
          foreach(var obj in arrObjects)  
          {  
             if(obj is Rectangle)  
             {    
                area += obj.Height * obj.Width;  
             }  
             else  
             {  
                objCircle = (Circle)obj;  
                area += objCircle.Radius * objCircle.Radius * Math.PI;  
             }  
          }  
          return area;  
          }   
         */
        //therefore this code is not CLOSED for modification
    }
}

#endregion

#region Good Example (by abstract class)
//we can handle this problem by using the interfaces or abstract classes. Such interfaces can be fixed once developed so the class that depend upon them can rely upon unchanging abstractions. Functionality can be added by creating new classes that implement the interfaces. 
// This would look like this:

public abstract class Shape
{
    public abstract double Area();
}

public class Rectangle1 : Shape
{
    public double Height { get; set; }
    public double Width { get; set; }
    public override double Area()
    {
        return Height * Width;
    }
}
public class Circle1 : Shape
{
    public double Radius { get; set; }
    public override double Area()
    {
        return Radius * Radius * Math.PI;
    }
}

public class AreaCalculator1
{
    public double TotalArea(Shape[] arrShapes)
    {
        double area = 0;
        foreach (var objShape in arrShapes)
        {
            area += objShape.Area();
        }
        return area;
    }
}

#endregion

#region Good Example (by interface)
//we can handle this problem by using the interfaces or abstract classes. Such interfaces can be fixed once developed so the class that depend upon them can rely upon unchanging abstractions. Functionality can be added by creating new classes that implement the interfaces. 
// This would look like this:

public interface IArea
{
    public double Area();
}

public class Rectangle2 : IArea
{
    public double Height { get; set; }
    public double Width { get; set; }
    public double Area()
    {
        return Height * Width;
    }
}
public class Circle2 : IArea
{
    public double Radius { get; set; }
    public double Area()
    {
        return Radius * Radius * Math.PI;
    }
}

public static class AreaCalculator2
{
    public static double TotalArea(IArea[] arrShapes)
    {
        double area = 0;
        foreach (var objShape in arrShapes)
        {
            area += objShape.Area();
        }
        return area;
    }
}

#endregion

