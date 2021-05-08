﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintPatterns.Command
{
    public class ActionLog
    {
        private String log;

        public ActionLog(String Log)
        {
            log = Log;
        }

        public String Log()
        {
            return log;
        }
    }

    public class LogCommand
    {
        private ActionLog action;
        public LogCommand(ActionLog Action)
        {
            action = Action;
        }

        public void Execute()
        {
            action.Log();
        }
    }

}
