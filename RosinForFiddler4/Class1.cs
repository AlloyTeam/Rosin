using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace AlloyTeam.Rosin
{
    public class Class1: IAutoTamper
    {
        public void AutoTamperRequestAfter(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperRequestBefore(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperResponseAfter(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void AutoTamperResponseBefore(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeReturningError(Session oSession)
        {
            throw new NotImplementedException();
        }

        public void OnBeforeUnload()
        {
            throw new NotImplementedException();
        }

        public void OnLoad()
        {
            throw new NotImplementedException();
        }
    }
}
