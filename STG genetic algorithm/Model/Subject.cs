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
        private String roomType;

        public Subject(int id, String name, SubjectType type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }

        public Subject(int id, String name, SubjectType type, String roomType)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.roomType = roomType;
        }

        public string ToString()
        {
            return id + ":" + name + "[" + type +"/"+roomType+ "]";
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

        public String getRoomType() {
            return roomType;
        }
    }
}
