using System;
using CommonStuff.BE;

namespace SearchAPI
{
    public class DocumentHit
    {
        public DocumentHit(BEDocument doc, int noOfHits)
        {
            Document = doc;
            NoOfHits = noOfHits;
        }

        public BEDocument Document { get;  }

        public int NoOfHits { get;  }
    }
}
