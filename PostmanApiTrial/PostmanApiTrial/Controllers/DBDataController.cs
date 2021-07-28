using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using PostmanApiTrial.Models;
using Npgsql;
using System.Data;
using System.Text.Json;

namespace PostmanApiTrial.Controllers
{
    public class DBDataController : Controller
    {
        
        [HttpGet("student")]
        public string studModel()
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

            foreach (var item in cd)
            {
                Console.WriteLine(item.Value.s_name);
            }
            rdr.Close();
            
            return JsonSerializer.Serialize(cd);

        }
    }
}
