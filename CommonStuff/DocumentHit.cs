using System;
using System.Collections.Generic;
using CommonStuff.BE;

namespace CommonStuff
{
    public class DocumentHit
    {

        public BEDocument Document { get; set; }

        public int NoOfHits { get; set; }

        public List<string> Missing { get; set; }
    }
}
