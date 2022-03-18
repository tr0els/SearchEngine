﻿using System;
using System.Collections.Generic;
using CommonStuff;
using CommonStuff.BE;

namespace SearchAPI.Controllers
{
    public class SearchLogic
    {
        Database mDatabase;

        Dictionary<string, int> mWords;

        public SearchLogic(Database database)
        {
            mDatabase = database;
            mWords = mDatabase.GetAllWords();

        }

        /* Perform search of documents containing words from query. The result will
         * contain details about amost maxAmount of documents.
         */
        public SearchResult Search(String[] query, int maxAmount)
        {
            List<string> ignored;

            DateTime start = DateTime.Now;

            // Convert words to wordids
            var wordIds = GetWordIds(query, out ignored);

            // perform the search - get all docIds
            var docIds =  mDatabase.GetDocuments(wordIds);

            // get ids for the first maxAmount             
            var top = new List<int>();
            foreach (var p in docIds.GetRange(0, Math.Min(maxAmount, docIds.Count)))
                top.Add(p.Key);

            // compose the result.
            // all the documentHit
            List<DocumentHit> docresult = new List<DocumentHit>();
            int idx = 0;
            foreach (var doc in mDatabase.GetDocDetails(top))
            {
                var missing = mDatabase.WordsFromIds(mDatabase.getMissing(doc.mId, wordIds));
                  
                docresult.Add(new DocumentHit { Document = doc, NoOfHits = docIds[idx++].Value, Missing = missing });
            }

            return new SearchResult { Query = query,
                                      Hits = docIds.Count,
                                      DocumentHits = docresult,
                                      Ignored = ignored,
                                      TimeUsed = DateTime.Now - start };
        }

        private List<int> GetWordIds(String[] query, out List<string> outIgnored)
        {
            var res = new List<int>();
            var ignored = new List<string>();
            
            foreach (var aWord in query)
            {
                if (mWords.ContainsKey(aWord))
                    res.Add(mWords[aWord]);
                else
                    ignored.Add(aWord);
            }
            outIgnored = ignored;
            return res;
        }

       
    }
}
