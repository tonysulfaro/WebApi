using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models {
    public class TodoItem {
        public int Id { get; set; }
        public string Todo { get; set; }
        public bool Completed { get; set; }
    }
}