using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortfolio.Core.Extensions
{
    public static class StringExtensions
    {

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string GetRandom()
        {
            Random rr = new Random(DateTime.Now.Millisecond);

            string cadena = "", caracteres = "ab0cde1fg2hij3kl4mno5pq6rst7uv8wxy9z";
            int posicioncaracter;

            for (int i = 1; i <= 10; i++)
            {
                posicioncaracter = rr.Next(caracteres.Length);
                cadena = cadena + caracteres[posicioncaracter];
            }

            return cadena;
        }
    }
}
