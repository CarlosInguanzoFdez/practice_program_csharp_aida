using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface QuoteService
{
    List<string> Get(string word);
}