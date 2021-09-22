using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_robot.Application.Handlers
{
    public class RandomHandler
    {
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "AB0CD1EF2GH3IJ4KL5MN6OP7QR8ST9UVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumeric(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
