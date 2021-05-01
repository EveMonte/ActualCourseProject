using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Курсач.Models
{
    //[Table("Employees")]
    class Employee
    {
        [Key]
        public string Email { get; set; }

        public string secondName;
        private string SecondName
        {
            get { return secondName; }
            set { secondName = value; }
        }
        public string name;
        private string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Employee()
        {

        }
    }
}
