﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quotes.DataAccess
{
    public class QuotesRepository
    {
        public List<string> QuotesList = new List<string>
        {
            "Attitude is a little thing that makes a big difference",
            "We are all in the gutter but some of us are looking at the stars",
            "Be not afraid of going slowly, but be afraid of standing still",
            "Don't let what you can't do stop you from doing what you can do",
            "The man who makes no mistakes does not usually make anything"
        };

        public string GetRandomQuote()
        {
            int randomNumber = (new Random()).Next(QuotesList.Count - 1);
            return QuotesList[randomNumber];
        }
    }
}
