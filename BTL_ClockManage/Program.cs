﻿using BTL_ClockManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_ClockManage
{
    static class Program
    {
        public static Model1 context = new Model1();
        public static String connectionString = @"Data Source=FATS\SQLEXPRESS;Initial Catalog=QUANLYCUAHANGDH;Persist Security Info=True;User ID=sa;Password=phat12112002";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Views.viewLogin());
            //Application.Run(new Views.viewHomeManager());
            //Application.Run(new Views.viewHomeStocker());
            Application.Run(new Views.viewHomeStaff());
        }
    }
}
