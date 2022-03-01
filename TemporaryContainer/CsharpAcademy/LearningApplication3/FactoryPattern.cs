using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public static class FactoryPattern<K,T> where T:class, K, new() //tutaj K to będzie interface, a T to będzie klasa 
    {
        public static K GetInstance()
        {
            K objK = new T();
            return objK;
        }

    }
}
