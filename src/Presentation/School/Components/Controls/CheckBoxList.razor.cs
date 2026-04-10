using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Components.Controls
{
    public partial class CheckBoxList
    {
        [Parameter] public CheckboxListModel Model { get; set; } = new();
        public class CheckboxListModel
        {
            public List<Data> Options { get; set; } = new List<Data>();
        }
        public class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Checked { get; set; }
        }
    }
}
