#r "nuget: BCrypt.Net-Next"

using System;
using BCrypt.Net;

Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin123"));
