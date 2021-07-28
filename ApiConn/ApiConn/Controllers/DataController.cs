using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiConn.Model;
using Npgsql;
using System.Collections.Concurrent;
using System.Data;

namespace ApiConn.Controllers
{
    public class DataController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("dataroute")]
        public List<int> idReturn()
        {
            List<int> idd = new List<int>()
            {
                1,2,3,775,45,99
            };
            return idd;
        }


        [HttpGet("databaserouting")]
        public ConcurrentDictionary<int,student> retunTeacherMethod()
        {

          
                string str = " ";

                var con = "Host=localhost;Username=postgres;Password=prats212;Database=school;";
                using var conn = new Npgsql.NpgsqlConnection(con);
                conn.Open();
                str = "select * from student";
                using var cmd = new Npgsql.NpgsqlCommand(str, conn);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                ConcurrentDictionary<int, student> cd = new ConcurrentDictionary<int, student>();

                while (rdr.Read())
                {
                    student s = new student();
                    s.s_id = rdr.GetInt16("s_id");
                    s.s_name = rdr.GetString("s_name");
                    s.s_age = rdr.GetInt32("s_age");

                    cd.TryAdd(s.s_id, s);

                }

                //foreach (var item in cd)
                //{
                //    Console.WriteLine(item.Value.s_name);
                //}
             

                //str = "select * from teacher";
                //using var com = new Npgsql.NpgsqlCommand(str, conn);
                //using NpgsqlDataReader read = com.ExecuteReader();

                //ConcurrentDictionary<int, teacher> teachDict = new ConcurrentDictionary<int, teacher>();

                //while (read.Read())
                //{
                //    teacher obj = new teacher();
                //    obj.t_id = read.GetInt32("t_id");
                //    obj.t_name = read.IsDBNull("t_name") ? null : read.GetString("t_name");

                //    /*  if (string.IsNullOrEmpty(read.GetString("t_name")) == true)
                //      {
                //          obj.t_name = null;
                //      }
                //      else
                //      {
                //          obj.t_name = read.GetString("t_name");
                //      }
                //    */
                //    obj.t_age = read.GetInt32("t_age");
                //    obj.s_id = read.GetInt32("s_id");

                //    /*
                //    if (read.GetInt32("s_id") == null)
                //    {
                //        obj.s_id = 0;
                //    }
                //    else
                //    {
                //        obj.s_id = read.GetInt32("s_id");
                //    }
                   
                //    */
                //    teachDict.TryAdd(obj.t_id, obj);
                //}

                //foreach (var item in teachDict)
                //{
                //    Console.WriteLine(item.Value.t_name);
                //}

                //var joinQuery = (from st_alias in cd
                //                 join td_alias in teachDict
                //                 on st_alias.Value.s_id equals td_alias.Value.s_id
                //                 orderby st_alias.Value.s_age
                //                 select new
                //                 {
                //                     st_alias.Value.s_name,
                //                     td_alias.Value.t_name,

                //                 }).ToList();

                //foreach (var jq in joinQuery)
                //{
                //    Console.WriteLine(jq);
                //}
                return cd;
          
           

        }
        
       
    }
    
}

