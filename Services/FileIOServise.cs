using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using ToDoList.Models;

namespace ToDoList.Services
{
    class FileIOServise
    {
        private readonly string PATH;
        public FileIOServise(string path)
        {
            PATH = path;
        }
        public BindingList<TodoModel> LoadData()
        {
            var fileExist = File.Exists(PATH);
            if (!fileExist)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<TodoModel>();
            }
            using (var stream = File.OpenText(PATH))
            {
                var fileText = stream.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
                new BindingList<TodoModel>();
            }
        }

        public void SaveData(object list)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(list);
               writer.Write(output);

            }
        }
    }
}
