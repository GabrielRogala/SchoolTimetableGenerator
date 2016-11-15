using STG_genetic_algorithm.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Model
{
    public class Subject
    {
        private int id;
        private String name;
        private SubjectType type;

        public Subject(int id, String name, SubjectType type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }

        public string ToString()
        {
            return id + ":" + name + "[" + type + "]";
        }

        public SubjectType getSubjectType()
        {
            return type;
        }

        public int getId() {
            return id;
        }

        public String getName() {
            return name;
        }
    }
}
