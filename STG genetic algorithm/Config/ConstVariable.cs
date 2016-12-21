using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG_genetic_algorithm.Config
{
    public static class ConstVariable
    {
        public const int NUMBER_OF_DAYS = 5;
        public const int NUMBER_OF_SLOTS_IN_DAY = 9;
        public const int NUMBER_OF_LESSONS_TO_POSITIONING = 5;
        public const int BOTTOM_BORDER_OF_BEST_SLOTS = 0;
        public const int TOP_BORDER_OF_BEST_SLOTS = 7;
        public const double CHANCE_TO_MUTATE = 0.01;
        public const double SUBJECT_PERCENT_TO_MUTATE = 0.1;
    }
}
