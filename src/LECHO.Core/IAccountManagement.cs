﻿using LECHO.Infrastructure;

namespace LECHO.Core
{
    public interface IAccountManagement
    {
        Users GetLecturer(int id);
        Students GetStudent(int id);
        Users GetUser(string username);
        bool Verify(string username, string password);
    }
}